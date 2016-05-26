using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wordless.Models
{
    public class File
    {
        public int FileId { get; set; }       
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        //Naigation Properties

        public int UserId { get; set; }             //varje fil tillhör minst en user
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        //public string FileType { get; set; }  
    }
}