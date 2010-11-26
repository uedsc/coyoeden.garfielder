using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.Models;
using Garfielder.ViewModels;
namespace Garfielder.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Current user name
        /// </summary>
        public string CurrentUserName {
            get {
               return (HttpContext.User == null) ? null : HttpContext.User.Identity.Name;
            }
        }
        /// <summary>
        /// Current user
        /// </summary>
        public User CurrentUser {
            get {
                if (string.IsNullOrEmpty(CurrentUserName)) return null;
                var user = HttpContext.Items["CurrentUser"] as User;
                if (user != null) return user;
                //Get user from data base
                using (var db = new GarfielderEntities()) {
                    db.CommandTimeout = 0;
                    user = db.Users.SingleOrDefault(x => x.Name.Equals(CurrentUserName));
                    if (null != user) {
                        HttpContext.Items["CurrentUser"] = user;
                    };
                };//using
                return user;
            }//get
        }
        /// <summary>
        /// Is current user authenticated
        /// </summary>
        public bool IsUserAuthenticated {
            get {
                if (CurrentUser == null || HttpContext.User.Identity.IsAuthenticated != true) return false;
                return true;
            }
        }
        /// <summary>
        /// User ip address
        /// </summary>
        public string UserIP {
            get {
                return HttpContext.Request.UserHostAddress;
            }
        }

        public T CreateViewData<T>() where T : VMBase, new()
        {
            T viewData = new T
            {
                SiteTitle ="I love garfield!!",
                MetaKeywords ="CMS,Blog,ASPNET,MVC",
                MetaDescription ="Test test",
                IsUserAuthenticated = IsUserAuthenticated,
                CurrentUser = CurrentUser
            };

            return viewData;
        }
    }
}
