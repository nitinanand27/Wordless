using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wordless.Models;
using System.Data.Entity;

namespace Wordless.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Register()
        {
            WordlessContext db = new WordlessContext();
            string username = Request["username"];
            string password= Request["password"];
            string email = Request["email"]; 
            string name = Request["name"];
           
            if (db.Users.Where(n => n.Username.ToLower() == username.ToLower()).Count() == 0)
            {
                User user = new User { Name = name, Username = username, Password = password, Email = email };
                db.Users.Add(user);
                db.SaveChanges();               
            }

            else
            {
                TempData["error"] = "Username taken";
                return View();
                //return Redirect("/User/Register");
            }

            return View();  //redirect
        }

        public ActionResult Login()
        {
            try
            {
                WordlessContext db = new WordlessContext();
                string username = Request["username"];
                string password = Request["password"];

                var userList = db.Users.Where(u => u.Username.ToLower() == username.ToLower()).ToList();

                if (userList.Count() >= 1 && userList.First().Password == password)
                {
                    ///Set session values
                    Session["currentUserId"] = userList.First().UserId;
                    Session["currentUsername"] = userList.First().Username;
                    Session["loginStatus"] = true;

                    TempData["error"] = "Welcome "+ Session["currentUsername"];
                    //return Redirect("Register");
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["error"] = "Check username and/or password";
                    return View(); // Redirect("/Default/Login");
                }
            }
            catch
            {              
                ///Return to login/register -view to try again
                TempData["error"] = "Database fail!";
                return View(); // Redirect("/Default/Login");           
            }
        }
        public ActionResult Logout()
        {
            ///reset session values
            Session["currentUserId"] = "";
            Session["currentUsername"] = "";
            Session["loginStatus"] = false;
                        
            return Redirect("/Home/Index");
        }
    }
}