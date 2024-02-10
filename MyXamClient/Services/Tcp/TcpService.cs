using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyXamLibrary;
using MyXamLibrary.Models;

namespace MyXamClient.Services.Tcp;

public class TcpService : ITcpService
{
    private static TcpClient _tcpClient = new("localhost", 5123);
    private static TcpConnection _tcpConnection;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    // public TcpService()
    // {
        // var endPoint = new IPEndPoint(IPAddress.Loopback, 5123);
        // _tcpClient = new TcpClient(endPoint);
        // _tcpClient.Connect(endPoint);
        // _tcpConnection = new TcpConnection(_tcpClient);
        // Task.Run(() => _tcpConnection.Run()).Start();
    // }

    public static void StartClient()
    {
        _tcpConnection = new TcpConnection(_tcpClient);
        new Thread(_tcpConnection.Run).Start();

        // Task.Run(() => _tcpConnection.Run());
    }

    public static void SendAgendaStatic(Agenda agenda)
    {
        Payload.SendJson(PayloadType.AddAgenda, agenda, _tcpClient.GetStream(), _jsonOptions);
    }

    public static void SendEventStatic(AgendaEvent agendaEvent)
    {
        SendAgendaStatic(new Agenda(agendaEvent.AgendaId, "Demo")); // For demo purposes
        Payload.SendJson(PayloadType.AddEvent, agendaEvent, _tcpClient.GetStream(), _jsonOptions);
    }

    public void RequestAvailableAgendas()
    {
        // TODO
    }

    public void SubscribeToAgenda(Guid agendaId)
    {
        // TODO
    }

    public void UnsubscribeFromAgenda(Guid agendaId)
    {
        // TODO
    }

    public void SendAgenda(Agenda agenda)
    {
        SendAgendaStatic(agenda);
    }

    public void SendEvent(AgendaEvent agendaEvent)
    {
        SendEventStatic(agendaEvent);
    }
}