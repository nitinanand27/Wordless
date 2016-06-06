using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Wordless.Models
{
    public class WordlessContext : DbContext
    {
        public WordlessContext() : base ("Wordless")
        {           
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PurchasedBook> PurchasedBooks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //validering , primär nyckel, förhållande

            var bookEntity = modelBuilder.Entity<Book>();
            var commentEntity = modelBuilder.Entity<Comment>();
            var userEntity = modelBuilder.Entity<User>();
            var fileEntity = modelBuilder.Entity<File>();
            var purchasedBookEntity = modelBuilder.Entity<PurchasedBook>();
                        
            bookEntity.ToTable("Books");
            commentEntity.ToTable("Comments");
            userEntity.ToTable("Users");
            fileEntity.ToTable("Files");
            purchasedBookEntity.ToTable("Purchased-Books");
            bookEntity.Property(p => p.BookText)
                .HasColumnName("Description");
            bookEntity.Property(p => p.TimesPurchased)
                .HasColumnName("No._of_Downloads");

            bookEntity.HasKey(p => p.BookId);
            bookEntity.Property(p => p.Title)
                .HasColumnName("Book-Title")
                .HasMaxLength(50);
            bookEntity.Property(p => p.Price)
                .HasPrecision(6, 2);           //1234.56
            bookEntity.Property(p => p.BookText)
                .HasMaxLength(200);

            purchasedBookEntity.Property(p => p.DateOfPurchase)
                .HasColumnName("Date of Purchase");
            purchasedBookEntity.HasKey(p => p.PurchasedBookId);
            purchasedBookEntity.Property(p => p.Recension)
                .HasMaxLength(200);
            purchasedBookEntity.Property(p => p.DateOfPurchase)
                .IsRequired();
            purchasedBookEntity.HasRequired(p => p.Buyer)
                .WithMany(p => p.PurchasedBooks)
                .WillCascadeOnDelete(false);

            commentEntity.Property(p => p.CommentText)
                .HasColumnName("Text");
            commentEntity.HasKey(p => p.CommentId);
            commentEntity.Property(p => p.CommentText)
                .HasMaxLength(300);
            commentEntity.Property(p => p.Date)
                .IsRequired();
            commentEntity.HasOptional(p => p.User);
            commentEntity.HasRequired(p => p.User)
                .WithMany(p => p.Comments)
                .WillCascadeOnDelete(false);
                

            userEntity.HasKey(p => p.UserId);
            userEntity.Property(p => p.Name)
                .HasMaxLength(20);
            userEntity.Property(p => p.Funds)
                .HasPrecision(6, 2);        //1234.56

            fileEntity.HasKey(p => p.FileId);
            fileEntity.Property(p => p.FileName)
                .HasMaxLength(200);
            fileEntity.HasRequired(p => p.User)
                .WithMany(p => p.Files)
                .WillCascadeOnDelete(false);
            fileEntity.Property(p => p.UploadedOn).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

    }
}