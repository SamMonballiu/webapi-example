$(function () {

    function DisplayResult1(call, data) {
        $('#result').append("<strong>" + call + "<strong>" + "<br/>");

        $.each(data, function (i, item) {

            $('#result').append(JSON.stringify(item));
            $('#result').append("<br/>");
        });
    };

    function DisplayResult2(call, data) {
        $('#result').append("<strong>" + call + "<strong>" + "<br/>");
        $('#result').append(JSON.stringify(data));
        $('#result').append("<br/>");

    };
    //change the below port num. The below url is the LibraryService url
    var serviceUrl = 'http://localhost:50070/api';
    $('#GetAll').on('click', function () {
        //alert("Hello");
        $.ajax({
            url: serviceUrl + '/books',
            method: 'GET',
            success: function (data) {
                DisplayResult1("Get All:", data);
            }
        });
    });

    $('#GetById').on('click', function () {
        var bookId = $('#id').val();
        $.ajax({
            url: serviceUrl + '/books/ ' + bookId,
            method: 'GET',
            success: function (data) {
                DisplayResult2("Book by id:", data);
            }
        });
    });


    $('#AddBook').on('click', function () {
        var inputData = $('input').serialize();
        $.ajax({
            url: serviceUrl + '/books/',
            method: 'POST',
            data: inputData,
            success: function (data) {
                DisplayResult2("Add Book", data);
            }
        });
    });


    $('#UpdateBook').on('click', function () {
        var inputData = $('input').serialize();
        var bookId = $('#id').val();
        $.ajax({
            url: serviceUrl + '/books/' + bookId,
            method: 'PUT',
            data: inputData,
            success: function (data) {
                DisplayResult1("Updated list:", data);
            }
        });
    });


    $('#AddCost').on('click', function () {
        var inputData = $('input').serialize();
        var bookId = $('#BookId').val();
        //alert(bookId);
        $.ajax({
            url: serviceUrl + '/books/updatecost/' + bookId,
            method: 'PUT',
            data: inputData,
            success: function (data) {
                DisplayResult2("Add Cost", data);
            }
        });
    });

});