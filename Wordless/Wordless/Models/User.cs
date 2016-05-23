using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wordless.Models
{
    public class User
    {
        //användarens ID
        public int UserId { get; set; }

        //användarens användarnamn
        public string Username { get; set; }

        //användarens lösenord
        public string Password { get; set; }

        //användarens namn
        public string Name { get; set; }

        //användarens e-mail
        public string Email { get; set; }

        //om användaren är en författaren
        public bool Author { get; set; }

        //om användaren är admin
        public bool Admin { get; set; }

        //en användares pengar
        public decimal Funds { get; set; }

        //En användare har en lista av bäcker användaren har köpt
        public virtual IList<PurchasedBook> PurchasedBooks { get; set; }

        //en användare har en lista av böcken användaren har skrivit
        public virtual IList<Book> WrittenBooks { get; set; }

        //en användare har en collection av kommentarer
        public virtual IList<Comment> Comments { get; set; }

        public virtual IList<File> Files { get; set; }

    }
}