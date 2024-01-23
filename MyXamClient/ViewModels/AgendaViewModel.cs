using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MyXamClient.ViewModels;

public partial class AgendaViewModel : ObservableObject
{
    public AgendaViewModel() 
    {
        Events = new ObservableCollection<string>();
    }

    [ObservableProperty]
    ObservableCollection<string> events;

    [ObservableProperty]
    private string selectedItem;

    [RelayCommand]
    void Add()
    {
        if (string.IsNullOrEmpty(selectedItem))
            return;

        Events.Add(selectedItem);
        selectedItem = string.Empty;
    }
}
