using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.Core.Infrastructure;
using Garfielder.ViewModels;
using System.Data;
using System.Data.Objects;

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

            var valid =ListAll().Count(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) == 0;
            return valid;
        }
        public static bool ValidateSlug(string slug) {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            var valid =ListAll().Count(x=>x.Slug.Equals(slug,StringComparison.OrdinalIgnoreCase))==0;

            return valid;
        }

        /// <summary>
        /// list all group data.TODO:Don't use Group!You should define an interface type such as IGroupData
        /// </summary>
        /// <returns></returns>
        public static List<Group> ListAll() {
            using (var db = new GarfielderEntities())
            {
                if ( _Groups== null)
				{
					lock (_SyncRoot)
					{
                        if (_Groups == null)
						{
                            _Groups = db.Groups.ToList();
                            _Groups.ForEach(x => x.CntTopic = x.Topics.Count);
						}
					}
				}

                return _Groups;
			}//using
            
        }
        /// <summary>
        /// list all group data.TODO:Don't use VMGroupEdit!You should define an interface type such as IGroupData
        /// </summary>
        /// <returns></returns>
        public static List<VMGroupEdit> ListAllData()
        {
            var items = ListAll();
            var r = new List<VMGroupEdit>();
            items.ForEach(x => r.Add(
                    new VMGroupEdit
                    {
                        Name = x.Name,
                        Slug = x.Slug,
                        Id = x.Id,
                        Level = x.Level,
                        Description = x.Description,
                        ParentID = x.ParentID,
                        ParentName = x.Parent == null ? "" : x.Parent.Name,
                        CntTopic = x.Topics.Count
                    }
                )
            );
            return r;

        }
        /// <summary>
        /// clear cache
        /// </summary>
        public static void ClearCache()
        {
            _Groups = null;
        }

        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dbToAttach"></param>
        /// <returns></returns>
        public static Group Get(Guid id,GarfielderEntities dbToAttach=null)
        {
            var obj=ListAll().SingleOrDefault(x => x.Id == id);
            if(dbToAttach!=null&&null!=obj)
            {
                dbToAttach.Groups.Attach(obj);
            }
                
            return obj;
        }

        #region private properties
        private static object _SyncRoot = new object();
        private static List<Group> _Groups;

        #endregion
    }
    
}