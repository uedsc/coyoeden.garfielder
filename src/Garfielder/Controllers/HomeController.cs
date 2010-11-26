﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.ViewModels;
using Garfielder.Models;
using System.Web.Security;

namespace Garfielder.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var topics = new List<VMTopic>();
            for (int i = 0; i < 12; i++) {
                topics.Add(new VMTopic { 
                    Id=i+1,
                    Title="Topic title "+i,
                    Desc="",
                    Icon = "/assets/img/default.jpg",
                    DateCreated=DateTime.Now
                });
            };
            var vm = new VMHome { 
                TopicNum=topics.Count,
                Topics=topics
            };

            //TODO:fake Login
            fakeLogin();

            return RedirectToAction("Index", "Camp");
            //return View(vm);
        }

        private void fakeLogin() {
            using (var db = new GarfielderEntities()) {
                var user = db.Users.First();
                HttpContext.Items["CurrentUser"] = user;
                FormsAuthentication.SetAuthCookie(user.Name, false);
            }
        }

    }
}
