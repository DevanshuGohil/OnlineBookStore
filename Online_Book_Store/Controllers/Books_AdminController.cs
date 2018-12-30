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

namespace Online_Book_Store.Controllers
{
    public class Books_AdminController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        // GET: Books



        public ActionResult Index(string searchStr, string author1, string sort)
        {
            try
            {
                if (Session["role"].Equals("admin"))
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
                else
                {
                    return Redirect("/UserInfoes/Error");
                }
            }
            catch
            {
                return Redirect("/UserInfoes/Error");
            }
        }

        public ActionResult Upload()
        {
            if (Session["role"].Equals("admin"))
            {
                return View();
            }
            else
            {
                return Redirect("/UserInfoes/Error");
            }

        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file1)
        {
            if (Session["role"].Equals("admin"))
            {
                if (file1 != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    file1.SaveAs(path + Path.GetFileName(file1.FileName));

                    ViewBag.Message = "File uploaded successfully.";
                }

                return View();
            }
            else
            {
                return Redirect("/UserInfoes/Error");
            }


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

        // GET: Books/Create
        public ActionResult Create()
        {
            try
            {
                if (Session["role"].Equals("admin"))
                {
                    return View();
                }
                else
                {
                    return Redirect("/UserInfoes/Error");
                }
            }
            catch
            {
                return Redirect("/UserInfoes/Error");
            }
        }


        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Bookname,Authorname,BookType,price")] Book book)
        {
            if (Session["role"].Equals("admin"))
            {
                if (ModelState.IsValid)
                {
                    db.Book.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Upload", book);
                }

                return View(book);
            }
            else
            {
                return Redirect("/UserInfoes/Error");
            }

        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (Session["role"].Equals("admin"))
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
                    return Redirect("/UserInfoes/Error");
                }
            }
            catch
            {
                return Redirect("/UserInfoes/Error");
            }
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Bookname,Authorname,BookType,price")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {


                if (Session["role"].Equals("admin"))
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
                    return Redirect("/UserInfoes/Error");
                }
            }
            catch
            {
                return Redirect("/UserInfoes/Error");
            }
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["role"].Equals("admin"))
            {
                Book book = db.Book.Find(id);
                db.Book.Remove(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect("/UserInfoes/Error");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
