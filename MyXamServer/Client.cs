using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConcurrentCollections;
using MyXamLibrary;
using MyXamLibrary.Models;

namespace MyXamServer;

public class Client
{
    public Guid Id { get; }
    private TcpClient TcpClient { get; }
    private ConcurrentHashSet<Guid> SubscribedAgendaIds { get; } = [];
    private MyXamServer _server;

    public Client(Guid clientId, TcpClient tcpClient, MyXamServer server)
    {
        Id = clientId;
        TcpClient = tcpClient;
        _server = server;
    }

    public void Listen()
    {
        Console.WriteLine("New client started: " + Id);
        try
        {
            var stream = TcpClient.GetStream();
            using var reader = new BinaryReader(stream);
            while (true)
            {
                var unsignedPayloadLength = reader.ReadUInt32();
                var payloadLength = Convert.ToInt32(unsignedPayloadLength); // Ignore first bit for now
                var payloadTypeByte = reader.ReadByte();

                var payloadBytes = new byte[payloadLength];
                var bytesReceived = 0;
                while (bytesReceived < payloadLength)
                {
                    bytesReceived += reader.Read(payloadBytes, bytesReceived, payloadLength - bytesReceived);
                }

                var payloadType = Payload.GetPayloadType(payloadTypeByte);
                Console.WriteLine("Received payload of type {0} and length {1}.", payloadType, payloadLength);

                JsonSerializerOptions jsonOptions = new()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };

                switch (payloadType)
                {
                    case PayloadType.AddAgenda:
                    {
                        Console.WriteLine("Received agenda.");
                        var agenda = JsonSerializer.Deserialize<Agenda>(payloadBytes);
                        if (agenda == null)
                        {
                            Console.WriteLine("Deserialized agenda is null.");
                            break;
                        }

                        // DEMO
                        foreach (var client in _server.Clients.Values)
                        {
                            if (client == this) continue;
                            Stream clientStream = client.TcpClient.GetStream();
                            Payload.SendJson(PayloadType.AddAgenda, agenda, clientStream, jsonOptions);
                        }

                        // Add agenda to server
                        _server.Agendas[agenda.Id] = agenda;
                        Console.WriteLine("Agenda added to server.");
                        break;
                    }
                    case PayloadType.AddEvent:
                    {
                        Console.WriteLine("Received event.");
                        var agendaEvent = JsonSerializer.Deserialize<AgendaEvent>(payloadBytes);
                        if (agendaEvent == null)
                        {
                            Console.WriteLine("Deserialized event is null.");
                            break;
                        }

                        // Add event to server if its agenda exists
                        var agendaId = agendaEvent.AgendaId;
                        if (!AgendaExists(agendaId)) break;
                        var agenda = _server.Agendas[agendaId];
                        agenda.Events.Add(agendaId, agendaEvent);
                        Console.WriteLine("Event added to server.");

                        // // Send event to all clients that are subscribed to the event's agenda
                        // foreach (var client in _server.Clients.Values.Where(client =>
                        //              client.SubscribedAgendaIds.Contains(agendaId)))
                        // {
                        //     Stream clientStream = client.TcpClient.GetStream();
                        //     Payload.SendJson(PayloadType.AddEvent, agendaEvent, clientStream, jsonOptions);
                        // }

                        // DEMO: Send event to all clients that are subscribed to the event's agenda
                        foreach (var client in _server.Clients.Values)
                        {
                            if (client == this) continue;
                            Stream clientStream = client.TcpClient.GetStream();
                            Payload.SendJson(PayloadType.AddEvent, agendaEvent, clientStream, jsonOptions);
                        }

                        Console.WriteLine("Event sent to other clients.");
                        break;
                    }
                    case PayloadType.RequestAvailableAgendas:
                    {
                        Console.WriteLine("Received request for available agendas.");
                        // Send server agendas without events
                        var emptyAgendas = new List<Agenda>(_server.Agendas.Count);
                        emptyAgendas.AddRange(
                            _server.Agendas.Values.Select(agenda => new Agenda(agenda.Id, agenda.Name)));
                        Payload.SendJson(PayloadType.AvailableAgendas, emptyAgendas, stream, jsonOptions);
                        Console.WriteLine("Sending available agendas.");
                        break;
                    }
                    case PayloadType.SubscribeToAgenda:
                    {
                        Console.WriteLine("Received subscribe request.");
                        var requestedIdAgenda = JsonSerializer.Deserialize<Agenda>(payloadBytes);
                        if (requestedIdAgenda == null)
                        {
                            Console.WriteLine("Deserialized agenda is null.");
                            break;
                        }

                        var agendaId = requestedIdAgenda.Id;
                        if (!AgendaExists(agendaId)) break;

                        SubscribeToAgenda(agendaId);
                        // Send subscribed agenda
                        var agenda = _server.Agendas[agendaId];
                        Payload.SendJson(PayloadType.AddAgenda, agenda, stream, jsonOptions);
                        Console.WriteLine("Subscribe successful.");
                        break;
                    }
                    case PayloadType.UnsubscribeFromAgenda:
                    {
                        Console.WriteLine("Received unsubscribe request.");
                        var requestedIdAgenda = JsonSerializer.Deserialize<Agenda>(payloadBytes);
                        if (requestedIdAgenda == null)
                        {
                            Console.WriteLine("Deserialized agenda is null.");
                            break;
                        }

                        var agendaId = requestedIdAgenda.Id;
                        if (!AgendaExists(agendaId)) break;

                        UnsubscribeFromAgenda(agendaId);
                        Console.WriteLine("Unsubscribe successful.");
                        break;
                    }
                    case PayloadType.AvailableAgendas:
                    default:
                        throw new ArgumentOutOfRangeException("Unexpected type value: " + payloadType);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine("Client disconnected.");
        }
        finally
        {
            _server.Clients.Remove(Id);
        }
    }

    private bool AgendaExists(Guid agendaId)
    {
        var exists = _server.Agendas.ContainsKey(agendaId);
        if (!exists) Console.WriteLine("Referenced agenda in event does not exist: " + agendaId);
        return exists;
    }

    private void SubscribeToAgenda(Guid agendaId)
    {
        SubscribedAgendaIds.Add(agendaId);
    }

    private void UnsubscribeFromAgenda(Guid agendaId)
    {
        SubscribedAgendaIds.TryRemove(agendaId);
    }
}