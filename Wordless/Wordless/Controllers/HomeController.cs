using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wordless.Models;
using System.Data.Entity;

namespace Wordless.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            
           
            return View();
        }

        //Need books in Database to Run Search Function
        public ActionResult Search()
        {
            WordlessContext db = new WordlessContext();

            string searchString = Request["searchString"];

            var bookList = db.Books.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                //checks if search text matches book title, description or author name irrespective of case
                var searchResult = bookList.Where(b => (b.Title.ToLower().Contains(searchString.ToLower())) ||
                (b.BookText.ToLower().Contains(searchString.ToLower()))
                || (b.Author.Name.ToLower().Contains(searchString.ToLower()))).ToList();

                //if match result is positive
                if (searchResult.Count() != 0)
                {
                    //returns a view with search result
                    return View("Index", searchResult);
                }
            }

            return RedirectToAction("Index");

        }
        public ActionResult Test()
        {
            WordlessContext db = new WordlessContext();
            db.Books.Count();
            return View();
        }
    }
}