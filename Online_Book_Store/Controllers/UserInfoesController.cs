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
using System.Web.Security;

namespace Online_Book_Store.Controllers
{
    public class UserInfoesController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        // GET: UserInfoes
        public ActionResult Index()
        {
            try
            {
                if (Session["role"].Equals("admin"))
                {
                    return View(db.UserInfo.ToList());
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

        // GET: UserInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserInfoId,Username,emailId,password,MobileNo,city,role")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.UserInfo.Add(userInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userInfo);
        }

        // GET: UserInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserInfoId,Username,emailId,password,MobileNo,city,role")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInfo userInfo = db.UserInfo.Find(id);
            db.UserInfo.Remove(userInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(UserInfo usr)
        {
            if (check(usr))
            {
                FormsAuthentication.SetAuthCookie(usr.Username, false);
                ViewBag.Msg = "success";

                UserInfo u = db.UserInfo.Where(s => s.emailId == usr.emailId).Single();
                Session["userName"] = u.Username;
                Session["role"] = u.role;
                if (u.role.Equals("admin"))
                {
                    return Redirect("/Books_Admin/Index");
                }
                return Redirect("/Books_Customer/Index");
            }
            else
            {
                ViewBag.Msg = "Invalid UserName or password";

                return View();
            }

        }

        public bool check(UserInfo usr)
        {
            //userdbEntities db = new userdbEntities();
            UserInfo user;
            try
            {
                if ((user = db.UserInfo.Where(x => x.emailId == usr.emailId).Single()) != null)
                {
                    user = db.UserInfo.Where(x => x.emailId == usr.emailId).Single();
                    if (usr.password == user.password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException e)
            {
                return false;
            }

        }
        public ActionResult Logout()
        {
            Session["userName"] = null;
            Session["role"] = null;
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserInfo usr)
        {
            //userdbEntities db = new userdbEntities();
            UserInfo newuser = new UserInfo();
            newuser.Username = usr.Username;
            newuser.password = usr.password;
            newuser.emailId = usr.emailId;
            newuser.MobileNo = usr.MobileNo;
            newuser.city = usr.city;
            newuser.role = "customer";
            try
            {
                db.UserInfo.Add(newuser);
                db.SaveChanges();
            }catch
            {
                ViewBag.RegMsg = "Please Enter All Data Properly..";
                return View();
            }
            
            
            return Redirect("/UserInfoes/Login");
        }
        public ActionResult Error()
        {
            return View();
        }
    }

}
