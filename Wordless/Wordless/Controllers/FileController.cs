using System;
using System.Collections.Generic;
using Wordless.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wordless.Controllers
{
    public class FileController : Controller
    {
        WordlessContext db = new WordlessContext();
        List<File> fileList = new List<File>();
        public List<File> CreateFileList()
        {
            fileList = (from l in db.Files
                        select l).ToList();
            return fileList;
        }
        public ActionResult Index()
        {
            return View(CreateFileList());
        }
        public ActionResult ChangeFile(int id)
        {
            Session["pdfIdToShow"] = id;
            return RedirectToAction("Index");
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
        public ActionResult UploadFile(HttpPostedFileBase upload, int? id)
        {
            if (upload == null)
            {
                return View("Index");
            }
            else
            {                
                //skall ändras till Find(vilken-änvändare-som-är-inloggad)
                var user = db.Users.Find(2);
                
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




                    return View("Index", CreateFileList());
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