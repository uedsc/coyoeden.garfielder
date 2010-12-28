using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.Core.Infrastructure;
using Garfielder.ViewModels;
namespace Garfielder.Models
{
    public partial class Tag
    {

        public int CntTopic { get; set; }

        public static bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var valid = true;
            using (var db = new GarfielderEntities())
            {
                valid = db.Tags.Count(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) == 0;
                return valid;
            }
        }
        public static bool ValidateSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            var valid = true;
            using (var db = new GarfielderEntities())
            {
                valid = db.Tags.Count(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)) == 0;
                return valid;
            }
        }
        /// <summary>
        /// add a tag
        /// </summary>
        /// <param name="name"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public static VMTagEdit AddTag(string name,string slug=null,string user="Sys") {
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
                    //persist
                    db.CommandTimeout = 0;
                    db.AddToTags(dbm);
                    db.SaveChanges();
                };
            };//using
            return retVal;
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
                dbToAttach.Attach(obj);
            }
                
            return obj;
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