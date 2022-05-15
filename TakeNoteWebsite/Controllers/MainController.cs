using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TakeNoteWebsite.Models;
using TakeNoteWebsite.Models.Data;
using TakeNoteWebsite.Models.DeepLearningModel;

namespace TakeNoteWebsite.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        //---------------- GET --------------------------
        //---------------- GET --------------------------
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> SignIn(string username, string password)
        {
            
            if (AuthenticationController.GetCurrentUser(HttpContext) == null)
            await AuthenticationController.SignIn(HttpContext, username, password);
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Entry()
        {
            return View();
        }

        public IActionResult AllEntry()
        {
            return View();
        }

        public IActionResult AllImage()
        {
            return View();
        }

        public IActionResult FolderImage()
        {
            return View();
        }

        //---------------- POST --------------------------
        //---------------- POST --------------------------

        [HttpPost]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            return View();
        }

        [HttpPost]
        public IActionResult FilterEntry(Filter filter)
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveEntry(Entry entry)
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewEntry(Entry entry)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteEntry(int EntryID)
        {
            return View();
        }

        [HttpPost]
        public IActionResult StarEntry(int EntryID)
        {
            return View();
        }

        [HttpPost]
        public IActionResult FilterImage(int EntryID)
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewImage(Image image)
        {
            return View();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
