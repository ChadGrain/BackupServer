using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DAL.objects;
using ErrorLogger;

namespace DAL
{
    public class UserDataAccess
    {
        //Reference the Web config File and create a connection to the sql Server.
        static string connectionstring = ConfigurationManager.ConnectionStrings["churchDB"].ConnectionString;

        //Method to Delete User.
        public bool DeleteUser(int UserID)
        {
            bool success = false;
            try
            {
                //Establish connection to Database
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    //establish the command that will be passed to the database
                    //then defining the command
                    using (SqlCommand _command = new SqlCommand("sp_DeleteUser", _connection))
                    {
                        //This specifies what command is being used.
                        _command.CommandType = CommandType.StoredProcedure;
                        //Here the values get passed to the command.
                        _command.Parameters.AddWithValue("@UserID", UserID);
                        //Now We open the Connection
                        _connection.Open();
                        //Exexute the command with our addded Data
                        _command.ExecuteNonQuery();
                        success = true;
                        //Once the action is completed Close the connection
                        _connection.Close();
                    }
                }
            }
            catch (Exception error)
            {
                Errorlog Problem = new Errorlog();
                Problem.LogError(error);
            }

            return success;
        }

        public List<UserDAO> GetUserList()
        {
            //Create List Variables named userlist.
            List<UserDAO> _userlist = new List<UserDAO>();
            try
            {
                //connect to database.
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_ViewUsers", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _connection.Open();
                        using (SqlDataReader _reader = _command.ExecuteReader())
                        {
                            while (_reader.Read())
                            {
                                UserDAO _usertoList = new UserDAO();
                                _usertoList.UserID = _reader.GetInt32(0);
                                _usertoList.Username = _reader.GetString(1);
                                _usertoList.Password = _reader.GetString(2);
                                _usertoList.Firstname = _reader.GetString(3);
                                _usertoList.Lastname = _reader.GetString(4);
                                _usertoList.Address = _reader.GetString(5);
                                _usertoList.RoleID = _reader.GetInt32(6);
                                _usertoList.RoleName = _reader.GetString(7);
                                _userlist.Add(_usertoList);
                            }

                        }
                    }
                }
            }
            catch (Exception error)
            {
                Errorlog Problem = new Errorlog();
                Problem.LogError(error);
            }

            return _userlist;
        }

        public UserDAO _login(UserDAO _userLogin)
        {
            UserDAO _loginUser = new UserDAO();
            bool success = false;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_login", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@Username", _userLogin.Username);
                        _command.Parameters.AddWithValue("@Password", _userLogin.Password);
                        _connection.Open();
                        _command.ExecuteNonQuery();
                        using (SqlDataReader _butterbean = _command.ExecuteReader())
                        {
                            while (_butterbean.Read())
                            {
                                _loginUser.UserID = _butterbean.GetInt32(0);
                                _loginUser.Username = _butterbean.GetString(1);
                                _loginUser.Password = _butterbean.GetString(2);
                                _loginUser.Firstname = _butterbean.GetString(3);
                                _loginUser.Lastname = _butterbean.GetString(4);
                                _loginUser.Address = _butterbean.GetString(5);
                                _loginUser.RoleID = _butterbean.GetInt32(6);
                                _loginUser.RoleName = _butterbean.GetString(7);
                            }
                        }
                        _connection.Close();

                    }
                }
            }
            catch (Exception error)
            {
                Errorlog Problem = new Errorlog();
                Problem.LogError(error);
            }
            finally
            {

            }
            return _loginUser;
        }

        public bool AddUser(UserDAO _userCreate)
        {
            bool success = false;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_CreateUser", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@Username", _userCreate.Username);
                        _command.Parameters.AddWithValue("@Password", _userCreate.Password);
                        _command.Parameters.AddWithValue("@Firstname", _userCreate.Firstname);
                        _command.Parameters.AddWithValue("@Lastname", _userCreate.Lastname);
                        _command.Parameters.AddWithValue("@Address", _userCreate.Address);
                        _command.Parameters.AddWithValue("@RoleID", _userCreate.RoleID);
                        _connection.Open();
                        _command.ExecuteNonQuery();
                        success = true;
                        _connection.Close();
                    }
                }
            }
            catch (Exception error)
            {
                Errorlog Problem = new Errorlog();
                Problem.LogError(error);
            }

            return success;
        }

        public bool UpdateUser(UserDAO UD)
        {
            bool success = false;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_UpdateUser", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@UserID", UD.UserID);
                        _command.Parameters.AddWithValue("@Username", UD.Username);
                        _command.Parameters.AddWithValue("@Password", UD.Password);
                        _command.Parameters.AddWithValue("@Firstname", UD.Firstname);
                        _command.Parameters.AddWithValue("@Lastname", UD.Lastname);
                        _command.Parameters.AddWithValue("@Address", UD.Address);
                        _command.Parameters.AddWithValue("@RoleID", UD.RoleID);
                        _connection.Open();
                        _command.ExecuteNonQuery();
                        success = true;
                        _connection.Close();
                    }
                }
            }
            catch (Exception error)
            {
                Errorlog Problem = new Errorlog();
                Problem.LogError(error);
            }

            return success;
        }

    }
}
