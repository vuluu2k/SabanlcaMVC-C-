using Sablanca.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sablanca.Controllers
{
    public class InvoiceController : ProtectController
    {
        SablancaDB db = new SablancaDB();
        // GET: Invoice
        public ActionResult Index(int id)
        {
            var order = db.Orders.Where(o => o.order_id == id && o.user_id == user.user_id).SingleOrDefault();
            if (order == null)
            {
                return Redirect("/NotFound/Index");
            }
            var quantity = db.Order_detail.Select(od => od)
                .Where(od => od.order_id == id)
                .Sum(od => od.quantity);
            var price = db.Order_detail.Select(od => od)
                .Where(od => od.order_id == id)
                .Sum(od => od.quantity * od.price);
            var detail = db.Order_detail.Where(od => od.order_id == id).ToList();

            ViewBag.CreatedAt = order.created_at;
            ViewBag.Address = user.address;
            ViewBag.OrderId = id;
            ViewBag.Quantity = quantity;
            ViewBag.Price = price;

            return View(detail);
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            string ids = frm["ids"];
            if (user.address == null || user.phone_number == null ||
                user.full_name == null || user.email == null)
            {
                TempData["ErrorMessage"] = "Vui lòng cập nhật đầy đủ thông tin trước khi thanh toán!";
                return RedirectToAction("Index", "Profile");
            }

            string[] listId = ids.Split(',');
            List<int> idsInt = new List<int>();
            for (int i = 0; i < listId.Length; i++)
            {
                idsInt.Add(int.Parse(listId[i]));
            }

            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < idsInt.Count; i++)
            {
                int value = 1;
                if (dic.ContainsKey(idsInt[i]))
                {
                    value = dic[idsInt[i]];
                    dic[idsInt[i]] = value + 1;
                }
                else
                {
                    dic.Add(idsInt[i], value);
                }

            }

            var products = db.Products.Where(p => idsInt.Contains(p.product_id)).ToList();
            int uid = user.user_id;

            DbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                Order order = new Order();

                order.user_id = uid;
                order.status = 1;
                db.Orders.Add(order);

                for (int i = 0; i < idsInt.Count; i++)
                {
                    if (dic.ContainsKey(idsInt[i]))
                    {
                        Order_detail od = new Order_detail();
                        int value = dic[idsInt[i]];
                        od.order_id = order.order_id;
                        od.product_id = idsInt[i];
                        od.quantity = value;
                        Product product = products.Find(p => p.product_id == idsInt[i]);

                        if (product.product_amount < value)
                        {
                            transaction.Rollback();
                            TempData["ErrorMessage"] = "Số lượng sản phẩm " + product.product_name + " không hợp lệ";
                            return RedirectToAction("Index", "Checkout");
                        }

                        product.product_amount = product.product_amount - value;
                        od.price = product.product_price;
                        db.Order_detail.Add(od);
                        dic.Remove(idsInt[i]);
                    }
                }

                db.SaveChanges();

                transaction.Commit();
                return RedirectToAction("Index", "Invoice", new { id = order.order_id });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                TempData["ErrorMessage"] = "Lỗi khi thanh toán " + ex.Message;
                return RedirectToAction("Index", "Checkout");
            }
        }
    }
}