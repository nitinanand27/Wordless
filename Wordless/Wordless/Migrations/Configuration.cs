namespace Wordless.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Wordless.Models.WorldlessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Wordless.Models.WorldlessContext context)
        {
          //  context.User.AddOrUpdate(
          //      p => p.UserId,
          //      new User
          //      {
          //          Username = "Patrik",
          //          Password = "123",
          //          Name = "Patrik Nilsson",
          //          Email = "hej@gmail.com",
          //          Author = true,
          //          Admin = true,
          //          Funds = 100,
          //      }
          //      );
          //  context.Comment.AddOrUpdate(c => c.CommentId, new Comment
          //  {
          //      BookId = 1,
          //      UserId = 1,
          //      CommentText = "Massor med bra kommentarer"
          //  }
          //  );
          //  context.PurchaseBook.AddOrUpdate(p => p.PurchasedBookId, new PurchasedBook
          //  {
          //      BookId = 1,
          //      BuyerId = 1,
          //  }
          //  );
          //  context.Book.AddOrUpdate(b => b.BookId, new Book
          //  {
          //      Title = "Hej",
          //      AuthorId = 1,

          //  }
          //);

          //  context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
