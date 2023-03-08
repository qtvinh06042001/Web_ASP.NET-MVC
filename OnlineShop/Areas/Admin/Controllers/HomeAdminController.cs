using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/Home
        ShopEntities1 dbModel = new ShopEntities1();
        public ActionResult Index()
        {
            if(Session["idUser"]!=null){
                var lstProduct = dbModel.Product.ToList();
                return View(lstProduct);
            }
            else
            {
                return View("Login");
            }
            return View();
        }
    }
}