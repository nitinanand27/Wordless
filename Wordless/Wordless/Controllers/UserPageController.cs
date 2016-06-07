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
            PurchasedBook PurchasedBook = new PurchasedBook();
            PurchasedBookViewModel PurchasedBookViewModel = new PurchasedBookViewModel();

            //ViewBag.List = author.BooksResult.ToList();

            PurchasedBookViewModel.PurchasedBookResult = PurchasedBook.GetAll();

            return View(PurchasedBookViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(PurchasedBookViewModel PurchasedBookViewModel)
        {
            if (ModelState.IsValid)
            {
                PurchasedBook PurchasedBook = new PurchasedBook();

                var books = PurchasedBook.GetAll();
                
                if (!String.IsNullOrEmpty(PurchasedBookViewModel.Keyword))
                {
                    books = books.Where(b => b.Book.Title.ToLower().Contains(PurchasedBookViewModel.Keyword.ToLower())).ToList();
                }
                PurchasedBookViewModel.PurchasedBookResult = books;
            }
            return View(PurchasedBookViewModel);
        }

        public ActionResult Details(int id)
        {
            Book book = db.Books.Single(b => b.BookId == id);

            return View(book);
        }
        
        public ActionResult AddBook(int id)
        {
            Book book = new Book();
            PurchasedBook PurchasedBook = new PurchasedBook();

            if (ModelState.IsValid)
            {
                book = db.Books.Where(b => b.BookId == id).First();

                var currentUserId = int.Parse(Session["currentUserId"].ToString());
                var user = db.Users.Where(u => u.UserId == currentUserId).First();



                db.PurchasedBooks.Add(new PurchasedBook()
                {
                    //Book = book.Title,
                    BookId = book.BookId,
                    //DateOfPurchase = DateTime.Now,
                    //Rating = 0,
                    //Recension = "a",
                    //Buyer = user,
                    BuyerId =user.UserId
                });
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
         
        public ActionResult RemoveBook(int id)
        {
            PurchasedBook book = db.PurchasedBooks.Single(b => b.BookId == id);
            db.PurchasedBooks.Remove(book);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}