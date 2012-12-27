App.Course = DS.Model.extend({
	subject: DS.attr('string'),
	period: DS.attr('number'),
	teacherId: DS.attr('number')
})