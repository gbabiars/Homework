using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Models
{
	public class Teacher
	{
		public Teacher() {
			CourseIds = new int[0];
		}
		public int Id { get; set; }

		public string Name { get; set; }

		public int[] CourseIds { get; set; }
	}
}