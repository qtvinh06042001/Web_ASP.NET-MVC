using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderAdminController : Controller
    {
        // GET: Admin/OrderAdmin
        ShopEntities1 db = new ShopEntities1();
        public ActionResult Index()
        {
            
            var cus = db.Order.ToList();
            Order_OrderDetail obj = new Order_OrderDetail();
            obj.lstOrder = cus;
            return View(obj);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var cus = db.Order.Where(n => n.Id == Id).FirstOrDefault();
            return View(cus);
        }
        [HttpPost]
        public ActionResult Delete(Order order)
        {
            var cus = db.Order.Where(n => n.Id == order.Id).FirstOrDefault();
            db.Order.Remove(cus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(Order order)
        {
            var objOrder = db.Order.Where(n => n.Id == order.Id).FirstOrDefault();
            var objOrderDetail = db.OrderDetail.Where(n => n.OrderId == order.Id).FirstOrDefault();
            var lstOrder = db.Order.ToList();
            var lstOrderDetail = db.OrderDetail.Where(n=>n.OrderId==order.Id).ToList();
            var lstproduct = db.Product.Where(n => n.Id == objOrderDetail.ProductId).ToList();
            Order_OrderDetail obj = new Order_OrderDetail();
            obj.objOrderDetail = objOrderDetail;
            obj.objOrder = objOrder;
            obj.lstOrder = lstOrder;
            obj.lstProduct = lstproduct;
            obj.lstOrderDetail = lstOrderDetail;
            return View(obj);
        }
    }
}