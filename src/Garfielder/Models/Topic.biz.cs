using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.ViewModels;
using Garfielder.Core.Infrastructure;

namespace Garfielder.Models
{
    public partial class Topic
    {
        /// <summary>
        /// validate a slug
        /// </summary>
        /// <param name="slug">slug</param>
        /// <param name="db">GarfielderEntities instance</param>
        /// <returns></returns>
        public static Msg ValidateSlug(string slug,GarfielderEntities db=null)
        {
            var r = new Msg();
            if (string.IsNullOrWhiteSpace(slug))
            {
                r.Error = true;
                r.Body = "Slug can't be empty!";
                r.Context["Slug"] = Utils.RandomStr(8);
                return r;
            }
            var isNewConnection = db == null;
            db = db ?? new GarfielderEntities();
            r.Context["Slug"] = slug;
            try
            {
                var topic = db.Topics.SingleOrDefault(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
                if (topic == null) return r;
                r.Error = true;
                r.Body = string.Format("Slug [{0}] has been used by another topic!", slug);
                r.Context["Slug"] = string.Format("{0}{1}", slug, Utils.RandomStr(4));
            }
            catch (Exception ex)
            {

                r.Error = true;
                r.Body = string.Format("Error:{0}", ex.Message);
            }finally
            {
                if (isNewConnection)
                    db.Dispose();
            }
            return r;

        }
        
        public static VMCampTopicShow  GetTopic(Guid id) {
            using (var db = new GarfielderEntities())
            {
                
                var dbm= db.Topics.Single(x => x.Id == id);
                if (dbm == null) return null;
                var vm = new VMCampTopicShow();
                vm.Id = dbm.Id;
                vm.Title = dbm.Title;
                vm.Slug = dbm.Slug;
                vm.Url = dbm.Url;
                vm.Grade = dbm.Grade;
                vm.Description = dbm.Description;
                vm.ContentX = dbm.ContentX;

                vm.Groups = dbm.Groups.ToList();

                vm.Tags = dbm.Tags.ToList();

                return vm; 

            };
        }
        public static Msg DeleteByID(params Guid[] id)
        {
            var r = new Msg();
            if(id==null||id.Length==0)
            {
                r.Error = true;
                r.Body = "No selected topics to be deleted!";
                return r;
            }
            using (var db=new GarfielderEntities())
            {
                try
                {
                    var items = db.Topics.Where(x => id.Contains(x.Id)).ToList();
                    items.ForEach(obj=> {
                        obj.Tags.Clear();
                        obj.Groups.Clear();
                        db.Topics.DeleteObject(obj);
                    });
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    r.Error = true;
                    r.Body = ex.Message;
                }
            }//using
            return r;
        }
        /// <summary>
        /// topic statistics
        /// </summary>
        /// <returns></returns>
        public static dynamic TopicStat()
        {
            using(var db=new GarfielderEntities())
            {
                var sdate = DateTime.Today;
                var edate = sdate.AddDays(1);
                return new{
                    CntTopic = db.Topics.Count(),
                    CntTopicToday = db.Topics.Count(x => x.CreatedAt>=sdate||x.CreatedAt<edate),
                    CntGroup = Group.ListAll().Count,
                    CntComment = db.TopicComments.Count(),
                    CntTag = Tag.ListAll().Count
                };


            }
            
        }//TopicStat
        /// <summary>
        /// check the specified slug whether exists.if it exists we provide a new one 
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static string AutoSlug(string slug,GarfielderEntities db=null)
        {
            if (string.IsNullOrWhiteSpace(slug)) return Utils.RandomStr(8);
            slug = slug.Trim().CHSToPinyin("-").ToLower().RemoveWhitespace();
            //slug length limit
            slug = slug.Length > 45 ? slug.Substring(0, 45) : slug;
            var needDispose = false;
            try
            {
                if (db == null)
                {
                    db = new GarfielderEntities();
                    needDispose = true;
                }
                    

                var obj = db.Topics.SingleOrDefault(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
                if (null != obj)
                {
                    slug = string.Format("{0}{1}", slug, Utils.RandomStr(5));
                }
            }
            catch (Exception ex)
            {
                //TODO:log the exception
                slug=string.Format("{0}{1}", slug, Utils.RandomStr(5));
            }finally
            {
                if(needDispose)
                    db.Dispose();
            }//try

            return slug;
        }//autoslug
        /// <summary>
        /// list topic files for specified topic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static VMXFileList ListFileData(Guid id)
        {
            var r = new VMXFileList();
            r.FileList=new List<VMXFileEdit>();
            if (id == Guid.Empty) return r;
            using(var db=new GarfielderEntities())
            {
                var obj = db.Topics.SingleOrDefault(x => x.Id.Equals(id));
                if (obj == null) return r;
                obj.XFiles.ToList().ForEach(x => r.FileList.Add(new VMXFileEdit
                                                           {
                                                               Id = x.Id,
                                                               Title = x.Title,
                                                               Extension = x.Extension,
                                                               Description = x.Description,
                                                               UserName = x.User.Name
                                                           }));
                r.RefTopic = obj;
            }//using
            return r;
        }
    }
}