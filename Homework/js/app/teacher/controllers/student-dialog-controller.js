App.StudentDialogController = Em.ArrayController.extend({
	content: [],
	selected: null,
	isOpen: false,
	cancel: function () {
		this.set('isOpen', false);
	},
	contentLoaded: function () {
		if (this.get('content.isLoaded') && this.get('content.length'))
			this.set('selected', this.get('content').objectAt(0));
	}.observes('content.isLoaded')
})