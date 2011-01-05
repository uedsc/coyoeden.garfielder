using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMCameTopicListFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public string Action { get; set; }
        public string Date { get; set; }
        public List<Guid> TopicIDList { get; set; }
        public string GroupID { get; set; }

        public string term { get; set; }
        public string published { get; set; }
    }
}