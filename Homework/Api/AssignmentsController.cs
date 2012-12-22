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
		    new Assignment { Id = 1, Title = "Math Ch 1, 1-29 Odd" },
			new Assignment { Id = 2, Title = "Math Ch 2, 1-21" },
			new Assignment { Id = 3, Title = "Math Ch 2, 25-28, 29-27 Odd" }
	    };

		public object Get([FromUri] AssignmentsRequest request) {
			return new AssignmentsResponse {Assignments = assignments};
		}

		public object Post(Assignment assignment) {
			assignment.Id = 4;
			return new AssignmentResponse {Assignment = assignment};
		}
    }

	public class AssignmentsRequest
	{
		public int Id { get; set; }

		public int CourseId { get; set; }
	}

	public class AssignmentsResponse
	{
		public IList<Assignment> Assignments { get; set; }
	}

	public class AssignmentResponse
	{
		public Assignment Assignment { get; set; }
	}
}
