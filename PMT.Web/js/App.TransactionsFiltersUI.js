var transactionsFiltersUI = function () {

    var caretHtml = '<span class="caret"></span>';
    

    function SetCaretInAccountFilter() {
        var accountFilterId = $("#AccountFilterId");
        var target = $('.dropdown-accountFilter >li > a').filter('[data-id="' + accountFilterId.val() + '"]');
        accountFilterId.html(target.html());
        accountFilterId.append(caretHtml);
    }
    function SetCaretInPeriodFilter() {
        var periodFilterId = $("#PeriodFilterId");
        var target = $('.dropdown-PeriodFilter >li > a').filter('[data-id="' + periodFilterId.val() + '"]');
        periodFilterId.html(target.html());
        periodFilterId.append(caretHtml);
    }

    function onAccountFilterClick() {
        $('.dropdown-accountFilter >li > a').on("click", function (e) {
            var newElement = $(this).html() +" "+ caretHtml;
            var id = $(this).data('id');
            var accountFilterId = $("#AccountFilterId");
            accountFilterId.html(newElement);
            accountFilterId.val(id);
            SetPreferences("");
        });
    };

    function onPeriodFilterClick() {
        $('.dropdown-PeriodFilter >li > a').on("click", function (e) {
            var newElement = $(this).html() +" "+ caretHtml;
            var id = $(this).data('id');
            var periodFilterId = $("#PeriodFilterId");
            periodFilterId.html(newElement);
            periodFilterId.val(id);
            SetPreferences("");
        });
    };

    function onMoveToPreviousDateClick() {
        $('#MoveToPreviousDateBtn').on("click", function (e) {
            SetPreferences("MoveToPrevious");
        });
    };

    function onResetDateClick() {
        $('#ResetPeriodBtn').on("click", function (e) {
            SetPreferences("Reset");
        });
    };

    function onMoveToNextDateClick() {
        $('#MoveToNextDateBtn').on("click", function (e) {
            SetPreferences("MoveToNext");
        });
    };

    function SetPreferencesCookie(moveToNext) {
        var d = new Date();
        d.setTime(d.getTime() + (24 * 60 * 60 * 30*1000));
        var expires = "expires=" + d.toUTCString();
        var transactionPreferences = "transactionPreferences";
        var obj = {
            "AccountFilterId": $("#AccountFilterId").val(),
            "PeriodFilterId": $("#PeriodFilterId").val(),
            "SelectedDateFull": $("#SelectedDateFull").val(),
            "UserId": $("#UserId").val(),
            "Operation": moveToNext
        };
        var myJSON = JSON.stringify(obj);
        document.cookie = transactionPreferences + "=" + myJSON + ";" + expires;
        window.location.reload(true);
    }

    function SetPreferences(operation) {
        var obj = {
            "AccountFilterId": $("#AccountFilterId").val(),
            "PeriodFilterId": $("#PeriodFilterId").val(),
            "SelectedDateFull": $("#SelectedDateFull").val(),
            "UserId":$("#UserId").val(),
            "Operation": operation
        };
        var myJSON = JSON.stringify(obj);
        $.post(pmt.rootPath + "Transactions/SetUserPreferences/", { preferences: myJSON },
                function (retURL) {
                    window.location.reload(true);
                });
    }

    function onTransactionsFiltersInit() {
        SetCaretInAccountFilter();
        SetCaretInPeriodFilter();
        onAccountFilterClick();
        onPeriodFilterClick();
        onMoveToPreviousDateClick();
        onMoveToNextDateClick();
        onResetDateClick();
        $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    };

    return {
        onTransactionsFiltersInit: onTransactionsFiltersInit,
    };
}();