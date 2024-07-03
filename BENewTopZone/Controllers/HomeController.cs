using BENewTopZone.Areas.Admin.Views.ViewModel;
using BENewTopZone.Models;
using BENewTopZone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BENewTopZone.Controllers
{
    public class HomeController : Controller
    {
        private DBNewTopZoneEntities db=new DBNewTopZoneEntities();
        public ActionResult Index()
        {
            ProHome home = new ProHome();
             home.ListCategory =db.Categories.ToList();

                // home.ListProDetail= db.ProDetails.ToList();
                home.ListProduct =db.Products.ToList();

                return View(home);
        }
       
    }
        //} ProHome home=new ProHome();
        //    home.ListCategory =db.Categories.ToList();

        //    // home.ListProDetail= db.ProDetails.ToList();
        //    home.ListProduct =db.Products.ToList();

        //    return View(home);
        //public class ProductWithDetails : Product
        //{
        //    public double Price { get; set; }
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    
}