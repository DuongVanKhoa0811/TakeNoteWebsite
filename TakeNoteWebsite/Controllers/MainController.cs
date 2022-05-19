using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
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
            /*if (DeepLearningModel.PositiveNegative("I am very happy."))
                ViewData["Tit"] = "Positive";
            else
                ViewData["Tit"] = "Negative";*/

            /*ViewData["Tit"] = DeepLearningModel.SimilarFeature(
                Path.Combine(Environment.CurrentDirectory, "Images", "download1.jpg")
                , Path.Combine(Environment.CurrentDirectory, "Images", "download3.jpg"));*/
            ViewData["Tit"] = "Hello!";
            var a = "fwfawef";

                // , Path.Combine(Environment.CurrentDirectory, "Images", "download3.jpg"));
            User currentUser = AuthenticationController.GetCurrentUser(HttpContext);
            if (currentUser != null)
                ViewData["Username"] = currentUser.UserName;
            else
                ViewData["Username"] = "";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            User currentUser = AuthenticationController.GetCurrentUser(HttpContext);
            if (currentUser == null)
                return View();
            else
            {
                //Entry firstEntry = DatabaseQuery.GetFirstEntry(currentUser.ID);
                return RedirectToAction("Index");//RedirectToAction("Entry", firstEntry.ID);
            }
        }
        
        public async Task<IActionResult> signOut()
        {
            await AuthenticationController.SignOut(HttpContext);
            return RedirectToAction("SignIn");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Entry(int userID)
        {
            dynamic mymodel = new ExpandoObject();
            Entry a = new Entry();
            a.Star = true;
            a.Content = "<b><div id=\"diary\" contenteditable=\"true\" role=\"textbox\" style=\"background-color: whitesmoke; \" data-placeholder=\"Note what you want in here ...\"></div></b>";
            mymodel.MainEntry = a;
            List<Entry> result = new List<Entry>();
            Entry c = new Entry();
            c.ID = 10;
            c.Title = "This is the title of the entry!";
            c.Date = new DateTime();
            c.Content = "Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do!";
            c.IsPositive = true;
            result.Add(c);
            if (userID == 10)
            {
                Entry b = new Entry();
                b.ID = 20;
                b.Title = "This is the title of the entry!";
                b.Date = new DateTime();
                b.Content = "Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do!";
                b.IsPositive = false;
                result.Add(b);
            }
            mymodel.ListEntry = result;
            return View(mymodel);
        }

        public IActionResult AllEntry()
        {
            List<Entry> result = new List<Entry>();
            Entry a = new Entry();
            a.ID = 10;
            a.Title = "This is the title of the entry!";
            a.Date = new DateTime();
            a.Content = "Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do!";
            a.IsPositive = true;
            result.Add(a);
            Entry b = new Entry();
            b.ID = 20;
            b.Title = "This is the title of the entry!";
            b.Date = new DateTime();
            b.Content = "Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do!";
            b.IsPositive = false;
            result.Add(b);
            return View(result);
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
        public async Task<IActionResult> SignIn(string userName, string password)
        {
            try
            {
                if (await AuthenticationController.SignIn(HttpContext, userName, password))
                {
                    //sign in successed
                    User currentUser = AuthenticationController.GetCurrentUser(HttpContext);
                    //Entry firstEntry = DatabaseQuery.GetFirstEntry(currentUser.ID);
                    return RedirectToAction("Index"); //RedirectToAction("Entry", firstEntry.ID);
                }
                else
                {
                    //sign in failed
                    ViewData["Error"] = "Invalid username or password";
                    return View();
                }

            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            string error = AuthenticationController.SignUp(user);
            if (error == "success")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["Error"] = error;
                return View();
            }
            
        }


        [HttpPost]
        public IActionResult FilterEntry(Filter filter)
        {
            return View();
        }

        [HttpPost]
        public bool SaveEntry(string content, string title, bool star)
        {
            return true;
        }

        [HttpPost]
        public IActionResult NewEntry(Entry entry)
        {
            return View();
        }

        [HttpPost]
        public bool DeleteEntry(int EntryID)
        {

            return true;
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
