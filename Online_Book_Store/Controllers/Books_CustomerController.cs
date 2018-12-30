using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Book_Store;
using Online_Book_Store.Models;
using System.IO;
using PagedList;

namespace Online_Book_Store.Controllers
{
    public class Books_CustomerController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        // GET: Books
        public ActionResult Index(string searchStr, string author1, string sort)
        {
            ViewBag.bname = string.IsNullOrEmpty(sort) ? "bname_dec" : "";
            ViewBag.bname = sort == "bname" ? "bname_dec" : "bname";
            ViewBag.author = sort == "author" ? "author_dec" : "author";
            ViewBag.price = sort == "price" ? "price_dec" : "price";
            ViewBag.type = sort == "type" ? "type_dec" : "type";
            var books = from b in db.Book select b;

            switch (sort)
            {
                case "bname_dec":
                    books = books.OrderByDescending(x => x.Bookname);
                    break;
                case "author_dec":
                    books = books.OrderByDescending(x => x.Authorname);
                    break;
                case "price_dec":
                    books = books.OrderByDescending(x => x.price);
                    break;
                case "bname":
                    books = books.OrderBy(x => x.Bookname);
                    break;
                case "type":
                    books = books.OrderBy(x => x.BookType);
                    break;

                case "type_dec":
                    books = books.OrderByDescending(x => x.BookType);
                    break;
                case "author":
                    books = books.OrderBy(x => x.Authorname);
                    break;
                case "price":
                    books = books.OrderBy(x => x.price);
                    break;
            }

            if (!string.IsNullOrEmpty(searchStr))
            {
                books = books.Where(x => x.Bookname.Contains(searchStr));
               
            }
            if (!string.IsNullOrEmpty(author1))
            {
                books = books.Where(x => x.Authorname.Contains(author1));

            }



            return View(books);
        }


        public FileResult download(int id)
        {

            Book b = db.Book.Where(x => x.BookId == id).FirstOrDefault();
            string path = "~/Uploads/" + b.Bookname + ".pdf";
            return new FilePathResult(Server.MapPath(path), "application/pdf");


        }
        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userName"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Book book = db.Book.Find(id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
            else
            {
                return Redirect("/UserInfoes/Login");
            }

        }

        // GET: Books/Create


        // GET: Books/Edit/5

    }
}
