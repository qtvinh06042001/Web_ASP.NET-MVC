using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        ShopEntities1 objModel = new ShopEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductsCategory(int Id)
        {
            var ListProduct = objModel.Product.Where(n => n.CategoryId == Id).ToList();

            return View(ListProduct);
        }
    }
}