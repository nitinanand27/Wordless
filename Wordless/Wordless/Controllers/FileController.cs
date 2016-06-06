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
            return View(CreateFileList());
        }
        public ActionResult ChangeFile(int fileId)
        {
            Session["pdfIdToShow"] = fileId;
            return RedirectToAction("ReadPDF");
        }
        public ActionResult ShowFile()
        {
            {
                var fileToGet = db.Files.Find(Session["pdfIdToShow"]);
                if (fileToGet == null)
                {
                    return null;
                }
                return File(fileToGet.Content, fileToGet.ContentType);
            }
        }
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
                var userId = (int)Session["currentUserId"];
                var user = db.Users.Find(userId);
                
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
    }
}