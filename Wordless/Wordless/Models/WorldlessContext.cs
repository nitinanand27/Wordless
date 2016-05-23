using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Wordless.Models
{
    public class WorldlessContext : DbContext
    {
        public WorldlessContext() : base ("Wordless")
        {           
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<PurchasedBook> PurchaseBook { get; set; }
        public DbSet<User> User { get; set; }
    }
}