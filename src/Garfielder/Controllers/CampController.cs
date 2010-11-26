using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;

namespace Garfielder.Controllers
{
    public partial class CampController : BaseController
    {
        //
        // GET: /Camp/
        public ActionResult Index()
        {
            var vm = CreateViewData<VMCampHome>();
            
            return View(vm);
        }

    }
}
