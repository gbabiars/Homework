App.Assignment = DS.Model.extend({
	title: DS.attr('string'),
	dueDate: DS.attr('string'),
	courseId: DS.attr('number'),
	
	isDueThisWeek: function () {
		var saturdayDayOfWeek = 6;
		var dueDateAsDate = moment(this.get('dueDate'));
		var today = moment();
		var daysToSaturday = saturdayDayOfWeek - today.day();
		var saturday = today.add('days', daysToSaturday);
		var dueDateDaysToEndOfWeek = saturday.diff(dueDateAsDate, 'days');
		if (dueDateDaysToEndOfWeek < 7 && dueDateDaysToEndOfWeek >= 0)
			return true;
		return false;
	}.property('dueDate'),
	
	isPastDue: function() {
		var dueDateAsDate = moment(this.get('dueDate'));
		var today = moment();
		var daysUntilDue = dueDateAsDate.diff(today, 'days');
		var isPastDue = daysUntilDue < 0;
		return isPastDue;
	}.property('dueDate')
})