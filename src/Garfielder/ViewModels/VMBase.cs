using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.Models;

namespace Garfielder.ViewModels
{
    /// <summary>
    /// ViewModel base class:Store the data accessed by all or many pages
    /// </summary>
    public abstract class VMBase
    {

        /// <summary>
        /// flag for the current page.Can be used as body tag's css class.
        /// </summary>
        public string PageFlag { get; set; }
        /// <summary>
        /// Current logined user
        /// </summary>
        public User CurrentUser { get; set; }

        public bool IsUserAuthenticated { get; set; }

        #region TODO:Settings resolved from appsetting
        /// <summary>
        /// Current papge title
        /// </summary>
        public string SiteTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        #endregion

        public string AssertStr(Func<bool> fun, string trueStr, string falseStr="") {
            if (fun()) {
                return trueStr;
            };
            return falseStr;
        }
    }
}