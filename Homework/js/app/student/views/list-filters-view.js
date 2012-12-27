App.ListFiltersView = Em.View.extend({
	templateName: 'list-filters',

	didInsertElement: function() {
		this.$().find('a').on('click', function(e) {
			$(e.target).addClass('selected').siblings().removeClass('selected');
		});
	}
});