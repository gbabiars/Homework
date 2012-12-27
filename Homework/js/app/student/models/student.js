App.Student = DS.Model.extend({
	firstName: DS.attr('string'),
	lastName: DS.attr('string'),
	grade: DS.attr('number'),
	
	name: function () {
		return this.get('firstName') + ' ' + this.get('lastName');
	}.property('firstName', 'lastName')
})