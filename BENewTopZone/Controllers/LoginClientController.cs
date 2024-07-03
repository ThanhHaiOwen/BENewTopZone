using BENewTopZone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BENewTopZone.Controllers
{
    public class LoginClientController : Controller
    {
        DBNewTopZoneEntities db=new DBNewTopZoneEntities();
        // GET: LoginClient
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAcount(Customer customer)
        {
            var existingCustomer = db.Customers.FirstOrDefault(c => c.CusEmail == customer.CusEmail && c.CusPassword == customer.CusPassword);
            if (existingCustomer != null)
            {
                Session["CusPhone"] = existingCustomer.CusPhone;
                Session["CusLastName"] = existingCustomer.CusLastName;
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                ViewBag.ErrorTT = "Nhập sai thông tin hãy thử lại ";
                return View("Index");
            }


        }

        public ActionResult RegisterClient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterClient(Customer customer)
        {
            if (ModelState.IsValid)
            {
               var existingCustomer = db.Customers.FirstOrDefault(c => c.CusPhone == customer.CusPhone);
                if (existingCustomer != null)
                {
                    ViewBag.Errorrs = "số điện thoại đã được đăng kí trước đó";
                    return View("RegisterClient");
                }
                else
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index", "LoginClient");
                }
            }
            return View();
        }

    }
}