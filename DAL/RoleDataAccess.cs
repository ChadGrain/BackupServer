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
        public class RoleDataAccess
        {
            //Reference the Web config File and create a connection to the sql Server.
            static string connectionstring = ConfigurationManager.ConnectionStrings["churchDB"].ConnectionString;

            //Method to Delete Role.
            public bool DeleteRole(int RoleID)
            {
                bool success = false;
                try
                {
                    //Establish connection to Database
                    using (SqlConnection _connection = new SqlConnection(connectionstring))
                    {
                        //establish the command that will be passed to the database
                        //then defining the command
                        using (SqlCommand _command = new SqlCommand("sp_DeleteRole", _connection))
                        {
                            //This specifies what command is being used.
                            _command.CommandType = CommandType.StoredProcedure;
                            //Here the values get passed to the command.
                            _command.Parameters.AddWithValue("@RoleID", RoleID);
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

            public List<RoleDAO> GetRoleList()
            {
                //Create List Variables named rolelist.
                List<RoleDAO> _rolelist = new List<RoleDAO>();
                try
                {
                    //connect to database.
                    using (SqlConnection _connection = new SqlConnection(connectionstring))
                    {
                        using (SqlCommand _command = new SqlCommand("sp_ViewRole", _connection))
                        {
                            _command.CommandType = CommandType.StoredProcedure;
                            _connection.Open();
                            using (SqlDataReader _reader = _command.ExecuteReader())
                            {
                                while (_reader.Read())
                                {
                                    RoleDAO _roletoList = new RoleDAO();
                                    _roletoList.RoleID = _reader.GetInt32(0);
                                    _roletoList.RoleName = _reader.GetString(1);
                                    _roletoList.RoleDesc = _reader.GetString(2);
                                    _rolelist.Add(_roletoList);
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

                return _rolelist;
            }

            public bool AddRole(RoleDAO _roleCreate)
            {
                bool success = false;
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connectionstring))
                    {
                        using (SqlCommand _command = new SqlCommand("sp_CreateRole", _connection))
                        {
                            _command.CommandType = CommandType.StoredProcedure;
                            _command.Parameters.AddWithValue("@RoleName", _roleCreate.RoleName);
                            _command.Parameters.AddWithValue("@Password", _roleCreate.RoleDesc);
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

            public bool UpdateRole(RoleDAO RD)
            {
                bool success = false;
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connectionstring))
                    {
                        using (SqlCommand _command = new SqlCommand("sp_UpdateRole", _connection))
                        {
                            _command.CommandType = CommandType.StoredProcedure;
                            _command.Parameters.AddWithValue("@RoleID", RD.RoleID);
                            _command.Parameters.AddWithValue("@RoleName", RD.RoleName);
                            _command.Parameters.AddWithValue("@RoleDesc", RD.RoleDesc);
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
