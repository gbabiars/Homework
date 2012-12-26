App.AssignmentsController = Em.ArrayController.extend({
	content: [],
	
	assignments: Em.A([]),
	
	sortedAssignments: function() {
		var assignemntsSortedByDueDate = _.sortBy(this.get('assignments'), function(a) {
			return moment(a.get('dueDate')).toDate();
		});
		return assignemntsSortedByDueDate;
	}.property('assignments', 'assignments.@each.dueDate'),
		
	addAssignmentDialog: function (event) {
		var courseId = parseInt(this.get('courseDetailsController.id'));
		var assignmentDialogController = this.get('assignmentDialogController');
		assignmentDialogController.set('content', App.Assignment.createRecord({ title: '', courseId: courseId }));
		assignmentDialogController.set('dialogTitle', 'Create Assignment');
		assignmentDialogController.set('isOpen', true);
	},

	editAssignmentDialog: function (event) {
		var assignmentDialogController = this.get('assignmentDialogController');
		assignmentDialogController.set('content', event.context);
		assignmentDialogController.set('dialogTitle', 'Edit Assignment');
		assignmentDialogController.set('isOpen', true);
	},
	
	deleteAssignment: function(event) {
		this.get('assignments').removeObject(event.context);
	},
		
	contentLoaded: function () {
		if (this.get('content.isLoaded')) {
			this.set('assignments', this.get('content').toArray());
			var arrayObserver = Em.Object.create({
				arrayWillChange: function (array, start, removeCount, addCount) {
					if (removeCount > 0) {
						var assignmentToRemove = array[start];
						App.router.send('deleteAssignment', { context: assignmentToRemove });
					}
				},
				arrayDidChange: function (array, start, removeCount, addCount) { }
			});
			this.get('assignments').addArrayObserver(arrayObserver);
		}
	}.observes('content.isLoaded')
})