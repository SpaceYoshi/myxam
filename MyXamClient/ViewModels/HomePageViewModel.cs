using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using MyXamClient.Models;
using MyXamClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyXamClient.ViewModels
{
    public partial class HomePageViewModel
    {
        string filePath;

        private void Init()
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\agenda.json");
        }

        [RelayCommand]
        async Task Write()
        {
            Init();

            await using (FileStream createStream = File.Create(filePath))
            {
                var items = await Task.Run(() => AgendaService.getEvents());

                await JsonSerializer.SerializeAsync(createStream, items);
                createStream.Close();
            }
        }

        [RelayCommand]
        async Task Read()
        {

            if (File.Exists(filePath))
            {
                using (FileStream openStream = File.OpenRead(filePath))
                {
                    ObservableCollection<AgendaEvent> agenda = await JsonSerializer.DeserializeAsync<ObservableCollection<AgendaEvent>>(openStream);

                    AgendaService.ClearEvents();

                    foreach (var item in agenda)
                    {
                        AgendaService.addEvent(item);
                    }

                    AgendaViewModel.ClearAgenda();
                    await AgendaViewModel.UpdateAgenda();
                    openStream.Close();
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error Message",
                    "Can't read file, because file doesn't exists. \nTry writing to file first", "OK");

                return;
            }
        }
    }
}
