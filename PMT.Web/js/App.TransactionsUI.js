var transactionsUI = function () {

    var calculateActiveElement = function () {
        var $currentLink = $(".transTable > tbody > tr");
        $currentLink.click(function (e) {
            $this = $(this);
            $currentLink.siblings().removeClass('active').removeClass("info");
            $this.addClass('active').addClass("info");
        });
    };

    var subscribeButtonEvents = function () {
        $("#transTableEditButton").click(function (e) {
            var transId = $(".transTable .active .transTableId").val();
            var newUrl = pmt.rootPath + "/Transaction/Edit/" + transId;
            document.location.href = newUrl;
            e.preventDefault();
        });
        $("#transTableDeleteButton").click(function (e) {
            var transId = $(".transTable .active .transTableId").val();
            var newUrl = pmt.rootPath + "/Transaction/Delete/1";
            document.location.href = newUrl;
            e.preventDefault();
        });
    };

    return {
        calculateActiveElement: calculateActiveElement,
        subscribeButtonEvents: subscribeButtonEvents,
    };

}();
