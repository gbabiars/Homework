App.Student = DS.Model.extend({
	name: DS.attr('string'),
	grade: DS.attr('number'),
	courses: DS.hasMany('App.Course')
})