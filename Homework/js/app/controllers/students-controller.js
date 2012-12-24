App.StudentsController = Em.ArrayController.extend({
	content: [],
	addListVisible: false,
	showAddList: function() {
		this.set('addListVisible', true);
	},
	hideAddList: function() {
		this.set('addListVisible', false);
	},
	openStudentDialog: function () {
		var courseId = parseInt(this.get('courseDetailsController.id'));
		var studentDialogController = this.get('studentDialogController');
		studentDialogController.set('content', App.store.findQuery(App.Student, { courseId: courseId, inverted: true }));
		studentDialogController.set('isOpen', true);
	}
})