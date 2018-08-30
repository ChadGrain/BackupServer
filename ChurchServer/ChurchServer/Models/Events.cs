using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchServer.Models
{
    public class Events
    {
        public int EventID { get; set; }
        public string Event { get; set; }
        public string EventDesc { get; set; }
        public DateTime EventDate { get; set; }
        public string Date { get; set; }
        public int AddedBy { get; set; }
        public string Username { get; set; }
    }
}