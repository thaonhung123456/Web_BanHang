using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_BanHang.Models
{
    public class CartItem
    {
        public Product _product { get; set; }
        public int _quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        //Lấy sản phẩm bỏ vào giỏ hàng
        public void Add_Product_Cart(Product _pro, int _quan = 1)
        {
            var item = Items.FirstOrDefault(s => s._product.ProductID == _pro.ProductID);
            if (item == null)
                items.Add(new CartItem
                {
                    _product = _pro,
                    _quantity = _quan
                });
            else
                item._quantity += _quan;
        }

        //Tính tổng số lượng trong giỏi hàng
        public int Total_quantity()
        {
            return items.Sum(s => s._quantity);
        }

        //Tính thành tiền cho mỗi dòng sản phẩm trong giỏ hàng
        public decimal Total_money()
        {
            var total = items.Sum(s => s._quantity * s._product.Price);
            return (decimal)total;
        }

        //cập nhật lại số lượng sản phẩm ở mỗi dòng sản phẩm khi khách hàng muốn đặt mua thêm
        public void Update_quantity(int id, int _new_quan)
        {
            var item = items.Find(s => s._product.ProductID == id);
            if (item != null)
            {
              //  if (items.Find(s => s._product.Quantity > _new_quan) != null)
                    item._quantity = _new_quan;
               // else item._quantity = -1;
            }    
                //item._quantity = _new_quan;
        }

        //Xóa sản phẩm trong giỏi hàng
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._product.ProductID == id);
        }

        //Xóa giỏ hàng sau khi Khách hàng thực hiện thanh toán
        public void ClearCart()
        {
            items.Clear();
        }
    }  
}