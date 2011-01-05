using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.Models;

namespace Garfielder.ViewModels
{
    public class VMCampTopicEdit:VMBase
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string ContentX { get; set; }
        public double Grade { get; set; }
        public string UserName { get; set; }
        public DateTime CreateAt { get; set; }
        public int CntComment { get; set; }
        public string GroupsTxt { get; set; }
        public string TagsTxt { get; set; }

        /*新建主题时需要的属性*/
        public List<Guid> GroupID { get; set; }
        public List<Guid> TagID { get; set; }

        /// <summary>
        /// whether is a new topic 
        /// </summary>
        public bool IsNew
        {
           get { return Id == Guid.Empty;}
        }
    }
}