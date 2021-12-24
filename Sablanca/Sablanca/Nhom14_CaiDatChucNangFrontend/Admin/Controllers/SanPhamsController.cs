using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sablanca.Models;

namespace Sablanca.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private SablancaDB db = new SablancaDB();

        // GET: Admin/SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc);
            return View(sanPhams.ToList());
        }

        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaDM,TenSP,GiaBan,GiaGoc,MauSac,ChatLieu,LoaiDayDeo,KichThuoc,Dong,MoTaSP,AnhSP")] SanPham sanPham)
        {
            
                /*if (ModelState.IsValid)
                {
                    sanPham.AnhSP = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/wwwroot/AnhSP/" + FileName);
                        f.SaveAs(UploadPath);
                        sanPham.AnhSP = FileName;
                    }
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
                
           
               
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                ViewBag.Error = "Dữ liệu không hợp lệ!";
                return View(sanPham);*/
            if (ModelState.IsValid)
            {
                sanPham.AnhSP = "";
                var f = Request.Files["ImageFile"];

                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    //lấy tên file upload
                    string UploadPath = Server.MapPath("~/wwwroot/AnhSP/" + FileName);
                    //Copy và lưu file vào server.
                    f.SaveAs(UploadPath);
                    //Lưu tên file vào trường Image
                    sanPham.AnhSP = FileName;
                }
                else
                {
                    ViewBag.Error = "Chưa nhập ảnh ";
                    return View("Create");
                }


                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            ViewBag.Error = "Lỗi nhập dữ liệu! ";
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
            return View(sanPham);

            /*if (ModelState.IsValid)
            {
                sanPham.AnhSP = "";
                var f = Request.Files["ImageFile"]; 
                if (f != null && f.ContentLength > 0)
                {
                    //Use  Namespace  called  :	System.IO
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    //Lấy  tên  file  upload
                    string UploadPath = Server.MapPath("~/wwwroot/AnhSP/" + FileName);
                    //Copy  Và  lưu  file  vào  server.
                    f.SaveAs(UploadPath);
                    //Lưu  tên  file  vào  trường  Image
                    sanPham.AnhSP  =  FileName;
                }

                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                *//*return RedirectToAction("Index");*/


            /*ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
            return View(sanPham);*/

        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaDM,TenSP,GiaBan,GiaGoc,MauSac,ChatLieu,LoaiDayDeo,KichThuoc,Dong,MoTaSP,AnhSP")] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/wwwroot/AnhSP/" + FileName);
                        f.SaveAs(UploadPath);
                        sanPham.AnhSP = FileName;
                    }
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!";
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                return View(sanPham);
            }
            /*if (ModelState.IsValid)
                {
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                return View(sanPham);
            }*/

            /*// GET: Admin/SanPhams/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SanPham sanPham = db.SanPhams.Find(id);
                if (sanPham == null)
                {
                    return HttpNotFound();
                }
                return View(sanPham);
            }

            // POST: Admin/SanPhams/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                SanPham sanPham = db.SanPhams.Find(id);
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
        }
        /*[HttpPost]
        public JsonResult Delete(int? id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            try
            {
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();
                bool success = true;

                return Json(success, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                bool success = false;
                return Json(success, JsonRequestBehavior.AllowGet);
            }

        }*/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
