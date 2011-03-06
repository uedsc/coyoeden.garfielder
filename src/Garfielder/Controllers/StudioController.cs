using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Garfielder.Models;
using Garfielder.ViewModels;

namespace Garfielder.Controllers
{
    public class StudioController : Controller
    {
		public ActionResult Index()
		{
			var topics = new List<VMTopic>();
			var topics1 = Topic.ListAllStarred(items => items.ForEach(x => topics.Add(new VMTopic
			{
				Id = x.Id,
				Title = x.Title,
				Slug = x.Slug,
				Desc = x.Description,
				Logo = x.Icon == null ? Url.Content("~/assets/img/default.jpg") : x.Icon.Url(ImageFlags.S160X100),
				DateCreated = x.CreatedAt
			})));
			var vm = new VMHome
			{
				TopicNum = topics.Count,
				Topics = topics.OrderBy(x=>x.Slug).ToList()
			};

			//TODO:fake Login
			fakeLogin();
			return View(vm);
		}

		private void fakeLogin()
		{
			using (var db = new GarfielderEntities())
			{
				var user = db.Users.First();
				HttpContext.Items["CurrentUser"] = user;
				FormsAuthentication.SetAuthCookie(user.Name, false);
			}
		}

    }
}
