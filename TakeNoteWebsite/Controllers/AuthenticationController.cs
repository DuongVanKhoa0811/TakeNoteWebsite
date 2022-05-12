using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeNoteWebsite.Data;

namespace TakeNoteWebsite.Controllers
{
    public class AuthenticationController : Controller
    {
        static User GetCurrentUser()
        {
            return new User();
        } 
        static bool SignIn(string UserName, string password)
        {
            return true;
        }
        static bool CheckAuthenticationEntry(int EntryID)
        {
            return true;
        }
        static bool CheckAuthenticationImage(int ImageID)
        {
            return true;
        }
    }
}
