$(document).ready(function () {
    function Javel(data) {
        $('#Jävel').html(data.Name + " " + data.Score);
    }
    $.ajax({
        method: "GET",
        url: 'http://beta.jaaveel.ecstudenter.se/Api/HighScore',
        data:{},
        dataType: 'jsonp',
        callback: 'Javel',
        //success: function (data) {
        //    $('#Jävel').html(data.Name + " " + data.Score);
        //},
        error: function (jqXHR, statusText, errorThrown) {
            $('#Jävel').html('Ett fel inträffade: <br>'
                + statusText + errorThrown);
        }
    });
    
    $('#Search').on('input', function () {
        
        $.ajax({
            method:"GET",
            url: '/Book/BooksAPI',
            dataType: 'json',
            data: {
                
                searchstring: $("#Search").val()
            },
            success: function (data) {
                $('#BookDisplay').html(data.HtmlString);
            },
            error: function (jqXHR, statusText, errorThrown) {
                $('#BookDisplay').html('Ett fel inträffade: <br>'
                    + statusText);
            }
        });
    });
});
$(document).ready(function () {
    $(".book-divs").hover(function () {
        $(this).animate({
            opacity: '1'
        });
    }, function () {
        $(this).animate({
            opacity: '0.9'
        });
    });
});