using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Garfielder.Models;
using System.Web.Script.Serialization;


namespace Garfielder.ViewModels
{
    /// <summary>
    /// ViewModel base class:Store the data accessed by all or many pages
    /// </summary>
    [Bind(Exclude = "PageFlag,IsUserAuthenticated,SiteTitle,MetaKeywords,MetaDescription,CurrentUser")]
    public abstract class VMBase
    {
        /// <summary>
        /// flag for the current page.Can be used as body tag's css class.
        /// </summary>
        [ScaffoldColumn(false)]
        [ScriptIgnore]
        public string PageFlag { get; set; }
        /// <summary>
        /// Current logined user
        /// </summary>
        [ScriptIgnore]
        public User CurrentUser { get; set; }
        [ScaffoldColumn(false)]
        [ScriptIgnore]
        public bool IsUserAuthenticated { get; set; }

        #region TODO:Settings resolved from appsetting
        /// <summary>
        /// Current papge title
        /// </summary>
        [ScaffoldColumn(false)]
        [ScriptIgnore]
        public string SiteTitle { get; set; }
        [ScaffoldColumn(false)]
        [ScriptIgnore]
        public string MetaKeywords { get; set; }
        [ScaffoldColumn(false)]
        [ScriptIgnore]
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