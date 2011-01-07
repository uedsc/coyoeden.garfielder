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
        public ActionResult ListTag(string siteTip=null)
        {
            var vm = CreateViewData<VMTagList>();
            vm.TagList = Tag.ListAllData();
            vm.Msg = siteTip;
            return View(vm);
        }
        /// <summary>
        /// list items by specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ListTag(VMCampCommonFilter filter)
        {
            var vm = CreateViewData<VMTagList>();
            switch (filter.Action)
            {
                case "trash":
                    var msg = Tag.DeleteByID(filter.ObjIDList.ToArray());
                    if (!msg.Error) return ListTag(string.Format("Items with id [{0}] have been deleted!", String.Join(",", filter.ObjIDList)));
                    vm.Error = msg.Error;
                    vm.Msg = msg.Body;
                    vm.TagList = Tag.ListAllData();
                    
                    break;
                default:
                    //no action,just render the default list
                    return ListTag();
                    break;
            }
            return View(vm);
        }
        [HttpPost]
        public JsonResult EditTag(VMTagEdit obj)
        {
            var msg = new Msg();
            if (!TryUpdateModel(obj)||!ModelState.IsValid)
            {
                msg.Error = true;
                msg.Body = ModelErrors();
                return Json(msg);
            }
            var obj1 = Tag.Save(obj);
            return Json(obj1);
        }
        /// <summary>
        /// check a tag's existence,if not exists,we will add a new one!
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public JsonResult CheckTag(string tag) {
            var vm = Tag.QuickAddTag(tag);
            return Json(vm);
        }
        /// <summary>
        /// delete a specified tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteTag(Guid id)
        {
            var msg = new Msg();
            if (id.Equals(Guid.Empty))
            {
                msg.Error = true;
                msg.Body = string.Format("Invalid parameter id [{0}]", id);
            }
            else
            {
                msg = Tag.DeleteByID(id);
            }
            return Json(msg);
        }
    }
}