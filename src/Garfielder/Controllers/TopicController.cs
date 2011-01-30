using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.Models;

namespace Garfielder.Controllers
{
    public class TopicController : BaseController
    {
        //
        // GET: /Album/

        public void Index()
        {
            RedirectToAction("Index","Home");
        }
        [ActionName("Group")]
        public string List(string id) {
            return "TODO:显示指定组的主题";        
        }
        /// <summary>
        /// View a topic
        /// </summary>
        /// <param name="id">slug</param>
        /// <returns></returns>
        public ViewResult View(string id)
        {
            var vm = Topic.GetTopic(id);
            vm.UrlGoBack = Request.UrlReferrer==null?Url.Action("Index","Home"):Request.UrlReferrer.ToString();
            return View(vm);
        }
    }
}
