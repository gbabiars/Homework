App.Router = Em.Router.extend({
	enableLogging: true,
	
	displayList: Em.Route.transitionTo('root.index.list'),

	displayCourse: Em.Route.transitionTo('root.index.course'),
			
	saveAssignment: function (router, event) {
		var assignmentDialogController = router.get('assignmentDialogController');
		assignmentDialogController.set('isOpen', false);
		var assignment = assignmentDialogController.get('content');
		if (!assignment.get('id')) {
			this.get('assignmentsController.assignments').insertAt(0, assignment);
		}
		assignment.transaction.commit();
	},
	
	deleteAssignment: function (router, event) {
		var assignment = event.context;
		App.store.deleteRecord(assignment);
		assignment.transaction.commit();
	},
	
	addStudentToCourse: function (router, event) {
		var studentDialogController = router.get('studentDialogController');
		studentDialogController.set('isOpen', false);
		var course = router.get('courseDetailsController.content');
		App.store.addStudentToCourse({
			student: studentDialogController.get('selected'),
			course: course,
			callback: function() {
				router.get('studentsController').set('content', App.store.findQuery(App.Student, { courseId: course.get('id') }));
			}
		});
	},
	
	removeStudentFromCourse: function(router, event) {
		var course = router.get('courseDetailsController.content');
		App.store.removeStudentFromCourse({
			student: event.context,
			course: course,
			callback: function() {
				router.get('studentsController').set('content', App.store.findQuery(App.Student, { courseId: course.get('id') }));
			}
		});
	},
	
	root: Em.Route.extend({
		index: Em.Route.extend({
			route: '/',
			connectOutlets: function (router, context) {
				var teacher = App.Teacher.create({ id: App.metadata.id, name: App.metadata.name });
				router.get('applicationController').connectOutlet('teacher', teacher);
				router.get('assignmentsController').connectControllers('assignmentDialog', 'courseDetails');
				router.get('studentsController').connectControllers('studentDialog', 'courseDetails');
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
					router.get('courseDetailsController').connectOutlet({
						outletName: 'assignments', 
						name: 'assignments',
						context: App.store.findQuery(App.Assignment, { courseId: context.get('id') })
					});
					router.get('courseDetailsController').connectOutlet({
						outletName: 'students',
						name: 'students',
						context: App.store.findQuery(App.Student, { courseId: context.get('id') })
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