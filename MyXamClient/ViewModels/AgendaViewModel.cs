using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyXamClient.Views;
using System.Collections.ObjectModel;

namespace MyXamClient.ViewModels;

public partial class AgendaViewModel : ObservableObject
{
    public AgendaViewModel() 
    {
        Events = new ObservableCollection<string>();
    }

    [ObservableProperty]
    public static ObservableCollection<string> events;

    [ObservableProperty]
    private string selectedItem;

    [RelayCommand]
    async Task NavigateToEventPage()
    {
        await Shell.Current.GoToAsync("EventPage");
    }
}
