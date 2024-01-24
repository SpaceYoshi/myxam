using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyXamClient.Models;

namespace MyXamClient.ViewModels
{
    public partial class EventViewModel : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string type;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string startTime;

        [ObservableProperty]
        string endTime;


        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task AddEvent()
        {
            AgendaViewModel.events.Add(this.Name);
        }

        private Event CreateEvent()
        {
            Event newEvent = new Event{
                Name = this.Name,
                Type = this.Type,
                Description = this.Description,
                StartTime = this.StartTime,
                EndTime = this.EndTime,
            };

            return newEvent;
        }
    }
}
