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

            string name = Request["name"];
            string username = Request["username"];
            string password = Request["password"];
            string email = Request["email"];
            bool author = false;

            if (Request["isAuthor"] != null)
            {
                author = bool.Parse(Request["isAuthor"]);
            }
            if (name == null || username == null || password == null || email == null)
            {
                TempData["error"] = "";
                return View();
            }       

            if (db.Users.Where(n => n.Username.ToLower() == username.ToLower()).Count() == 0)
            {
                User user = new User { Name = name, Username = username, Password = password, Email = email, Author = author };
                db.Users.Add(user);
                db.SaveChanges();
                TempData["error"] = "Registered successfully";
                return View();
            }

            else if (db.Users.Where(u => u.Username == username).Count() > 0 || db.Users.Where(e => e.Email == email).Count() > 0)
            {
                TempData["error"] = "User already exists";
                return View();
            }

            else
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            try
            {
                WordlessContext db = new WordlessContext();
                string username = Request["username"];
                string password = Request["password"];


                var userList = db.Users.Where(u => u.Username.ToLower() == username.ToLower()).ToList();



                if (userList.Count() == 1 && userList.First().Password == password)
                {
                    ///Set session values
                    Session["currentUserId"] = userList.First().UserId;
                    Session["currentUsername"] = userList.First().Username;
                    Session["loginStatus"] = true;

                    if (username.ToLower() == "patrik")
                    {
                        Session["Admin"] = true;
                    }

                    else if (userList.Where(u => u.Username == username).First().Author)
                    {
                        Session["isAuthor"] = true;
                    }

                    TempData["error"] = "Welcome " + Session["currentUsername"];
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["error"] = "Check username and/or password";
                    return RedirectToAction("Register"); // Redirect("/Default/Login");
                }
            }
            catch
            {
                ///Return to login/register -view to try again
                TempData["error"] = "Database fail!";
                return View("Register"); // Redirect("/Default/Login");           
            }
        }
        public ActionResult Logout()
        {
            ///reset session values
            Session["currentUserId"] = "";
            Session["currentUsername"] = "";
            Session["currentUserLastName"] = "";
            Session["loginStatus"] = false;
            Session["Admin"] = false;
            Session["isAuthor"] = false;

            return RedirectToAction("Register"); //Go back to register view on succesful logout
        }
        public ActionResult UserHome()
        {
            if ((bool)Session["loginStatus"])
            {
                ViewBag.Message = "Welcome " + Session["currentUsername"];
                var userId = Convert.ToDouble(Session["currentUserId"]);
                WordlessContext db = new WordlessContext();
                var userInfo = (db.Users
                    .Include(b => b.WrittenBooks)
                    .Include(c => c.Comments)
                    .Include(p => p.PurchasedBooks)
                    .Where(u => u.UserId == userId)).SingleOrDefault();
                return View(userInfo);
            }
            else
            {
                return Redirect("/user/Register");
            }

        }

        public ActionResult RegisterLogin()
        {
            return View();
        }
    }
}