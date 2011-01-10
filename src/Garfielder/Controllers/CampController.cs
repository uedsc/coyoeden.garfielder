using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.Core.Infrastructure;
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
        /// <summary>
        /// generate a slug according to the specified text
        /// </summary>
        /// <param name="src">slug source text</param>
        /// <param name="type">slug type:possible values are 'topic','tag','group'</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AutoSlug(string src,string type="topic")
        {
            switch (type)
            {
                case "topic":
                    return Json(new {slug = Topic.AutoSlug(src)},JsonRequestBehavior.AllowGet);
                    break;
                default:
                    return Json(new { slug = Utils.RandomStr(8) }, JsonRequestBehavior.AllowGet);
                    break;
            }//switch
        }//AutoSlug

    }
}
