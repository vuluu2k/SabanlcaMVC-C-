using Sablanca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Sablanca.Controllers
{
    public class CategoryController : Controller
    {
        SablancaDB db = new SablancaDB();
        // GET: Category
        public ActionResult Detail(int id, string order, decimal? fromPrice, decimal? toPrice, int? page)
        {
            if (id == null)
            {
                return Redirect("/NotFound/Index");
            }
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return Redirect("/NotFound/Index");
            }

            var products = db.Products.Where(p => p.category_id == id).OrderByDescending(p => p.product_id);

            if (order != null)
                switch (order)
                {
                    case "desc":
                        products = products.OrderByDescending(p => p.product_price);
                        ViewBag.order = "desc";
                        break;
                    case "asc":
                        products = products.OrderBy(p => p.product_price);
                        ViewBag.order = "asc";
                        break;
                    default:
                        ViewBag.order = "default";
                        break;
                }

            if (fromPrice != null)
            {
                ViewBag.from = fromPrice;
                products = (IOrderedQueryable<Product>)products.Where(p => p.product_price >= fromPrice);
            }

            if (toPrice != null)
            {
                ViewBag.to = toPrice;
                products = (IOrderedQueryable<Product>)products.Where(p => p.product_price <= toPrice);
            }

            int pageNumber = (page ?? 1);
            ViewBag.Category = category.category_name;

            return View(products.ToPagedList(pageNumber, 8));
        }
    }
}