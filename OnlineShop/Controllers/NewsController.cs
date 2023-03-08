using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class NewsController : Controller
    {
        ShopEntities1 objModel = new ShopEntities1();
        // GET: News
        public ActionResult Index()
        {
            var lstNews = objModel.News.ToList();
            return View(lstNews);
        }
    }
}