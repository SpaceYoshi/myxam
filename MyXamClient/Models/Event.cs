using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyXamClient.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    public enum EventType
    {
        School, Personal, Work, Sport
    }
}
