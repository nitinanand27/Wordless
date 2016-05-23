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
        // GET: File
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetFile ()
        {
            WordlessContext db = new WordlessContext();
            var pdfFile = db.File.Find(1);
            return File(pdfFile.Content, pdfFile.ContentType);
        }
        [HttpPost]
        public ActionResult UploadFile (HttpPostedFileBase upload)
        {
            WordlessContext db = new WordlessContext();
            var user = db.User.Find(1);
            var newPDF = new File
            {
                FileName = System.IO.Path.GetFileName(upload.FileName),
                ContentType = upload.ContentType,                                
                User = user
                
            };
            using (var reader = new System.IO.BinaryReader(upload.InputStream))
            {
                newPDF.Content = reader.ReadBytes(upload.ContentLength);
            }
            db.File.Add(newPDF);
            db.SaveChanges();
            return View("Index");
        }
    }
}