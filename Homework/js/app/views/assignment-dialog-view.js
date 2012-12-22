App.AssignmentDialogView = Em.View.extend({
	templateName: 'assignment-dialog',
	classNames: ['modal', 'hide'],
	didInsertElement: function () {
		var self = this;
		this.$().on('hidden', function() {
			self.get('controller').set('isOpen', false);
		});
	},
	toggleDialog: function () {
		if (this.get('controller.isOpen'))
			this.$().modal('show');
		else
			this.$().modal('hide');
	}.observes('controller.isOpen')
})