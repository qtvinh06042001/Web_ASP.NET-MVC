using OnlineShop.Context;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ShopEntities1 db = new ShopEntities1();
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else {
                return View((List<Cart>)Session["cart"]);
            }
            
        }
        
        public ActionResult AddToCart(int id, int quantity)
        {
            if(Session["cart"] == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { Product = db.Product.Find(id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<Cart> cart = (List<Cart>)Session["cart"];
                //Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
                int index = isExist(id);
                if(index != -1)
                {
                    //Nếu sản phẩm tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                }
                else
                {
                    //Nếu không tồn tại thì thêm vào giỏ hàng
                    cart.Add(new Cart { Product = db.Product.Find(id), Quantity = quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        private int isExist(int id)
        {
            List<Cart> cart = (List<Cart>)Session["cart"];
            for(int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        public ActionResult Remove(int Id)
        {
            List<Cart> li = (List<Cart>)Session["cart"];
            li.RemoveAll(n => n.Product.Id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }
}