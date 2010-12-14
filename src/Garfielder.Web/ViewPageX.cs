using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Garfielder.Web
{
    public partial class ViewPageX:ViewPage
    {
        /// <summary>
        /// Given a boolean factor,if it is true,use the trueStr,otherwise use the falseStr
        /// </summary>
        /// <param name="fact"></param>
        /// <param name="trueStr"></param>
        /// <param name="falseStr"></param>
        /// <returns></returns>
        public string Str(bool fact, string trueStr, string falseStr = "")
        {
            if (fact)
            {
                return trueStr;
            };
            return falseStr;
        }
    }
    public partial class ViewPageX<TModel> : ViewPage<TModel> {
        /// <summary>
        /// Given a boolean factor,if it is true,use the trueStr,otherwise use the falseStr
        /// </summary>
        /// <param name="fact"></param>
        /// <param name="trueStr"></param>
        /// <param name="falseStr"></param>
        /// <returns></returns>
        public string Str(bool fact, string trueStr, string falseStr = "")
        {
            if (fact)
            {
                return trueStr;
            };
            return falseStr;
        }
    }   
}
