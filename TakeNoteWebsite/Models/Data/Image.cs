using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
{
    public class Image
    {
        public int ID { get; set; }
        public int EntryID { get; set; }
        public DateTime Date { get; set; }
        public string Path { get; set; }
    }
}
