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
    public class AssignmentsController : RedisApiControllerBase
    {
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
				if (request.StudentId != 0)
				{
					var student = redis.As<Student>().GetById(request.StudentId);
					var courses = redis.As<Course>().GetByIds(student.CourseIds);
					var assignmentIds = courses.SelectMany(x => x.AssignmentIds).Distinct().ToList();
					var assignments = redis.GetByIds<Assignment>(assignmentIds)
										   .OrderBy(x => x.DueDate).ToList();
					return new AssignmentsResponse {
						Assignments = assignments.Select(x => new AssignmentResponseItem(x)).ToList()
					};
				}
				if (request.CourseId != 0)
				{
					var course = redis.As<Course>().GetById(request.CourseId);
					var assignments = redis.As<Assignment>().GetByIds(course.AssignmentIds)
					                       .OrderBy(x => x.DueDate).ToList();
					return new AssignmentsResponse {
						Assignments = assignments.Select(x => new AssignmentResponseItem(x)).ToList()
					};
				}
				return null;
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

		public object Delete(int id) {
			using (var redis = GetRedisClient())
			{
				var assignmentsClient = redis.As<Assignment>();
				var coursesClient = redis.As<Course>();

				var assignment = assignmentsClient.GetById(id);

				var course = redis.As<Course>().GetById(assignment.CourseId);
				var assignmentIds = course.AssignmentIds.ToList();
				assignmentIds.Remove(id);
				course.AssignmentIds = assignmentIds.ToArray();
				coursesClient.Store(course);
			}
			return new HttpResponseMessage(HttpStatusCode.OK);
		}
    }

	public class AssignmentsRequest
	{
		public int Id { get; set; }

		public int CourseId { get; set; }

		public int StudentId { get; set; }

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
