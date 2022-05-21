using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
{
    public class Filter
    {
        public string Since { get; set; }
        public string PositiveNegative { get; set; }
        public string Starred { get; set; }
        public string KeyWord { get; set; }
    }
}
