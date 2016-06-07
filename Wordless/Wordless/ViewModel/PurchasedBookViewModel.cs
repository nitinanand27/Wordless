using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wordless.Models;

namespace Wordless.ViewModel
{
    public class PurchasedBookViewModel
    {
        public string Keyword { get; set; }
        
        public string Title { get; set; }

        public int TimesPurchased { get; set; }

        public Genres Genre { get; set; }

        public decimal Price { get; set; }

        public List<PurchasedBook> PurchasedBookResult { get; set; }

        public PurchasedBookViewModel()
        {
            Keyword = "";

            Title = "Here the title goes";
            TimesPurchased = 0;
            Price = 0;

            PurchasedBookResult = new List<PurchasedBook>();
        }
    }
}