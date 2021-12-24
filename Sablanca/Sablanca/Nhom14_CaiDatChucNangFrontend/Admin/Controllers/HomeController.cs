using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sablanca.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Admin/Home
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Account()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Add_account()
        {
            return View();
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
        public ActionResult Edit_account()
        {
            return View();
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