using Sablanca.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sablanca.Controllers
{
    public class ProfileController : ProtectController
    {
        SablancaDB db = new SablancaDB();
        // GET: Profile
        public ActionResult Index()
        {
            User oldUser = Session["user"] as User;
            User newUser = db.Users.Where(us => us.email.Equals(oldUser.email)).FirstOrDefault();
            Session["user"] = newUser;
            var errMsg = TempData["ErrorMessage"] as string;
            ViewBag.Infor = errMsg;
            return View(newUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(FormCollection frm)
        {
            string fullname = frm["full_name"];
            string phone = frm["phone_number"];
            string email = frm["email"];
            string address = frm["address"];

            User user = db.Users.Where(us => us.email == email).SingleOrDefault();
            user.full_name = fullname;
            user.phone_number = phone;
            user.address = address;
            if (user != null)
            {
                db.Entry(user).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                ViewBag.Information = "Cập nhật thành công";
            }
            else
            {
                ViewBag.Information = "Có lỗi xảy ra khi cập nhật";
            }

            return View("Index", user);
        }

        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                User user = Session["user"] as User;
                string old_password = frm["old_password"];
                string new_password = frm["password"];
                string confirm_password = frm["confirm_password"];
                if (!Helper.EncodePassword(old_password).Equals(user.password))
                {
                    ViewBag.Error = "Mật khẩu cũ không đúng!";
                    return View(frm);
                }
                User userEntity = db.Users.Where(us => us.email == user.email).SingleOrDefault();
                userEntity.password = Helper.EncodePassword(new_password);
                db.Entry(userEntity).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                ViewBag.Information = "Đổi mật khẩu thành công";

                return View("Index", userEntity);

            }
            ViewBag.Error = "Chưa validate!";
            return View();
        }

        [NotAuthorize]
        public ActionResult Logout()
        {
            Session["user"] = null;
            return Redirect("/");
        }
        [NotAuthorize]
        public ActionResult Login()
        {
            if (Session["user"] == null)
            {
                return View();
            }
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotAuthorize]
        public ActionResult Login(FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                string email = frm["email"];
                string password = frm["password"];

                string currentPass = Helper.EncodePassword(password);
                var user = db.Users.Where(s => s.email.Equals(email) && s.password.Equals(currentPass)).SingleOrDefault();
                if (user != null && !user.is_active)
                {
                    ViewBag.error = "Tài khoản của bạn đã bị khoá";
                    return View();
                }
                else if (user != null && user.is_active)
                {
                    //add session
                    Session["user"] = user;
                    return Redirect("/");
                }
                else
                {
                    ViewBag.error = "Đăng nhập thất bại";
                    return View();
                }
            }
            ViewBag.error = "Đăng nhập thất bại";
            return View();
        }
        [NotAuthorize]
        public ActionResult Register()
        {
            if (Session["user"] == null)
            {
                return View();
            }
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotAuthorize]
        public ActionResult Register(FormCollection frm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string full_name = frm["full_name"];
                    string email = frm["email"];
                    string password = frm["password"];
                    string confirm_password = frm["confirm_password"];
                    string phone = frm["phone_number"];
                    string address = frm["address"];

                    if (!password.Equals(confirm_password))
                    {
                        ViewBag.Error = "Mật khẩu không khớp.";
                        return View();
                    }

                    var user = db.Users.Where(us => us.email == email).SingleOrDefault();

                    if (user != null)
                    {
                        ViewBag.Error = "Vui lòng nhập địa chỉ email khác.";
                        return View();
                    }

                    User u = new User();
                    u.full_name = full_name;
                    u.email = email;
                    u.password = Helper.EncodePassword(password);
                    u.address = address;
                    u.phone_number = phone;
                    u.role = 0;
                    u.is_active = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(u);
                    db.SaveChanges();
                }
                return Redirect("/Profile/Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi dữ liệu " + ex;
                return View();
            }
        }
    }
}