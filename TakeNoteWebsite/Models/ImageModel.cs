using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TakeNoteWebsite.Models
{
    public class ImageModel
    {
        [DisplayName("Image name")]
        /*[StringLength(maximumLength: 25, MinimumLength = 10, ErrorMessage = "Length must be between 10 to 25")]*/
        public string ImageName { get; set; }

        [DisplayName("Upload files")]
        public IFormFile ImageFile { get; set; }
    }
}
