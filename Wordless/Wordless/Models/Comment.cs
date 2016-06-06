using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wordless.Models
{
    
    public class Comment
    {
        public int CommentId { get; set; }        
        public string CommentText { get; set; }     //texten på kommentaren   max 200  
        public DateTime Date { get; set; }          //vilket datum kommentaren blev skriven (required)

        //Navigation Properties

        public int UserId { get; set; }             //användare som skrev kommentaren
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        public int BookId { get; set; }             //vilken bok kommentaren handlar om
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}