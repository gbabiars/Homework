App.DateField = Em.TextField.extend({
	didInsertElement: function() {
		this.$().kendoDatePicker();
	}
})