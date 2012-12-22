using Homework.Models;
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
			var teacher = new Teacher {
				Id = 1,
				Name = "Jane Doe"
			};
			return View(teacher);
		}
    }
}
