using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using MyXamClient.Models;

if (args.Length != 1)
{
    Console.WriteLine("Please specify a port number to listen on.");
    return;
}

var port = int.Parse(args[0]);
new MyXamServer.MyXamServer(port).Run();

namespace MyXamServer
{
    public class MyXamServer(int port)
    {
        private readonly ConcurrentDictionary<Guid, Agenda> _agendas = new();

        public async void Run()
        {
            var endPoint = new IPEndPoint(IPAddress.Loopback, port);
            Console.WriteLine("Server started on {0}:{1}.", endPoint.Address, port);
            var listener = new TcpListener(endPoint);
            listener.Start();

            while (true)
            {
                using var client = await listener.AcceptTcpClientAsync();
                HandleClient(client);
            }
        }

        private void HandleClient(TcpClient client)
        {
            using var reader = new BinaryReader(client.GetStream(), Encoding.UTF8);

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
                case PayloadType.Agenda:
                {
                    var agenda = JsonSerializer.Deserialize<Agenda>(payloadBytes);
                    if (agenda == null)
                    {
                        Console.WriteLine("Deserialized agenda is null.");
                        break;
                    }
                    _agendas[agenda.Id] = agenda;
                    break;
                }
                case PayloadType.Event:
                {
                    var agendaEvent = JsonSerializer.Deserialize<AgendaEvent>(payloadBytes);
                    if (agendaEvent == null)
                    {
                        Console.WriteLine("Deserialized event is null.");
                        break;
                    }
                    var agendaId = agendaEvent.AgendaId;
                    if (!_agendas.ContainsKey(agendaId))
                    {
                        Console.WriteLine("Referenced agenda in event does not exist: " + agendaId);
                        break;
                    }
                    var agenda = _agendas[agendaEvent.AgendaId];
                    agenda.Events.Add(agendaId, agendaEvent);
                    break;
                }
                case PayloadType.AvailableAgendasRequest:
                {
                    // tuple list?
                    foreach (var agenda in _agendas)
                    {
                        
                    }
                    break;
                }
                case PayloadType.AgendaRequest:
                {
                    break;
                }
                default: throw new ArgumentOutOfRangeException("Unexpected type value: " + payloadType);
            }
        }
    }
}