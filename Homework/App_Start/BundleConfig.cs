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
			RegisterTeacherBundles(bundles);
			RegisterStudentBundles(bundles);
		}

		private static void RegisterTeacherBundles(BundleCollection bundles) {
			var modelsBundle = new ScriptBundle("~/bundles/teacher/models")
				.Include("~/js/app/teacher/models/*.js");

			var controllersBundle = new ScriptBundle("~/bundles/teacher/controllers")
				.Include("~/js/app/teacher/controllers/*.js");

			var viewsBundle = new ScriptBundle("~/bundles/teacher/views")
				.Include("~/js/app/teacher/views/*.js");

			bundles.Add(modelsBundle);
			bundles.Add(controllersBundle);
			bundles.Add(viewsBundle);
		}

		private static void RegisterStudentBundles(BundleCollection bundles) {
			var modelsBundle = new ScriptBundle("~/bundles/student/models")
				.Include("~/js/app/student/models/*.js");

			var controllersBundle = new ScriptBundle("~/bundles/student/controllers")
				.Include("~/js/app/student/controllers/*.js");

			var viewsBundle = new ScriptBundle("~/bundles/student/views")
				.Include("~/js/app/student/views/*.js");

			bundles.Add(modelsBundle);
			bundles.Add(controllersBundle);
			bundles.Add(viewsBundle);
		}
	}
}