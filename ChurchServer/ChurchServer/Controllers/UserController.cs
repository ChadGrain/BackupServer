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
            return View();
        }

        [HttpPost]
        public ActionResult UpdateUser(User update, int UserID)
        {
            //States User model is equal to the Userid Recieved From the View.
            update.UserID = UserID;
            //Takes the New User model and sends it to the UDA
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
            //Resets the session data back to an empty slate.
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
                //states that any new acccount gats the user role.
                newuser.RoleID = 3;
                //Sends instance of User to the Uda through mapper
                UDA.AddUser(Umap.Map(newuser));
            }
                return RedirectToAction("Login");
            
        }
    }
}