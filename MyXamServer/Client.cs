using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConcurrentCollections;
using MyXamClient.Models;

namespace MyXamServer;

public class Client(Guid clientId, TcpClient tcpClient, MyXamServer server)
{
    public Guid Id { get; } = clientId;
    private TcpClient TcpClient { get; } = tcpClient;
    private ConcurrentHashSet<Guid> SubscribedAgendaIds { get; } = [];

    public void Listen()
    {
        try
        {
            using var stream = TcpClient.GetStream();
            using var reader = new BinaryReader(stream, Encoding.UTF8, true);

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
                    var agenda = JsonSerializer.Deserialize<Agenda>(payloadBytes);
                    if (agenda == null)
                    {
                        Console.WriteLine("Deserialized agenda is null.");
                        break;
                    }

                    // Add agenda to server
                    server.Agendas[agenda.Id] = agenda;
                    break;
                }
                case PayloadType.AddEvent:
                {
                    var agendaEvent = JsonSerializer.Deserialize<AgendaEvent>(payloadBytes);
                    if (agendaEvent == null)
                    {
                        Console.WriteLine("Deserialized event is null.");
                        break;
                    }

                    // Add event to server if its agenda exists
                    var agendaId = agendaEvent.AgendaId;
                    if (!AgendaExists(agendaId)) break;
                    var agenda = server.Agendas[agendaEvent.AgendaId];
                    agenda.Events.Add(agendaId, agendaEvent);

                    // Send event to all clients that are subscribed to the event's agenda
                    foreach (var client in server.Clients.Values.Where(client => client.SubscribedAgendaIds.Contains(agendaId)))
                    {
                        Stream clientStream = client.TcpClient.GetStream();
                        Payload.SendJson(PayloadType.AddEvent, agendaEvent, clientStream, jsonOptions);
                    }
                    break;
                }
                case PayloadType.RequestAvailableAgendas:
                {
                    // Send server agendas without events
                    var emptyAgendas = new List<Agenda>(server.Agendas.Count);
                    emptyAgendas.AddRange(server.Agendas.Values.Select(agenda => new Agenda(agenda.Id, agenda.Name)));
                    Payload.SendJson(PayloadType.AvailableAgendas, emptyAgendas, stream, jsonOptions);
                    break;
                }
                case PayloadType.SubscribeToAgenda:
                {
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
                    var agenda = server.Agendas[agendaId];
                    Payload.SendJson(PayloadType.AddAgenda, agenda, stream, jsonOptions);
                    break;
                }
                case PayloadType.UnsubscribeFromAgenda:
                {
                    var requestedIdAgenda = JsonSerializer.Deserialize<Agenda>(payloadBytes);
                    if (requestedIdAgenda == null)
                    {
                        Console.WriteLine("Deserialized agenda is null.");
                        break;
                    }

                    var agendaId = requestedIdAgenda.Id;
                    if (!AgendaExists(agendaId)) break;

                    UnsubscribeFromAgenda(agendaId);
                    break;
                }
                case PayloadType.AvailableAgendas:
                default: throw new ArgumentOutOfRangeException("Unexpected type value: " + payloadType);
            }
        }
        catch (IOException)
        {
            Console.WriteLine("Client disconnected.");
        }
        finally
        {
            server.Clients.Remove(Id);
        }
    }

    private bool AgendaExists(Guid agendaId)
    {
        var exists = server.Agendas.ContainsKey(agendaId);
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