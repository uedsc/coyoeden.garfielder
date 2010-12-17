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
        /// 所有组
        /// </summary>
        public List<Group> GroupsAll { get; set; }
        /// <summary>
        /// 常用组
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
        /// 所有tag
        /// </summary>
        public List<Tag> TagsAll { get; set; }
        /// <summary>
        /// 常用tag
        /// </summary>
        public List<Tag> TagsAllHot {
            get
            {
                if (TagsAll == null || TagsAll.Count == 0) return null;
                return TagsAll.OrderByDescending(x => x.CntTopic).ToList();
            }
        }
    }
}