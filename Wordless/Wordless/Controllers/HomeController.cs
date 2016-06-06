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
            SetListsForViews();
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
        public void SetListsForViews()
        {
            WordlessContext db = new WordlessContext();
            //sparar mest nedladdade i en lista
            ViewBag.MostDownloaded = (from d in db.Books
                                         orderby d.TimesPurchased descending
                                         select d).Take(4).ToList();
            var listFromDb = db.PurchasedBooks.Include(u => u.Buyer).ToList();
            List<PurchasedBook> purchasedList = new List<PurchasedBook>();
            //räknar ut snittet på rating
            foreach (var item in listFromDb)
            {
                //hur många gånger en bok har blvit köpt
                var times = db.PurchasedBooks.Where(t => t.BookId == item.BookId).Count();
                if (times > 1 && purchasedList.Any(f => f.BookId == item.BookId) == false)
                {
                    //totala summan av alla ratings
                    var sum = db.PurchasedBooks.Where(t => t.BookId == item.BookId).Sum(t => t.Rating);
                    //summan delat på antalet nedladdingar(förutsatt att alla har gett en rating :) ) 
                    var avgRating = sum / times;
                    item.Rating = avgRating;
                }
                if (purchasedList.Any(f => f.BookId == item.BookId) == false)
                {
                    purchasedList.Add(item);
                }
                //sorterar listan
                var sortedList = (from x in purchasedList
                                  orderby x.Rating descending
                                  select x).Take(4).ToList();
                ViewBag.BestRating = sortedList;
                //skapar en lista av antalet kommentarer
                ViewBag.MostCommented = (from b in db.Books
                                            orderby b.Comments.Count() descending
                                            select b).Take(4).ToList();
            }
        }
    }
}