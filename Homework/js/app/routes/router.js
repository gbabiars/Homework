App.Router = Em.Router.extend({
	enableLogging: true,
	
	displayCourse: Em.Route.transitionTo('root.index.course'),
	
	root: Em.Route.extend({
		index: Em.Route.extend({
			route: '/',
			connectOutlets: function (router, context) {
				var teacher = App.Teacher.create({ id: App.metadata.id, name: App.metadata.name });
				router.get('applicationController').connectOutlet('teacher', teacher);
				router.get('teacherController').connectOutlet({
					outletName: 'courses',
					name: 'courses',
					context: App.store.findQuery(App.Course, { teacherId: teacher.get('id') })
				});
			},
			
			list: Em.Route.extend({
				route: '/'
			}),
			
			course: Em.Route.extend({
				route: '/course/:id',
				connectOutlets: function (router, context) {
					router.get('teacherController').connectOutlet({
						outletName: 'details',
						viewClass: App.CourseDetailsView,
						controller: router.get('selectedCourseController'),
						context: context
					});
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