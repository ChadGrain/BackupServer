using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.objects;
using ChurchServer.Models;

namespace ChurchServer.Controllers
{
    public class EventController : Controller
    {
        //Create instance of the Event Mapper named Emap
        static eventmapper Emap = new eventmapper();
        //Create an instance of The EventDataAccess Called EDA
        static EventDataAccess EDA = new EventDataAccess();

        // GET: Events
        public ActionResult ViewEvent()
        {
            //Create an instance of EventViewModel named selectevent
            EventViewModel selectevent = new EventViewModel();
            //Sends Eventlist to the mapper to be converted for EDA
            selectevent.EventList = Emap.Map(EDA.GetEventList());
            return View(selectevent);
        }

        public ActionResult AddEvent()
        {
            return View();
        }

        public ActionResult Delete(int EventID)
        {
            //Sends our EventID to EDA for the Delete Function
            EDA.DeleteEvent(EventID);

            return RedirectToAction("ViewEvent");
        }

        public ActionResult UpdateEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateEvent(Events Update, int EventID)
        {
            //States the EventID
            Update.EventID = EventID;
           int AddedBy = (int)Session["UserID"];
            Update.AddedBy = AddedBy;
            //Sends Updated to the update command
            EDA.UpdateEvent(Emap.Map(Update));

            return RedirectToAction("ViewEvent", "Event");
        }

        [HttpPost]
        public ActionResult AddEvent(Events addevent)
        {
            //check if user is accessing view in web browser.
            if (ModelState.IsValid)
            {
                addevent.AddedBy = (int)Session["UserID"];
                //Adds what we recieved from the view to the Create command
                EDA.AddEvent(Emap.Map(addevent));
            }
            return RedirectToAction("ViewEvent");
        }
    }
}