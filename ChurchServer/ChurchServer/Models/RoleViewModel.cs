using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchServer.Models
{
    public class RoleViewModel
    {
        public Role SingleRole { get; set; }
        public List<Role> RoleList { get; set; }

        public RoleViewModel()
        {
            SingleRole = new Role();
            RoleList = new List<Role>();
        }
    }

}