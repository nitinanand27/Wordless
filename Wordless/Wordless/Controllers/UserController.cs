using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wordless.Models;

namespace Wordless.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            WordlessContext db = new WordlessContext();
           
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