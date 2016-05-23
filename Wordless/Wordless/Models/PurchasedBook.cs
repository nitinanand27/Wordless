using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wordless.Models
{
    public class PurchasedBook
    {
        //id för varje köp
        public int PurchasedBookId { get; set; }

        //en bok tillhör varje köp
        //public int BookId { get; set; }
        //[ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        //en köpare tillhör varje köp4
        //public int BuyerId { get; set; }
        //[ForeignKey("BuyerId")]
        public virtual User Buyer { get; set; }

        //varje bokköp kan ha ett betyg som användaren har gett
        public int Rating { get; set; }

        //om användaren har skrivit en recension på köpet
        public string Recension { get; set; }

        //datum för köp
        public DateTime DateOfPurchase { get; set; }
    }
}