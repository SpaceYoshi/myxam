using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using MyXamLibrary.Models;

namespace MyXamClient.Services;

public static class AgendaService
{
    public static ConcurrentDictionary<Guid, Agenda> Agendas { get; } = new();
    public static ObservableCollection<AgendaEvent> Events { get; } = [];
}