using BENewTopZone.Areas.Admin.Views.ViewModel;
using BENewTopZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BENewTopZone.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        DBNewTopZoneEntities db = new DBNewTopZoneEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var productList = (from p in db.Products
                               join pd in db.ProDetails on p.ProID equals pd.ProID
                               join c in db.Categories on p.CatID equals c.CatID
                               join col in db.Colors on pd.ColorID equals col.ColorID
                               select new ProListadmin
                               {
                                   ProID = p.ProID,
                                   ProName = p.ProName,
                                   ProImage = p.ProImage,
                                   NameDecription = p.NameDecription,
                                   CatID = p.CatID,
                                   NameCate = c.NameCate,
                                   ProDeID = pd.ProDeID,
                                   Price = pd.Price,
                                   RemainQuantity = pd.RemainQuantity,
                                   SoldQuantity = pd.SoldQuantity,
                                   ViewQuantity = pd.ViewQuantity,
                                   ColorID = pd.ColorID,
                                   ColorName = col.ColorName,
                                   RGB = col.RGB
                               }).ToList();

            return View(productList);
        }
    }
}