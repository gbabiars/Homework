window.App = Em.Application.create({
	ready: function() {
		this.metadata = JSON.parse($('#app-metadata').html());
	}
})