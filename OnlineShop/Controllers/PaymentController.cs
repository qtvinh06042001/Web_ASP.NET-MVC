using OnlineShop.Context;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class PaymentController : Controller
    {
        ShopEntities1 db = new ShopEntities1();
        // GET: Payment
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Name,string Phone, string Address)
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<Cart>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = Name;
                objOrder.Phone = Phone;
                objOrder.Adress = Address;
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreateOnUtc = DateTime.Now;
                objOrder.Status = 1;
                db.Order.Add(objOrder);
                db.SaveChanges();
                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach(var item in lstCart)
                {
                    OrderDetail objOrderDetail = new OrderDetail();
                    objOrderDetail.Quantity = item.Quantity;
                    objOrderDetail.ProductId = item.Product.Id;
                    objOrderDetail.OrderId = intOrderId;
                    lstOrderDetail.Add(objOrderDetail);
                }
                db.OrderDetail.AddRange(lstOrderDetail);
                db.SaveChanges();
                lstCart.Clear();
                Session["count"] = 0;
                
            }
            return RedirectToAction("Success");
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}