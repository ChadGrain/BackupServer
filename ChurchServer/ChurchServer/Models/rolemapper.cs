using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.objects;

namespace ChurchServer.Models
{
    public class rolemapper
    {
        public List<Role> Map(List<RoleDAO> _RoleListMap)
        {
            //mapper used to get all the data from the data access layer for th book view mode
            //From the player dao object
            //Use this mapper in the httpGET user controller action
            List<Role> _RoleListToReturn = new List<Role>();

            foreach (RoleDAO _roletomap in _RoleListMap)
            {
                Role _roletoview = new Role();
                _roletoview.RoleID = _roletomap.RoleID;
                _roletoview.RoleName = _roletomap.RoleName;
                _roletoview.RoleDesc = _roletomap.RoleDesc;
                _RoleListToReturn.Add(_roletoview);
            }
            return _RoleListToReturn;
        }

        public RoleDAO Map(Role _roletomap)
        {
            RoleDAO _roletoview = new RoleDAO();
            _roletoview.RoleID = _roletomap.RoleID;
            _roletoview.RoleName = _roletomap.RoleName;
            _roletoview.RoleDesc = _roletomap.RoleName;
            return _roletoview;
        }
    }
}