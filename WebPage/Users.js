$(document).ready(function () {

    var baseAddress = 'http://localhost:4711/';

    $.getJSON(baseAddress + 'users', function (users) {
        for (i = 0; i < users.length; i++) {
            //--- add indicators
            $('<li data-target="#carousel-example-generic" data-slide-to="' + i + '"></li>').appendTo('.carousel-indicators')

            //--- add items
            var imageUrl = 'http://herrober.rdeadmin.waters.com/Unknown.jpg';
            $('<div class="item"><img src="' + imageUrl + '"><div class="carousel-caption"><h3>' + users[i].DisplayName + '</h3><p>' + users[i].EmailAddress + '</p></div></div>').appendTo('.carousel-inner');
        }

        $('.item').first().addClass('active');
        $('.carousel-indicators > li').first().addClass('active');
        $('#carousel-example-generic').carousel();
    });
});
