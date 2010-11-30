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
        public ActionResult ListGroup()
        {
            var vm = CreateViewData<VMGroupList>();
            using (var db = new GarfielderEntities()) {
                var items = db.Groups.ToList();
                vm.GroupList=new List<VMGroupEdit>();
                items.ForEach(x => {
                    vm.GroupList.Add(new VMGroupEdit { 
                        Name=x.Name,
                        Slug=x.Slug,
                        Id=x.Id
                    });
                });
            }
            return View(vm);
        }
        [HttpGet]
        public ActionResult EditGroup(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NewGroup();
            };
            var vm = CreateViewData<VMGroupEdit>();
            using (var db = new GarfielderEntities())
            {
                var dbModel = db.Groups.Single(x => x.Id == id.Value);
                vm.Id = dbModel.Id;
                vm.Name = dbModel.Name;
                vm.Slug = dbModel.Slug;
                vm.Description = dbModel.Description;
            };
            return View(vm);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditGroup(VMGroupEdit obj)
        {
            if (this.ModelState.IsValid)
            {
                var dbm = new Group();
                dbm.Id = Guid.NewGuid();
                dbm.Name = obj.Name;
                dbm.Slug = obj.Slug;
                dbm.Description = obj.Description;
                dbm.CreatedAt = DateTime.Now;
                //TODO:
                dbm.CreatedBy = "Sys";
                using (var db = new GarfielderEntities())
                {
                    db.CommandTimeout = 0;
                    db.AddToGroups(dbm);
                    db.SaveChanges();
                };
            };
            return View(obj);
        }
        private ActionResult NewGroup()
        {
            var vm = CreateViewData<VMGroupEdit>();
            vm.Id = Guid.NewGuid();
            return View(vm);
        }
    }
}