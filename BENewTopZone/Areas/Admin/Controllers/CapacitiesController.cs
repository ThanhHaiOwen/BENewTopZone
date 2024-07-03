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
    public class CapacitiesController : Controller
    {
        private DBNewTopZoneEntities db = new DBNewTopZoneEntities();

        // GET: Admin/Capacities
        public ActionResult Index()
        {
            return View(db.Capacities.ToList());
        }

        // GET: Admin/Capacities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacity capacity = db.Capacities.Find(id);
            if (capacity == null)
            {
                return HttpNotFound();
            }
            return View(capacity);
        }

        // GET: Admin/Capacities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Capacities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CapacityID,CapacityName")] Capacity capacity)
        {
            if (ModelState.IsValid)
            {
                db.Capacities.Add(capacity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(capacity);
        }

        // GET: Admin/Capacities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacity capacity = db.Capacities.Find(id);
            if (capacity == null)
            {
                return HttpNotFound();
            }
            return View(capacity);
        }

        // POST: Admin/Capacities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CapacityID,CapacityName")] Capacity capacity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capacity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(capacity);
        }

        // GET: Admin/Capacities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacity capacity = db.Capacities.Find(id);
            if (capacity == null)
            {
                return HttpNotFound();
            }
            return View(capacity);
        }

        // POST: Admin/Capacities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Capacity capacity = db.Capacities.Find(id);

            // Kiểm tra xem Capacity có được sử dụng trong bảng ProDetail không
            bool isUsedInProDetail = db.ProDetails.Any(pd => pd.CapacityID == id);

            if (isUsedInProDetail)
            {
                return Content("Không thể xóa Capacity vì nó đang được sử dụng trong bảng ProDetail.");
            }

            try
            {
                db.Capacities.Remove(capacity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Đã xảy ra lỗi khi xóa Capacity: " + ex.Message);
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
