using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Homework.Models;
using ServiceStack.Redis;
using System.Configuration;

namespace Homework.Api
{
    public class CoursesController : RedisApiControllerBase
    {
		public object Get([FromUri] CoursesRequest request) {
			using (var redis = GetRedisClient())
			{
				var coursesStore = redis.As<Course>();
				if (request.Id != 0)
					return new CourseResponse {
						Course = coursesStore.GetById(request.Id)
					};
				var teacher = redis.As<Teacher>().GetById(request.TeacherId);
				return new CoursesResponse {
					Courses = coursesStore.GetAll()
						.Where(x => teacher.CourseIds.Contains(x.Id))
						.OrderBy(x => x.Period).ToList()
				};
			}
		}

		public object Put(CoursesRequest request) {
			using (var redis = GetRedisClient())
			{
				var coursesStore = redis.As<Course>();
				var studentsStore = redis.As<Student>();

				var course = coursesStore.GetById(request.Id);
				var student = studentsStore.GetById(request.StudentId);

				var studentIds = course.StudentIds.ToList();
				studentIds.Add(student.Id);
				course.StudentIds = studentIds.ToArray();
				coursesStore.Store(course);

				var courseIds = student.CourseIds.ToList();
				courseIds.Add(course.Id);
				student.CourseIds = courseIds.ToArray();
				studentsStore.Store(student);
			}
			return new HttpResponseMessage(HttpStatusCode.OK);
		}
    }

	public class CoursesRequest
	{
		public int Id { get; set; }
		public int TeacherId { get; set; }
		public int StudentId { get; set; }
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
