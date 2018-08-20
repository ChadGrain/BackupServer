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
    public class PrayerController : Controller
    {
        static prayermapper Pmap = new prayermapper();
        static PrayerDataAccess PDA = new PrayerDataAccess();

        // GET: Prayer
        public ActionResult ViewPrayer()
        {
            PrayerViewModel selectprayer = new PrayerViewModel();
            selectprayer.PrayerList = Pmap.Map(PDA.GetPList());
            return View(selectprayer);
        }

        public ActionResult AddList()
        {
            return View();
        }

        public ActionResult Delete(int ListID)
        {
            PDA.DeleteList(ListID);
        
            return RedirectToAction("ViewPrayer");
        }

        public ActionResult UpdateList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateList(Prayer Update,int ListID)
        {
            Update.ListID = ListID;
            PDA.UpdateList(Pmap.Map(Update));

            return RedirectToAction("ViewPrayer", "Prayer");
        }

        [HttpPost]
        public ActionResult AddList(Prayer addprayer)
        {
            //check if user is accessing view in web browser.
            if (ModelState.IsValid)
            {                
                    PDA.AddList(Pmap.Map(addprayer));
            }
            return RedirectToAction("ViewPrayer");
        }
    }
}