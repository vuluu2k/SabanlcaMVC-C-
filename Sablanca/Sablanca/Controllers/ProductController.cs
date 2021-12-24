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
    public class ProductController : Controller
    {
        SablancaDB db = new SablancaDB();
        // GET: Product
        public ActionResult Detail(int id)
        {
            if (id == null)
            {
                return Redirect("/NotFound/Index");
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return Redirect("/NotFound/Index");
            }
            return View(product);
        }
        public ActionResult Search(string keyword, string order, decimal? fromPrice, decimal? toPrice, string category, int page = 1)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var products = db.Products.Where(p => p.product_name.Contains(keyword)).OrderByDescending(p => p.product_id);

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

            if (category != null)
            {
                string[] ids = category.Split(',');
                ViewBag.category = category;
                products = (IOrderedQueryable<Product>)products.Where(p => ids.Contains(p.category_id.ToString()));
            }

            ViewBag.keyword = keyword;
            ViewBag.Categories = db.Categories.OrderBy(c => c.category_id).ToList();
            return View(products.ToPagedList(page, 12));
        }
        public JsonResult Index(int id)
        {
            if (id == null)
            {
                return Json("NOT_FOUND", JsonRequestBehavior.AllowGet);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return Json("NOT_FOUND", JsonRequestBehavior.AllowGet);
            }
            return Json(
                new
                {
                    id = product.product_id,
                    name = product.product_name,
                    price = product.product_price,
                    quantity = product.product_amount,
                    image = product.product_image
                },
                JsonRequestBehavior.AllowGet
                );
        }
        public JsonResult List(string ids)
        {
            if (ids == null)
            {
                return Json("NOT_FOUND", JsonRequestBehavior.AllowGet);
            }
            string[] listId = ids.Split(',');
            var product = db.Products.Where(p => listId.Contains(p.product_id.ToString())).ToList();
            if (product == null)
            {
                return Json("NOT_FOUND", JsonRequestBehavior.AllowGet);
            }

            List<object> list = new List<object>();

            foreach (var item in product)
            {
                list.Add(new
                {
                    id = item.product_id,
                    name = item.product_name,
                    image = item.product_image,
                    price = item.product_price,
                    quantity = item.product_amount
                });
            }

            return Json(
                list,
                JsonRequestBehavior.AllowGet
                );
        }
    }
}