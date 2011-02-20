using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;
using Garfielder.Models;
using System.Web.Security;

namespace Garfielder.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
        	return RedirectToAction("Index", "Studio");
        }

        private void fakeLogin() {
            using (var db = new GarfielderEntities()) {
                var user = db.Users.First();
                HttpContext.Items["CurrentUser"] = user;
                FormsAuthentication.SetAuthCookie(user.Name, false);
            }
        }

    }
}
