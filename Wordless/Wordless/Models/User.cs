using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proejket.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Author { get; set; }

        public bool Admin { get; set; }
        //en användares 
        public decimal Funds { get; set; }
        //En användare har en lista av bäcker användaren har köpt
        public virtual ICollection<PurchasedBook> PurchasedBooks { get; set; }
        //en användare har en lista av böcken användaren har skrivit
        public virtual ICollection<Book> WrittenBooks { get; set; }
        //en användare har en collection av kommentarer
        public virtual ICollection<Comment> Comments { get; set; }
    }
}