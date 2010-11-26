using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garfielder.Extension
{
    public static class UrlHelperX
    {
        public static string Home(this UrlHelper url) {
            return url.Content("~/");
        }
        public static string JS(this UrlHelper url, string jsName) {
            return url.Content(string.Format("~/assets/js/{0}.js",jsName));
        }
        public static string Img(this UrlHelper url, string img) {
            return url.Content(string.Format("~/assets/img/{0}",img));
        }
        public static string ImgDefault(this UrlHelper url) {
            return Img(url, "default.jpg");
        }
        public static string CSS(this UrlHelper url, string cssName) {
            return url.Content(string.Format("~/assets/css/{0}.css",cssName));
        }
    }
}