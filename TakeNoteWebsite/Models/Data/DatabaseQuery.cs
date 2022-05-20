using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TakeNoteWebsite.Models.Data
{
    public class DatabaseQuery
    {
        private static string connectionString = "";
        public static Entry GetFirstEntry(string UserID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("somethinghere", connection);
            return new Entry();
        }
        public static Entry GetEntry(string EntryID)
        {
            return new Entry();
        }
        public static List<Entry> GetListEntry(string UserID)
        {
            List<Entry> results = new List<Entry>();
            return results;
        }
        public static bool NewEntry(string UserID, Entry entry)
        {
            return true;
        }
        public static bool DeleteEntry(string EntryID)
        {
            return true;
        }
        public static bool SaveEntry(string UserID, Entry entry)
        {
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

        public List<Image> searchImage(ImageFilter filter)
        {
            return new List<Image>();
        }
        public List<Folder> getAllImageFolder()
        {
            return new List<Folder>();
        }
    }
}
