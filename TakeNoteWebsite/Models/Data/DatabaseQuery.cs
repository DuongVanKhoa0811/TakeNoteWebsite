﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TakeNoteWebsite.Models.Data
{
    public class DatabaseQuery
    {
        //Tuan: Data Source=LAPTOP-HSGL6DT0\\SQLEXPRESS;Initial Catalog=PenZu;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
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
                    result.ID = oReader["EntryID"].ToString();
                    result.Title = oReader["NameEntry"].ToString();
                    result.Date = (DateTime)oReader["DateOfEntry"];
                    if (oReader["Emotion"] == DBNull.Value || oReader["Emotion"].ToString() == "P") 
                        result.IsPositive = true;
                    else
                        result.IsPositive = false;
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
                    if (oReader["Emotion"] == DBNull.Value || (string)oReader["Emotion"] == "P")
                        result.IsPositive = true;
                    else
                        result.IsPositive = false;
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
                    result.ID = oReader["EntryID"].ToString();
                    result.Content = oReader["Content"].ToString();
                    result.Title = oReader["NameEntry"].ToString();
                    result.Date = (DateTime)oReader["DateOfEntry"];
                    if (oReader["Emotion"] == DBNull.Value || (string)oReader["Emotion"] == "P")
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
            cmd.Parameters.AddWithValue("@emotion", entry.IsPositive ? "P" : "N");
            cmd.Parameters.AddWithValue("@dateofentry", entry.Date);
            cmd.Parameters.AddWithValue("@dateupload", entry.Date);
            cmd.Parameters.AddWithValue("@userid", UserID);
            cmd.Parameters.AddWithValue("@textfont", DBNull.Value);
            cmd.Parameters.AddWithValue("@fontstyle", DBNull.Value);
            cmd.Parameters.AddWithValue("@sizeoftext", DBNull.Value);
            cmd.Parameters.AddWithValue("@result", DBNull.Value);
            cmd.ExecuteReader();
            string check = cmd.Parameters["@result"].Value.ToString();
            if (check == "0")
            {
                connection.Close();
                return false;
            }
            connection.Close();
            return true;
        }
        public static bool DeleteEntry(string entryID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_DeleteEntry", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@entryID", entryID);
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
            cmd.Parameters.AddWithValue("@emotion", entry.IsPositive ? "P" : "N");
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
        public static Image GetFirstImageOfFolder(string UserID, string FolderName)
        {
            Image result = new Image();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select* from GetFirstImageOfFolder(@UserID, @FolderName)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@FolderName", FolderName);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    result.Path = oReader["ImagePath"].ToString();
                }
            }
            connection.Close();
            return result;
        }
        public static bool NewImage(string UserID, Image image, string FolderName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_NewImage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@FolderName", FolderName);
            cmd.Parameters.AddWithValue("@EntryID", image.EntryID);
            cmd.Parameters.AddWithValue("@ImagePath", image.Path);
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
        public static bool DeleteImage(string ImageID)
        {
            return true;
        }

        public static bool NewUser(User user)
        {
            return true;
        }

        public static string GetUserID(string username)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select* from GetUserID(@userName)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserName", username);

            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    return oReader["UserID"].ToString();
                }
            }
            return "";
        }
        public static User GetUser(string UID)
        {
            User result = new User();
            
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select u.UserID, u.LastName, u.FirstName, u.UserName from UserAccount as u where u.UserID = '" + UID  +"'", connection);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    result.ID = oReader["UserID"].ToString();
                    result.LastName = oReader["LastName"].ToString();
                    result.FirstName = oReader["FirstName"].ToString();
                    result.UserName = oReader["UserName"].ToString();
                }
            }

            return result;
        }
        public static bool SignIn(string username, string password)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select u.UserID from UserAccount as u where u.UserName = '" + username +"' and u.Pass = '" +password +"'", connection);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    return true;
                }
            }
            return false;
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
            string query = "select i.ImageID, i.EntryID, e.DateOfEntry, i.ImagePath from Img as i join EntryTable as e on i.EntryID=e.EntryID join Folder as f on i.FolderID=f.FolderID where i.UserID = '" + UserID + "' ";
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
				query = query + "and e.Emotion= '"+ filter.PositiveNegative+"' ";
					
			if (filter.Folder != "")
				query = query + "and f.FolderID= '"+ filter.Folder+ "' ";

            if (filter.SortBy == "latest")
                query = query + "Order by e.DateOfEntry DESC ";

            else
                if (filter.SortBy == "oldest")
                query = query + "Order by e.DateOfEntry ";
					
			
            SqlCommand cmd = new SqlCommand(query, connection);
            using ( SqlDataReader oReader = cmd.ExecuteReader())
            {
                cmd.Dispose();
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
            string query = "select e.EntryID, e.NameEntry, e.DateOfEntry, e.Emotion, e.Star, e.Content from EntryTable as e where e.UserID = '" + UserID + "' ";
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
				query = query + "and e.Emotion= '"+ filter.PositiveNegative+ "' ";
			
			if (filter.Starred != "N/A")
				query = query + "and e.Star=1 ";

            if (filter.KeyWord != "")
                query = query + "and e.Content like '%" + filter.KeyWord + "%' ";
				
			
            SqlCommand cmd = new SqlCommand(query, connection);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    Entry result = new Entry();
                    result.ID = oReader["EntryID"].ToString();
                    result.Title = oReader["NameEntry"].ToString();
                    result.Date = (DateTime)oReader["DateOfEntry"];
                    result.Content = oReader["Content"].ToString();
					result.TextFont = "";
					result.TextSize = "";
					result.TextColour = "";
					result.Star = (bool)oReader["Star"];
					if (oReader["Emotion"].ToString()=="P")
						result.IsPositive = true;
					else result.IsPositive = false;

                    results.Add(result);
                }
            }

            string queryForEachEntry;
            SqlCommand cmd1;
            foreach (Entry entry in results)
            {
                queryForEachEntry = "select i.ImagePath from Img as i where i.EntryID = '" + entry.ID + "' ";
                cmd1 = new SqlCommand(queryForEachEntry, connection);
                using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                {
                    
                    entry.ImagePaths = new List<string>();

                    while (oReader1.Read())
                    {
                        entry.ImagePaths.Add(oReader1["ImagePath"].ToString());
                    }
                }
            }

            
            connection.Close();
            return results;
        }
		
        public static List<Folder> getAllImageFolder(string UserID)
        {
            List<Folder> result = new List<Folder>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select* from getAllImageFolderByUserID(@UserID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);

            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    Folder tmp = new Folder
                    {
                        ID = oReader["FolderID"].ToString(),
                        Name = oReader["FolderName"].ToString(),
                        numImage = (int)oReader["NumberOfImage"]
                    };
                }
            }

            return result;
        }
        public static List<string> GetAllFolderName(string UserID)
        {
            List<string> results = new List<string>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select* from GetListFolderNameByUserID(@UserID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    string result = (string)oReader["FolderName"];
                    results.Add(result);
                }
            }
            connection.Close();
            return results;
        }
        public static bool CreaetNewFolder(string userID, string folderName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_createFolder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@folderName", folderName);
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
    }
}
