using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;
using Garfielder.Models;
using System.Web.Hosting;
using System.IO;

namespace Garfielder.Controllers
{
    public partial class CampController
    {
        public ActionResult ListFile()
        {
            var vm = CreateViewData<VMXFileList>();
            using (var db = new GarfielderEntities()) {
                var items = db.XFiles.ToList();
                vm.FileList=new List<VMXFileEdit>();
                items.ForEach(x =>
                {
                    vm.FileList.Add(new VMXFileEdit
                    { 
                        Name=x.Name,
                        Name1=x.Name.Substring(0,x.Name.Length-x.Extension.Length),
                        Title=x.Title,
                        CreatedAt=x.CreatedAt,
                        Extension=x.Extension,
                        Id=x.Id,
                        UserName=x.User.Name
                    });
                });
            }
            return View(vm);
        }
        [HttpGet]
        public ActionResult EditFile(Guid id)
        {
            var vm = CreateViewData<VMXFileEdit>();
            using (var db = new GarfielderEntities())
            {
                var dbModel = db.XFiles.Single(x => x.Id == id);
                vm.Id = dbModel.Id;
                vm.Name = dbModel.Name;
                vm.Title = dbModel.Title;
                vm.Extension = dbModel.Extension;
                vm.CreatedAt = dbModel.CreatedAt;
                vm.Description = dbModel.Description;
            };
            return View(vm);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditFile(VMXFileEdit obj)
        {
            if (this.ModelState.IsValid)
            {
                using (var db = new GarfielderEntities())
                {
                    var dbm = db.XFiles.SingleOrDefault(x => x.Id == obj.Id);
                    if (dbm != null) {
                        dbm.Title = obj.Title;
                        dbm.Description = obj.Description;
                        db.CommandTimeout = 0;
                        db.SaveChanges();
                    };

                };
            };
            return View(obj);
        }

        [HttpGet]
        public ActionResult AddMedia(bool flash=true) {
            var vm = CreateViewData<VMXFileEdit>();
            vm.NoFlash = !flash;
            return View(vm);
        }
        /// <summary>
        /// Normal upload via html form
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMedia() {
            var vm = XFile.SaveFile(this.Request);
            vm.CurrentUser = this.CurrentUser;
            return View(vm);
        }
        [HttpGet]
        public ActionResult UploadMedia(string RelId="",string Src="local",bool flash=true) {
            var vm = CreateViewData<VMUploadMedia>();
            vm.NoFlash = !flash;
            var rel=Guid.Empty;
            Guid.TryParse(RelId,out rel);
            vm.RelId = rel;
            vm.Src = Src;
            vm.FileList = new List<VMXFileEdit>();
            return View(vm);
        }

        /// <summary>
        /// Ajax upload via SWFUpload
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveMedia() {
            var vm = XFile.SaveFile(this.Request);
            return Json(vm);
        }

    }
}