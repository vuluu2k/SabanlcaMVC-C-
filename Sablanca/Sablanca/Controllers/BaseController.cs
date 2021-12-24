using Sablanca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sablanca.Controllers
{
    public class BaseController : ProtectController
    {
        SablancaDB db = new SablancaDB();
        [NotAuthorize]
        public PartialViewResult _Header()
        {
            return PartialView(user);
        }
        [NotAuthorize]
        public PartialViewResult _MenuPC()
        {
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }
        [NotAuthorize]
        public PartialViewResult _MenuMobile()
        {
            ViewBag.User = user.full_name;
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }
        [NotAuthorize]
        public PartialViewResult _BreadcrumbLevelOne(string id)
        {
            var category = db.Categories.Where(c => c.category_id.ToString() == id).SingleOrDefault();

            return PartialView(category);
        }
        [NotAuthorize]
        public PartialViewResult _BreadcrumbLevelTwo(string id)
        {
            var product = db.Products.Where(c => c.product_id.ToString() == id).SingleOrDefault();

            return PartialView(product);
        }
        public PartialViewResult _ListOrder()
        {
            var order = db.Orders.Join(db.Users, o => o.user_id, u => u.user_id, (o, u) => new
            {
                order = o,
                user = u
            }).Where(u => u.user.user_id == user.user_id)
            .Select(o => new
            {
                order_id = o.order.order_id,
                user_id = o.user.full_name,
                status = o.order.status,
                created_at = o.order.created_at
            }).Join(db.Order_detail, x => x.order_id, od => od.order_id, (x, od) => new
            {
                ele = x,
                order_detail = od
            }).GroupBy(x => new
            {
                order_id = x.ele.order_id,
                user_id = x.ele.user_id,
                status = x.ele.status,
                created_at = x.ele.created_at
            })
            .Select(e => new
            {
                order_id = e.Key.order_id,
                user_id = e.Key.user_id,
                status = e.Key.status,
                created_at = e.Key.created_at,
                amount = e.Sum(v => v.order_detail.quantity * v.order_detail.price)
            }).OrderByDescending(o => o.created_at).ToList();

            List<Custom_order> list = new List<Custom_order>();
            foreach (var item in order)
            {
                Custom_order c = new Custom_order();
                c.order_id = item.order_id;
                c.user_id = item.user_id;
                if (item.status == 1)
                {
                    c.status = "Đang giao";
                }
                else if (item.status == 2)
                {
                    c.status = "Đã giao";
                }
                else if (item.status == 3)
                {
                    c.status = "Đã huỷ";
                }
                c.amount = decimal.Parse(item.amount.ToString());
                c.created_at = DateTime.Parse(item.created_at.ToString());

                list.Add(c);
            }

            return PartialView(list);
        }
    }
}