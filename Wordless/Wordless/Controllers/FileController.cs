using System;
using System.Collections.Generic;
using Wordless.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Wordless.Controllers
{
    public class FileController : Controller
    {
        WordlessContext db = new WordlessContext();        
        public List<PurchasedBook> CreateFileList()
        {
            //en lista på vilka filer en användare har laddat upp
            var userId = (int)Session["currentUserId"];
            var purchasedBookList = (from l in db.PurchasedBooks
                        where l.BuyerId == userId
                        select l).ToList();
            return purchasedBookList;
        }
        //var bookList = db.PurchasedBooks.Include(b => b.Book).ToList();
        //List<Book> bookList = db.Books.Include(b => b.Author).Include(c => c.Comments).ToList();
        public ActionResult UploadPDF()
        {
            return View();
        }
        public ActionResult ReadPDF()
        {
            SetListsForViews();
            return View(CreateFileList());
        }
        public ActionResult ChangeFile(int fileId)
        {
            //ändrar vilken fil som ska visas i browsern som ett sessions-objekt
            Session["pdfIdToShow"] = fileId;
            return RedirectToAction("ReadPDF");
        }
        public ActionResult ShowFile()
        {
            {
                //visa filen i browsern från sessions-objektet
                var fileToGet = db.Files.Find(Session["pdfIdToShow"]);
                if (fileToGet == null)
                {
                    return null;
                }
                return File(fileToGet.Content, fileToGet.ContentType);
            }
        }
        //när man ska se PDF:en i helskärm
        public ActionResult GetFile(int fileId)
        {
            if (fileId == 0)
            {
                return View("Index");
            }
            else
            {
                var fileToGet = db.Files.Find(fileId);
                return File(fileToGet.Content, fileToGet.ContentType);
            }
        }
        //när man vill ladda hem PDF:en
        public ActionResult DownloadFIle(int fileId)
        {
            if (fileId == 0)
            {
                return View("Index");
            }
            else
            {
                var fileToGet = db.Files.Find(fileId);
                return File(fileToGet.Content, fileToGet.ContentType, fileToGet.FileName);
            }
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase upload)
        {
            if (upload == null)
            {
                return View("Index");
            }
            else
            {
                // hämtar userid och användare
                var userId = (int)Session["currentUserId"];
                var user = db.Users.Find(userId);
                //skapa ny fil
                var newFile = new File
                {
                    FileName = System.IO.Path.GetFileName(upload.FileName),
                    ContentType = upload.ContentType,
                    User = user,
                    UploadedOn = DateTime.Now
                    
                };
                if (newFile.ContentType == "application/pdf")
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        newFile.Content = reader.ReadBytes(upload.ContentLength);
                    }        
                    db.Files.Add(newFile);

                    Book book = new Book
                    {
                        Title = "Mästerdetektiven Blomkvist",
                        TimesPurchased = 10,
                        BookText = "Det är sommarlov och de tre 13-åriga kompisarna Kalle, Anders och Eva-Lotta har det lite småtråkigt.",
                        Price = 20,
                        Author = user,
                        Genre = Genres.Crime,
                        File = newFile
                    };
                    db.Books.Add(book);


                    db.SaveChanges();




                    return Redirect("/AuthorPage/Create");
                }
                else
                {
                    return View("Index", CreateFileList());
                }
            }
        }
        public ActionResult PDFPartialView()
        {
            return PartialView();
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