using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        // GET: Admin/ProductAdmin
        ShopEntities1 db = new ShopEntities1();
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var sp = new List<Product>();
            if(SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                sp = db.Product.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                sp = db.Product.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            sp = sp.OrderByDescending(n => n.Id).ToList();
            return View(sp.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product sp)
        {
            try
            {
                if (sp.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sp.ImageUpload.FileName);
                    string extention = Path.GetExtension(sp.ImageUpload.FileName);
                    fileName = fileName + extention;
                    sp.Avatar = fileName;
                    sp.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                db.Product.Add(sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            var sp = db.Product.Where(n => n.Id == Id).FirstOrDefault();
            return View(sp);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var sp = db.Product.Where(n => n.Id == Id).FirstOrDefault();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Delete(Product _sp)
        {
            var sp = db.Product.Where(n => n.Id == _sp.Id).FirstOrDefault();
            db.Product.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var sp = db.Product.Where(n => n.Id == Id).FirstOrDefault();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Edit(Product sp)
        {
            if (sp.ImageUpload != null)
            {
                if (sp.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sp.ImageUpload.FileName);
                    string extention = Path.GetExtension(sp.ImageUpload.FileName);
                    fileName = fileName + extention;
                    sp.Avatar = fileName;
                    sp.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}