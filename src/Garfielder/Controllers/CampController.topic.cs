using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;
using Garfielder.Models;
namespace Garfielder.Controllers
{
    public partial class CampController: BaseController
    {
        public ActionResult ListTopic() {
            var vm = CreateViewData<VMCampTopicList>();
            using (var db = new GarfielderEntities())
            {
                var items = db.Topics.ToList();
                vm.TopicList = new List<VMCampTopicEdit>();
                var item=default(VMCampTopicEdit);
                items.ForEach(x =>
                {
                    item = new VMCampTopicEdit
                    {
                        Id = x.Id,
                        Title = x.Title,
                        UserName = x.User.Name,
                        Grade = x.Grade,
                        CreateAt = x.CreatedAt,
                        CntComment = x.Comments.Count
                    };

                    item.GroupsTxt = String.Join(",", x.Groups.ToList().ConvertAll(y => y.Name));
                    item.TagsTxt = String.Join(",", x.Tags.ToList().ConvertAll(z => z.Name));

                    vm.TopicList.Add(item);
                });
            }
            return View(vm);
        }
        [HttpGet]
        public ActionResult EditTopic(Guid? id) {
            if (id == null || id == Guid.Empty) {
                return NewTopic();
            };
            var vm0 = CreateViewData<VMCampTopicShow>();
            var vm1=Topic.GetTopic(id.Value);
            if (vm1 == null) return NewTopic();
            vm1.CurrentUser = vm0.CurrentUser;
            return View(vm1);
        }
        [HttpPost/*,ValidateInput(false)*/]
        public ActionResult EditTopic(VMCampTopicEdit obj)
        {
            if (this.ModelState.IsValid)
            {
                var dbm = new Topic();
                dbm.Id = Guid.NewGuid();
                dbm.Title = obj.Title;
                dbm.ContentX = obj.ContentX;
                dbm.CreatedAt = DateTime.Now;
                //TODO:
                dbm.Slug = dbm.Id.ToString();
                dbm.Description = dbm.ContentX;
                using (var db = new GarfielderEntities()) {
                    db.CommandTimeout = 0;
                    //dbm.UserID = CurrentUser.Id;
                    //db.AddToTopics(dbm);
                    db.Attach(CurrentUser);
                    dbm.User = CurrentUser;
                    db.AddToTopics(dbm);
                    db.SaveChanges();
                };
            };
            return View(obj);
        }
        private ActionResult NewTopic() {
            var vm = CreateViewData<VMCampTopicShow>();
            vm.Id=Guid.NewGuid();
            vm.GroupsAll = Group.ListAll();
            vm.TagsAll = Tag.ListAll();
            return View(vm);
        }
    }
}
