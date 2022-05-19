using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
{
    public class ImageFilter
    {
        public string Since { get; set; }
        public string SortBy { get; set; }
        public int Folder { get; set; }
        public string PositiveNegative { get; set; }
    }
}
