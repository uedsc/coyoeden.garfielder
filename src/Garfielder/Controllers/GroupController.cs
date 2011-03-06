using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.Models;
using Garfielder.ViewModels;

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
		[OutputCache(Duration = 300)]
        public ActionResult View(string id)
        {
            id = id.Trim().ToLower();
            var vm = Group.Get(id);
            return View(vm);
        }

    }
}
