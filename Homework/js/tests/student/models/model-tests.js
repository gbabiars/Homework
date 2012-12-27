/// <reference path="../../../vendor/qunit.js" />
/// <reference path="../../../vendor/moment.min.js" />
/// <reference path="../../../vendor/handlebars.min.js" />
/// <reference path="../../../vendor/moment.min.js" />
/// <reference path="../../../vendor/jquery-1.8.3.min.js" />
/// <reference path="../../../vendor/ember-1.0.0-pre.2.min.js" />
/// <reference path="../../../vendor/ember-data-latest.min.js" />
/// <reference path="../../../app/student/app.js" />
/// <reference path="../../../app/student/store/store.js" />
/// <reference path="../../../app/student/models/student.js" />
/// <reference path="../../../app/student/models/assignment.js" />

module('Student');

test('name should return concatenated first and last name', function () {
	var student = App.Student.createRecord({ firstName: 'John', lastName: 'Doe' });

	var name = student.get('name');

	equal(name, 'John Doe', 'fullName should be John Doe');
});


module('Assignment');

test('isDueThisWeek for today should return true', function() {
	var today = moment();
	var assignment = App.Assignment.createRecord({ dueDate: today.format('M/D/YYYY') });

	var isDueThisWeek = assignment.get('isDueThisWeek');

	ok(isDueThisWeek);
});

test('isDueThisWeek for 8 days from today should return false', function() {
	var nextWeek = moment().add('days', 8);
	var assignment = App.Assignment.createRecord({ dueDate: nextWeek.format('M/D/YYYY') });

	var isDueThisWeek = assignment.get('isDueThisWeek');

	ok(!isDueThisWeek);
});

test('isDueThisWeek for 8 days before today should return false', function () {
	var lastWeek = moment().add('days', -8);
	var assignment = App.Assignment.createRecord({ dueDate: lastWeek.format('M/D/YYYY') });

	var isDueThisWeek = assignment.get('isDueThisWeek');

	ok(!isDueThisWeek);
});

test('isPastDue should be true when the due date is yesterday', function() {
	var yesterday = moment().add('days', -1);
	var assignment = App.Assignment.createRecord({ dueDate: yesterday.format('M/D/YYYY') });

	var isPastDue = assignment.get('isPastDue');

	ok(isPastDue);
});

test('isPastDue should be false when the due date is tomorrow', function () {
	var tomorrow = moment().add('days', 1);
	var assignment = App.Assignment.createRecord({ dueDate: tomorrow.format('M/D/YYYY') });

	var isPastDue = assignment.get('isPastDue');

	ok(!isPastDue);
})