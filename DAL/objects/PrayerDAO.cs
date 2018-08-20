using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.objects
{
    public class PrayerDAO
    {
        public int ListID { get; set; }
        public string Firstname { set; get; }
        public string Lastname { get; set; }
        public DateTime DateAdded { get; set; }
        public string Situation { get; set; }
        public string date { get; set; }
    }
}
