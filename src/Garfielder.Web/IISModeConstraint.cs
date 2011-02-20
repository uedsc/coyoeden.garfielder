using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace Garfielder.Web
{
	public class ClassicModeConstraint : IRouteConstraint
	{
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return !HttpRuntime.UsingIntegratedPipeline;
		}
	}

	public class IntegratedModeConstraint : IRouteConstraint
	{
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return HttpRuntime.UsingIntegratedPipeline;
		}
	}

}
