using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchServer.Models
{
    public class UserViewModel
    {
        public Users SingleUser { get; set; }
        public List<Users> UserList { get; set; }

        public UserViewModel()
        {
            SingleUser = new Users();
            UserList = new List<Users>();
        }
    }
}