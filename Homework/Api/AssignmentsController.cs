using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Homework.Models;

namespace Homework.Api
{
    public class AssignmentsController : ApiController
    {
	    private IList<Assignment> assignments = new List<Assignment> {
		    new Assignment { Id = 1, Title = "Math Ch 1, 1-29 Odd", DueDate = new DateTime(2012, 12, 27) },
			new Assignment { Id = 2, Title = "Math Ch 2, 1-21", DueDate = new DateTime(2012, 12, 28) },
			new Assignment { Id = 3, Title = "Math Ch 2, 25-28, 29-27 Odd", DueDate = new DateTime(2012, 12, 19) }
	    };

		public object Get([FromUri] AssignmentsRequest request) {
			return new AssignmentsResponse {
				Assignments = assignments.Select(x => new AssignmentResponseItem {
					Id = x.Id,
					Title = x.Title,
					DueDate = x.DueDate.ToShortDateString()
				}).ToList()
			};
		}

		public object Post(AssignmentsRequest request) {
			request.Assignment.Id = 4;
			return new AssignmentResponse { Assignment = new AssignmentResponseItem(request.Assignment) };
		}

		public object Put(AssignmentsRequest request) {
			return new HttpResponseMessage(HttpStatusCode.OK);
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
		}

		public int Id { get; set; }

		public string Title { get; set; }

		public string DueDate { get; set; }
	}
}
