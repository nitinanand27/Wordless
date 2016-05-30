using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wordless.Models;

namespace Wordless.ViewModel
{
    public class AuthorPageViewModel
    {
        public string Keyword { get; set; }

        [Required(ErrorMessage = "Please enter a title")]        
        public string Title { get; set; }

        [Required(ErrorMessage = "Please set a timelimit for the loan")]
        [Range(0,240, ErrorMessage = "Time must be between 0 and 240 hours")]
        public int TimesPurchased { get; set; }

        public Genres Genre { get; set; }

        [Required(ErrorMessage = "Please enter a price")]
        [Range(0, 10000, ErrorMessage = "Price must have a price between 0 and 10000 dollares")]
        public decimal Price { get; set; }

        public List<Book> BooksResult { get; set; }

        public AuthorPageViewModel()
        {
            Keyword = "";

            Title = "Here the title goes";
            TimesPurchased = 0;
            Price = 0;            

            BooksResult = new List<Book>();
        }
    }
}