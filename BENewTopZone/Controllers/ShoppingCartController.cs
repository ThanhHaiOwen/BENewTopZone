using BENewTopZone.Models;
using BENewTopZone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BENewTopZone.Controllers
{
    public class ShoppingCartController : Controller
    {
        DBNewTopZoneEntities db = new DBNewTopZoneEntities();
        // GET: ShoppingCart
       
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        //public PartialViewResult BagCart()
        //{
        //    int toltal_quantity_item = 0;
        //    Cart cart = Session["Cart"] as Cart;
        //    if (cart != null)

        //        toltal_quantity_item = cart.Total_quantity();
        //    ViewBag.QuantityCart = toltal_quantity_item;
        //    return PartialView("BagCart");

        //}

        public ActionResult CheckOuttt(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;

                if (cart == null || cart.Total_quantity() == 0)
                {
                    return Content("Giỏ hàng trống. Vui lòng thêm sản phẩm vào giỏ hàng trước khi thanh toán.");
                }

                foreach (var item in cart.Items)
                {
                    var product = db.ProDetails.FirstOrDefault(p => p.ProDeID == item._proDetail.ProDeID);

                    if (product == null || product.RemainQuantity < item._quantity)
                    {
                        return Content("Sản phẩm trong giỏ hàng không còn đủ số lượng để mua.");
                    }
                }

                Order _order = new Order();
                _order.OrderDate = DateTime.Now;
                _order.AddressDeliverry = form["AddressDelivery"];
                _order.CusPhone = int.Parse(form["CodeCustomer"]).ToString();

                db.Orders.Add(_order);

                foreach (var item in cart.Items)
                {
                    OrderDetail _order_detail = new OrderDetail();
                    _order_detail.OrderID = _order.OrderID;
                    _order_detail.ProSupID = item._proDetail.ProDeID;
                    _order_detail.UnitPrice = (double)item._proDetail.Price;
                    _order_detail.Quantity = item._quantity;

                    db.OrderDetails.Add(_order_detail);

                    var product = db.ProDetails.FirstOrDefault(p => p.ProDeID == item._proDetail.ProDeID);
                    product.RemainQuantity -= item._quantity;
                }

                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch (Exception ex)
            {
                return Content("Lỗi khi thanh toán. Vui lòng kiểm tra thông tin của bạn. Lỗi: " + ex.Message);
            }
        }
        // tạo mới giỏ hàng
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null /*|| Session["Cart"] == null*/)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // Trong controller ShoppingCartController
        public ActionResult AddToCart(int productId)
        {
            var product = db.Products.FirstOrDefault(p => p.ProID == productId);
            var proDetail = db.ProDetails.FirstOrDefault(pd => pd.ProID == productId); 

            if (product != null && proDetail != null)
            {
                GetCart().Add_Product_Cart(product, proDetail, 21);
            }

            return RedirectToAction("ShowCart", "ShoppingCart");
        }



        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;

                if (cart != null)
                {
                    int id_pro = int.Parse(form["idPro"]);
                    int _quantity = int.Parse(form["carQuantity"]);

                    // Gọi phương thức cập nhật số lượng trong giỏ hàng
                    cart.Update_quantity(id_pro, _quantity);
                }
                else
                {
                    // Xử lý khi giỏ hàng không tồn tại
                    return Content("Giỏ hàng không tồn tại.");
                }

                return RedirectToAction("ShowCart", "ShoppingCart");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi cập nhật số lượng
                return Content("Lỗi khi cập nhật số lượng. Lỗi: " + ex.Message);
            }
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public PartialViewResult BagCart()
        {
            int toltal_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)

                toltal_quantity_item = cart.Total_quantity();
            ViewBag.QuantityCart = toltal_quantity_item;
            return PartialView("BagCart");

        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;

                if (cart == null || cart.Total_quantity() == 0)
                {
                    return Content("Giỏ hàng trống. Vui lòng thêm sản phẩm vào giỏ hàng trước khi thanh toán.");
                }

                Order _order = new Order();
                _order.OrderDate = DateTime.Now;
                _order.AddressDeliverry = form["AddressDelivery"];
                _order.CusPhone = form["CodeCustomer"]; // Không cần chuyển đổi ngay lập tức về int

                db.Orders.Add(_order);
                db.SaveChanges(); // Lưu đơn hàng trước để có được OrderID

                foreach (var item in cart.Items)
                {
                    OrderDetail _order_detail = new OrderDetail();
                    _order_detail.OrderID = _order.OrderID; // Sử dụng OrderID vừa tạo
                    _order_detail.ProSupID = item._proDetail.ProDeID;
                    _order_detail.UnitPrice = (double)item._proDetail.Price;
                    _order_detail.Quantity = item._quantity;

                    db.OrderDetails.Add(_order_detail);

                    var product = db.ProDetails.FirstOrDefault(p => p.ProDeID == item._proDetail.ProDeID);
                    if (product != null)
                    {
                        product.RemainQuantity -= item._quantity;
                    }
                }

                db.SaveChanges(); // Lưu thông tin chi tiết đơn hàng và cập nhật số lượng tồn

                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch (Exception ex)
            {
                return Content("Lỗi khi thanh toán. Vui lòng kiểm tra thông tin của bạn. Lỗi: " + ex.Message);
            }
        }

        public ActionResult CheckOut_Success()
        {
            return View();
        }
    }
}
