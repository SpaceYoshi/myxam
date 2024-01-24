using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyXamLibrary;
using MyXamLibrary.Models;

namespace MyXamClient.Services.Tcp;

public class TcpConnection(TcpClient tcpClient)
{
    private bool isConnected = false;

    public void Run()
    {
        Listen();
        while (true)
        {
            if (tcpClient.Connected) continue;
            Console.WriteLine("Retrying TcpConnection.");
            Listen();
        }
    }

    private void Listen()
    {
        try
        {
            using var stream = tcpClient.GetStream();
            using var reader = new BinaryReader(stream, Encoding.UTF8, true);

            while (tcpClient.Connected)
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

                        // Add agenda
                        AgendaService.Agendas[agenda.Id] = agenda;
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

                        // Add event if agenda exists
                        var agendaId = agendaEvent.AgendaId;
                        if (!AgendaExists(agendaId)) break;
                        var agenda = AgendaService.Agendas[agendaEvent.AgendaId];
                        agenda.Events.Add(agendaId, agendaEvent);

                        // TODO Demo
                        AgendaService.Events.Add(agendaEvent);
                    }
                        break;
                    case PayloadType.AvailableAgendas:
                    {
                        // TODO
                    }
                        break;
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
    }

    private static bool AgendaExists(Guid agendaId)
    {
        var exists = AgendaService.Agendas.ContainsKey(agendaId);
        if (!exists) Console.WriteLine("Referenced agenda in event does not exist: " + agendaId);
        return exists;
    }
}