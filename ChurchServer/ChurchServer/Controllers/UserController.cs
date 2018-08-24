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
        //This creates an instance of the Usermapper
        static usermapper Umap = new usermapper();
        //This creates an instance of The UserDataAcces
        static UserDataAccess UDA = new UserDataAccess();
        static RoleDataAccess RDA = new RoleDataAccess();
        static rolemapper Rmap = new rolemapper();

        private void PopulateDropDowns()
        {
            RoleViewModel selectrole = new RoleViewModel();

            ViewBag.RoleID = new List<SelectListItem>();

            selectrole.RoleList = Rmap.Map(RDA.GetRoleList());
            foreach (Role role in selectrole.RoleList)
            {
                ViewBag.RoleID.Add(new SelectListItem { Text = role.RoleName.ToString(), Value = role.RoleID.ToString() });
            }
        }

        private void loginfailed()
        {
            int loginfail = 1;
            ViewBag.LoginFail = loginfail;
        }

        // GET: User
        public ActionResult User()
        {
            return View();
        }

        public ActionResult ViewUsers()
        {
            //Creates an instance of UserViewmodel
            UserViewModel selectuser = new UserViewModel();
            //This takes the Viewmodel and sends it through the mapper to the UserDataAccess
            selectuser.UserList = Umap.Map(UDA.GetUserList());
            return View(selectuser);
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            
            return View();
        }

        public ActionResult Delete(int UserID)
        {
            //This is to check if you're an admin
            if (Session["RoleID"].ToString() == "1")
            {
                //This makes sure you dont delete yourself
                if (Session["UserID"].ToString() != UserID.ToString())
                {
                    //Sends the UserID recived from the view and sends it to the UDA.
                    UDA.DeleteUser(UserID);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UpdateUser()
        {
            PopulateDropDowns();
            return View();
        }

        [HttpPost]
        public ActionResult UpdateUser(Users update, int UserID)
        {
            ActionResult response;
            //check if user is accessing view in web browser.
            if (ModelState.IsValid)
            {
                //States User model is equal to the Userid Recieved From the View.
                update.UserID = UserID;
                //Takes the New User model and sends it to the UDA
                UDA.UpdateUser(Umap.Map(update));
                
            }
            else
            {
                PopulateDropDowns();
                response = View(UserID);
            }
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
            //Resets the session data back to an empty slate.
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(Users viewModel)
        {
            //check if user is accessing view in web browser.
            if (ModelState.IsValid)
            {
                try
                {
                    //Run login sp using my view.
                    UserDAO _user = UDA._login(Umap.Map(viewModel));
                    // Put the _user values into session variable
                    Session["UserID"] = _user.UserID;
                    Session["Username"] = _user.Username;
                    Session["RoleID"] = _user.RoleID;
                    ViewBag.LoginFail = 0;
                }
                catch
                {
                    return RedirectToAction("Login", "User");
                }
                if ((int)Session["UserID"] == 0)
                {
                    loginfailed();
                }
            }
            else
            {
                return RedirectToAction("ViewUsers");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Users newuser)
        {
            //check if user is accessing view in web browser.
            if (ModelState.IsValid)
            {
                //states that any new acccount gats the user role.
                newuser.RoleID = 3;
                //Sends instance of User to the Uda through mapper
                UDA.AddUser(Umap.Map(newuser));
            }
                return RedirectToAction("Login");
            
        }
    }
}