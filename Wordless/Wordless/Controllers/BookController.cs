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
        public ActionResult Index()
        {
            WordlessContext db = new WordlessContext();
            var bookList = db.Books.Include(b => b.Author).Include(c => c.Comments).ToList();
            
            return View(bookList);
        }
        public ActionResult BookByGenre(int id)
        {
            WordlessContext db = new WordlessContext();
            var booklist = db.Books.Include(b => b.Author).Include(c => c.Comments).Where(b => b.Genre== (Genres)id).ToList();
            return View("Index", booklist);
        }

    }
}