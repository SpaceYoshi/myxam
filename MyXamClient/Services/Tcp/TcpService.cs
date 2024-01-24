using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyXamLibrary;
using MyXamLibrary.Models;

namespace MyXamClient.Services.Tcp;

public class TcpService
{
    private static readonly TcpClient _tcpClient = new(new IPEndPoint(IPAddress.Loopback, 5123));
    private readonly TcpConnection _tcpConnection;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    public TcpService()
    {
        var endPoint = new IPEndPoint(IPAddress.Loopback, 5123);
        // _tcpClient = new TcpClient(endPoint);
        _tcpClient.Connect(endPoint);
        _tcpConnection = new TcpConnection(_tcpClient);
        Task.Run(() => _tcpConnection.Run()).Start();
    }

    public static void SendAgenda(Agenda agenda)
    {
        Payload.SendJson(PayloadType.AddAgenda, agenda, _tcpClient.GetStream(), _jsonOptions);
    }

    public static void SendEvent(AgendaEvent agendaEvent)
    {
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
}