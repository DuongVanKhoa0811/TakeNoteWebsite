using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TakeNoteWebsite.Models.Data;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace TakeNoteWebsite.Controllers
{
    public static class AuthenticationController
    {
        private static int GetCurrentUserID(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return -1;

            string userId = httpContext.User.FindFirst("UID")?.Value;
            return Int32.Parse(userId);
        }
        public static User GetCurrentUser(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return null;
            User user = new User
            {
                FirstName = httpContext.User.FindFirst("FirstName")?.Value,
                LastName = httpContext.User.FindFirst("LastName")?.Value,
                UserName = httpContext.User.FindFirst("UserName")?.Value,
                ID = httpContext.User.FindFirst("UID")?.Value
            };
            return user;
        }
        public static async Task<bool> SignIn(HttpContext httpContext, string userName, string password)
        {
            //check if the username and password is correct
            if (!DatabaseQuery.SignIn(userName, password))
            {
                return false;
            }
            //sign in if correct
            string uid = DatabaseQuery.GetUserID(userName);
            User user = DatabaseQuery.GetUser(uid);
            /*
            User user = new User
            {
                FirstName = "Tony",
                LastName = "Stark",
                ID = "00000",
                UserName = userName
            };
            */

            var claims = new List<Claim>
            {
                new Claim("Username", user.UserName),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("UID", user.ID),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return true;
        }
        public static async Task<bool> SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }
        public static bool CheckAuthenticationEntry(HttpContext httpContext, int EntryID)
        {
            return true;
        }
        public static bool CheckAuthenticationImage(HttpContext httpContext, int ImageID)
        {
            return true;
        }
        public static string SignUp(User user)
        {
            return "Success";
        }
    }
}
