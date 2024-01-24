using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyXamLibrary;
using MyXamLibrary.Models;

namespace MyXamClient.Services.Tcp;

public class TcpService
{
    private readonly TcpClient _tcpClient;
    private readonly TcpConnection _tcpConnection;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    public TcpService(IPEndPoint endPoint)
    {
        _tcpClient = new TcpClient(endPoint);
        _tcpConnection = new TcpConnection(_tcpClient);
        Task.Run(() => _tcpConnection.Run()).Start();
    }

    public void SendAgenda(Agenda agenda)
    {
        Payload.SendJson(PayloadType.AddAgenda, agenda, _tcpClient.GetStream(), _jsonOptions);
    }

    public void SendEvent(AgendaEvent agendaEvent)
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