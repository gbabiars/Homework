using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Models
{
	public class Student
	{
		public Student() {
			CourseIds = new int[0];
		}

		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int Grade { get; set; }

		public int[] CourseIds { get; set; }
	}
}