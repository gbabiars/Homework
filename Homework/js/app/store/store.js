App.store = DS.Store.create({
	revision: 7,
	adapter: DS.RESTAdapter.create({
		bulkCommit: false,
		namespace: 'api',
		plurals: {}
	})
})

App.store.adapter.serializer.keyForAttributeName = function (type, name) {
	return name;
};
