function BookIndexViewModel(resultList) {

    var self = this;

    self.pagingService = new PagingService(resultList);

    self.showDeleteModal = function (data, event) {
        self.sending = ko.observable(false);

        $.get($(event.target).attr('href'), function (d) {
            $('.body-content').prepend(d);
            $('#deleteModal').modal('show');

            ko.applyBindings(self, document.getElementById('deleteModal'));

        });
    };

    self.deleteBook = function (form) {
        self.sending(true);
        return true;
    };

 
}
