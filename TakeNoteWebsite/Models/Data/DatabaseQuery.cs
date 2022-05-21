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
        private static string connectionString = "Data Source=LAPTOP-OKIJ6G4N\\SQLEXPRESS;Initial Catalog=PenZu;Integrated Security=True";
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
            if (cmd.Parameters["@result"].ToString() == "0")
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

        public static int GetUserID(string username)
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
        public static bool SignIn(string username, string password)
        {
            return true;
        }

        public List<Image> SearchImage(ImageFilter filter)
        {
            return new List<Image>();
        }
        public List<Folder> GetAllImageFolder()
        {
            return new List<Folder>();
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
