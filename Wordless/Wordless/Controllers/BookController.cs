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
        public ActionResult BooksAPI( string searchstring)
        {
            WordlessContext context = new WordlessContext();
            var booklist1 = context.Books.Where(b => b.Title.ToLower().Contains(searchstring.ToLower()));
            var booklist2 = context.Books.Where(b => b.Genre.ToString().ToLower().Contains(searchstring.ToLower()));
            var booklist3 = context.Books.Where(b => b.BookText.ToLower().Contains(searchstring.ToLower()));
            var booklist4 = context.Books.Where(b => b.Author.Name.ToLower().Contains(searchstring.ToLower()));

            var FinalBookList = booklist1.ToList();
            foreach(var b in booklist2)
            {
                if(!FinalBookList.Any(v=> v == b))
                {
                    FinalBookList.Add(b);
                }
            }
            foreach (var b in booklist3)
            {
                if (!FinalBookList.Any(v => v == b))
                {
                    FinalBookList.Add(b);
                }
            }
            foreach (var b in booklist4)
            {
                if (!FinalBookList.Any(v => v == b))
                {
                    FinalBookList.Add(b);
                }
            }
            if (searchstring=="")
            {
                FinalBookList = context.Books.ToList();
            }
            string htmlstring = "";
            foreach(Book b in FinalBookList)
            {
                htmlstring = htmlstring + "<div class='bodyDiv col-md-5 col-lg-5'>"
                            + "<p class='bookTitle'>"+ b.Title+"<button class='pull-right btn btn-default' type='button' onclick='location.href='@Url.Action('Buy', 'Book')''>Buy</button></p>"
                            + "<p class='bookAuthor'>Written by: "+ b.Author.Name+"</p>"
                            + "<p class='bookAuthor'>Genre: "+ b.Genre+"</p>"
                            + "<p class='booktText'>Price: "+ b.Price+"</p>"
                            + "<p class='booktText'>"+ b.BookText+" </p>"
                        + "</div>";
            }
            return Json(new { HtmlString = htmlstring },
                JsonRequestBehavior.AllowGet);
        }

    }
}