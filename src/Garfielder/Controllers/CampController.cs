using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;
using Garfielder.Models;

namespace Garfielder.Controllers
{
    public partial class CampController : BaseController
    {
        //
        // GET: /Camp/
        [OutputCache(Duration = 300)]
        public ActionResult Index(string timestamp="")
        {
            var vm = CreateViewData<VMCampHome>();
            //site stat
            var s = Topic.TopicStat();
            vm.CntGroup = s.CntGroup;
            vm.CntComment = s.CntComment;
            vm.CntTag = s.CntTag;
            vm.CntTopic = s.CntTopic;
            vm.CntTopicToday = s.CntTopicToday;

            vm.Sort();
            
            return View(vm);
        }

    }
}
