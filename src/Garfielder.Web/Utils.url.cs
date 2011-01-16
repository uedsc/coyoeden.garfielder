using System;
using System.Web;
using Garfielder.Core.Infrastructure;

namespace Garfielder.Web
{
    public static  partial class Utils
    {
        private static string _RelativeWebRoot;
        /// <summary>
        /// Gets the relative root of the website.
        /// </summary>
        /// <value>A string that ends with a '/'.</value>
        public static string RelativeWebRoot
        {
            get
            {
                if (_RelativeWebRoot == null)
                {
                    var virtualPath = AppSettingsHelper.Instance.GetString("App.VirtualPath", "~/");
                    _RelativeWebRoot = VirtualPathUtility.ToAbsolute(virtualPath);
                }

                return _RelativeWebRoot;
            }
        }

        //private static Uri _AbsoluteWebRoot;

        /// <summary>
        /// Gets the absolute root of the website.
        /// </summary>
        /// <value>A string that ends with a '/'.</value>
        public static Uri AbsoluteWebRoot
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context == null)
                    throw new System.Net.WebException("The current HttpContext is null");

                if (context.Items["absoluteurl"] == null)
                    context.Items["absoluteurl"] = new Uri(context.Request.Url.GetLeftPart(UriPartial.Authority) + RelativeWebRoot);

                return context.Items["absoluteurl"] as Uri;
            }
        }

    }
}
