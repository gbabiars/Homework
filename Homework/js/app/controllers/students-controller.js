App.StudentsController = Em.ArrayController.extend({
	content: [],
	
	students: Em.A([]),
	
	sortedStudents: function() {
		var studentsSortedByLastName = _.sortBy(this.get('students'), function(s) {
			return s.get('lastName');
		});
		return studentsSortedByLastName;
	}.property('students', 'students.@each'),
	
	openStudentDialog: function () {
		var courseId = parseInt(this.get('courseDetailsController.id'));
		var studentDialogController = this.get('studentDialogController');
		studentDialogController.set('content', App.store.findQuery(App.Student, { courseId: courseId, inverted: true }));
		studentDialogController.set('isOpen', true);
	},
	
	removeStudentFromCourse: function(event) {
		this.get('students').removeObject(event.context);
	},
	
	contentLoaded: function () {
		if (this.get('content.isLoaded')) {
			this.set('students', this.get('content').toArray());
			var arrayObserver = Em.Object.create({
				arrayWillChange: function (array, start, removeCount, addCount) {
					if (removeCount > 0) {
						var studentToRemove = array[start];
						App.router.send('removeStudentFromCourse', { context: studentToRemove });
					}
				},
				arrayDidChange: function (array, start, removeCount, addCount) { }
			});
			this.get('students').addArrayObserver(arrayObserver);
		}
	}.observes('content.isLoaded')
})