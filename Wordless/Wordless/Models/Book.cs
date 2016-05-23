using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proejket.Models
{
    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public User Author { get; set; }

        public int Downloads { get; set; }

        public Genres Genre { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

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