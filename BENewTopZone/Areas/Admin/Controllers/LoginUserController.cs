using BENewTopZone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace BENewTopZone.Areas.Admin.Controllers
{
    public class LoginUserController : Controller
    {
        private DBNewTopZoneEntities db = new DBNewTopZoneEntities();
        // GET: Admin/LoginUser
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAcount(AminUser _user)
        {
            // bài thực hành số 3 var check=database.AdminUsers.Where(s=> s.ID == _user.ID&& s.PasswordUser==_user.PasswordUser).FirstOrDefault();
            var check = db.AminUsers.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                // bài thực hành số 3Session["ID"]=_user.ID;
                //Session["PasswordUser"] = _user.PasswordUser;
                Session["nameUers"] = _user.NameUser;
                return RedirectToAction("Index", "HomeAdmin");
            }

        }
        public ActionResult RegisterUser()
        { return View(); }
        [HttpPost]
        public ActionResult RegisterUser(AminUser _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = db.AminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();
                if (check_ID == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.AminUsers.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index", "LoginUser");
                }
                else
                {
                    ViewBag.ErrorRegister = "This ID is exixst";
                    return View();
                }
            }
            return View();
        }
        // hàm logoutuser
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginUser");
        }


    }
}
