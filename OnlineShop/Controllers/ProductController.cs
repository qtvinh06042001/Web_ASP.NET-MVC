using OnlineShop.Context;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        ShopEntities1 objModel = new ShopEntities1();
        // GET: Product
        public ActionResult Index(int Id)
        {
            var objProduct = objModel.Product.Where(n => n.Id == Id).FirstOrDefault();
            var lstCategory = objModel.Category.ToList();
            var lstProduct = objModel.Product.Where(n => n.CategoryId == objProduct.CategoryId).ToList();
            ProductDetailModel objproductDetailModel = new ProductDetailModel();
            objproductDetailModel.objProduct = objProduct;
            objproductDetailModel.ListCategory = lstCategory;
            objproductDetailModel.ListProduct = lstProduct;
            return View(objproductDetailModel);
        }
    }
}