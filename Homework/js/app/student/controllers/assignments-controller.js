App.AssignmentsController = Em.ArrayController.extend({
	content: [],
	aggregatedAssignments: function () {
		var courses = this.get('coursesController.content').toArray();
		var assignments = this.get('coursesController.selected')
			? this.get('content').filterProperty('courseId', parseInt(this.get('coursesController.selected')))
			: this.get('content');
		var aggregatedAssignments = this.aggregateAssignments(assignments, courses);
		var sortedAssignments = _.sortBy(aggregatedAssignments, function(a) {
			return moment(a.dueDate).toDate();
		});
		return sortedAssignments;
	}.property('content.@each', 'coursesController.selected'),
	
	aggregateAssignments: function (assignments, courses) {
		var result = [];
		assignments.forEach(function (assignment) {
			var course = _.find(courses, function (c) {
				return parseInt(c.get('id')) === assignment.get('courseId');
			});
			result.push({
				id: assignment.get('id'),
				title: assignment.get('title'),
				dueDate: assignment.get('dueDate'),
				courseSubject: course.get('subject')
			});
		});
		return result;
	}
})