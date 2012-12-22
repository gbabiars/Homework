using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Homework.HtmlHelpers
{
    public static class ToJsonHtmlHelper
    {
        public static MvcHtmlString ToJson(this HtmlHelper html, object obj) {
            return MvcHtmlString.Create(JsonConvert.SerializeObject(obj, Formatting.None,
                GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings));
        }
    }
}