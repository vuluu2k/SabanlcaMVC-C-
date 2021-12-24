using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sablanca.Models;
using System.Data.Entity;
using System.Net;


namespace Sablanca.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private SablancaDB db = new SablancaDB();
        // GET: Admin/Home
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Account()
        {
            var Taikhoan = db.TaiKhoans.Select(p=> p).ToList();
            return View(Taikhoan);
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Add_account()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_account([Bind(Include = "MaTK,HoTen,SDT,DiaChi,Email,MatKhau")] TaiKhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taikhoan);
                db.SaveChanges();
                return RedirectToAction("Account");
            }

            return View(taikhoan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_account([Bind(Include = "MaTK,HoTen,SDT,DiaChi,Email,MatKhau")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Account");
            }
            return View(taiKhoan);
        }
        public ActionResult Add_category()
        {
            return View();
        }
        public ActionResult Add_product()
        {
            return View();
        }
        public ActionResult Categories()
        {
            return View();
        }
        public ActionResult Edit_account(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }
        public ActionResult Edit_category()
        {
            return View();
        }
        public ActionResult Edit_product()
        {
            return View();
        }
        public ActionResult Manage_order()
        {
            return View();
        }
        public ActionResult Order_detail()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
    }
}