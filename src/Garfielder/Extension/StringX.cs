using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.Extension
{
    public static class StringX
    {
        public static int Index(this string str,string[] strList) {
            return strList.ToList().IndexOf(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">target string</param>
        /// <param name="listStr">string like 'xx,yy,zz'</param>
        /// <param name="split">separator for the listStr</param>
        /// <returns></returns>
        public static int Index(this string str, string listStr, string split = ",") {
            var list = listStr.Split(split.ToCharArray());
            return Index(str, list);
        }
        public static bool In(this string str, string[] strList)
        {
            return Index(str,strList)>-1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">target string</param>
        /// <param name="listStr">string like 'xx,yy,zz'</param>
        /// <param name="split">separator for the listStr</param>
        /// <returns></returns>
        public static bool In(this string str, string listStr, string split = ",")
        {
            return Index(str, listStr, split) > -1;
        }
        
    }
}