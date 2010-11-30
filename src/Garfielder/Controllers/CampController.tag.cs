using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;
using Garfielder.Models;

namespace Garfielder.Controllers
{
    public partial class CampController
    {
        public ActionResult ListTag()
        {
            var vm = CreateViewData<VMTagList>();
            using (var db = new GarfielderEntities()) {
                var items = db.Tags.ToList();
                vm.TagList=new List<VMTagEdit>();
                items.ForEach(x => {
                    vm.TagList.Add(new VMTagEdit { 
                        Name=x.Name,
                        Slug=x.Slug,
                        Id=x.Id
                    });
                });
            }
            return View(vm);
        }
        [HttpGet]
        public ActionResult EditTag(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NewTag();
            };
            var vm = CreateViewData<VMTagEdit>();
            using (var db = new GarfielderEntities())
            {
                var dbModel = db.Tags.Single(x => x.Id == id.Value);
                vm.Id = dbModel.Id;
                vm.Name = dbModel.Name;
                vm.Slug = dbModel.Slug;
             
            };
            return View(vm);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditTag(VMTagEdit obj)
        {
            if (this.ModelState.IsValid)
            {
                var dbm = new Tag();
                dbm.Id = Guid.NewGuid();
                dbm.Name = obj.Name;
                dbm.Slug = obj.Slug;
                dbm.CreatedAt = DateTime.Now;
                //TODO:
                dbm.CreatedBy = "Sys";
                using (var db = new GarfielderEntities())
                {
                    db.CommandTimeout = 0;
                    db.AddToTags(dbm);
                    db.SaveChanges();
                };
            };
            return View(obj);
        }
        private ActionResult NewTag()
        {
            var vm = CreateViewData<VMTagEdit>();
            vm.Id = Guid.NewGuid();
            return View(vm);
        }
    }
}