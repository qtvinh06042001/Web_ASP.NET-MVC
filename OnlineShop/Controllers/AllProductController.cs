using OnlineShop.Context;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class AllProductController : Controller
    {
        // GET: All
        ShopEntities1 objModel = new ShopEntities1();
        public ActionResult Index()
        {
            var lstCategory = objModel.Category.ToList();
            var lstProduct = objModel.Product.ToList();
            Product_Category objProduct_Category = new Product_Category();
            objProduct_Category.ListCategory = lstCategory;
            objProduct_Category.ListProduct = lstProduct;
            return View(objProduct_Category);
        }
    }
}