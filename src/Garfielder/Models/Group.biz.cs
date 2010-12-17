using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.Core.Infrastructure;
using Garfielder.ViewModels;

namespace Garfielder.Models
{
    /// <summary>
    /// biz logic for Group.TODO:via repository pattern
    /// </summary>
    public partial class Group
    {
        public int CntTopic { get; set; }

        public static bool ValidateName(string name) {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var valid = true;
            using (var db = new GarfielderEntities()) {
                valid = db.Groups.Count(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) == 0;
                return valid;
            }   
        }
        public static bool ValidateSlug(string slug) {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            var valid = true;
            using (var db = new GarfielderEntities()) {
                valid = db.Groups.Count(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)) == 0;
                return valid;
            }
        }
        /// <summary>
        /// list all group data.TODO:Don't use Group!You should define an interface type such as IGroupData
        /// </summary>
        /// <returns></returns>
        public static List<Group> ListAll() {
            using (var db = new GarfielderEntities())
            {
                var items = db.Groups.ToList();
                items.ForEach(x=>x.CntTopic=x.Topics.Count);
                return items;
            }//using
            
        }
        /// <summary>
        /// list all group data.TODO:Don't use Group!You should define an interface type such as IGroupData
        /// </summary>
        /// <returns></returns>
        public static List<VMGroupEdit> ListAllData()
        {
            using (var db = new GarfielderEntities())
            {
                var items = db.Groups.ToList();
                var r = new List<VMGroupEdit>();
                items.ForEach(x =>
                {
                    r.Add(new VMGroupEdit
                    {
                        Name = x.Name,
                        Slug = x.Slug,
                        Id = x.Id,
                        Level = x.Level,
                        Description = x.Description,
                        ParentID = x.ParentID,
                        ParentName = x.Parent == null ? "" : x.Parent.Name,
                        CntTopic = x.Topics.Count
                    });
                });
                return r;
            }//using

        }
    }
    
}