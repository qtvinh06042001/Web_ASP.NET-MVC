using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        ShopEntities1 db = new ShopEntities1();
        // GET: Contact
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Name, string Email, string CompanyName, string Subject)
        {
            Response response = new Response();
            response.Name = Name;
            response.Email = Email;
            response.CompanyName = CompanyName;
            response.Subject = Subject;
            db.Response.Add(response);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}