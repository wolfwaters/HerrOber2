$(document).ready(function () {

    var baseAddress = 'http://localhost:4711/';

    $.getJSON(baseAddress + 'users', function (users) {

        //--- loop through all users to fill the carousel
        for (i = 0; i < users.length; i++) {

            //--- add indicators
            $('<li data-target="#myCarousel" data-slide-to="' + i + '"></li>').appendTo('.carousel-indicators')

            //--- add items
            var imageUrl = 'http://herrober.rdeadmin.waters.com/Unknown2.jpg';
            $('<div class="item"><img class="img-responsive center-block" src="' + imageUrl + '"><div class="carousel-caption"><h3>' + users[i].DisplayName + '</h3><p>' + users[i].EmailAddress + '</p></div></div>').appendTo('.carousel-inner');
        }

        $('.item').first().addClass('active');
        $('.carousel-indicators > li').first().addClass('active');
        $('#myCarousel').carousel();
    });
});
