using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
{
    public class Folder
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int numImage { get; set; }
        public string RepresentativeImagePaths { get; set; }
    }
}
