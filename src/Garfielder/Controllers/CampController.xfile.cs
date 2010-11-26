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
        public ActionResult AddMedia() {
            var vm = CreateViewData<VMXFileEdit>();
            return View(vm);
        }
        private string _uploadsFolder = HostingEnvironment.MapPath("~/Assets/Upload/");
        public JsonResult SaveMedia() {
            var vm = CreateViewData<VMXFileEdit>();

            var pic = "Assets/Upload/{0}/{1}/{2}";

            using (var db = new GarfielderEntities())
            {
                var dbm = db.XFiles.SingleOrDefault(x => x.Name ==vm.Name);
                if (dbm == null)
                {
                    //save to disk
                    vm.Name = Request.Files[0].FileName;

                    var uid=Request.Params["UserID"];
                    var uname = Request.Params["UserName"];
                    var path=Path.Combine(_uploadsFolder,uname ,DateTime.Now.ToString("yyyyMMdd"));

                    if (!Directory.Exists(path)) {
                        Directory.CreateDirectory(path);
                    }

                    pic = string.Format(pic, uname, DateTime.Now.ToString("yyyyMMdd"),vm.Name);

                    try
                    {

                        
                        vm.Title = vm.Name;
                        vm.Description = vm.Title;
                        vm.Extension = vm.Name.Substring(vm.Name.LastIndexOf("."));
                        vm.Id = Guid.NewGuid();
                        vm.CreatedAt = DateTime.Now;

                        Request.Files[0].SaveAs(path+"\\"+vm.Name);


                        dbm = new XFile();
                        dbm.Id = vm.Id;
                        dbm.Name =pic;
                        dbm.Title = vm.Title;
                        dbm.Description = vm.Description;
                        dbm.Extension = vm.Extension;
                        dbm.CreatedAt = vm.CreatedAt;
                        
                        dbm.UserID = Guid.Parse(uid);
                        db.CommandTimeout = 0;
                        db.AddToXFiles(dbm);
                        db.SaveChanges();
                    }
                    catch (Exception ex) { 
                        
                    }

                };

            };

            var retVal = new JsonResult();
            retVal.Data = new { pic=pic};

            return retVal;
        }

    }
}