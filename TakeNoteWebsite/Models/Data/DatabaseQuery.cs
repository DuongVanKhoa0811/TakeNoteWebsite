﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
{
    public class DatabaseQuery
    {
        public static Entry GetFirstEntry(int UserID)
        {
            return new Entry();
        }
        public static Entry GetEntry(int EntryID)
        {
            return new Entry();
        }
        public static List<Entry> GetListEntry(int UserID)
        {
            List<Entry> results = new List<Entry>();
            return results;
        }
        public static bool NewEntry(int UserID, Entry entry)
        {
            return true;
        }
        public static bool DeleteEntry(int EntryID)
        {
            return true;
        }
        public static bool SaveEntry(int UserID, Entry entry)
        {
            return true;
        }
        public static List<Image> GetListImage(int UserID, string Folder, Filter filter)
        {
            List<Image> result = new List<Image>();
            return result;
        }
        public static bool NewImage(int UserID, Image image)
        {
            return true;
        }
        public static bool DeleteImage(int ImageID)
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
        public static User GetUser(int UID)
        {
            return new User
            {
                FirstName = "Tony",
                LastName = "Stark",
                UserName = "TonyStark",
                ID = 123
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
