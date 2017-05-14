var transactionsFiltersUI = function () {

    function refreshDropDownLists() {

    };

    function SetCaretInAccountFilter() {
        var accountId = $("AccountFilterId").val();
        var target = ('.dropdown-category  li a [data-Id=' + accountId + ']').html();

    }
    function SetCaretInPeriodFilter() {

    }

    function onTransactionsFiltersInit() {

    };

    return {
        onTransactionsFiltersInit: onTransactionsFiltersInit,
    };
}();