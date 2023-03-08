using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class NewAdminController : Controller
    {
        // GET: Admin/NewAdmin
        ShopEntities1 dbModel = new ShopEntities1();
        public ActionResult Index()
        {
            var lstNew = dbModel.News.ToList();
            return View(lstNew);
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(News objNew)
        {
            try
            {
                if (objNew.ImageUpload != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(objNew.ImageUpload.FileName);
                    var extension = Path.GetExtension(objNew.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objNew.Avatar = fileName;
                    objNew.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                dbModel.News.Add(objNew);
                dbModel.SaveChanges();
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
            var objNew = dbModel.News.Where(n => n.Id == Id).FirstOrDefault();
            return View(objNew);
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var objNew = dbModel.News.Where(n => n.Id == Id).FirstOrDefault();
            return View(objNew);
        }
        [HttpPost]
        public ActionResult Edit(News objNew)
        {
            try
            {
                if (objNew.ImageUpload != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(objNew.ImageUpload.FileName);
                    var extension = Path.GetExtension(objNew.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objNew.Avatar = fileName;
                    objNew.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                dbModel.Entry(objNew).State = EntityState.Modified;
                dbModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objNew = dbModel.News.Where(n => n.Id == Id).FirstOrDefault();
            return View(objNew);
        }
        [HttpPost]
        public ActionResult Delete(News objNew)
        {
            var objNews = dbModel.News.Where(n => n.Id == objNew.Id).FirstOrDefault();
                dbModel.News.Remove(objNews);
                dbModel.SaveChanges();
                return RedirectToAction("Index");
           
        }
    }
}