using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wordless.Controllers
{
    [Authorize]
    public class FacebookController : Controller
    {
        // GET: Facebook
        public ActionResult Index()
        {
            return View();
        }
    }
}