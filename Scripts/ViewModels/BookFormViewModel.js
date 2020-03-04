function BookFormViewModel(book) {

    var self = this;

    self.saveCompleted = ko.observable(false);
    self.sending = ko.observable(false);

    self.isCreating = book.id === 0;

    self.book = {
        id: book.id,
        title: book.title,
        edition: book.edition,
        binding: book.binding,
        publisher: book.publisher,
        isbn: book.isbn,
        description: book.description,
        thumbnailImage: book.thumbnailImage,
        largeImage: book.largeImage,
        price: book.price
        //,authors: book.authors
    };

    // EF doesn't know anything about book.authors, so . . .
    // just verify that the updated list gets in here for now
    //  and save the rest of the book correctly 

    //  then we will have to update the BookAuthor records
    //  after the book is successfully saved 
    // (if we can do it that way or will have to update both in one call).
    
    //self.authors = book.authors;
    
    self.validateAndSave = function (form) {
        if (!$(form).valid())
            return false;

        self.sending(true);

        // include the anti forgery token
        self.book.__RequestVerificationToken = form[0].value;

        var winLoc = window.location.href; // for debugging

        // var url = '/BookStore/api';  // for real
        // var url = '/api';            // for test

        var paths = window.location.pathname.split("Books");
        var url = paths[0] + 'api/Books';

        var bookObject = ko.toJSON(self.book);

        $.ajax({
            url: url,
            type: (self.isCreating) ? 'post' : 'put',
            contentType: 'application/json',
            dataType: 'json',
            data: bookObject
            // data: ko.toJSON(self.book)                       // look into this
        })
            //    .success(self.successfulSave)  previous ko version . . .
            .done(self.successfulSave)

            .fail(function (response) {
                $('.body-content').prepend('<div class="alert alert-danger">' +
                    response.status + ' : ' + response.statusText +

                    '</div> ');

                // $('.body-content').prepend(response.status + ' : ' + response.statusText + );
                self.errorSave(url);
            })
            .always(function () { self.sending(false); });

        ////////////////
        
        //var authorObject = ko.toJSON(self.authors);
        // we need to load all BookAuthor recs to this book, compare to self.authors
        // and insert and delete as necessary.
        
    };

    self.successfulSave = function () {
        self.saveCompleted(true);

        $('.body-content').prepend(
            '<div class="alert alert-success">' +
            '<strong>Success!</strong> The new book has been saved. ' +
            // response.status + ' : ' + response.statusText +
            // '<br />window.location.href: ' + winLoc + ' <br /> ' +
            // 'url: ' + url + '<br />' +
            // 'type: ' + ((self.isCreating) ? 'post' : 'put') + '</div> ');
            '</div> ');
        setTimeout(function () {
            if (self.isCreating)
                location.href = './';
            else
                location.href = '../';
        }, 10000);
    };

    self.errorSave = function (url) {
        $('.body-content').prepend(
            '<div class="alert alert-danger">' +
            '<strong> Error!</strong> There was an error saving the book.<br /> ' +
            'url: ' + url + '<br />' +
            // 'type: ' + ((self.isCreating) ? 'post' : 'put') + 
            '</div> ');
    };

}
