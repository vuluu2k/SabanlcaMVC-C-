using Sablanca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sablanca.Controllers
{
    public class CheckoutController : Controller
    {
        SablancaDB db = new SablancaDB();
        // GET: Checkout
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            ViewBag.Categories = categories;
            return View();
        }
    }
}