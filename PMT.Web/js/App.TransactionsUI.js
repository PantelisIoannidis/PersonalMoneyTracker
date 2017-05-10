var transactionsUI = function () {

    function calculateIndexActiveElement() {
        var $currentLink = $(".transTable > tbody > tr");
        $currentLink.click(function (e) {
            $this = $(this);
            $currentLink.siblings().removeClass('active').removeClass("info");
            $this.addClass('active').addClass("info");
        });
    };

    function addIndexButtonEvents() {
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
    function hideTransferToDropDown() {
        var transferDropDown = $(".transferDropDown");
        if (!transferDropDown.hasClass("hidden"))
            transferDropDown.addClass("hidden");
    }

    function showTranferToDropDown() {
        var transferDropDown = $(".transferDropDown");
        if (transferDropDown.hasClass("hidden"))
            transferDropDown.removeClass("hidden");
    }


    function hideCategoryDropDown() {
        var categoryId = $("#CategoryId");
        var subCategoryId = $("#SubCategoryId");
        var categoryListDropDown = $(".categoryListDropDown");
        var subCategoryListDropDown = $(".subCategoryListDropDown");

        categoryId.html("");
        categoryId.val("");
        subCategoryId.html("");
        subCategoryId.val("");
        $('.dropdown-category  li').remove();
        $('.dropdown-subcategory  li').remove();

        if (!categoryListDropDown.hasClass("hidden"))
            categoryListDropDown.addClass("hidden");
        if (!subCategoryListDropDown.hasClass("hidden"))
            subCategoryListDropDown.addClass("hidden");
    }

    function showCategoryDropDown() {
        var categoryListDropDown = $(".categoryListDropDown");
        var subCategoryListDropDown = $(".subCategoryListDropDown");
        if (categoryListDropDown.hasClass("hidden"))
            categoryListDropDown.removeClass("hidden");
        if (subCategoryListDropDown.hasClass("hidden"))
            subCategoryListDropDown.removeClass("hidden");
    }

    function refreshDropDownLists() {
        var tranType = $('#TransactionType').val();
        if (tranType == 2 || tranType == 3) {
            hideCategoryDropDown();
            if (tranType == 2) {
                showTranferToDropDown();
            } else {
                hideTransferToDropDown();
            }
            return $.when(null);
        } else {
            hideTransferToDropDown();
            showCategoryDropDown();
        }
        fillCategory();
    }

    function fillCategory() {
        var tranGetCategoriesUrl = '/Transactions/GetCategories';
        var tranType = $('#TransactionType').val();
        var categoryId = $('#CategoryId').val();
        var firstCategoryId = "";
        return $.ajax({
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'GET',
            cache: false,
            data: { type: tranType },
            url: tranGetCategoriesUrl,
            success: function (data) {
                $('.dropdown-category  li').remove();
                $.each(data, function (id, option) {
                    var element = "<li><a data-id='" + option.CategoryId + "' href='#'><i class='fa fa-fw  " + option.IconId + "'></i> "
                        + option.Name + "</a></li>";
                    $('.dropdown-category').append(element);

                    if (firstCategoryId == "")
                        firstCategoryId = option.CategoryId;
                    if (categoryId == "")
                        categoryId = firstCategoryId;
                    if (option.CategoryId == categoryId) {
                        var element = "<i class='fa fa-fw  " + option.IconId + "'></i> "
                        + option.Name + ' <span class="caret"></span>';

                        $("#CategoryId").html(element);
                        $("#CategoryId").val(categoryId);
                        $("#SubCategoryId").val("");
                    }
                });
                fillSubCategoryOnCategoryChange();
            },
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                console.log(msg);
            }
        });

    };

    function fillSubCategoryOnCategoryChange() {
        var tranGetSubCategoriesUrl = '/Transactions/GetSubCategories';
        var categoryId = $("#CategoryId").val();
        var subcategoryId = $("#SubCategoryId").val();
        var firstSubCategoryId = "";
        $.getJSON(tranGetSubCategoriesUrl, { categoryId: categoryId }, function (data) {
            $('.dropdown-subcategory li').remove();
            $.each(data, function (id, option) {
                var element = "<li><a data-id='" + option.SubCategoryId + "' href='#'><i class='fa fa-fw  " + option.IconId + "'></i> "
                    + option.Name + "</a></li>";
                $('.dropdown-subcategory').append(element);

                if (firstSubCategoryId == "")
                    firstSubCategoryId = option.SubCategoryId;
                if (subcategoryId == "")
                    subcategoryId = firstSubCategoryId;
                if (option.SubCategoryId == subcategoryId) {
                    var element = "<i class='fa fa-fw  " + option.IconId + "'></i> "
                    + option.Name + ' <span class="caret"></span>';
                    $("#SubCategoryId").html(element);
                    $("#SubCategoryId").val(subcategoryId);
                }
            });
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.log('Error getting subcategories!');
        });
    };

    function fillTransferToOnAccountChange() {
        var tranGetAccountsAvailableForTransfer = '/Transactions/GetAccountsAvailableForTransfer';
        var accountId = $("#MoneyAccountId").val();
        return $.getJSON(tranGetAccountsAvailableForTransfer, { accountId: accountId }, function (data) {
            $('#TransferTo option').remove();
            $.each(data, function (id, option) {
                $('#TransferTo').append("<option value=" + option.MoneyAccountId + ">" + option.Name + "</option");
            });
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.log('Error getting subcategories!');
        });
    };

    function onAccountChange() {
        $("#MoneyAccountId").change(function () {
            fillTransferToOnAccountChange();
        });
    };

    function onCategoryChange() {
        $(document.body).on('click', '.dropdown-category li > a', function () {
            var element = $(this).html() + ' <span class="caret"></span>';
            var id = $(this).data('id');

            $("#CategoryId").html(element);
            $("#CategoryId").val(id);
            $("#SubCategoryId").val("");
            fillSubCategoryOnCategoryChange();
        });
    };

    function onSubCategoryChange() {
        $(document.body).on('click', '.dropdown-subcategory li > a', function () {
            var element = $(this).html() + ' <span class="caret"></span>';
            var id = $(this).data('id');

            $("#SubCategoryId").html(element);
            $("#SubCategoryId").val(id);
        });
    };

    function onTransactionTypeChange() {
        $("#TransactionType").change(function () {
            $("#SubCategoryId").val("");
            $("#CategoryId").val("");
            refreshDropDownLists();
        });
    };

    function onLoadCreateInit() {
        $.when(
            refreshDropDownLists(),
            fillTransferToOnAccountChange()
            ).done(function () {
                $.when(
                    onCategoryChange()
                ).done(function () {
                    onSubCategoryChange();
                    onAccountChange();
                    onTransactionTypeChange();
                });
            });
        $('.datepicker').datepicker({
            locale: pmt.currentLocal
        });
    };

    return {
        calculateIndexActiveElement: calculateIndexActiveElement,
        addIndexButtonEvents: addIndexButtonEvents,
        onLoadCreateInit: onLoadCreateInit,
    };

}();
