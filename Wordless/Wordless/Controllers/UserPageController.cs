using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wordless.Models;
using Wordless.ViewModel;

namespace Wordless.Controllers
{
    public class UserPageController : Controller
    {
        WordlessContext db = new WordlessContext();

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            User user = new User();

            UserPageViewModel userfilter = new UserPageViewModel();

            userfilter.UserResult = user.GetAll;

            return View(userfilter);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(UserPageViewModel userfilter)
        {
            if (ModelState.IsValid)
            {
                User user = new User();

                var users = user.GetAll;

                if (!String.IsNullOrEmpty(userfilter.username))
                {
                    users = users.Where(u => u.Username.ToLower().Contains(userfilter.username.ToLower())).ToList();
                }
                userfilter.UserResult = users;
            }

            return View(userfilter);
        }
    }
}