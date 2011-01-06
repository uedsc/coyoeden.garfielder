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
        public ActionResult ListTag()
        {
            var vm = CreateViewData<VMTagList>();
            using (var db = new GarfielderEntities()) {
                var items = db.Tags.ToList();
                vm.TagList=new List<VMTagEdit>();
                items.ForEach(x => vm.TagList.Add(
                    new VMTagEdit { 
                    Name=x.Name,
                    Slug=x.Slug,
                    Id=x.Id,
                    CntTopic=x.Topics.Count
                }));
            }//using
            return View(vm);
        }
        [HttpPost]
        public JsonResult EditTag(VMTagEdit obj)
        {
            if (!TryUpdateModel(obj))
            {
                return new JsonResult() { Data = false };
            }
            if (this.ModelState.IsValid)
            {
                obj.Id = Guid.NewGuid();
                obj.Slug = string.IsNullOrEmpty(obj.Slug) ? obj.Name.CHSToPinyin("-").ToLower() : obj.Slug.ToLower();

                var dbm = new Tag();
                dbm.Id = obj.Id;
                dbm.Name = obj.Name;
                
                dbm.CreatedAt = DateTime.Now;
                //TODO:
                dbm.CreatedBy = "Sys";
                using (var db = new GarfielderEntities())
                {
                    dbm.Slug =Tag.CheckSlug(db,obj.Slug);
                    db.CommandTimeout = 0;
                    db.AddToTags(dbm);
                    Tag.ClearCache();
                    db.SaveChanges();
                };
            };
            return Json(obj);
        }
        /// <summary>
        /// check a tag's existence
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public JsonResult CheckTag(string tag) {
            var vm = Tag.AddTag(tag);
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