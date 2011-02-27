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
        public ActionResult ListGroup(string siteTip=null)
        {
            var vm = CreateViewData<VMGroupList>();
            //load list data
            vm.GroupList = Group.ListAllData();
            vm.Msg = siteTip;
            return View(vm);
        }
        /// <summary>
        /// list items by specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ListGroup(CommonFilterData filter)
        {
            var vm = CreateViewData<VMGroupList>();
            switch (filter.Action)
            {
                case "trash":
                    var msg = Group.DeleteByID(filter.ObjIDList.ToArray());
                    if (!msg.Error) return ListGroup(string.Format("Items with id [{0}] have been deleted!",String.Join(",",filter.ObjIDList)));
                    vm.Error = msg.Error;
                    vm.Msg = msg.Body;
                    vm.GroupList = Group.ListAllData();
                    break;
                default:
                    //no action,just render the default list
                    return ListGroup();
                    break;
            }
            return View(vm);
        }
        [HttpPost]
        public JsonResult EditGroup(VMGroupEdit obj)
        {
            var msg = new Msg();
            if (!TryUpdateModel(obj)||!ModelState.IsValid) {
                msg.Error = true;
                msg.Body =String.Format("Fields [{0}] are invalid!",string.Join(",",ModelState.Keys));
                return Json(msg);
            }
            var obj1 = Group.Save(obj);
            return Json(obj1);
        }
        /// <summary>
        /// delete a specified item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteGroup(Guid id)
        {
            var msg = new Msg();
            if (id.Equals(Guid.Empty))
            {
                msg.Error = true;
                msg.Body = string.Format("Invalid parameter id [{0}]", id);
            }
            else
            {
                msg = Group.DeleteByID(id);
            }
            return Json(msg);
        }
    }
}