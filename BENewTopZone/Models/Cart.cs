using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BENewTopZone.Models
{
    public class CartItem
    {
        public Product _product { get; set; }
        public int _quantity { get; set; }
        public ProDetail _proDetail { get; set; }
    }

    public class Cart
    {
        List<CartItem> items = new List<CartItem>();

        public IEnumerable<CartItem> Items { get { return items; } }

        

        public void Add_Product_Cart(Product _pro, ProDetail _proDetail, int _quan = 1)
        {
            var item = Items.FirstOrDefault(s => s._product.ProID == _pro.ProID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _product = _pro,
                    _proDetail = _proDetail,
                    _quantity = _quan
                });
            }
            else
            {
                item._quantity += _quan;
            }
        }

        public decimal Total_Money()
        {
            var total = items.Sum(s => s._quantity * s._proDetail.Price);
            return (decimal)total;
        }
        // tính tổng số lượng giỏ hàng
        public int Total_quantity()
        {
            return items.Sum(s => s._quantity);
        }

        public void Update_quantity(int id, int _new_quan)
        {
            var item = items.Find(s => s._product.ProID == id);

            if (item != null)
            {
                //if (item._proDetail.RemainQuantity >= _new_quan)
                if (items.Find(s => s._proDetail.RemainQuantity > _new_quan) != null)
                {
                    item._quantity = _new_quan;
                }
                else
                {
                    item._quantity = 1;
                }
            }
        }

        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._product.ProID == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }

}