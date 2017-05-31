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

    var showNotifications = function () {
        var mainSuccessNotifications = $("#mainSuccessNotifications");
        if (mainSuccessNotifications) {
            mainSuccessNotifications.slideDown();
            window.setTimeout(function () { mainSuccessNotifications.slideUp(); }, 2000);
        }
        var mainDangerNotifications = $("#mainDangerNotifications");
        if (mainDangerNotifications) {
            mainDangerNotifications.slideDown();
            window.setTimeout(function () { mainDangerNotifications.slideUp(); }, 2000);
        }
        var mainWarningNotifications = $("#mainWarningNotifications");
        if (mainWarningNotifications) {
            mainWarningNotifications.slideDown();
            window.setTimeout(function () { mainWarningNotifications.slideUp(); }, 2000);
        }
    }

    return {
        calculateActiveElement: calculateActiveElement,
        loadingEvents: loadingEvents,
        loadingFinished: loadingFinished,
        showNotifications: showNotifications,
    };
}();
