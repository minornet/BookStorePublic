function PagingService(resultList) {

    var self = this;

    self.queryOptions = {
        curPage: ko.observable(),
        totalPages: ko.observable(),
        pageSize: ko.observable(),
        sortField: ko.observable(),
        sortOrder: ko.observable()
    };

    self.entities = ko.observableArray();

    self.updateResultList = function (resultList) {
        self.queryOptions.curPage(resultList.queryOptions.CurPage);
        self.queryOptions.totalPages(resultList.queryOptions.TotalPages);
        self.queryOptions.pageSize(resultList.queryOptions.PageSize);
        self.queryOptions.sortField(resultList.queryOptions.SortField);
        self.queryOptions.sortOrder(resultList.queryOptions.SortOrder);

        self.entities(resultList.results);
    };
    
    self.updateResultList(resultList);

    self.sortEntitiesBy = function (data, event) {

        // var sortField = $(event.target).data('sortField');

         var sortField = $(event.currentTarget).data('sortField');
        // var sortField = event.currentTarget.dataset.sortField;

        // event.currentTarget.dataset

        // SortOrder Enumerable not working in js
        //if (sortField == self.queryOptions.sortField() && self.queryOptions.sortOrder() == "ASC")
        //    self.queryOptions.sortOrder("DESC");
        //else
        //    self.queryOptions.sortOrder("ASC");

        if (sortField === self.queryOptions.sortField() && self.queryOptions.sortOrder() == 0)
            self.queryOptions.sortOrder(1);
        else
            self.queryOptions.sortOrder(0);


        self.queryOptions.sortField(sortField);
        self.queryOptions.curPage(1);

        self.fetchEntities(event);

    };

    self.previousPage = function (data, event) {
        if (self.queryOptions.curPage() > 1) {
            self.queryOptions.curPage(self.queryOptions.curPage() - 1);
            self.fetchEntities(event);
        }
    };

    self.nextPage = function (data, event) {
        if (self.queryOptions.curPage() < self.queryOptions.totalPages()) {
            self.queryOptions.curPage(self.queryOptions.curPage() + 1);
            self.fetchEntities(event);
        }
    };

    // ok, here's what blew us away this time.  
    // The '¤' character is the 'currency' sign and is resented by '&curren;'
    // it blew the url to hell and took me hours to find. It shouldn't have done that without the ';'

    self.fetchEntities = function (event) {

        var winLoc = window.location.href; // for debugging

        // really probably want to assign the path  (which api)
        // coming in instead of getting it from target

        // var url = winLoc + 'api/' + $(event.target).attr('href');

        var url = $(event.currentTarget).attr('href');   // changed with MapMvcAttributeRoutes

        // var url = $(event.target.baseURI) + '/api';
        url = url.replace('/Books', '/api/Books');  // temporary fix for production
        url = url.replace('/Authors', '/api/Authors');  // temporary fix for production
        

        url += "?sortField=" + self.queryOptions.sortField();
        url += "&sortOrder=" + self.queryOptions.sortOrder();
        url += "&curPage=" + self.queryOptions.curPage();
        url += "&pageSize=" + self.queryOptions.pageSize();

        $.ajax({
            type: "get",
            // contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: url
        //}).success(function (data) {
        }).done(function (data) {
            // console.log(data);
            self.updateResultList(data);

            // self.updateResultList(JSON.stringify(data));
            // var msg = JSON.parse(data);
            // self.updateResultList(msg);    // this looks more like xml than json
            

            // comment this out when it works right

             //$('.body-content').prepend('<div class="alert ">' +
             //   '<br />(event.target).attr: ' + $(event.target).attr('href') + ' <br /> ' +
             //   '<br />window.location.href: ' + winLoc + ' <br /> ' +
             //   '<br />url: ' + url + ' <br /> ' +
             //   '</div > ');
        
            }).fail(function (response) {
            $('.body-content').prepend('<div class="alert alert-danger">' +
                '<strong>Error!</strong> There was an error fetching the data. ' +
                '<br />status: ' + response.status + ' <br />' +
                '<br />(event.target).attr: ' + $(event.target).attr('href') + ' <br /> ' +
                '<br />(event.currentTarget).attr: ' + $(event.currentTarget).attr('href') + ' <br /> ' +
                '<br />window.location.href: ' + winLoc + ' <br /> ' +
                '<br />url: ' + url + ' <br /> ' +

                '<ul> <li> ' + self.queryOptions.sortField() + '</li>' +
                '     <li> ' + self.queryOptions.sortOrder() + '</li>' +
                '     <li> ' + self.queryOptions.curPage() + '</li>' +
                '     <li> ' + self.queryOptions.pageSize() + '</li>' +
                '</ul> ' +
                '</div > ');
        });
    };

    self.buildSortIcon = function (sortField) {
        return ko.pureComputed(function () {
            var sortIcon = 'sort';

            if (self.queryOptions.sortField() == sortField) {
                sortIcon += '-by-alphabet';

                // if (self.queryOptions.sortOrder() == "DESC") sortIcon += '-alt';  // enum not working here
                if (self.queryOptions.sortOrder() == 1) sortIcon += '-alt';
            }
            
            return 'glyphicon glyphicon-' + sortIcon;
        });
    };

    self.buildPreviousClass = ko.pureComputed(function () {
        var className = 'previous';

        if (self.queryOptions.curPage() == 1)
            className += ' disabled';

        return className;
    });

    self.buildNextClass = ko.pureComputed(function () {
        var className = 'next';

        if (self.queryOptions.curPage() == self.queryOptions.totalPages())
            className += ' disabled';

        return className;
    });
    
}