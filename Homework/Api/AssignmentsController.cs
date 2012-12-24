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
    public class AssignmentsController : ApiController
    {
	    private readonly string connectionString = ConfigurationManager.AppSettings["REDISTOGO_URL"];

		public object Get([FromUri] AssignmentsRequest request) {
			using (var redis = GetRedisClient())
			{
				if (request.Id != 0)
				{
					var assignment = redis.As<Assignment>().GetById(request.Id);
					return new AssignmentResponse {
						Assignment = new AssignmentResponseItem(assignment)
					};
				}
				var course = redis.As<Course>().GetById(request.CourseId);
				var assignments = redis.As<Assignment>().GetByIds(course.AssignmentIds)
					.OrderBy(x => x.DueDate).ToList();
				return new AssignmentsResponse {
					Assignments = assignments.Select(x => new AssignmentResponseItem {
						Id = x.Id,
						Title = x.Title,
						DueDate = x.DueDate.ToShortDateString(),
						CourseId = x.CourseId
					}).ToList()
				};
			}
		}

		public object Post(AssignmentsRequest request) {
			using (var redis = GetRedisClient())
			{
				var assignmentsClient = redis.As<Assignment>();
				var coursesClient = redis.As<Course>();

				var assignment = request.Assignment;
				assignment.Id = assignmentsClient.GetAll().OrderBy(x => x.Id).Last().Id + 1;

				var course = redis.As<Course>().GetById(assignment.CourseId);
				var assignmentIds = course.AssignmentIds.ToList();
				assignmentIds.Add(assignment.Id);
				course.AssignmentIds = assignmentIds.ToArray();
				coursesClient.Store(course);

				assignmentsClient.GetNextSequence();
				assignmentsClient.Store(assignment);

				return new AssignmentResponse { Assignment = new AssignmentResponseItem(assignment) };
			}
		}

		public object Put(int id, AssignmentsRequest request) {
			using (var redis = GetRedisClient())
			{
				var assignmentsClient = redis.As<Assignment>();
				var assignment = assignmentsClient.GetById(id);
				assignment.Title = request.Assignment.Title;
				assignment.DueDate = request.Assignment.DueDate;
				assignmentsClient.Store(assignment);
			}
			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		private RedisClient GetRedisClient() {
			return new RedisClient(new Uri(connectionString));
		}
    }

	public class AssignmentsRequest
	{
		public int Id { get; set; }

		public int CourseId { get; set; }

		public Assignment Assignment { get; set; }
	}

	public class AssignmentsResponse
	{
		public IList<AssignmentResponseItem> Assignments { get; set; }
	}

	public class AssignmentResponse
	{
		public AssignmentResponseItem Assignment { get; set; }
	}

	public class AssignmentResponseItem
	{
		public AssignmentResponseItem() {}

		public AssignmentResponseItem(Assignment assignment) {
			Id = assignment.Id;
			Title = assignment.Title;
			DueDate = assignment.DueDate.ToShortDateString();
			CourseId = assignment.CourseId;
		}

		public int Id { get; set; }

		public string Title { get; set; }

		public string DueDate { get; set; }

		public int CourseId { get; set; }
	}
}
