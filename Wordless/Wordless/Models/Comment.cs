using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wordless.Models
{
    public class Comment
    {
        //ID
        public int CommentId { get; set; }

        //texten på kommentaren
        public string CommentText { get; set; }

        //vilket datum kommentaren blev skriven
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        //vilken användare som skrev kommentaren
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


        //vilken bok kommentaren handlar om
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}