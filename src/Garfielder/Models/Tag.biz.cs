using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Garfielder.Core.Infrastructure;
using Garfielder.ViewModels;
using System.Data.Objects;
namespace Garfielder.Models
{
    public partial class Tag
    {

        public int CntTopic { get; set; }

        public static bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var valid = ListAll().Count(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) == 0;
            return valid;
        }
        public static bool ValidateSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            var valid = ListAll().Count(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)) == 0;
            return valid;
        }
        /// <summary>
        /// add a simple tag 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public static VMTagEdit QuickAddTag(string name,string slug=null,string user="Sys") {
            var retVal = new VMTagEdit();
            if (string.IsNullOrWhiteSpace(name)) {
                retVal.Error = true;
                retVal.Msg = "Tag name can't be blank";
                return retVal;
            };

            retVal.Name =name;
            retVal.Slug = string.IsNullOrWhiteSpace(slug) ? name.CHSToPinyin("-") : slug.CHSToPinyin("-");
            retVal.Slug = retVal.Slug.ToLower();

            using (var db = new GarfielderEntities()) {
                var dbm = db.Tags.SingleOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (dbm != null)
                {
                    retVal.Id = dbm.Id;
                }
                else {
                    retVal.Id = Guid.NewGuid();
                    //add a new tag
                    dbm = new Tag();
                    dbm.Id = retVal.Id;
                    dbm.Name = retVal.Name;
                    dbm.Slug = CheckSlug(db,retVal.Slug);
                    dbm.CreatedAt = DateTime.Now;
                    //TODO:
                    dbm.CreatedBy =user;
                    db.AddToTags(dbm);
                    //persist
                    db.CommandTimeout = 0;
                    db.SaveChanges();
                    ClearCache();
                };

            };//using
            return retVal;
        }
        /// <summary>
        /// save a tag object.Update if exists
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static dynamic Save(VMTagEdit obj) {
            var dbm = default(Tag);
            obj.Slug = string.IsNullOrEmpty(obj.Slug) ? obj.Name.CHSToPinyin("-").ToLower() : obj.Slug.ToLower();
            var msg = new Msg();
            //validate name
            if (!ValidateName(obj.Name)) {
                msg.Error = true;
                msg.Body = string.Format("Name [{0}] exists!Please choose another one!",obj.Name);
                return msg;
            };
            using (var db = new GarfielderEntities())
            {
                if (obj.IsNew)//add new
                {
                    obj.Id = Guid.NewGuid();
                    dbm = new Tag();
                    dbm.Id = obj.Id;
                    dbm.CreatedAt = DateTime.Now;
                    //TODO
                    dbm.CreatedBy = "Sys";
                    db.AddToTags(dbm);
                }
                else
                { //update
                    dbm = db.Tags.SingleOrDefault(x => x.Id.Equals(obj.Id));
                    if (dbm == null)
                    { //has been deleted!
                        msg.Error = true;
                        msg.Body = string.Format("Obj {0} has been deleted!", obj.Id);
                        return msg;
                    }
                }//if



                dbm.Name = obj.Name;
                dbm.Slug = Tag.CheckSlug(db, obj.Slug);
                db.CommandTimeout = 0;
                db.SaveChanges();
                ClearCache();
            };//using
            return obj;
        }
        /// <summary>
        /// check the specified slug whether exists.if it exists we provide a new one 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public static string CheckSlug(GarfielderEntities db, string slug) {
            var tag = db.Tags.SingleOrDefault(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
            if (null == tag) {
                return slug;
            }
            return string.Format("{0}{1}",slug,Utils.RandomStr(5));
        }
        /// <summary>
        /// list all tags
        /// </summary>
        /// <returns></returns>
        public static List<Tag> ListAll()
        {
            using (var db = new GarfielderEntities())
            {
                if (_Tags == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Tags == null)
                        {
                            _Tags = db.Tags.ToList();
                            _Tags.ForEach(x => x.CntTopic = x.Topics.Count);
                        }
                    }
                }

                return _Tags;
            }//using

        }
        /// <summary>
        /// list all tag data.TODO:Don't use VMTagEdit!You should define an interface type such as IGroupData
        /// </summary>
        /// <returns></returns>
        public static List<VMTagEdit> ListAllData()
        {
            var items = ListAll().OrderByDescending(x => x.CreatedAt).ToList();
            var r = new List<VMTagEdit>();
            items.ForEach(x => r.Add(
                new VMTagEdit
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    Id = x.Id,
                    CntTopic = x.Topics.Count
                }));
            return r;

        }
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dbToAttach"></param>
        /// <returns></returns>
        public static Tag Get(Guid id, GarfielderEntities dbToAttach = null)
        {
            var obj = ListAll().SingleOrDefault(x => x.Id == id);
            if (dbToAttach != null && null != obj)
            {
                dbToAttach.Tags.Attach(obj);
            }
                
            return obj;
        }
        /// <summary>
        /// delete by specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Msg DeleteByID(params Guid[] id)
        {
            var r = new Msg();
            if (id == null || id.Length == 0)
            {
                r.Error = true;
                r.Body = "No selected topics to be deleted!";
                return r;
            }
            using (var db = new GarfielderEntities())
            {
                try
                {
                    var items = db.Tags.Where(x => id.Contains(x.Id)).ToList();
                    items.ForEach(obj =>
                    {
                        obj.Topics.Clear();
                        db.Tags.DeleteObject(obj);
                    });
                    db.SaveChanges();
                    ClearCache();
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
        /// clear cache
        /// </summary>
        public static void ClearCache()
        {
            _Tags = null;
        }

        #region private properties
        private static object _SyncRoot = new object();
        private static List<Tag> _Tags;
        #endregion

    }
}