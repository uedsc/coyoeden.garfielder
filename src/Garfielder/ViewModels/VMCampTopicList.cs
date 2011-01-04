using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    /// <summary>
    /// 后台主题首页
    /// </summary>
    public class VMCampTopicList:VMBase
    {
        public List<VMCampTopicEdit> TopicList { get; set; }
        public List<VMGroupEdit> GroupList { get; set; }
    }
}