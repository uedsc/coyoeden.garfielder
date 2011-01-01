using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.Data;
using Garfielder.ViewModels;
using Garfielder.Models;
using Garfielder.Core.Infrastructure;
using Group = Garfielder.Models.Group;
using Tag = Garfielder.Models.Tag;
using Topic = Garfielder.Models.Topic;

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
            if (vm1 == null)
            {
                vm0.Error = true;
                vm0.Msg = string.Format("Topic with id [{0}] has been deleted!", id.Value);
                return View(vm0);
            }
            vm1.CurrentUser = vm0.CurrentUser;
            return View(vm1);
        }
        [HttpPost,ValidateInput(false)]/* TODO：MVC3正式版时可以将ValidateInput注释掉,因为ContentX有AllowHtml属性 */
        public ActionResult EditTopic(VMCampTopicShow obj)
        {
            if (this.ModelState.IsValid)
            {
                
                var dbm = default(Topic);
                using(var db=new GarfielderEntities())
                {
                    if (obj.IsNew)
                    {
                        
                        obj.CreateAt = DateTime.Now;
                        obj.Id = Guid.NewGuid();
                        dbm = new Topic();
                        dbm.CreatedAt = obj.CreateAt;
                        
                        dbm.Id = obj.Id;
                        db.AddToTopics(dbm);
                    }
                    else
                    {
                        dbm = db.Topics.SingleOrDefault(x => x.Id == obj.Id);
                        if(dbm==null)
                        {
                            obj.Error = true;
                            obj.Msg = string.Format("Topic with id [{0}] has been deleted!", obj.Id);
                            return View(obj);
                        }
                    }
                    dbm.Title = obj.Title;
                    dbm.ContentX = obj.ContentX;

                    dbm.Slug = string.IsNullOrWhiteSpace(obj.Slug) ? obj.Title.CHSToPinyin("-") : obj.Slug.CHSToPinyin("-");
                    dbm.Description = dbm.ContentX;
                    //save to db
                    var msg = Topic.ValidateSlug(dbm.Slug, db);
                    dbm.Slug = msg.Context["Slug"].ToString();

                    db.Attach(CurrentUser);
                    dbm.User = CurrentUser;
                    db.SaveChanges();

                    //tags
                    if (obj.TagID != null && obj.TagID.Count > 0)
                    {
                        dbm.Tags.Clear();

                        obj.TagID.ForEach(x => dbm.Tags.Attach(db.Tags.Where(y=>y.Id.Equals(x))));
                    }
                    //groups
                    if (obj.GroupID != null && obj.GroupID.Count > 0)
                    {
                        dbm.Groups.Clear();
                        
                        obj.GroupID.ForEach(
                            x =>dbm.Groups.Attach(db.Groups.Where(y=>y.Id.Equals(x)))
                        );

                    }
                    //保存关系表
                    db.SaveChanges();

                    //update tags and groups
                    obj.Groups = dbm.Groups.ToList();
                    obj.Tags = dbm.Tags.ToList();
                }//using      
                
         
                    

                
            };
            return View(obj);
        }
        private ActionResult NewTopic() {
            var vm = CreateViewData<VMCampTopicShow>();
            vm.Id = Guid.Empty;
            return View(vm);
        }
    }
}
