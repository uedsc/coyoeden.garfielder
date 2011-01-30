using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;
using Garfielder.Models;
using Garfielder.Core.Infrastructure;

namespace Garfielder.Controllers
{
    public partial class CampController: BaseController
    {
        public ActionResult ListTopic(bool published=false,string term="") {
            var vm = CreateViewData<VMCampTopicList>();
            term = (term ?? "").ToLower();
            vm.Published = published;
            vm.Term = term;
            //get group data
            vm.GroupList = Group.ListAllData();
            vm.TopicList=new List<VMCampTopicEdit>();
            //get topic data
            Topic.ListAll(published, term, items => items.ForEach(x => vm.TopicList.Add(new VMCampTopicEdit
                                                                                            {
                                                                                                Id = x.Id,
                                                                                                Title = x.Title,
                                                                                                UserName = x.User.Name,
                                                                                                Grade = x.Grade,
                                                                                                CreateAt = x.CreatedAt,
                                                                                                CntComment = x.Comments.Count,
                                                                                                GroupsTxt = String.Join(",", x.Groups.ToList().ConvertAll(y => y.Name)),
                                                                                                TagsTxt = String.Join(",", x.Tags.ToList().ConvertAll(z => z.Name)),
                                                                                                Starred = TopicElected.Exists(x.Id)
                                                                                            })));
            return View(vm);
        }
        [HttpPost]
        public ActionResult ListTopic(VMCampTopicListFilter filter)
        {
            var vm = CreateViewData<VMCampTopicList>();
            vm.Term = filter.term ?? "";
            vm.Published = filter.published;
            vm.GroupList = Group.ListAllData();
            switch (filter.Action)
            {
                case "trash":
                    var msg = Topic.DeleteByID(filter.ObjIDList.ToArray());
                    vm.Error = msg.Error;
                    vm.Msg = msg.Body;
                    return ListTopic(vm.Published, vm.Term);
                    break;
                case "pub":
                    //TODO:batch publish
                    vm.Error = true;
                    vm.Msg = "Not implemented";
                    return ListTopic(vm.Published, vm.Term);
                    break;
                case "unpub":
                    //TODO:batch unpublish
                    vm.Error = true;
                    vm.Msg = "Not implemented!";
                    return ListTopic(vm.Published, vm.Term);
                    break;
                case "-1":
                    //no action,just render the default list
                    return ListTopic(vm.Published, vm.Term);
                    break;
            }
            return View(vm);
        }
        /// <summary>
        /// list attachements for specified topic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ListTopicFile(Guid id)
        {
            var vm = CreateViewData<VMXFileList>();
            var vm1 = Topic.ListFileData(id);
            vm.FileList=vm1.FileList;
            vm.RefTopic = vm1.RefTopic;
            
            return View(vm);
        }
        /// <summary>
        /// delete a specified topic via ajax
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteTopic(Guid id)
        {
            var msg = new Msg();
            if(id.Equals(Guid.Empty))
            {
                msg.Error = true;
                msg.Body = string.Format("Invalid parameter id [{0}]", id);
            }else
            {
                msg = Topic.DeleteByID(id);
            }
            return Json(msg);
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
                        dbm.CreatedAt=dbm.PublishedAt = obj.CreateAt;
                        
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
                    dbm.ModifiedAt = DateTime.Now;

                    db.Attach(CurrentUser);
                    dbm.User = CurrentUser;
                    db.SaveChanges();

                    //tags
                    if (obj.TagID != null && obj.TagID.Count > 0)
                    {
                        dbm.Tags.Clear();

                        obj.TagID.ForEach(x => dbm.Tags.Add(db.Tags.Single(y=>y.Id.Equals(x))));
                    }
                    //groups
                    if (obj.GroupID != null && obj.GroupID.Count > 0)
                    {
                        dbm.Groups.Clear();
                        
                        obj.GroupID.ForEach(
                            x => dbm.Groups.Add(db.Groups.Single(y => y.Id.Equals(x)))
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
        }//edit topic
        /// <summary>
        /// Detach an attachment
        /// </summary>
        /// <param name="tid">topic id</param>
        /// <param name="fid">file id</param>
        /// <returns></returns>
        public JsonResult DetachFile(Guid tid,Guid fid)
        {
            var msg = new Msg();
            msg = Topic.DetachFiles(tid, fid);
            return Json(msg);

        }//detach file
        /// <summary>
        /// star a topic
        /// </summary>
        /// <param name="id"></param>
        /// <param name="star"></param>
        /// <returns></returns>
        public JsonResult StarTopic(Guid id,bool star)
        {
            var msg = default(Msg);
            msg = star ? Topic.Star(id, CurrentUserName) : TopicElected.Delete(id);
            return Json(msg);
        }

        #region helper methods
        private ActionResult NewTopic() {
            var vm = CreateViewData<VMCampTopicShow>();
            vm.Id = Guid.Empty;
            return View(vm);
        }
        #endregion
    }
}
