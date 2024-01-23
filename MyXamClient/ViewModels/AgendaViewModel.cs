using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyXamClient.ViewModels;

public partial class AgendaViewModel : ObservableObject
{
    [ObservableProperty]
    private string itemSelected;

    [RelayCommand]
    void AddButton()
    {

    }
}
