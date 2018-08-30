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
    public class EventDataAccess
    {
        //Reference the Web config File and create a connection to the sql Server.
        static string connectionstring = ConfigurationManager.ConnectionStrings["churchDB"].ConnectionString;

        //Method to Delete Event.
        public bool DeleteEvent(int EventID)
        {
            bool success = false;
            try
            {
                //Establish connection to Database
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    //establish the command that will be passed to the database
                    //then defining the command
                    using (SqlCommand _command = new SqlCommand("sp_DeleteEvent", _connection))
                    {
                        //This specifies what command is being used.
                        _command.CommandType = CommandType.StoredProcedure;
                        //Here the values get passed to the command.
                        _command.Parameters.AddWithValue("@EventID", EventID);
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

        public List<EventDAO> GetEventList()
        {
            //Create List Variables named eventlist.
            List<EventDAO> _eventlist = new List<EventDAO>();
            try
            {
                //connect to database.
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_ViewEvent", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _connection.Open();
                        using (SqlDataReader _reader = _command.ExecuteReader())
                        {
                            while (_reader.Read())
                            {
                                DateTime datetime;
                                EventDAO _eventtoList = new EventDAO();
                                _eventtoList.EventID = _reader.GetInt32(0);
                                _eventtoList.Event = _reader.GetString(1);
                                _eventtoList.EventDesc = _reader.GetString(2);
                                _eventtoList.EventDate = _reader.GetDateTime(3);
                                datetime = _eventtoList.EventDate;
                                string date = datetime.ToString("MM/dd/yyyy");
                                _eventtoList.Date = date;
                                _eventtoList.AddedBy = _reader.GetInt32(4);
                                _eventlist.Add(_eventtoList);
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

            return _eventlist;
        }

        public bool AddEvent(EventDAO _eventCreate)
        {
            bool success = false;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_CreateEvent", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@Event", _eventCreate.Event);
                        _command.Parameters.AddWithValue("@EventDesc", _eventCreate.EventDesc);
                        _command.Parameters.AddWithValue("@EventDate", _eventCreate.EventDate);
                        _command.Parameters.AddWithValue("@AddedBy", _eventCreate.AddedBy);
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

        public bool UpdateEvent(EventDAO ED)
        {
            bool success = false;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand _command = new SqlCommand("sp_UpdateEvent", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@EventID", ED.EventID);
                        _command.Parameters.AddWithValue("@Event", ED.Event);
                        _command.Parameters.AddWithValue("@EventDesc", ED.EventDesc);
                        _command.Parameters.AddWithValue("@EventDate", ED.EventDate);
                        _command.Parameters.AddWithValue("@AddedBy", ED.AddedBy);
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
