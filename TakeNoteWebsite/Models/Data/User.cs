using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models.Data
{
    public class User
    {
        public int ID { get; set; }

        [DisplayName("User name")]
        public string Name { get; set; }
    }
}
