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
            using (var db = new GarfielderEntities()) {
                var items = db.Groups.ToList();
                vm.GroupList=new List<VMGroupEdit>();
                items.ForEach(x => {
                    vm.GroupList.Add(new VMGroupEdit { 
                        Name=x.Name,
                        Slug=x.Slug,
                        Id=x.Id,
                        Level=x.Level,
                        Description=x.Description,
                        ParentID=x.ParentID,
                        ParentName=x.Parent==null?"":x.Parent.Name
                    });
                });
            }
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
                //TODO:
                dbm.CreatedBy = "Sys";
                using (var db = new GarfielderEntities())
                {
                    db.CommandTimeout = 0;
                    db.AddToGroups(dbm);
                    db.SaveChanges();

                    if (dbm.Parent != null) {
                        obj.ParentName = dbm.Parent.Name;
                    }

                };
            };
            return new JsonResult { Data=obj};
        }
    }
}