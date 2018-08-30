using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.objects;

namespace ChurchServer.Models
{
    public class eventmapper
    {
        public List<Events> Map(List<EventDAO> _EventListMap)
        {
            List<Events> _EventListToReturn = new List<Events>();
            foreach (EventDAO _eventtomap in _EventListMap)
            {
                Events _eventtoview = new Events();
                _eventtoview.EventID = _eventtomap.EventID;
                _eventtoview.Event = _eventtomap.Event;
                _eventtoview.EventDesc = _eventtomap.EventDesc;
                _eventtoview.EventDate = _eventtomap.EventDate;
                _eventtoview.Date = _eventtomap.Date;
                _eventtoview.AddedBy = _eventtomap.AddedBy;
                _eventtoview.Username = _eventtomap.Username;
                _EventListToReturn.Add(_eventtoview);
            }
            return _EventListToReturn.OrderBy(x => x.Event).ToList();
        }

        public EventDAO Map(Events _eventtomap)
        {
            EventDAO _eventtoview = new EventDAO();
            _eventtoview.EventID = _eventtomap.EventID;
            _eventtoview.Event = _eventtomap.Event;
            _eventtoview.EventDesc = _eventtomap.EventDesc;
            _eventtoview.EventDate = _eventtomap.EventDate;
            _eventtoview.AddedBy = _eventtomap.AddedBy;
            _eventtoview.Username = _eventtomap.Username;
            return _eventtoview;
        }
    }
}