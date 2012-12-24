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
			data: { id: options.course.get('id'), studentId: options.student.get('id') },
			success: options.callback
		});
	}
});

App.store.adapter.serializer.keyForAttributeName = function (type, name) {
	return name;
};
