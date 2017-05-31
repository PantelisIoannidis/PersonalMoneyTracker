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

    function showSuccessNotifications(message) {
        commonShowNotification($('#mainSuccessNotifications'),message);
    }

    function showDangerNotifications(message) {
        commonShowNotification($('#mainDangerNotifications'), message);
    }

    function showWarningNotifications(message) {
        commonShowNotification($('#mainWarningNotifications'), message);
    }

    function commonShowNotification(notificationArea,message) {
        notificationArea.attr("data-message", message);
        notificationArea.html(message);
        showNotifications();
    }

    function showNotifications() {
        var mainSuccessNotifications = $("#mainSuccessNotifications");
        if (mainSuccessNotifications.attr('data-message')!=="") {
            mainSuccessNotifications.slideDown();
            window.setTimeout(function () {
                mainSuccessNotifications.slideUp();
                mainSuccessNotifications.attr('data-message', "");
            }, 2000);
        }
        var mainDangerNotifications = $("#mainDangerNotifications");
        if (mainDangerNotifications.attr('data-message') !== "") {
            mainDangerNotifications.slideDown();
            window.setTimeout(function () {
                mainDangerNotifications.slideUp();
                mainDangerNotifications.attr('data-message', "");
            }, 2000);
        }
        var mainWarningNotifications = $("#mainWarningNotifications");
        if (mainWarningNotifications.attr('data-message') !== "") {
            mainWarningNotifications.slideDown();
            window.setTimeout(function () {
                mainWarningNotifications.slideUp();
                mainWarningNotifications.attr('data-message', "");
            }, 2000);
        }
    }

    return {
        calculateActiveElement: calculateActiveElement,
        loadingEvents: loadingEvents,
        loadingFinished: loadingFinished,
        showNotifications: showNotifications,
        showSuccessNotifications: showSuccessNotifications,
        showDangerNotifications: showDangerNotifications,
        showWarningNotifications: showWarningNotifications

    };
}();
