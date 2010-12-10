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
        /// <summary>
        /// has error message
        /// </summary>
        [ScaffoldColumn(false)]
        public bool Error { get; set; }
        /// <summary>
        /// message responsed by server side
        /// </summary>
        [ScaffoldColumn(false)]
        public string Msg { get; set; }

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
        /// <summary>
        /// Given a function fun,if it returns true,use the trueStr,otherwise use the falseStr
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="trueStr"></param>
        /// <param name="falseStr"></param>
        /// <returns></returns>
        public string AssertStr(Func<bool> fun, string trueStr, string falseStr="") {
            if (fun()) {
                return trueStr;
            };
            return falseStr;
        }
    }
}