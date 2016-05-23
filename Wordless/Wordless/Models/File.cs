using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wordless.Models
{
    public class File
    {
        public int FileId { get; set; }

        public virtual User User { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }



        //public string FileType { get; set; }  
    }
}