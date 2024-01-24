using CommunityToolkit.Mvvm.Input;
using MyXamClient.Services;
using System.Collections.ObjectModel;
using System.Text.Json;
using MyXamLibrary.Models;

namespace MyXamClient.ViewModels;

public partial class HomePageViewModel
{
    private string filePath;

    private void Init()
    {
        filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\agenda.json");
    }

    [RelayCommand]
    private async Task Write()
    {
        Init();

        await using var createStream = File.Create(filePath);
        var items = await Task.Run(() => AgendaService.Events);

        await JsonSerializer.SerializeAsync(createStream, items);
        createStream.Close();
    }

    [RelayCommand]
    private async Task Read()
    {

        if (File.Exists(filePath))
        {
            await using var openStream = File.OpenRead(filePath);
            var agenda = await JsonSerializer.DeserializeAsync<ObservableCollection<AgendaEvent>>(openStream);

            AgendaService.Events.Clear();

            foreach (var item in agenda)
            {
                AgendaService.Events.Add(item);
            }

            AgendaViewModel.ClearAgenda();
            await AgendaViewModel.UpdateAgenda();
            openStream.Close();
        }
        else
        {
            await Shell.Current.DisplayAlert("Error Message",
                "Can't read file, because file doesn't exists. \nTry writing to file first", "OK");
        }
    }
}