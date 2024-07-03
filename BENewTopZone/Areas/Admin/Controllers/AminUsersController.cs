using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BENewTopZone.Models;

namespace BENewTopZone.Areas.Admin.Controllers
{
    public class AminUsersController : Controller
    {
        private DBNewTopZoneEntities db = new DBNewTopZoneEntities();

        // GET: Admin/AminUsers
        public ActionResult Index()
        {
            return View(db.AminUsers.ToList());
        }

        // GET: Admin/AminUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AminUser aminUser = db.AminUsers.Find(id);
            if (aminUser == null)
            {
                return HttpNotFound();
            }
            return View(aminUser);
        }

        // GET: Admin/AminUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AminUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NameUser,RoleUser,PasswordUser")] AminUser aminUser)
        {
            if (ModelState.IsValid)
            {
                db.AminUsers.Add(aminUser);
                db.SaveChanges();
                return RedirectToAction("RegisterUser","LoginUser");
            }

            return View(aminUser);
        }

        // GET: Admin/AminUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AminUser aminUser = db.AminUsers.Find(id);
            if (aminUser == null)
            {
                return HttpNotFound();
            }
            return View(aminUser);
        }

        // POST: Admin/AminUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NameUser,RoleUser,PasswordUser")] AminUser aminUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aminUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aminUser);
        }

        // GET: Admin/AminUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AminUser aminUser = db.AminUsers.Find(id);
            if (aminUser == null)
            {
                return HttpNotFound();
            }
            return View(aminUser);
        }

        // POST: Admin/AminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AminUser aminUser = db.AminUsers.Find(id);
            db.AminUsers.Remove(aminUser);
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
    }
}
