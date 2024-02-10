using MyXamLibrary.Models;

namespace MyXamClient.Services.Tcp;

public interface ITcpService
{
    void SendAgenda(Agenda agenda);
    void SendEvent(AgendaEvent agendaEvent);
    void RequestAvailableAgendas();
    void SubscribeToAgenda(Guid agendaId);
    void UnsubscribeFromAgenda(Guid agendaId);
}