using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wordless.Models;

namespace Wordless
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            //Id for which pdf to show
            Session["pdfIdToShow"] = 0;
            ///Id for the current logged in user(int)
            Session["currentUserId"] = 0;
            ///Name for the current logged in user (string)
            Session["currentUsername"] = "";
            ///Logged in or not (bool)
            Session["loginStatus"] = false;
            //list of purchase
            Session["MostDownloaded"] = new List<Book>();
            Session["BestRating"] = new List<Book>();
            Session["MostCommented"] = new List<Book>();
            Session["bookPurchaseList"] = new List<PurchasedBook>();
            Session["Admin"] = false;
            Session["isAuthor"] = false;

            WordlessContext db = new WordlessContext();
            Session["MostDownloaded"] = (from d in db.Books
                                         orderby d.TimesPurchased descending
                                         select d).Take(4).ToList();
            var listFromDb = db.PurchasedBooks.Include(u => u.Buyer).ToList();
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

                var sortedList = (from x in purchasedList
                                  orderby x.Rating descending
                                  select x).Take(4).ToList();
                Session["BestRating"] = sortedList;
                Session["MostCommented"] = (from b in db.Books
                                            orderby b.Comments.Count() descending
                                            select b).Take(4).ToList();
            }
        }
    }
}
