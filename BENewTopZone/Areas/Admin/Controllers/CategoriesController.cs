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
    public class CategoriesController : Controller
    {
        private DBNewTopZoneEntities db = new DBNewTopZoneEntities();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatID,NameCate")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatID,NameCate")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
       
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);

            // Kiểm tra xem có sản phẩm nào sử dụng Category này không
            bool isUsedInProduct = db.Products.Any(p => p.CatID == id);

            if (isUsedInProduct)
            {
                return Content("Không thể xóa Category vì có sản phẩm đang sử dụng nó.");
            }

            try
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Đã xảy ra lỗi khi xóa Category: " + ex.Message);
            }
        }


        //public ActionResult Delete(int id, Category cate)
        //{
        //    try
        //    {
        //        cate = db.Categories.Where(s => s.CatID== id).FirstOrDefault();
        //        db.Categories.Remove(cate);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return Content("This data is using in other table, Error Delete!");
        //    }
        //}
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
