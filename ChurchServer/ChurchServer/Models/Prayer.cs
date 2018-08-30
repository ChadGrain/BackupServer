using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchServer.Models
{
    public class Prayer
    {
        public int ListID { get; set; }
        public string Firstname { set; get; }
        public string Lastname { get; set; }
        public DateTime DateAdded { get; set; }
        public string Situation { get; set; }
        public string date { get; set; }
        public int AddedBy { get; set; }
        public string Username { get; set; }
    }
}