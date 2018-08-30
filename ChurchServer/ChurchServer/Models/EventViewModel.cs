using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchServer.Models
{
    public class EventViewModel
    {
        public Events SingleEvent { get; set; }
        public List<Events> EventList { get; set; }

        public EventViewModel()
        {
            SingleEvent = new Events();
            EventList = new List<Events>();
        }

    }
}