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
            //if a genre was provided, return a list of books of the genre
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
        [HttpPost]
        public ActionResult SaveComment (string comment, int bookId)
        {
            var hej = comment.Replace("\r\n", "<br />");

            WordlessContext db = new WordlessContext();
            if (!(bool)Session["loginStatus"] || comment == null)
            {
                List<Book> bookReturn = (from b in db.Books
                                  select b).ToList();
                return View("Book", bookReturn);
            }
            if ((bool)Session["loginStatus"])
            {

            
            var findBook = db.Books.Where(b => b.BookId == bookId).FirstOrDefault();
            var userId = (int)Session["currentUserId"];
            var newComment = new Comment()
            {
                BookId = findBook.BookId,
                CommentText = hej,
                Date = DateTime.Now,
                UserId = (int)Session["currentUserId"]
            };
            db.Comments.Add(newComment);
            db.SaveChanges();
            }
            db = new WordlessContext();
            List<Book> book = (from b in db.Books
                        where b.BookId == bookId
                        select b).ToList();
            return View("BookDetails", book);
        }
        public ActionResult BuyBook (int bookId, bool? confirmed)
        {
            WordlessContext db = new WordlessContext();
            //check if logged in
            if (!(bool)Session["loginStatus"] || bookId == 0)
            {
                return RedirectToAction("Index");
            }
            //if logged in and a bookId is provided
            if (bookId != 0 && (bool)Session["loginStatus"])
            {
                //get userId
                var userId = (int)Session["currentUserId"];
                //get book
                Book book = (from b in db.Books
                            where b.BookId == bookId
                            select b).First();
                //get user
                User user = (from u in db.Users
                            where u.UserId == userId
                            select u).FirstOrDefault();
                //if user confirmed
                if (confirmed != null)
                {
                    if ((bool)confirmed)
                    {
                        //create new book-purchase
                        var bookToBuy = new PurchasedBook
                        {
                            DateOfPurchase = DateTime.Now,
                            BookId = book.BookId,
                            BuyerId = userId,
                            Rating = 0
                        };
                        //add a purchase
                        db.PurchasedBooks.Add(bookToBuy);
                        //give author cash
                        book.Author.Funds += book.Price;
                        //remove cash from user
                        user.Funds -= book.Price;
                        //incremenet TimesPurchase by 1
                        book.TimesPurchased++;
                        //save changes
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }               
                }
                List<Book> bookToBuyView = new List<Book>()
                {
                    book
                };
                return View(bookToBuyView);
            }
            else
            {
                var book = (from b in db.Books.Include(c => c.Comments)
                            where b.BookId == bookId
                             select b).ToList();
                return View("Index", book);
            }          

            
        }
        public ActionResult BookDetails (int? bookId)
        {
            WordlessContext db = new WordlessContext();
            List<Book> bookList = new List<Book>();
            if (bookId != 0)
            {
                bookList = (from b in db.Books
                            where b.BookId == bookId
                            select b).ToList();
            }
            else
            {
                bookList = (from b in db.Books
                                select b).ToList();
            }
            
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