using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sablanca.Models;
namespace Sablanca.Controllers
{
    public class HomeController : Controller
    {
        SablancaDB db = new SablancaDB();
        // GET: Home
        public ActionResult Index(string id)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            if(id==null)
            {
                sanPhams = db.SanPhams.Select(h => h).ToList();
            }   
            else
            {
                sanPhams = db.SanPhams.Where(h => h.MaDM.Equals(id)).Select(h => h).ToList();
            }    
            /*var SanPhams = db.SanPhams.Select(h => h);*/
            return View(sanPhams);
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Product(int id )
        {
            var sanphams = db.SanPhams.SingleOrDefault(x => x.MaSP == id);
            return View(sanphams);
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Category(string id)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            if (id == null)
            {
                sanPhams = db.SanPhams.Select(h => h).ToList();
            }
            else
            {
                sanPhams = db.SanPhams.Where(h => h.MaDM.Equals(id)).Select(h => h).ToList();
            }
            /*var SanPhams = db.SanPhams.Select(h => h);*/
            return View(sanPhams);
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Invoice()
        {
            return View();
        }
        public PartialViewResult _MenuPC()
        {
            var danhmuc = db.DanhMucs.Select(n => n);
            return PartialView(danhmuc);
        }
        public PartialViewResult _Header()
        {
            return PartialView();
        }
        public PartialViewResult _BreadcrumbLevelTwo()
        {
            var sanpham = db.SanPhams.Select(n => n);
            return PartialView(sanpham);
        }
        /*public PartialViewResult _BreadcrumbLevelOne()
        {

            var danhmuc = db.DanhMucs.Select(n => n);
            return PartialView(danhmuc);
        }*/
    }
}