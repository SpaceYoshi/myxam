using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using MyXamLibrary.Models;

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
            const string address = "127.0.0.1";
            Console.WriteLine("Server started on {0}:{1}.", address, port);
            var listener = new TcpListener(IPAddress.Parse(address), port);
            listener.Start();

            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                var client = new Client(Guid.NewGuid(), listener.AcceptTcpClient(), this);
                Clients[client.Id] = client;
                new Thread(client.Listen).Start();

                // var listenerThread = new Thread(client.Listen);
                // listenerThread.Start();

                // Task.Run(() => client.Listen());
            }
        }
    }
}