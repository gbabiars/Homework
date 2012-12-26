App.store = DS.Store.create({
	revision: 7,
	adapter: DS.RESTAdapter.create({
		bulkCommit: false,
		namespace: 'api',
		plurals: {}
	}),
	
	addStudentToCourse: function(options) {
		var root = this.adapter.rootForType(App.Course);

		$.ajax({
			type: 'PUT',
			url: this.adapter.buildURL(root),
			data: { changeType: 'add', id: options.course.get('id'), studentId: options.student.get('id') }
		});
	},
	
	removeStudentFromCourse: function (options) {
		var root = this.adapter.rootForType(App.Course);

		$.ajax({
			type: 'PUT',
			url: this.adapter.buildURL(root),
			data: { changeType: 'remove', id: options.course.get('id'), studentId: options.student.get('id') }
		});
	}
});

App.store.adapter.serializer.keyForAttributeName = function (type, name) {
	return name;
};
