using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
{
    public class Image
    {
        public string ID { get; set; }
        public string EntryID { get; set; }
        public DateTime Date { get; set; }
        public string Path { get; set; }
    }
}
