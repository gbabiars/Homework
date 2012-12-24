using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Homework.Models;
using ServiceStack.Redis;

namespace Homework
{
	public static class RedisConfig
	{
		public static void Init() {
			using (var redis = new RedisClient("127.0.0.1"))
			{
				redis.FlushAll();

				var assignmentsStore = redis.As<Assignment>();
				var assignments = new List<Assignment> {
					new Assignment {Id = 1, CourseId = 1, Title = "Ch 1, 1-29 Odd", DueDate = new DateTime(2012, 12, 27)},
					new Assignment {Id = 2, CourseId = 2, Title = "Ch 2, 1-21", DueDate = new DateTime(2012, 12, 28)},
					new Assignment {Id = 3, CourseId = 2, Title = "Ch 2, 25-28, 29-27 Odd", DueDate = new DateTime(2013, 1, 5)},
					new Assignment {Id = 4, CourseId = 4, Title = "Ch 3, 33-39, 41", DueDate = new DateTime(2012, 12, 20)}
				};
				assignmentsStore.StoreAll(assignments);

				var coursesStore = redis.As<Course>();
				var courses = new List<Course> {
					new Course {Id = 1, Period = 1, TeacherId = 1, Subject = "Math", AssignmentIds = new int[] {1}},
					new Course {Id = 2, Period = 2, TeacherId = 1, Subject = "Advanced Math", AssignmentIds = new int[] {2, 3}},
					new Course {Id = 3, Period = 3, TeacherId = 1, Subject = "Math"},
					new Course {Id = 4, Period = 4, TeacherId = 1, Subject = "Science", AssignmentIds = new int[] {4}},
					new Course {Id = 5, Period = 5, TeacherId = 1, Subject = "Science"},
					new Course {Id = 6, Period = 6, TeacherId = 1, Subject = "Math"},
					new Course {Id = 7, Period = 1, TeacherId = 2, Subject = "History"}
				};
				coursesStore.StoreAll(courses);

				var teachersStore = redis.As<Teacher>();
				var teacher = new Teacher { Id = 1, Name = "Jane Doe", CourseIds = new int[] { 1, 2, 3, 4, 5, 6 } };
				teachersStore.Store(teacher);
			}
		}
	}
}