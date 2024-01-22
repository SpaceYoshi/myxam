using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyXamClient.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EventType Type { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    public enum EventType
    {
        School, Personal, Work, Sports
    }
}
