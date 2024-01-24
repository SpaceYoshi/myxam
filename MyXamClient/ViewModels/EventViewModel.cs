using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyXamClient.Models;
using MyXamClient.Services;
using System.Collections.ObjectModel;

namespace MyXamClient.ViewModels
{
    public partial class EventViewModel : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        DateTimeOffset startTime;

        [ObservableProperty]
        DateTimeOffset endTime;

        [RelayCommand]
        async Task GoBack()
        {
            AgendaViewModel.UpdateAgenda();
            await Shell.Current.GoToAsync("AgendaPage");
        }

        [RelayCommand]
        async Task AddEvent()
        {
            AgendaService.addEvent(CreateEvent());
        }

        private AgendaEvent CreateEvent()
        {
            AgendaEvent newEvent = new AgendaEvent
            (
                id: Guid.NewGuid(),
                agendaId: Guid.NewGuid(),
                name: Name,
                startTime: StartTime,
                endTime: EndTime
            );

            return newEvent;
        }
    }
}
