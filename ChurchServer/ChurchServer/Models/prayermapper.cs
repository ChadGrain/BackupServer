using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.objects;

namespace ChurchServer.Models
{
    public class prayermapper
    {
        public List<Prayer> Map(List<PrayerDAO> _PrayerListMap)
        {
            List<Prayer> _PrayerListToReturn = new List<Prayer>();
            foreach (PrayerDAO _prayertomap in _PrayerListMap)
            {
                Prayer _prayertoview = new Prayer();
                _prayertoview.ListID = _prayertomap.ListID;
                _prayertoview.Firstname = _prayertomap.Firstname;
                _prayertoview.Lastname = _prayertomap.Lastname;
                _prayertoview.date = _prayertomap.date;
                _prayertoview.DateAdded = _prayertomap.DateAdded;
                _prayertoview.Situation = _prayertomap.Situation;
                _PrayerListToReturn.Add(_prayertoview);
            }
            return _PrayerListToReturn.OrderBy(x => x.Firstname).ToList();
        }

        public PrayerDAO Map(Prayer _prayertomap)
        {
            PrayerDAO _prayertoview = new PrayerDAO();
            _prayertoview.ListID = _prayertomap.ListID;
            _prayertoview.Firstname = _prayertomap.Firstname;
            _prayertoview.Lastname = _prayertomap.Lastname;
            _prayertoview.DateAdded = _prayertomap.DateAdded;
            _prayertoview.Situation = _prayertomap.Situation;
            return _prayertoview;
        }
    }
}