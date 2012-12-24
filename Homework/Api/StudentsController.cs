using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Homework.Models;

namespace Homework.Api
{
    public class StudentsController : RedisApiControllerBase
    {
		public object Get([FromUri] StudentsRequest request) {
			using (var redis = GetRedisClient()) {
				if (request.Id != 0) {
					var student = redis.As<Student>().GetById(request.Id);
					return new StudentResponse {
						Student = new StudentResponseItem(student)
					};
				} else {
					var course = redis.As<Course>().GetById(request.CourseId);
					var students = !request.Inverted
						               ? redis.As<Student>().GetByIds(course.StudentIds)
						                      .OrderBy(x => x.Name).ToList()
						               : redis.As<Student>().GetAll().Where(x => !course.StudentIds.Contains(x.Id))
						                      .OrderBy(x => x.Name).ToList();
					return new StudentsResponse {
						Students = students.Select(x => new StudentResponseItem(x)).ToList()
					};
				}
			}
		}
	}

	public class StudentsRequest
	{
		public int Id { get; set; }

		public int CourseId { get; set; }

		public bool Inverted { get; set; }
	}

	public class StudentsResponse
	{
		public IList<StudentResponseItem> Students { get; set; }
	}

	public class StudentResponse
	{
		public StudentResponseItem Student { get; set; }
	}

	public class StudentResponseItem
	{
		public StudentResponseItem() {}

		public StudentResponseItem(Student student) {
			Id = student.Id;
			Name = student.Name;
			Grade = student.Grade;
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public int Grade { get; set; }
	}
}
