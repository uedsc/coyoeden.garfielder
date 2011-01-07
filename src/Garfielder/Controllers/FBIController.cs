using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.Models;

namespace Garfielder.Controllers
{
    /// <summary>
    /// Validation services controller
    /// </summary>
    public class FBIController : Controller
    {
        #region Group validation
        public JsonResult GroupName(string Name,Guid Id)
        {
            if (!Id.Equals(Guid.Empty)) return Json(true);
            dynamic r = Group.ValidateName(Name);
            if (r == false)
                r = string.Format("Name [{0}] exists,please choose another one", Name);

            return Json(r);
        }
        public JsonResult GroupSlug(string Slug,Guid Id) {
            if (!Id.Equals(Guid.Empty)) return Json(true);
            dynamic r = Group.ValidateSlug(Slug);
            if(r==false)
                r = string.Format("Slug [{0}] exists,please choose another one", Slug);
            return Json(r);
        }
        #endregion

        #region Tag validation
        public JsonResult TagName(string Name,Guid Id)
        {
            if (!Id.Equals(Guid.Empty)) return Json(true);
            dynamic r = Tag.ValidateName(Name);
            if (r == false)
                r = string.Format("Name [{0}] exists,please choose another one", Name);

            return Json(r);
        }
        public JsonResult TagSlug(string Slug,Guid Id)
        {
            if (!Id.Equals(Guid.Empty)) return Json(true);
            dynamic r = Tag.ValidateSlug(Slug);
            if (r == false)
                r = string.Format("Slug [{0}] exists,please choose another one", Slug);
            return Json(r);
        }
        #endregion
    }
}
