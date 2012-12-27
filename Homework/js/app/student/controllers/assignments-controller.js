App.AssignmentsController = Em.ArrayController.extend({
	content: [],
	
	filter: 'course',
	
	displayCourses: function () {
		return this.get('filter') === 'course';
	}.property('filter'),
	
	setFilterToCourse: function () {
		this.set('filter', 'course');
	},
	
	setFilterToWeek: function () {
		this.set('filter', 'week');
	},
	
	setFilterToPastDue: function() {
		this.set('filter', 'pastDue');
	},

	aggregatedAssignments: function () {
		var courses = this.get('coursesController.content').toArray();
		var assignments = [];
		var filter = this.get('filter');
		if(filter === 'course') {
			assignments = this.get('coursesController.selected')
				? this.get('content').filterProperty('courseId', parseInt(this.get('coursesController.selected')))
				: this.get('content');
		}
		if (filter === "week") {
			assignments = this.get('content').filter(function (a) {
				return a.get('isDueThisWeek');
			});
		}
		if (filter === "pastDue") {
			assignments = this.get('content').filter(function (a) {
				return a.get('isPastDue');
			});
		}
		var aggregatedAssignments = this.aggregateAssignments(assignments, courses);
		var sortedAssignments = _.sortBy(aggregatedAssignments, function(a) {
			return moment(a.dueDate).toDate();
		});
		return sortedAssignments;
	}.property('content.@each', 'coursesController.selected', 'filter'),
	
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