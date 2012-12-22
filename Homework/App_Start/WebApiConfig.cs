using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Homework
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config) {
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			var formatters = GlobalConfiguration.Configuration.Formatters;
			formatters.Remove(formatters.FirstOrDefault(x => x.SupportedMediaTypes.Any(y => y.MediaType.Contains("xml"))));

			var js = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
			js.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
			js.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
		}
	}
}
