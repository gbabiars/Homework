App.AssignmentsController = Em.ArrayController.extend({
	content: [],
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
})