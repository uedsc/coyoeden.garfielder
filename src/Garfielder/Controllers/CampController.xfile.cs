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
        public ActionResult ListFile(string siteTip=null)
        {
            var vm = CreateViewData<VMXFileList>();
            vm.FileList = XFile.ListAllData();
        	vm.Msg = siteTip;
            return View(vm);
        }
		[HttpPost]
		public ActionResult ListFile(CommonFilterData filter)
		{
			var vm = CreateViewData<VMXFileList>();

			switch (filter.Action)
			{
				case "trash":
					var msg = XFile.DeleteByID(filter.ObjIDList.ToArray());
					if (!msg.Error) return ListFile(string.Format("Items with id [{0}] have been deleted!", String.Join(",", filter.ObjIDList)));
					vm.Msg = msg.Body;
					vm.Error = msg.Error;
					break;
			}

			vm.FileList = XFile.ListAllData();
			return View(vm);
		}

    	[HttpGet]
        public ActionResult EditFile(Guid id)
        {
            var vm = CreateViewData<VMXFileEdit>();
            var dbModel = XFile.ListAll().SingleOrDefault(x => x.Id == id);
            vm.Id = dbModel.Id;
            vm.Name = dbModel.Name;
            vm.Title = dbModel.Title;
            vm.Extension = dbModel.Extension;
            vm.CreatedAt = dbModel.CreatedAt;
            vm.Description = dbModel.Description;
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
        public ActionResult UploadMedia(string RefId="",string Src="local",bool flash=true,string mode="list") {
            var vm = CreateViewData<VMUploadMedia>();
            vm.NoFlash = !flash;
            var rel=Guid.Empty;
            Guid.TryParse(RefId,out rel);
            vm.RefId = rel;
            vm.Src = Src;
            vm.ViewMode = mode;
            vm.FileList = XFile.ListAllData();
			if(!rel.Equals(Guid.Empty))
			{
				vm.RefAttachments = Topic.GetAttachments(rel);
			}
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
        /// <summary>
        /// attach a file to a topic
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public JsonResult AttachFileToTopic(string fid,string tid)
        {
            var msg = XFile.AttachToTopic(fid, tid);
            return Json(msg);
        }

    }
}