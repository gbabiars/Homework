App.AssignmentDialogController = Em.ObjectController.extend({
	content: null,
	isOpen: false,
	cancel: function () {
		var assignment = this.get('content');
		if(assignment.get('isDirty'))
			this.get('content').rollback();
		this.set('isOpen', false);
	}
})