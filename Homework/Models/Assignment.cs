using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Models
{
	public class Assignment
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public DateTime DueDate { get; set; }
	}
}