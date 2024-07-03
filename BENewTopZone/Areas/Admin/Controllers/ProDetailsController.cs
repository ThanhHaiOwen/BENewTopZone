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
    public class ProDetailsController : Controller
    {
        private DBNewTopZoneEntities db = new DBNewTopZoneEntities();

        // GET: Admin/ProDetails
        public ActionResult Index()
        {
            var proDetails = db.ProDetails.Include(p => p.Capacity).Include(p => p.Color).Include(p => p.Discount).Include(p => p.Product);
            return View(proDetails.ToList());
        }

        // GET: Admin/ProDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProDetail proDetail = db.ProDetails.Find(id);
            if (proDetail == null)
            {
                return HttpNotFound();
            }
            return View(proDetail);
        }

        // GET: Admin/ProDetails/Create
        public ActionResult Create()
        {
            ViewBag.CapacityID = new SelectList(db.Capacities, "CapacityID", "CapacityName");
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName");
            ViewBag.DiscountID = new SelectList(db.Discounts, "DiscountID", "DiscountCode");
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ProName");
            return View();
        }

        // POST: Admin/ProDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProDeID,ProID,ColorID,Price,DiscountID,CapacityID,RemainQuantity,SoldQuantity,ViewQuantity")] ProDetail proDetail)
        {
            if (ModelState.IsValid)
            {
                db.ProDetails.Add(proDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CapacityID = new SelectList(db.Capacities, "CapacityID", "CapacityName", proDetail.CapacityID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", proDetail.ColorID);
            ViewBag.DiscountID = new SelectList(db.Discounts, "DiscountID", "DiscountCode", proDetail.DiscountID);
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ProName", proDetail.ProID);
            return View(proDetail);
        }

        // GET: Admin/ProDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProDetail proDetail = db.ProDetails.Find(id);
            if (proDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CapacityID = new SelectList(db.Capacities, "CapacityID", "CapacityName", proDetail.CapacityID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", proDetail.ColorID);
            ViewBag.DiscountID = new SelectList(db.Discounts, "DiscountID", "DiscountCode", proDetail.DiscountID);
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ProName", proDetail.ProID);
            return View(proDetail);
        }

        // POST: Admin/ProDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProDeID,ProID,ColorID,Price,DiscountID,CapacityID,RemainQuantity,SoldQuantity,ViewQuantity")] ProDetail proDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CapacityID = new SelectList(db.Capacities, "CapacityID", "CapacityName", proDetail.CapacityID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", proDetail.ColorID);
            ViewBag.DiscountID = new SelectList(db.Discounts, "DiscountID", "DiscountCode", proDetail.DiscountID);
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ProName", proDetail.ProID);
            return View(proDetail);
        }

        // GET: Admin/ProDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProDetail proDetail = db.ProDetails.Find(id);
            if (proDetail == null)
            {
                return HttpNotFound();
            }
            return View(proDetail);
        }

        // POST: Admin/ProDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProDetail proDetail = db.ProDetails.Find(id);
            db.ProDetails.Remove(proDetail);
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
