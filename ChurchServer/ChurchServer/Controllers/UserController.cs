using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChurchServer.Models;
using DAL.objects;
using DAL;

namespace ChurchServer.Controllers
{
    public class UserController : Controller
    {
        static usermapper Umap = new usermapper();
        static UserDataAccess UDA = new UserDataAccess();

        // GET: User
        public ActionResult User()
        {
            return View();
        }

        public ActionResult ViewUsers()
        {
            UserViewModel selectuser = new UserViewModel();
            selectuser.UserList = Umap.Map(UDA.GetUserList());
            return View(selectuser);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult Delete(int UserID)
        {
            if (Session["RoleID"].ToString() == "1")
            {
                if (Session["UserID"].ToString() != UserID.ToString())
                {
                    UDA.DeleteUser(UserID);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UpdateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateUser(User update, int UserID)
        {
            update.UserID = UserID;
            UDA.UpdateUser(Umap.Map(update));

            return RedirectToAction("ViewUsers", "User");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(User viewModel)
        {
            //check if user is accessing view in web browser.
            if (ModelState.IsValid)
            {

                //Run login sp using my view.
                UserDAO _user = UDA._login(Umap.Map(viewModel));
                // Put the _user values into session variable
                Session["UserID"] = _user.UserID;
                Session["Username"] = _user.Username;
                Session["RoleID"] = _user.RoleID;
            }
            else
            {
                return RedirectToAction("ViewUsers");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User newuser)
        {
            //check if user is accessing view in web browser.
            if (ModelState.IsValid)
            {
                newuser.RoleID = 3;
                UDA.AddUser(Umap.Map(newuser));
            }
                return RedirectToAction("Login");
            
        }
    }
}