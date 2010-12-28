using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;
using Garfielder.Models;
using Garfielder.Core.Infrastructure;

namespace Garfielder.Controllers
{
    public partial class CampController
    {
        public ActionResult ListGroup()
        {
            var vm = CreateViewData<VMGroupList>();
            //load list data
            vm.GroupList = Group.ListAllData();

            return View(vm);
        }
        [HttpPost]
        public JsonResult EditGroup(VMGroupEdit obj)
        {
            if (!TryUpdateModel(obj)) {
                return new JsonResult() { Data=false};
            }
            if (this.ModelState.IsValid)
            {
                obj.Id = Guid.NewGuid();
                obj.Slug=string.IsNullOrEmpty(obj.Slug)?obj.Name.CHSToPinyin().ToLower():obj.Slug.ToLower();

                var dbm = new Group();
                dbm.Id =obj.Id;
                dbm.Name = obj.Name;
                dbm.Slug = obj.Slug;
                dbm.Description = obj.Description;
                dbm.CreatedAt = DateTime.Now;
                dbm.ParentID = obj.ParentID.Equals(Guid.Empty) ? default(Guid?): obj.ParentID;
                dbm.Level = obj.Level;
                //TODO:
                dbm.CreatedBy = "Sys";
                using (var db = new GarfielderEntities())
                {
                    db.CommandTimeout = 0;
                    db.AddToGroups(dbm);
                    db.SaveChanges();

                    //clear cache
                    Group.ClearCache();

                    if (dbm.Parent != null) {
                        obj.ParentName = dbm.Parent.Name;
                    }

                };
            };
            return Json(obj);
        }
    }
}