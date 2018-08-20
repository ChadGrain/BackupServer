using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.objects;

namespace ChurchServer.Models
{
    public class usermapper
    {
        public List<User> Map(List<UserDAO> _UserListMap)
        {
            //mapper used to get all the data from the data access layer for th book view mode
            //From the player dao object
            //Use this mapper in the httpGET user controller action
            List<User> _UserListToReturn = new List<User>();

            _UserListMap.OrderBy(x => x.Firstname).ToList();

            foreach (UserDAO _usertomap in _UserListMap)
            {
                User _usertoview = new User();
                _usertoview.UserID = _usertomap.UserID;
                _usertoview.Username = _usertomap.Username;
                _usertoview.Password = _usertomap.Password;
                _usertoview.Firstname = _usertomap.Firstname;
                _usertoview.Lastname = _usertomap.Lastname;
                _usertoview.Address = _usertomap.Address;
                _usertoview.RoleID = _usertomap.RoleID;
                _usertoview.RoleName = _usertomap.RoleName;
                _UserListToReturn.Add(_usertoview);
            }
            return _UserListToReturn;
        }

        public UserDAO Map(User _usertomap)
        {
            UserDAO _usertoview = new UserDAO();
            _usertoview.UserID = _usertomap.UserID;
            _usertoview.Username = _usertomap.Username;
            _usertoview.Password = _usertomap.Password;
            _usertoview.Firstname = _usertomap.Firstname;
            _usertoview.Lastname = _usertomap.Lastname;
            _usertoview.Address = _usertomap.Address;
            _usertoview.RoleID = _usertomap.RoleID;
            _usertoview.RoleName = _usertomap.RoleName;
            return _usertoview;
        }
    }
}