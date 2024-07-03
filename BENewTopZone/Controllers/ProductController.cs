using BENewTopZone.Models;
using BENewTopZone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BENewTopZone.Controllers
{
    public class ProductController : Controller
    {
        DBNewTopZoneEntities db = new DBNewTopZoneEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int? id)
        {
            if (id.HasValue)
            {
                var product = db.Products.FirstOrDefault(p => p.ProID == id);
                if (product != null)
                {
                    var proDetail = product.ProDetails.FirstOrDefault();
                    if (proDetail != null)
                    {

                        ProHome detailProductVM = new ProHome
                        {
                            ProDeID = proDetail.ProDeID,
                            Price = proDetail.Price,
                            RemainQuantity = proDetail.RemainQuantity,
                            SoldQuantity = proDetail.SoldQuantity,
                            ViewQuantity = proDetail.ViewQuantity,
                            ProID = product.ProID,
                            ProName = product.ProName,
                            ProImage = product.ProImage,
                            NameDecription = proDetail.Product.NameDecription,
                            CatID = proDetail.Product.CatID,
                            NameCate = proDetail.Product.Category.NameCate,
                            CapacityID = proDetail.Capacity.CapacityID,
                            CapacityName = proDetail.Capacity.CapacityName,
                            ColorID = proDetail.ColorID,
                            ColorName = proDetail.Color.ColorName,
                            RGB = proDetail.Color.RGB
                        };

                        return View(detailProductVM);

                    }


                }
              
            }  return View();

        }
    }
}

 //public ActionResult Detail(int id)
        //{
        //    var objPro = db.Products.Where(n => n.ProID == id).FirstOrDefault();
        //    return View(objPro);
        //}