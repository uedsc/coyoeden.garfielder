using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garfielder.Web;

namespace Garfielder.Extension
{
    public static class UrlHelperX
    {
        public static string Home(this UrlHelper url,bool absolute=false) {
            if(!absolute)
                return url.Content("~/");

            return Utils.AbsoluteWebRoot.ToString();
        }
        public static string JS(this UrlHelper url, string jsName,string version="") {
            version = string.IsNullOrEmpty(version)?"":string.Format("?v={0}",version);
            return url.Content(string.Format("~/assets/js/{0}.js{1}",jsName,version));
        }
        public static string Img(this UrlHelper url, string img) {
            return url.Content(string.Format("~/assets/img/{0}",img));
        }
        public static string ImgDefault(this UrlHelper url) {
            return Img(url, "default.jpg");
        }
        public static string CSS(this UrlHelper url, string cssName, string version = "")
        {
            version = string.IsNullOrEmpty(version) ? "" : string.Format("?v={0}", version);
            return url.Content(string.Format("~/assets/css/{0}.css{1}",cssName,version));
        }
    }
}