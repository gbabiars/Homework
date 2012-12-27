App.Assignment = DS.Model.extend({
	title: DS.attr('string'),
	dueDate: DS.attr('string'),
	courseId: DS.attr('number')
})