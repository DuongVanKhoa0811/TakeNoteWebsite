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
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Firstname { get; set; }
    }
}
