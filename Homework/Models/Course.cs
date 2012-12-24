namespace Homework.Models
{
	public class Course
	{
		public Course() {
			AssignmentIds = new int[0];
		}

		public int Id { get; set; }

		public string Subject { get; set; }

		public int Period { get; set; }

		public int TeacherId { get; set; }

		public int[] AssignmentIds { get; set; }
	}
}