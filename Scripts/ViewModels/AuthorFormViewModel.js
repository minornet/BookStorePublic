function AuthorFormViewModel(author) {

    var self = this;

    self.saveCompleted = ko.observable(false);
    self.sending = ko.observable(false);

    self.isCreating = author.id == 0;

    self.author = {
        id: author.id,
        firstName: ko.observable(author.firstName),
        lastName: ko.observable(author.lastName),
        biography: ko.observable(author.biography)
    };

    // has to come in from the form submit
    //self.books = {
    //    books: book.books
    //};

    self.authorFormValidateAndSave = function (form) {
        if (!$(form).valid())
            return false;

        self.sending(true);

        // include the anti forgery token
        self.author.__RequestVerificationToken = form[0].value;

        var winLoc = window.location.href; // for debugging

        // var url = '/BookStore/api';  // for real
        // var url = '/api';            // for test

        var paths = window.location.pathname.split("Authors");
        var url = paths[0] + 'api/Authors';
        
        var dataObject = ko.toJSON(self.author);
        

        $.ajax({
            url: url,
            type: (self.isCreating) ? 'post' : 'put',
            contentType: 'application/json',                
            dataType: 'json',
            data: dataObject
            // data: ko.toJSON(self.author)                       // look into this
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
        
    };

    ////////////////

    //// do we need to edit an author's books?

    //var bookObject = ko.toJSON(self.books);
    //    // we need to load all BookAuthor recs to this author, compare to self.books
    //    // and insert and delete as necessary.
    
    self.successfulSave = function () {
        self.saveCompleted(true);

        $('.body-content').prepend(
            '<div class="alert alert-success">' +
            '<strong>Success!</strong> The new author has been saved. ' +
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
            '<strong> Error!</strong> There was an error saving the author.<br /> ' +
            'url: ' + url + '<br />' +
            // 'type: ' + ((self.isCreating) ? 'post' : 'put') + 
            '</div> ');
    };

}
