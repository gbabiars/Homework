using Homework.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
	        return RedirectToAction("Teacher");
        }

		public ViewResult Teacher() {
			Teacher teacher;
			using (var redis = new RedisClient("127.0.0.1")) {
				teacher = redis.As<Teacher>().GetAll().FirstOrDefault();
			}
			return View(teacher);
		}
    }
}
