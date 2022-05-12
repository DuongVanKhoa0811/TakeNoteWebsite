using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
{
    public class DatabaseQuery
    {
        public Entry GetFirstEntry(int UserID)
        {
            return new Entry();
        }
        public Entry GetEntry(int EntryID)
        {
            return new Entry();
        }
        public List<Entry> GetListEntry(int UserID)
        {
            List<Entry> results = new List<Entry>();
            return results;
        }
        public bool NewEntry(int UserID, Entry entry)
        {
            return true;
        }
        public bool DeleteEntry(int EntryID)
        {
            return true;
        }
        public bool SaveEntry(int UserID, Entry entry)
        {
            return true;
        }
        public List<Image> GetListImage(int UserID)
        {
            List<Image> result = new List<Image>();
            return result;
        }
        public bool NewImage(int UserID, Image image)
        {
            return true;
        }
        public bool DeleteImage(int ImageID)
        {
            return true;
        }
    }
}
