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
    public class PrayerDataAccess
    {
        //Reference the Web config File and create a connection to the sql Server.
        static string connectionstring = ConfigurationManager.ConnectionStrings["churchDB"].ConnectionString;

        //Method to Delete List.
        public bool DeleteList(int ListID)
        {
            bool success = false;
            try
            {
                //Establish connection to Database
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    //establish the command that will be passed to the database
                    //then defining the command
                    using (SqlCommand _command = new SqlCommand("sp_DeleteList", _connection))
                    {
                        //This specifies what command is being used.
                        _command.CommandType = CommandType.StoredProcedure;
                        //Here the values get passed to the command.
                        _command.Parameters.AddWithValue("@ListID", ListID);
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

        public List<PrayerDAO> GetPList()
        {
            //Create List Variables named plist.
            List<PrayerDAO> _Plist = new List<PrayerDAO>();
            try
            {
                //connect to database.
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_ViewList", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _connection.Open();
                        using (SqlDataReader _reader = _command.ExecuteReader())
                        {
                            while (_reader.Read())
                            {
                                DateTime datetime;
                                PrayerDAO _prayertoList = new PrayerDAO();
                                _prayertoList.ListID = _reader.GetInt32(0);
                                _prayertoList.Firstname = _reader.GetString(1);
                                _prayertoList.Lastname = _reader.GetString(2);
                                _prayertoList.DateAdded = _reader.GetDateTime(3);
                                datetime = _prayertoList.DateAdded;
                                string date = datetime.ToString("MM/dd/yyyy");
                                _prayertoList.date = date;
                                _prayertoList.Situation = _reader.GetString(4);
                                _Plist.Add(_prayertoList);
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

            return _Plist;
        }

        public bool AddList(PrayerDAO PD)
        {
            bool success = false;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_CreateList", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@Firstname", PD.Firstname);
                        _command.Parameters.AddWithValue("@Lastname", PD.Lastname);
                        _command.Parameters.AddWithValue("@DateAdded", PD.DateAdded);
                        _command.Parameters.AddWithValue("@Situation", PD.Situation);
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

        public bool UpdateList(PrayerDAO PD)
        {
            bool success = false;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_UpdateList", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@ListID", PD.ListID);
                        _command.Parameters.AddWithValue("@Firstname", PD.Firstname);
                        _command.Parameters.AddWithValue("@Lastname", PD.Lastname);
                        _command.Parameters.AddWithValue("@DateAdded", PD.DateAdded);
                        _command.Parameters.AddWithValue("@Situation", PD.Situation);
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
