﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Entry()
        {
            Entry a = new Entry();
            a.Star = true;
            a.Content = "<b><div id=\"diary\" contenteditable=\"true\" role=\"textbox\" style=\"background-color: whitesmoke; \" data-placeholder=\"Note what you want in here ...\"></div></b>";
            PartialView(a);
            return View(a);
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
        public IActionResult SignIn(string userName, string password)
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
