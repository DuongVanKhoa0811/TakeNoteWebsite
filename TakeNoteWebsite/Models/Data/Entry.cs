using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
{
    public class Entry
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string TextFont { get; set; }
        public string TextSize { get; set; }
        public string TextColour { get; set; }
        public bool Star { get; set; }
        public bool IsPositive { get; set; }
        public List<String> ImagePaths { get; set; }
    }
}
