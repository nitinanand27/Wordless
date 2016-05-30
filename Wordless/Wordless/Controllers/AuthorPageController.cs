using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wordless.Models;
using Wordless.ViewModel;

namespace Wordless.Controllers
{
    public class AuthorPageController : Controller
    {
        WordlessContext db = new WordlessContext();
        
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            Book book = new Book();
            AuthorPageViewModel author = new AuthorPageViewModel();

            //ViewBag.List = author.BooksResult.ToList();

            author.BooksResult = book.GetAll;
            
            return View(author);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(AuthorPageViewModel author)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book();

                var books = book.GetAll;

                //if (author.Price > 0)
                //{
                //    books = books.Where(b => b.Price >= author.Price).ToList();
                //}

                //books = books.Where(b => b.TimesPurchased >= author.TimesPurchased).ToList();
               
                if (!String.IsNullOrEmpty(author.Keyword))
                {
                    books = books.Where(b => b.Title.ToLower().Contains(author.Keyword.ToLower())).ToList();
                }
                author.BooksResult = books;
            }
            return View(author);
        }

        public ActionResult Details(int id)
        {
            Book book = db.Books.Single(b => b.BookId == id);
            
            return View(book);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book book = db.Books.Single(b => b.BookId == id);

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                Book bookFromDB = db.Books.Single(b => b.BookId == book.BookId);

                bookFromDB.Title = book.Title;
                bookFromDB.TimesPurchased = book.TimesPurchased;
                bookFromDB.Genre = book.Genre;
                bookFromDB.Price = book.Price;
                bookFromDB.Comments = book.Comments;

                db.SaveChanges();

                return RedirectToAction("Details", new { id = book.BookId });
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Book book = db.Books.Single(b => b.BookId == id);

            if (book == null)
            {
                return HttpNotFound();
            }
            
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Book book = db.Books.Single(b => b.BookId == id);
            db.Books.Remove(book);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}