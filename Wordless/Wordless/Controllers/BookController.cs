﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wordless.Models;
using System.Data.Entity;

namespace Wordless.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index(string genre)
        {
            WordlessContext db = new WordlessContext();
            if ((bool)Session["loginStatus"])
            {                
                var purchasedbooks = db.PurchasedBooks.ToList();
                var userId = (int)Session["currentUserId"];
                var userPurchases = (from p in purchasedbooks
                                     where p.BuyerId == userId
                                     select p).ToList();
                Session["bookPurchaseList"] = userPurchases;
            }           
            //if a genre was provided, return a list of books of the genre
            List<Book> bookList = db.Books.Include(b => b.Author).Include(c => c.Comments).ToList();
            if (genre != null)
            {
                var categorySort = from c in bookList
                                   where c.Genre.ToString() == genre
                                   select c;
                return View(categorySort);
            }
            else
            {
                return View(bookList);
            }            
            
            
        }       
        public ActionResult BuyBook (int bookId, bool? confirmed)
        {
            //check if logged in
            if (!(bool)Session["loginStatus"])
            {
                return RedirectToAction("Index");
            }
            //if logged in and a bookId is provided
            if (bookId != 0 && (bool)Session["loginStatus"])
            {
                WordlessContext db = new WordlessContext();
                //get userId
                var userId = (int)Session["currentUserId"];
                //get book
                Book book = (from b in db.Books
                            where b.BookId == bookId
                            select b).First();
                //get user
                User user = (from u in db.Users
                            where u.UserId == userId
                            select u).FirstOrDefault();
                //if user confirmed
                if (confirmed != null)
                {
                    if ((bool)confirmed)
                    {
                        //create new book-purchase
                        var bookToBuy = new PurchasedBook
                        {
                            DateOfPurchase = DateTime.Now,
                            BookId = book.BookId,
                            BuyerId = userId,
                            Rating = 0
                        };
                        //add a purchase
                        db.PurchasedBooks.Add(bookToBuy);
                        //give author cash
                        book.Author.Funds += book.Price;
                        //remove cash from user
                        user.Funds -= book.Price;
                        //incremenet TimesPurchase by 1
                        book.TimesPurchased++;
                        //save changes
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }               
                }
                List<Book> bookToBuyView = new List<Book>() { book };
                return View(bookToBuyView);
            }
            else
            {
                return RedirectToAction("Index");
            }          

            
        }
        public ActionResult BookByGenre(int id)
        {
            WordlessContext db = new WordlessContext();
            var booklist = db.Books.Include(b => b.Author).Include(c => c.Comments).Where(b => b.Genre== (Genres)id).ToList();
            return View("Index", booklist);
        }

    }
}