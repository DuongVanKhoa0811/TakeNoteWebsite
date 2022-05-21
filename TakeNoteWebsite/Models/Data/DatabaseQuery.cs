using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TakeNoteWebsite.Models.Data
{
    public class DatabaseQuery
    {
        //Tuan: Data Source=LAPTOP-HSGL6DT0\SQLEXPRESS;Initial Catalog=PenZu;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        //Khoa: Data Source=LAPTOP-OKIJ6G4N\\SQLEXPRESS;Initial Catalog=PenZu;Integrated Security=True
        private static string connectionString = "Data Source=LAPTOP-HSGL6DT0\\SQLEXPRESS;Initial Catalog=PenZu;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static Entry GetFirstEntry(string UserID)
        {
            Entry result = new Entry();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select* from GetFirstEntryOfUser(@UserID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    result.Title = oReader["NameEntry"].ToString();
                    result.Date = (DateTime)oReader["DateOfEntry"];
                    if (oReader["Emotion"] == DBNull.Value)
                        result.IsPositive = true;
                    else
                        result.IsPositive = (bool)oReader["Emotion"];
                    if (oReader["Star"] == DBNull.Value)
                        result.Star = false;
                    else
                        result.Star = (bool)oReader["Star"];
                }
            }
            connection.Close();
            return result;
        }
        public static Entry GetEntry(string EntryID)
        {
            Entry result = new Entry();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select* from GetEntryInforByID(@EntryID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@EntryID", EntryID);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    result.Content = oReader["Content"].ToString();
                    result.Title = oReader["NameEntry"].ToString();
                    result.Date = (DateTime)oReader["DateOfEntry"];
                    if (oReader["Emotion"] == DBNull.Value)
                        result.IsPositive = true;
                    else
                        result.IsPositive = (bool)oReader["Emotion"];
                    if (oReader["Star"] == DBNull.Value)
                        result.Star = false;
                    else
                        result.Star = (bool)oReader["Star"];
                }
            }
            connection.Close();
            return result;
        }
        public static List<Entry> GetListEntry(string UserID)
        {
            List<Entry> results = new List<Entry>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select* from GetListEntryByUserID(@UserID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    Entry result = new Entry();
                    result.Content = oReader["Content"].ToString();
                    result.Title = oReader["NameEntry"].ToString();
                    result.Date = (DateTime)oReader["DateOfEntry"];
                    if (oReader["Emotion"] == DBNull.Value || (string)oReader["Emotion"] == "1")
                        result.IsPositive = true;
                    else
                        result.IsPositive = false;
                    if (oReader["Star"] == DBNull.Value || !(bool)oReader["Star"])
                        result.Star = false;
                    else
                        result.Star = (bool)oReader["Star"];
                    results.Add(result);
                }
            }
            connection.Close();

            return results;
        }
        public static bool NewEntry(string UserID, Entry entry)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_NewEntry", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nameEntry", entry.Title);
            cmd.Parameters.AddWithValue("@content", entry.Content);
            cmd.Parameters.AddWithValue("@star", entry.Star);
            cmd.Parameters.AddWithValue("@emotion", entry.IsPositive);
            cmd.Parameters.AddWithValue("@dateofentry", entry.Date);
            cmd.Parameters.AddWithValue("@dateupload", entry.Date);
            cmd.Parameters.AddWithValue("@userid", UserID);
            cmd.Parameters.AddWithValue("@textfont", DBNull.Value);
            cmd.Parameters.AddWithValue("@fontstyle", DBNull.Value);
            cmd.Parameters.AddWithValue("@sizeoftext", DBNull.Value);
            cmd.Parameters.AddWithValue("@result", DBNull.Value);
            cmd.ExecuteReader();
            if (cmd.Parameters["@result"].ToString() == "0")
            {
                connection.Close();
                return false;
            }
            connection.Close();
            return true;
        }
        public static bool DeleteEntry(string EntryID)
        {
            return true;
        }
        public static bool SaveEntry(string UserID, Entry entry)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_SaveEntry", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@entryid", entry.ID);
            cmd.Parameters.AddWithValue("@nameEntry", entry.Title);
            cmd.Parameters.AddWithValue("@content", entry.Content);
            cmd.Parameters.AddWithValue("@star", entry.Star);
            cmd.Parameters.AddWithValue("@emotion", entry.IsPositive);
            cmd.Parameters.AddWithValue("@dateofentry", entry.Date);
            cmd.Parameters.AddWithValue("@dateupload", entry.Date);
            cmd.Parameters.AddWithValue("@userid", UserID);
            cmd.Parameters.AddWithValue("@textfont", DBNull.Value);
            cmd.Parameters.AddWithValue("@fontstyle", DBNull.Value);
            cmd.Parameters.AddWithValue("@sizeoftext", DBNull.Value);
            cmd.Parameters.AddWithValue("@result", DBNull.Value);
            cmd.ExecuteReader();
            if (cmd.Parameters["@result"].ToString() == "0")
            {
                connection.Close();
                return false;
            }
            connection.Close();
            return true;
        }
        public static List<Image> GetListImage(string UserID, string Folder, Filter filter)
        {
            List<Image> result = new List<Image>();
            return result;
        }
        public static bool NewImage(string UserID, Image image)
        {
            return true;
        }
        public static bool DeleteImage(string ImageID)
        {
            return true;
        }

        public static bool newUser(User user)
        {
            return true;
        }

        public static int getUserID(string username)
        {
            return 0;
        }
        public static User GetUser(string UID)
        {
            return new User
            {
                FirstName = "Tony",
                LastName = "Stark",
                UserName = "TonyStark",
                ID = "00123"
            };
        }
        public static bool signIn(string username, string password)
        {
                return true;
        }

        public static List<Image> searchImage(string UserID, ImageFilter filter)
        {
            DateTime localDate = DateTime.Now;
            DateTime StartDate;
            TimeSpan timeSpan;

            List<Image> results = new List<Image>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Since;
            string query = "select i.ImageID, i.EntryID, e.DateOfEntry, i.ImagePath from Img as i join EntryTable as e on i.EntryID=e.EntryID join Folder as f on i.FolderID=f.FolderID where i.UserID =" + UserID + " ";
            if (filter.Since != "N/A")
            {
                if (filter.Since == "yesterday")
                {
                    timeSpan = new System.TimeSpan(1, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }

                if (filter.Since == "thisWeek")
                {
                    timeSpan = new System.TimeSpan((int)localDate.DayOfWeek, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }

                if (filter.Since == "lastWeek")
                {
                    timeSpan = new System.TimeSpan((int)localDate.DayOfWeek + 7, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }

                if (filter.Since == "last7Days")
                {
                    timeSpan = new System.TimeSpan(7, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }

                if (filter.Since == "thisMonth")
                {
                    timeSpan = new System.TimeSpan(localDate.Day, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }

                if (filter.Since == "lastMonth")
                {
                    if (localDate.Month != 1)

                        StartDate = new DateTime(localDate.Year, localDate.Month - 1, 1);
                    else
                        StartDate = new DateTime(localDate.Year - 1, 12, 1);
                }

                if (filter.Since == "thisYear")
                {
                    StartDate = new DateTime(localDate.Year, 1, 1);
                }

                else
                {
                    StartDate = new DateTime(localDate.Year - 1, 1, 1);
                }

                Since = StartDate.ToString("yyyy’-‘MM’-‘dd");
                query = query + "and '" + StartDate + "' <= e.DateOfEntry ";
            }
                
				
			if (filter.PositiveNegative != "N/A")
				query = query + "and e.Emotion="+ filter.PositiveNegative+" ";
					
			if (filter.Folder != "")
				query = query + "and f.FolderID="+ filter.Folder+ " ";

            if (filter.SortBy == "latest")
                query = query + "Order by e.DateOfEntry DESC ";

            else
                if (filter.SortBy == "oldest")
                query = query + "Order by e.DateOfEntry ";
					
			
            SqlCommand cmd = new SqlCommand(query, connection);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    Image result = new Image();
                    result.ID = oReader["ImageID"].ToString();
                    result.EntryID = oReader["EntryID"].ToString();
                    result.Date = (DateTime)oReader["DateOfEntry"];
					result.Path = oReader["ImagePath"].ToString();
                    results.Add(result);
                }
            }
            connection.Close();
            return results;
        }

        public static List<Entry> searchEntry(string UserID, Filter filter)
        {
            DateTime localDate = DateTime.Now;
            DateTime StartDate;
            TimeSpan timeSpan;
            List<Entry> results = new List<Entry>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Since;
            string query = "select e.EntryID, e.NameEntry, e.DateOfEntry, e.Emotion, e.Star from EntryTable as e where e.UserID =" + UserID + " ";
            if (filter.Since != "N/A")
            {
                if (filter.Since == "yesterday")
                {
                    timeSpan = new TimeSpan(1, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }

                if (filter.Since == "thisWeek")
                {
                    timeSpan = new System.TimeSpan((int)localDate.DayOfWeek, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }

                if (filter.Since == "lastWeek")
                {
                    timeSpan = new System.TimeSpan((int)localDate.DayOfWeek + 7, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }

                if (filter.Since == "last7Days")
                {
                    timeSpan = new System.TimeSpan(7, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }
                     
                if (filter.Since == "thisMonth")
                {
                    timeSpan = new System.TimeSpan(localDate.Day, 0, 0, 0);
                    StartDate = localDate - timeSpan;
                }

                if (filter.Since == "lastMonth")
                    if (localDate.Month != 1)
                        StartDate = new DateTime(localDate.Year, localDate.Month - 1, 1);
                    else
                        StartDate = new DateTime(localDate.Year - 1, 12, 1);
                if (filter.Since == "thisYear")
                    StartDate = new DateTime(localDate.Year, 1, 1);
                else //lastYear
                    StartDate = new DateTime(localDate.Year - 1, 1, 1);

                Since = StartDate.ToString("yyyy’-‘MM’-‘dd");
                query = query + "and '" + StartDate + "' <= e.DateOfEntry ";
            }

				
				
				
				
			if (filter.PositiveNegative != "N/A")
				query = query + "and e.Emotion="+ filter.PositiveNegative+ " ";
			
			if (filter.Starred != "N/A")
				query = query + "and e.Star=1 ";

            if (filter.KeyWord != "")
                query = query + "and e.TEXT like '%" + filter.KeyWord + "%' ";
				
			
            SqlCommand cmd = new SqlCommand(query, connection);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    Entry result = new Entry();
                    result.ID = oReader["EntryID"].ToString();
                    result.Title = oReader["NameEntry"].ToString();
                    result.Date = (DateTime)oReader["DateOfEntry"];
					result.TextFont = oReader["TextFont"].ToString();
					result.TextSize = oReader["SizeOfText"].ToString();
					result.TextColour = oReader["TextColour"].ToString();
					result.Star = (bool)oReader["Star"];
					if (oReader["Emotion"].ToString()=="Positive")
						result.IsPositive = true;
					else result.IsPositive = false;
					
					string queryForEachEntry = "select i.ImagePath from Img as i where i.EntryIDID ="+result.ID+" ";
					SqlCommand cmd1 = new SqlCommand(queryForEachEntry, connection);
					result.ImagePaths = new List<string>();
					using (SqlDataReader oReader1 = cmd1.ExecuteReader())
					{
						while (oReader.Read())
						{
								result.ImagePaths.Add(oReader1["ImagePath"].ToString());
						}
					}
                    results.Add(result);
                }
            }
            connection.Close();
            return results;
        }
		
        public static List<Folder> getAllImageFolder()
        {
            return new List<Folder>();
        }
    }
}
