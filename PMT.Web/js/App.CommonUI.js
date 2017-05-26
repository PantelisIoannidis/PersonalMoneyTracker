var commonUI = function(){
    var calculateActiveElement = function () {
        var myBarCurrentLink = $("input[name='myBarSelector']").val();
        $(".my-bar  li").removeClass('active');
        $("#" + myBarCurrentLink).parent().addClass('active');
    };

    var loadingEvents = function () {
        $(".longProcess").on("click", function (e) {
            $("#divLoading").fadeIn("fast");
        });
        $(document).ajaxStart(function () {
            $("#divLoading").fadeIn("fast");
        });
        $(document).ajaxStop(function () {
            $("#divLoading").fadeOut("fast");
        });
    };

    var loadingFinished = function () {
        $("#divLoading").fadeOut("fast");
    };

    return {
        calculateActiveElement: calculateActiveElement,
        loadingEvents: loadingEvents,
        loadingFinished: loadingFinished,
    };
}();
