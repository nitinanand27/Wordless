using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proejket.Models
{
    public class PurchasedBook
    {
        public int PurchasedBookId { get; set; }

        public virtual Book Book { get; set; }

        public virtual User Buyer { get; set; }

        public int Rating { get; set; }

        public DateTime DateOfPurchase { get; set; }
    }
}