﻿using System;
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
    public class CoursesController : ApiController
    {
		private readonly string connectionString = ConfigurationManager.AppSettings["REDISTOGO_URL"];

		public object Get([FromUri] CoursesRequest request) {
			using (var redis = new RedisClient(connectionString))
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
