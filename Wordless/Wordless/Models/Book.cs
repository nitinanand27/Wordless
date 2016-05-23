using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wordless.Models
{
    public class Book
    {
        //ID
        public int BookId { get; set; }

        //titel på boken
        public string Title { get; set; }

        public string BookText { get; set; }

        //public int AuthorId { get; set; }
        ////vilken användare som har skrivit
        //[ForeignKey("AuthorId")]
        public User Author { get; set; }

        //hur många gånger boken har köpts
        public int TimesPurchased { get; set; }

        //vilken genrer
        public Genres Genre { get; set; }

        //vad boken kostar
        public decimal Price { get; set; }

        //en lista på kommentarer
        public virtual IList<Comment> Comments { get; set; }



    }
    public enum Genres
    {
        Thriller,
        Romance,
        ScienceFiction,
        Fantasy,
        Travel,
        Mystery,
        Philosophy,
        Religion,
        Crime,
        History
    }
}
