using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wordless.Models;
using System.Data.Entity;

namespace Wordless.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index(string genre)
        {
            WordlessContext db = new WordlessContext();
            if ((bool)Session["loginStatus"])
            {                
                var purchasedbooks = db.PurchasedBooks.ToList();
                var userId = (int)Session["currentUserId"];
                var userPurchases = (from p in purchasedbooks
                                     where p.BuyerId == userId
                                     select p).ToList();
                Session["bookPurchaseList"] = userPurchases;
            }           
            List<Book> bookList = db.Books.Include(b => b.Author).Include(c => c.Comments).ToList();
            if (genre != null)
            {
                var categorySort = from c in bookList
                                   where c.Genre.ToString() == genre
                                   select c;
                return View(categorySort);
            }
            else
            {
                return View(bookList);
            }            
            
            
        }       
        public ActionResult BuyBook (int bookId, bool? confirmed)
        {
            if (!(bool)Session["loginStatus"])
            {
                return RedirectToAction("Index");
            }
            if (bookId != 0 && (bool)Session["loginStatus"])
            {
                WordlessContext db = new WordlessContext();
                var userId = (int)Session["currentUserId"];
                var book = (from b in db.Books
                            where b.BookId == bookId
                            select b).SingleOrDefault();
                var user = (from u in db.Users
                            where u.UserId == userId
                            select u).SingleOrDefault();
                if (confirmed != null)
                {
                    if ((bool)confirmed)
                    {
                        var bookToBuy = new PurchasedBook
                        {
                            DateOfPurchase = DateTime.Now,
                            BookId = book.BookId,
                            BuyerId = userId,
                            Rating = 0
                        };
                        db.PurchasedBooks.Add(bookToBuy);
                        user.Funds -= book.Price;
                        book.TimesPurchased++;
                        book.Author.Funds += book.Price;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }               
                }
                List<Book> bookToBuyView = new List<Book>() { book };
                return View(bookToBuyView);
            }
            else
            {
                return RedirectToAction("Index");
            }          

            
        }
    }
}