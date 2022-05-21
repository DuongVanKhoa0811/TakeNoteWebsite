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

        public IActionResult Entry(string entryID)
        {
            dynamic mymodel = new ExpandoObject();
            Entry a = new Entry();
            
            if (entryID == null || entryID == "") // new entry
            {
                a.Title = "";
                a.Star = false;
                a.Content = "<div id=\"diary\" contenteditable=\"true\" role=\"textbox\" style=\"background-color: whitesmoke; \" data-placeholder=\"Note what you want in here ...\"></div>";
            }
            else
            {
                a = DatabaseQuery.GetEntry(entryID);
            }
            mymodel.MainEntry = a;
            List<Entry> result = DatabaseQuery.GetListEntry("00000");
            mymodel.MainEntry = a;
            mymodel.ListEntry = result;
            mymodel.ImageModel = new ImageModel();
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
            /*
            List<Entry> result = new List<Entry>();
            Entry a = new Entry();
            a.ID = "00001";
            a.Title = "This is the title of the entry!";
            a.Date = new DateTime();
            a.Content = "Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do!";
            a.IsPositive = true;
            result.Add(a);
            Entry b = new Entry();
            b.ID = "00001";
            b.Title = "This is the title of the entry!";
            b.Date = new DateTime();
            b.Content = "Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do! Noi dung gi do!";
            b.IsPositive = false;
            result.Add(b);
            */
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
            /*
            List<Image> imageList = new List<Image>
            {
                new Image
                {
                    Path= "https://tse3.mm.bing.net/th?id=OIP.hcuXQiMZpnCw5j2da_hcVQHaFY&pid=Api",
                    Date = new DateTime(2022, 05, 19),
                    EntryID = "00001",
                    ID = "00001"
                }
            };
            */
            List<Image> imageList = DatabaseQuery.searchImage(currentUser.ID, filter);
            ViewData["Filter"] = filter;
            return View(imageList);
        }
        public IActionResult AllImageFolder()
        {
            List<Folder> folderList = new List<Folder>
            {
                new Folder 
                {
                    ID = "00001",
                    Name = "FML",
                    numImage = 42,
                    RepresentativeImagePaths = "data:image/jpeg;base64,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",
                    
                }
            };
            return View(folderList);
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
        public IActionResult FilterEntry(Filter filter)
        {
            return View();
        }

        [HttpPost]
        public bool SaveEntry(string contentFormat, string content, string title, bool star)
        {
            Entry tmp = new Entry();
            tmp.Content = contentFormat;
            tmp.Date = DateTime.Now;
            tmp.Star = star;
            tmp.Title = title;
            tmp.IsPositive = DeepLearningModel.PositiveNegative(title);
            return DatabaseQuery.SaveEntry("00000", tmp); //AuthenticationController.GetCurrentUser(HttpContext).ID
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
        public bool DeleteEntry(string EntryID)
        {

            return true;
        }

        [HttpPost]
        public IActionResult StarEntry(string EntryID)
        {
            return View();
        }

        [HttpPost]
        public IActionResult FilterImage(string EntryID)
        {
            return View();
        }

        [HttpPost]
        public bool NewImage(List<IFormFile> files, string folderName, string entryID)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            if (folderName == null)
                return false;
            bool auto = (folderName == "Auto");
            List<string> listFolderName = new List<string>();
            List<Tuple<string, string>> firstImageOfFolder = new List<Tuple<string, string>>();

            if (auto)
            {
                listFolderName.Add("Folder 1"); // GetAllFolderName() 
                foreach (string _folderName in listFolderName)
                {
                    firstImageOfFolder.Add( new Tuple<string, string>("download1.jpg", "folderName")); //GetFirstImageOfFolder(_folderName)
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
                string path = Path.Combine(solutionPath + "\\Images\\Upload", fileName);
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
                            Path.Combine(Environment.CurrentDirectory, "Images", "Upload", fileName),
                            Path.Combine(Environment.CurrentDirectory, "Images", "Upload", firstImageOfFolder[i].Item1));
                        if (similarFeature < bestFit)
                        {
                            _folderName = firstImageOfFolder[i].Item2;
                            bestFit = similarFeature;
                        }
                    }
                    //SaveImageToFolder(file.FileName, _folderName, entryID);
                }
                else
                {
                    //SaveImageToFolder(file.FileName, folderName, entryID);
                }
            }
            return true;
        }

        [HttpPost]
        public bool CreateNewFolder(string folderName)
        {
            //SaveFolder(folderName, userID);
            return true;
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
