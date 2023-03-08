using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContactAdminController : Controller
    {
        ShopEntities1 db = new ShopEntities1();
        // GET: Admin/ContactAdmin
        public ActionResult Index()
        {
            var lst = db.Response.ToList();
            return View(lst);
        }
        public ActionResult Details(int Id)
        {
            var rp = db.Response.Where(n => n.Id == Id).FirstOrDefault();
            return View(rp);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var rp = db.Response.Where(n => n.Id == Id).FirstOrDefault();
            return View(rp);
        }
        [HttpPost]
        public ActionResult Delete(Response _rp)
        {
            var rp = db.Response.Where(n => n.Id == _rp.Id).FirstOrDefault();
            db.Response.Remove(rp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}