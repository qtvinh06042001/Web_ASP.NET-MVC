using OnlineShop.Context;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        ShopEntities1 objModel =  new ShopEntities1();
        public ActionResult Index()
        {
            var lstCategory = objModel.Category.ToList();
            var lstProduct = objModel.Product.OrderByDescending(x=>x.Id).Take(4).ToList();
            Product_Category objProduct_Category = new Product_Category();
            objProduct_Category.ListCategory = lstCategory;
            objProduct_Category.ListProduct = lstProduct;
            return View(objProduct_Category);
        }
        //GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users _user)
        {
            if (ModelState.IsValid)
            {
                var check = objModel.Users.FirstOrDefault(n => n.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objModel.Configuration.ValidateOnSaveEnabled = false;
                    objModel.Users.Add(_user);
                    objModel.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email da ton tai";
                    return View();
                }
            }
            return View();
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] formData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(formData);
            string byte2String = null;
            for(int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email,string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = objModel.Users.Where(n => n.Email.Equals(email) && n.Password == f_password);
                if (data.Count() > 0)
                {
                    Session["Fullname"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = Convert.ToInt32(data.FirstOrDefault().idUser);
                    if (Session["Email"].ToString() == "admin@gmail.com")
                    {
                        Session["Admin"] = 1;
                    }
                    else
                    {
                        Session["Admin"] = null;
                    }
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
                
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}