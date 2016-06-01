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
            using (WordlessContext db = new WordlessContext())
            {
                Session["MostDownloaded"] = (from d in db.Books
                                             orderby d.TimesPurchased descending
                                             select d).ToList();
                var listFromDb = db.PurchasedBooks.Include(b => b.Book).ToList();
                List<PurchasedBook> purchasedList = new List<PurchasedBook>();
                foreach (var item in listFromDb)
                {
                    var times = db.PurchasedBooks.Where(t => t.BookId == item.BookId).Count();
                    if (times > 1 && purchasedList.Any(f => f.BookId == item.BookId) == false)
                    {
                        var sum = db.PurchasedBooks.Where(t => t.BookId == item.BookId).Sum(t => t.Rating);
                        var avgRating = sum / times;
                        item.Rating = avgRating;
                    }
                    if (purchasedList.Any(f => f.BookId == item.BookId) == false)
                    {
                        purchasedList.Add(item);
                    }
                }
                Session["BestRating"] = purchasedList;
                Session["MostCommented"] = (from c in db.Books.Include(b => b.Comments)
                                            orderby c.Comments.Count() descending
                                            select c).ToList();
            }
            
           
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

    }
}