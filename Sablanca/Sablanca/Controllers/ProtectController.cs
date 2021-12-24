using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sablanca.Models;

namespace Sablanca.Controllers
{
    public class ProtectController : Controllers
    {
        public User user
        {
            get
            {
                User _user = Session["user"] as User;
                if (_user == null)
                    _user = new User();
                return _user;
            }
        }

        public class NotAuthorizeAttribute : FilterAttribute { }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (attributes.Any(a => a is NotAuthorizeAttribute)) return;
            if (Session["user"] == null)
            {
                filterContext.Result = new RedirectResult("/Profile/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}