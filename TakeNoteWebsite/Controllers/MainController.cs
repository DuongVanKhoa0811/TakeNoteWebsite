using Microsoft.AspNetCore.Http;
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
                Entry firstEntry = DatabaseQuery.GetFirstEntry(currentUser.ID);
                string firstID = firstEntry.ID;
                return RedirectToAction("Entry", "Main", firstID);
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

        public IActionResult Entry(string entryID)
        {
            User currentUser = AuthenticationController.GetCurrentUser(HttpContext);
            if (currentUser == null)
            {
                return RedirectToAction("SignIn");
            }
            ViewData["Username"] = currentUser.FirstName + " " + currentUser.LastName;
            dynamic mymodel = new ExpandoObject();
            Entry a = new Entry();
            if (entryID == null || entryID == "") // new entry
            {
                a.ID = "";
                a.Title = "";
                a.Star = false;
                a.Content = "<div id=\"diary\" contenteditable=\"true\" role=\"textbox\" style=\"background-color: whitesmoke; \" data-placeholder=\"Note what you want in here ...\"></div>";
            }
            else
            {
                a = DatabaseQuery.GetEntry(entryID);
                a.ID = entryID;
            }
            mymodel.MainEntry = a;
            List<Entry> result = DatabaseQuery.GetListEntry(currentUser.ID);
            for(int i = 0; i < result.Count; i++)
                if(result[i].ID == entryID)
                {
                    result.RemoveAt(i);
                    break;
                }
            mymodel.MainEntry = a;
            mymodel.ListEntry = result;
            mymodel.ImageModel = new ImageModel();
            mymodel.ListFolderName = DatabaseQuery.GetAllFolderName(currentUser.ID);
            return View(mymodel);
        }

        public IActionResult AllEntry(string keyword = "",  string positiveNegative = "N/A", string starred = "N/A", string since = "N/A")
        {
            User currentUser = AuthenticationController.GetCurrentUser(HttpContext);
            if (currentUser == null)
            {
                return RedirectToAction("SignIn");
            }
            Filter filter = new Filter
            {
                KeyWord = keyword,
                PositiveNegative = positiveNegative,
                Starred = starred,
                Since = since
            };
            ViewData["FilterSince"] = filter.Since;
            ViewData["FilterStarred"] = filter.Starred;
            ViewData["FilterPositiveNegative"] = filter.PositiveNegative;
            ViewData["FilterKeyword"] = filter.KeyWord;
            List<Entry> result = DatabaseQuery.searchEntry(currentUser.ID, filter);
            return View(result);
        }

        public IActionResult ImageFolder(string folderID, string since = "N/A", string sortBy = "latest", string positiveNegative = "N/A")
        {
            User currentUser = AuthenticationController.GetCurrentUser(HttpContext);
            if (currentUser == null)
            {
                return RedirectToAction("SignIn");
            }
            ImageFilter filter = new ImageFilter()
            {
                Folder = folderID,
                Since = since,
                SortBy = sortBy,
                PositiveNegative = positiveNegative
            };
            List<Image> imageList = DatabaseQuery.searchImage(currentUser.ID, filter);
            ViewData["Filter"] = filter;
            return View(imageList);
        }
        public IActionResult AllImageFolder()
        {
            User currentUser = AuthenticationController.GetCurrentUser(HttpContext);
            if (currentUser == null)
            {
                return RedirectToAction("SignIn");
            }
            List<Folder> folderList = DatabaseQuery.getAllImageFolder(currentUser.ID);
            return View(folderList);
        }
        public IActionResult AllImageEntry(string entryID)
        {
            List<Image> imageList = DatabaseQuery.GetListImageByEntry("00000", entryID);
            return View(imageList);
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
                    string userID = DatabaseQuery.GetUserID(userName);
                    Entry firstEntry = DatabaseQuery.GetFirstEntry(userID);
                    if (firstEntry.ID == null)
                    {
                        return RedirectToAction("Entry");
                    }
                    string firstID = firstEntry.ID;
                    return RedirectToAction("Entry", "Main", new { entryID = firstID });
                }
                else
                {
                    //sign in failed
                    ViewData["Error"] = "Something's wrong, please check your username and password again";
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
            if (error == "Success")
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
        public bool SaveEntry(string entryID, string contentFormat, string content, string title, bool star)
        {
            Entry tmp = new Entry();
            tmp.ID = entryID;
            tmp.Content = contentFormat;
            tmp.Date = DateTime.Now;
            tmp.Star = star;
            tmp.Title = title;
            tmp.IsPositive = DeepLearningModel.PositiveNegative(title);
            User currentUser = AuthenticationController.GetCurrentUser(HttpContext);

            return DatabaseQuery.SaveEntry(currentUser.ID, tmp); //AuthenticationController.GetCurrentUser(HttpContext).ID
        }

        [HttpPost]
        public bool NewEntry(string contentFormat, string content, string title, bool star)
        {
            Entry tmp = new Entry();
            tmp.Content = contentFormat;
            tmp.Date = DateTime.Now;
            tmp.Star = star;
            tmp.Title = title;
            tmp.IsPositive = DeepLearningModel.PositiveNegative(title);
            return DatabaseQuery.NewEntry("00000", tmp); //AuthenticationController.GetCurrentUser(HttpContext).ID
        }

        [HttpPost]
        public bool DeleteEntry(string entryID)
        {
            return DatabaseQuery.DeleteEntry(entryID);
        }

        [HttpPost]

        [HttpPost]
        public bool NewImage(List<IFormFile> files, string folderName, string entryID)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            if (folderName == null) // || typeof entryID === "undefined" || entryID == null
                return false;
            bool auto = (folderName == "Auto");
            List<string> listFolderName = new List<string>();
            List<Tuple<string, string>> firstImageOfFolder = new List<Tuple<string, string>>();
            Image imageTmp = new Image();
            imageTmp.EntryID = entryID;
            if (auto)
            {
                listFolderName = DatabaseQuery.GetAllFolderName("00000"); 
                foreach (string _folderName in listFolderName)
                {
                    var imageName = DatabaseQuery.GetFirstImageOfFolder("00000", _folderName).Path;
                    if (imageName == null)
                        return false;
                    firstImageOfFolder.Add( 
                        new Tuple<string, string>(imageName, _folderName));
                }
            }
            foreach(IFormFile file in files)
            {
                if (file == null)
                    continue;
                string solutionPath = Environment.CurrentDirectory;
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                imageTmp.Path = fileName;
                string path = Path.Combine(solutionPath + "\\wwwroot\\Images\\Upload", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                if (auto)
                {
                    float bestFit = float.MaxValue;
                    string _folderName = "";
                    for(int i = 0; i < firstImageOfFolder.Count; i++)
                    {
                        float similarFeature = DeepLearningModel.SimilarFeature(
                            Path.Combine(Environment.CurrentDirectory, "wwwroot", "Images", "Upload", fileName),
                            Path.Combine(Environment.CurrentDirectory , "wwwroot", "Images", "Upload", firstImageOfFolder[i].Item1));
                        if (similarFeature < bestFit)
                        {
                            _folderName = firstImageOfFolder[i].Item2;
                            bestFit = similarFeature;
                        }
                    }
                    if (!DatabaseQuery.NewImage("00000", imageTmp, _folderName))
                        return false;
                }
                else
                {
                    if (!DatabaseQuery.NewImage("00000", imageTmp, folderName))
                        return false;
                }
            }
            return true;
        }

        [HttpPost]
        public bool CreateNewFolder(string folderName)
        {
            return DatabaseQuery.CreaetNewFolder("00000", folderName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
