using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wordless.Models
{
    [Serializable]
    public class Book
    {
        public int BookId { get; set; }        
        public string Title { get; set; }             //titel på boken
        public string BookText { get; set; }        // kort beskrivning om boken
        public int TimesPurchased { get; set; }     //hur många gånger boken har köpts
        public decimal Price { get; set; }         //vad boken kostar

        /// Navigation Properties

        public Int64 AuthorId { get; set; }           //vilken användare som har skrivit          
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        public virtual Genres Genre { get; set; }

        public virtual IList<Comment> Comments { get; set; }    //en lista på kommentarer optional

        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public File File { get; set; }                  //varje bok har en pdf fil

        public List<Book> GetAll()
        {
            WordlessContext db = new WordlessContext();

            return db.Books.ToList();
        }        

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
