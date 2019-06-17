var i = 0;
$(document).ready(function () {
    $(".moreBox").slice(0, 4).show();
    if ($(".blogBox:hidden").length != 0) {
        $("#loadMore").show();
        
    }
    $("#loadMore").on('click', function (e) {
        i = i + 4;
        e.preventDefault();
        $(".moreBox:hidden").slice(0, i).slideDown('fast');
        if ($(".moreBox:hidden").length == 0) {
            $("#loadMore").fadeOut('fast');
        }
    });
});