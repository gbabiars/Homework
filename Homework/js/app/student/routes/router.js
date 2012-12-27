App.Router = Em.Router.extend({
	enableLogging: true,
	
	displayList: Em.Route.transitionTo('root.index.list'),

	displayCourse: Em.Route.transitionTo('root.index.course'),
	
	root: Em.Route.extend({
		index: Em.Route.extend({
			route: '/',
			connectOutlets: function (router, context) {
				router.get('applicationController').connectOutlet('student', App.store.findById(App.Student, App.metadata.id));
			},
			
			list: Em.Route.extend({
				route: '/',
				connectOutlets: function (router, context) {
					router.get('studentController').connectOutlet('assignments',
						App.store.findQuery(App.Assignment, { studentId: App.metadata.id }));
					router.get('assignmentsController').connectControllers('courses');
					router.get('coursesController').set('content',
						App.store.findQuery(App.Course, { studentId: App.metadata.id }));
				}
			}),
		}),
	})
})