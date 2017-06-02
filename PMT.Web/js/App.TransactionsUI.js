var transactionsUI = function () {

    var caretHtml = '<span class="caret"></span>';

    function calculateIndexActiveElement() {
        var $currentLink = $(".transTable > tbody > tr");
        $currentLink.click(function (e) {
            $this = $(this);
            if ($this.hasClass("active")){
                $currentLink.siblings().removeClass('active').removeClass("info");
            }
            else {
                $currentLink.siblings().removeClass('active').removeClass("info");
                $this.addClass('active').addClass("info");
            }
        });
    };

    function addIndexButtonEvents() {
        $("#transTableDeleteButton").click(function (e) {
            e.preventDefault();
            var transId = $(".transTable .active .transTableId").val();
            if (!transId) {
                commonUI.loadingFinished();
                commonUI.showWarningNotifications("Please select a transaction");
                return;
            }
            $("#transactionDeleteModal").modal("show");
            
        });
        $("#transTableDeleteConfirmButton").on('click', function (e) {
            e.preventDefault();
            var transId = $(".transTable .active .transTableId").val();
            if (!transId) {
                commonUI.loadingFinished();
                commonUI.showWarningNotifications("Please select a transaction");
                return;
            }
            var token = $('[name=__RequestVerificationToken]').val();
            $("#transactionDeleteModal").modal("hide");
            $.post(pmt.rootPath+"Transactions/Delete/", { __RequestVerificationToken: token, id: transId },
                function (retURL) { window.location.reload(true); });
        });
        $("#transTableEditButton").click(function (e) {
            e.preventDefault();
            var transId = $(".transTable .active .transTableId").val();
            if (!transId) {
                commonUI.loadingFinished();
                commonUI.showWarningNotifications("Please select a transaction");
                return;
            }
            var newUrl = pmt.rootPath+"Transactions/Edit/" + transId;
            document.location.href = newUrl;
            
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
        var categoryBtn = $("#categoryBtn");
        var subCategoryBtn = $("#subCategoryBtn");
        var categoryListDropDown = $(".categoryListDropDown");
        var subCategoryListDropDown = $(".subCategoryListDropDown");

        categoryBtn.html("");
        categoryBtn.val("");
        subCategoryBtn.html("");
        subCategoryBtn.val("");
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
            setCategoryId(-1);
            setSubCategoryId(-1);
            return $.when(null);
        } else {
            hideTransferToDropDown();
            showCategoryDropDown();
        }
        fillCategory();
    }

    function setCategoryId(value) {
        $('#CategoryId').val(value);
    }

    function setSubCategoryId(value) {
        $('#SubCategoryId').val(value);
    }

    function fillCategory() {
        var tranGetCategoriesUrl = pmt.rootPath+'Transactions/GetCategories';
        var tranType = $('#TransactionType').val();
        var categoryId = $('#CategoryId').val();
        var firstcategoryBtn = "";
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
                    var element = "<li><a data-id='" + option.CategoryId + "' href='#'><span><i class='fa fa-fw  " + option.IconId + "'></i> "
                        + option.Name + "</span></a></li>";
                    $('.dropdown-category').append(element);

                    if (firstcategoryBtn == "")
                        firstcategoryBtn = option.CategoryId;
                    if (categoryId == "" || categoryId=="0")
                        categoryId = firstcategoryBtn;
                    if (option.CategoryId == categoryId) {
                        var element = "<span><i class='fa fa-fw  " + option.IconId + "'></i> "
                        + option.Name + '</span>'+caretHtml;

                        $("#categoryBtn").html(element);
                        setCategoryId(categoryId);
                        setSubCategoryId("");
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
        var tranGetSubCategoriesUrl = pmt.rootPath+'Transactions/GetSubCategories';
        var categoryId = $("#CategoryId").val();
        var subCategoryId = $("#SubCategoryId").val();
        var firstsubCategoryBtn = "";
        var initialSubCategoryId = $("#initialSubCategoryId").val();
        $.getJSON(tranGetSubCategoriesUrl, { CategoryId: categoryId }, function (data) {
            $('.dropdown-subcategory li').remove();
            $.each(data, function (id, option) {
                var element = "<li><a data-id='" + option.SubCategoryId + "' href='#'><i class='fa fa-fw  " + option.IconId + "'></i> "
                    + option.Name + "</a></li>";
                $('.dropdown-subcategory').append(element);

                if (firstsubCategoryBtn == "")
                    firstsubCategoryBtn = option.SubCategoryId;
                if (subCategoryId == "" || subCategoryId == "0")
                    subCategoryId = firstsubCategoryBtn;
                if (initialSubCategoryId != "" && initialSubCategoryId != "0") {
                    subCategoryId = initialSubCategoryId;  
                }
                if (option.SubCategoryId == subCategoryId) {
                    var element = "<i class='fa fa-fw  " + option.IconId + "'></i> "
                    + option.Name + ' ' + caretHtml;
                    $("#subCategoryBtn").html(element);
                    $("#initialSubCategoryId").val("");
                    setSubCategoryId(subCategoryId);
                }
            });
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.log('Error getting subcategories!');
        });
    };

    function fillTransferToOnAccountChange() {
        var tranGetAccountsAvailableForTransfer = pmt.rootPath + 'Transactions/GetAccountsAvailableForTransfer';
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
            var newElement = $(this).html() + caretHtml;
            var id = $(this).data('id');

            $("#categoryBtn").html(newElement);
            setCategoryId(id);
            setSubCategoryId("");
            fillSubCategoryOnCategoryChange();
        });
    };

    function onSubCategoryChange() {
        $(document.body).on('click', '.dropdown-subcategory li > a', function () {
            var newElement = $(this).html() + caretHtml;
            var id = $(this).data('id');
            $("#subCategoryBtn").html(newElement);
            setSubCategoryId(id);
        });
    };

    function onTransactionTypeChange() {
        $("#TransactionType").change(function () {
            setCategoryId("");
            setSubCategoryId("");
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
