using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Homework
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles) {
			var modelsBundle = new ScriptBundle("~/bundles/models")
				.Include("~/js/app/models/*.js");

			var controllersBundle = new ScriptBundle("~/bundles/controllers")
				.Include("~/js/app/controllers/*.js");

			var viewsBundle = new ScriptBundle("~/bundles/views")
				.Include("~/js/app/views/*.js");

			bundles.Add(modelsBundle);
			bundles.Add(controllersBundle);
			bundles.Add(viewsBundle);
		}
	}
}