using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wordless.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            ///Get values from the form entered by the user in /Default/Login.cshtml
            string tmpUsername = Request["registerUsername"];
            string tmpEmail = Request["registerEmail"];
            string tmpPassword = Request["registerPassword"];

            if(tmpEmail.IndexOf || tmpUsername.Length <= 1 || 
                tmpUsername.Length > 15 || tmpPassword.Length<6)
            {

                ///Return to login/register -view to try again
                TempData["error"] = "Please check username, email and password!";
                return Redirect("/Default/Login");
            }

            try
            {
                ///Connect to dB and create user
                using (NewsterContext nc = new NewsterContext())
                {
                    string passwdEncrypted = HelpClass.Encrypt(tmpPassword);

                    ///Check for existing username
                    if (nc.Users.Where(x=>x.UserName.ToLower() == tmpUsername.ToLower()).Count() == 0)
                    {
                        User tmpUser = new User() { UserName = tmpUsername, Email = tmpEmail, Password = passwdEncrypted ,Confirmed=false };
                        nc.Users.Add(tmpUser);
                        nc.SaveChanges();
                    }

                    else
                    {
                        TempData["error"] = "User exists!";
                        return Redirect("/Default/Login");
                    }
                }

                return Redirect("/Default/Index");
            }

            catch
            {
                ///Return to login/register -view to try again
                TempData["error"] = "Database fail!";
                return Redirect("/Default/Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}