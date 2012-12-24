using Homework.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework.Controllers
{
    public class HomeController : Controller
    {
		private readonly string connectionString = ConfigurationManager.AppSettings["REDISTOGO_URL"];
		
		public ActionResult Index() {
	        return RedirectToAction("Teacher");
        }

		public ViewResult Teacher() {
			Teacher teacher;
			using (var redis = new RedisClient(new Uri(connectionString))) {
				teacher = redis.As<Teacher>().GetAll().FirstOrDefault();
			}
			return View(teacher);
		}
    }
}
