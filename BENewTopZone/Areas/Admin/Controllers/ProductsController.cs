using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BENewTopZone.Models;

namespace BENewTopZone.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private DBNewTopZoneEntities db = new DBNewTopZoneEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProID,ProName,CatID,ProImage,NameDecription,CreatedDate,UpLoadImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.UploadImage != null)
                {
                    string path = "~/Content/images/";
                    string filename = Path.GetFileName(product.UploadImage.FileName);
                    product.ProImage = path + filename;
                    product.UploadImage.SaveAs(Path.Combine(Server.MapPath(path), filename));
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", product.CatID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", product.CatID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProID,ProName,CatID,ProImage,NameDecription,CreatedDate,UploadImage")] Product editedProduct)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = db.Products.Find(editedProduct.ProID);

                if (existingProduct != null)
                {
                    // Update properties of the existingProduct with editedProduct's values
                    existingProduct.ProName = editedProduct.ProName;
                    existingProduct.CatID = editedProduct.CatID;
                    existingProduct.NameDecription = editedProduct.NameDecription;
                    existingProduct.CreatedDate = editedProduct.CreatedDate;

                    if (editedProduct.UploadImage != null && editedProduct.UploadImage.ContentLength > 0)
                    {
                        string path = "~/Content/images/";
                        string filename = Path.GetFileName(editedProduct.UploadImage.FileName);
                        existingProduct.ProImage = path + filename;
                        editedProduct.UploadImage.SaveAs(Path.Combine(Server.MapPath(path), filename));
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle case where the product with specified ID doesn't exist
                    return HttpNotFound();
                }
            }

            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", editedProduct.CatID);
            return View(editedProduct);
        }


        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
        //public ActionResult SelectCate()
        //{
        //    Category se_cate = new Category();
        //    se_cate = db.Categories.ToList<Category>();
        //    return PartialView(se_cate);
        //}
    }
}
