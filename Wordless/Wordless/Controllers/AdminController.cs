using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wordless.Models;
using Wordless.ViewModel;

namespace Wordless.Controllers
{
    public class AdminController : Controller
    {
        WordlessContext db = new WordlessContext();

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            User user = new User();

            AdminViewModel admin = new AdminViewModel();

            admin.UserResult = user.GetAll();

            return View(admin);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(AdminViewModel admin)
        {
            if (ModelState.IsValid)
            {
                User user = new User();

                var users = user.GetAll();

                if (!String.IsNullOrEmpty(admin.username))
                {
                    users = users.Where(u => u.Username.ToLower().Contains(admin.username.ToLower())).ToList();
                }
                admin.UserResult = users;
            }

            return View(admin);
        }


        public ActionResult Details(int id)
        {
            User user = db.Users.Single(u => u.UserId == id);

            return View(user);
        }
        

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            User user = db.Users.Single(u => u.UserId == id);

            return View(user);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                User userFromDB = db.Users.Single(u => u.UserId == user.UserId);

                userFromDB.Name = user.Name;
                userFromDB.Username = user.Username;
                userFromDB.Password = user.Password;
                userFromDB.Email = user.Email;
                userFromDB.Funds = user.Funds;

                db.SaveChanges();

                return RedirectToAction("Details", new { id = user.UserId });
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            User user = db.Users.Single(b => b.UserId == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            User user = db.Users.Single(b => b.UserId == id);
            db.Users.Remove(user);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}