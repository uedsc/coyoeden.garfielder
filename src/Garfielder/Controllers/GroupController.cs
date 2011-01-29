using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garfielder.Controllers
{
    public class GroupController : BaseController
    {
        //
        // GET: /Group/

        public ActionResult Index(string id)
        {
            return View();
        }

    }
}
