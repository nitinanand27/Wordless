namespace Wordless.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Wordless.Models.WordlessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Wordless.Models.WordlessContext context)
        {
            #region Users    
            var user1 = new User
            {
                Username = "Patrik",
                Password = "123",
                Name = "Patrik Nilsson",
                Email = "patrik@gmail.com",
                Author = true,
                Admin = true,
                Funds = 200
            };
            var temp = from x in context.User
                       where x.Username == user1.Username
                       select x;
            if (temp.Count() == 0)
            {
                context.User.Add(user1);
            }
            var user2 = new User
            {
                Username = "Nitin",
                Password = "123",
                Name = "Nitin Anand",
                Email = "nitin@anand.com",
                Author = true,
                Admin = true,
                Funds = 200
            };
            temp = from x in context.User
                   where x.Username == "Nitin"
                   select x;
            if (temp.Count() == 0)
            {
                context.User.Add(user2);
            }

            var user3 = new User
            {
                Username = "Alex",
                Password = "123",
                Name = "Alexander Litos",
                Email = "ales@litos.com",
                Author = true,
                Admin = true,
                Funds = 200
            };
            temp = from x in context.User
                   where x.Username == "Alex"
                   select x;
            if (temp.Count() == 0)
            {
                context.User.Add(user3);
            }
            var user4 = new User
            {
                Username = "Christian",
                Password = "123",
                Name = "Christian Jarenfors",
                Email = "Christian@Jarenfors.com",
                Author = true,
                Admin = true,
                Funds = 200
            };
            temp = from x in context.User
                   where x.Username == "Christian"
                   select x;
            if (temp.Count() == 0)
            {
                context.User.Add(user4);
            }
            #endregion
            #region Books
            var book1 = new Book
            {
                Title = "Pippi Långström",
                TimesPurchased = 15,
                Price = 25,
                Author = user2,
                Genre = Genres.Mystery,
                BookText = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            };
            var book2 = new Book
            {
                Title = "All vi barn i bullerbyn",
                TimesPurchased = 25,
                Price = 20,
                Author = user2,
                Genre = Genres.Fantasy,
                BookText = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            };
            var book3 = new Book
            {
                Title = "Mästerdetektiven Blomkvist",
                TimesPurchased = 55,
                Price = 30,
                Author = user3,
                Genre = Genres.ScienceFiction,
                BookText = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            };
            var book4 = new Book
            {
                Title = "Lillebror och Karlsson på takett",
                TimesPurchased = 5,
                Price = 3,
                Author = user4,
                Genre = Genres.Philosophy,
                BookText = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            };
            context.Book.AddOrUpdate(book1);
            context.Book.AddOrUpdate(book2);
            context.Book.AddOrUpdate(book3);
            context.Book.AddOrUpdate(book4);
            #endregion
            #region Purchase
            
            var purchase1 = new PurchasedBook
            {
                Buyer = user2,
                Book = book2,
                Rating = 5,
                Recension = "Astrid Lindgren började skriva på 1940-talet. När Lindgrens dotter var sjuk i lunginflammation bad hon om att få höra en berättelse och när modern då frågade:",
                DateOfPurchase = DateTime.Now
            };
            var purchase2 = new PurchasedBook
            {
                Buyer = user4,
                Book = book4,
                Rating = 4,
                Recension = "Astrid Lindgren började skriva på 1940-talet. När Lindgrens dotter var sjuk i lunginflammation bad hon om att få höra en berättelse och när modern då frågade",
                DateOfPurchase = DateTime.Now
            };
            var purchase3 = new PurchasedBook
            {
                Buyer = user1,
                Book = book1,
                Rating = 3,
                Recension = "Astrid Lindgren började skriva på 1940-talet. När Lindgrens dotter var sjuk i lunginflammation bad hon om att få höra en berättelse och när modern då frågade:",
                DateOfPurchase = DateTime.Now
            };
            var purchase4 = new PurchasedBook
            {
                Buyer = user4,
                Book = book4,
                Rating = 2,
                Recension = "Astrid Lindgren började skriva på 1940-talet. När Lindgrens dotter var sjuk i lunginflammation bad hon om att få höra en berättelse och när modern då frågade:",
                DateOfPurchase = DateTime.Now
            };
            context.PurchaseBook.AddOrUpdate(purchase1);
            context.PurchaseBook.AddOrUpdate(purchase2);
            context.PurchaseBook.AddOrUpdate(purchase3);
            context.PurchaseBook.AddOrUpdate(purchase4);
            #endregion
            #region Comments

            
            var comment1 = new Comment
            {
                CommentText = "'Vad ska jag berätta?' svarade hon 'Berätta om Pippi Långstrump!' Hon hittade på det namnet just i det ögonblicket. 	",
                User = user2,
                Book = book2,
                Date = DateTime.Now
            };
            var comment2 = new Comment
            {
                CommentText = "'Vad ska jag berätta?' svarade hon 'Berätta om Pippi Långstrump!' Hon hittade på det namnet just i det ögonblicket. 	",
                User = user2,
                Book = book2,
                Date = DateTime.Now
            };
            var comment3 = new Comment
            {
                CommentText = "'Vad ska jag berätta?' svarade hon 'Berätta om Pippi Långstrump!' Hon hittade på det namnet just i det ögonblicket. 	",
                User = user3,
                Book = book4,
                Date = DateTime.Now
            };
            var comment4 = new Comment
            {
                CommentText = "'Vad ska jag berätta?' svarade hon 'Berätta om Pippi Långstrump!' Hon hittade på det namnet just i det ögonblicket. 	",
                User = user4,
                Book = book3,
                Date = DateTime.Now
            };
            context.Comment.AddOrUpdate(comment1);
            context.Comment.AddOrUpdate(comment2);
            context.Comment.AddOrUpdate(comment3);
            context.Comment.AddOrUpdate(comment4);
            #endregion
            context.SaveChanges();

        }
    }
}
