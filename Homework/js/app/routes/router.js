App.Router = Em.Router.extend({
	enableLogging: true,
	
	displayList: Em.Route.transitionTo('root.index.list'),

	displayCourse: Em.Route.transitionTo('root.index.course'),
	
	addAssignmentDialog: function (router, context) {
		var assignmentDialogController = router.get('assignmentDialogController');
		assignmentDialogController.set('content', App.Assignment.createRecord({ title: '' }));
		assignmentDialogController.set('isOpen', true);
	},
	
	saveAssignment: function(router, context) {
		var assignmentDialogController = router.get('assignmentDialogController');
		assignmentDialogController.set('isOpen', false);
		assignmentDialogController.get('content').on('didCreate', function() {
			var course = router.get('courseDetailsController.content');
			router.get('assignmentsController').set('content', App.store.findQuery(App.Assignment, { courseId: course.get('id') }));
		});
		App.store.commit();
	},
	
	root: Em.Route.extend({
		index: Em.Route.extend({
			route: '/',
			connectOutlets: function (router, context) {
				var teacher = App.Teacher.create({ id: App.metadata.id, name: App.metadata.name });
				router.get('applicationController').connectOutlet('teacher', teacher);
				router.get('assignmentsController').connectControllers('assignmentDialog');
			},
			
			list: Em.Route.extend({
				route: '/',
				connectOutlets: function (router, context) {
					var teacher = router.get('teacherController.content');
					router.get('teacherController').connectOutlet('courses',
						App.store.findQuery(App.Course, { teacherId: teacher.get('id') }));
				},
			}),

			course: Em.Route.extend({
				route: '/course/:id',
				connectOutlets: function (router, context) {
					router.get('teacherController').connectOutlet('courseDetails', context);
					router.get('courseDetailsController').connectOutlet('assignments',
						App.store.findQuery(App.Assignment, { courseId: context.get('id') }));
				},
				serialize: function (router, context) {
					return { id: context.get('id') };
				},
				deserialize: function (router, context) {
					return App.store.findById(App.Course, context.id);
				}
			})
		}),
	})
})