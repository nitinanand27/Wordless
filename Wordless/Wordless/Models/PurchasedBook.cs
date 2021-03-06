﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wordless.Models
{
    [Serializable]
    public class PurchasedBook
    {
        public int PurchasedBookId { get; set; }
        

        [Range(0, 5)]
        public int Rating { get; set; }             //varje bokköp kan ha ett betyg som användaren har gett
        public string Recension { get; set; }       //om användaren har skrivit en recension på köpet max 200 tecken     
        public DateTime DateOfPurchase { get; set; }    //datum för köp

        //Navigation Properties

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }      //en bok tillhör varje köp

        public Int64 BuyerId { get; set; }
        [ForeignKey("BuyerId")]
        public virtual User Buyer { get; set; }     //en köpare tillhör varje köp4

        

        public List<PurchasedBook> GetAll()
        {
            WordlessContext db = new WordlessContext();
            return db.PurchasedBooks.ToList(); 
        }
    }
}