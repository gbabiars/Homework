using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Homework.Models;

namespace Homework.Api
{
    public class CoursesController : ApiController
    {
		private IList<Course> courses = new List<Course> {
			new Course {Id = 1, Period = 1, Subject = "Math" },
			new Course {Id = 2, Period = 2, Subject = "Math" },
			new Course {Id = 3, Period = 3, Subject = "Math" },
			new Course {Id = 4, Period = 4, Subject = "Science" },
			new Course {Id = 5, Period = 5, Subject = "Science" },
			new Course {Id = 6, Period = 6, Subject = "Math" },
		};

		public object Get([FromUri] CoursesRequest request) {
			if (request.Id != 0)
				return new CourseResponse {
					Course = courses.FirstOrDefault(x => x.Id == request.Id)
				};
			return new CoursesResponse { Courses = courses };
		}
    }

	public class CoursesRequest
	{
		public int Id { get; set; }
		public int TeacherId { get; set; }
	}

	public class CoursesResponse
	{
		public IList<Course> Courses { get; set; }
	}

	public class CourseResponse
	{
		public Course Course { get; set; }
	}
}
