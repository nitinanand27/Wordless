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
         
        public ActionResult RemoveBook(int id)
        {
            if (ModelState.IsValid)
            {
                PurchasedBook book = db.PurchasedBooks.Single(b => b.PurchasedBookId == id);
                db.PurchasedBooks.Remove(book);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
    }
}