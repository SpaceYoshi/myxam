using MyXamClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyXamClient.Services
{
    public static class AgendaService
    {
        private static ObservableCollection<AgendaEvent> events = new ObservableCollection<AgendaEvent>();
        
        public static void addEvent(AgendaEvent agendaEvent)
        {
            events.Add(agendaEvent);
        }
        public static ObservableCollection<AgendaEvent> getEvents()
        {
            return events;
        }
    }
}
