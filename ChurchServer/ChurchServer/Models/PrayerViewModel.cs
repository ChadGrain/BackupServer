using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchServer.Models
{
    public class PrayerViewModel
    {
        public Prayer SinglePrayer { get; set; }
        public List<Prayer> PrayerList { get; set; }

        public PrayerViewModel()
        {
            SinglePrayer = new Prayer();
            PrayerList = new List<Prayer>();
        }
    }
}