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
        //Create instance of the Prayer Mapper named Pmap
        static prayermapper Pmap = new prayermapper();
        //Create an instance of The PrayerDataAccess Called PDA
        static PrayerDataAccess PDA = new PrayerDataAccess();

        // GET: Prayer
        public ActionResult ViewPrayer()
        {
            //Create an instance of PrayerViewModel named selectprayer
            PrayerViewModel selectprayer = new PrayerViewModel();
            //Sends Prayerlist to the mapper to be converted for PDA
            selectprayer.PrayerList = Pmap.Map(PDA.GetPList());
            return View(selectprayer);
        }

        public ActionResult AddList()
        {
            return View();
        }

        public ActionResult Delete(int ListID)
        {
            //Sends our ListID to PDA for the Delete Function
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
            //States the ListID
            Update.ListID = ListID;
            int AddedBy = (int)Session["UserID"];
            Update.AddedBy = AddedBy;
            //Sends Updated to the update command
            PDA.UpdateList(Pmap.Map(Update));

            return RedirectToAction("ViewPrayer", "Prayer");
        }

        [HttpPost]
        public ActionResult AddList(Prayer addprayer)
        {
            //check if user is accessing view in web browser.
            if (ModelState.IsValid)
            {
                addprayer.AddedBy = (int)Session["UserID"];
                //Adds what we recieved from the view to the Create command
                PDA.AddList(Pmap.Map(addprayer));
            }
            return RedirectToAction("ViewPrayer");
        }
    }
}