using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.Models;

namespace Garfielder.ViewModels
{
    public class VMCampTopicShow:VMCampTopicEdit
    {
        public List<Group> Groups { get; set; }

        /// <summary>
        /// 所有组-只读
        /// </summary>
        public List<Group> GroupsAll
        {
            get { return Group.ListAll(); }
        }
        /// <summary>
        /// 所有非顶级组-只读
        /// </summary>
        public List<Group> GroupsAllGTELevel1
        {
            get
            {
                if (GroupsAll == null || GroupsAll.Count == 0) return null;
                return GroupsAll.Where(x => x.Level > 0).ToList();
            }
        }
        /// <summary>
        /// 常用组-只读
        /// </summary>
        public List<Group> GroupsAllHot
        {
            get
            {
                if (GroupsAll == null || GroupsAll.Count == 0) return null;
                return GroupsAll.OrderByDescending(x => x.CntTopic).ToList();
            }
        }
        /// <summary>
        /// 常用的非顶级组-只读
        /// </summary>
        public List<Group> GroupsAllHotGTELevel1
        {
            get
            {
                if (GroupsAllGTELevel1 == null || GroupsAllGTELevel1.Count == 0) return null;
                return GroupsAllGTELevel1.OrderByDescending(x => x.CntTopic).ToList();
            }
        }
        /// <summary>
        /// 指定的组是否属于该主题的组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsSelectedGroup(Guid id) {
            if (Groups == null || Groups.Count == 0) return false;
            return Groups.Any(x=>x.Id==id);
        }

        public List<Tag> Tags { get; set; }
        /// <summary>
        /// 所有tag-只读
        /// </summary>
        public List<Tag> TagsAll
        {
            get { return Tag.ListAll(); }
        }
        /// <summary>
        /// 常用tag-只读
        /// </summary>
        public List<Tag> TagsAllHot {
            get
            {
                if (TagsAll == null || TagsAll.Count == 0) return null;
                return TagsAll.OrderByDescending(x => x.CntTopic).ToList();
            }
        }
		/// <summary>
		/// attachments
		/// </summary>
		public List<XFile> Attachments { get; set; }
    }
}