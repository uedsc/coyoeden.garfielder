using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garfielder.Controllers
{
    public class TopicController : BaseController
    {
        //
        // GET: /Album/

        public string Index()
        {
            return "TODO：显示置顶的主题";
        }
        [ActionName("Group")]
        public string List(string id) {
            return "TODO:显示指定组的主题";        
        }
        public string Show(int id) {
            return "TODO：显示指定ID的主题";
        }
    }
}
