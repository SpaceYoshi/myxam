using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
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
        public ConcurrentDictionary<Guid, Agenda> Agendas { get; } = new();
        public Dictionary<Guid, Client> Clients { get; } = new();

        public void Run()
        {
            var endPoint = new IPEndPoint(IPAddress.Loopback, port);
            Console.WriteLine("Server started on {0}:{1}.", endPoint.Address, port);
            var listener = new TcpListener(endPoint);
            listener.Start();

            while (true)
            {
                var client = new Client(Guid.NewGuid(), listener.AcceptTcpClient(), this);
                Clients[client.Id] = client;
                Task.Run(() => client.Listen()).Start();
            }
        }
    }
}