var transactionsUI = function () {

    var calculateIndexActiveElement = function () {
        var $currentLink = $(".transTable > tbody > tr");
        $currentLink.click(function (e) {
            $this = $(this);
            $currentLink.siblings().removeClass('active').removeClass("info");
            $this.addClass('active').addClass("info");
        });
    };

    var addIndexButtonEvents = function () {
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

    var FillCategoryOnLoad = function () {
        var tranGetCategoriesUrl = '/Transactions/GetCategories';
        var tranType = $('#TransactionType').val();
        return $.ajax({
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'GET',
            cache: false,
            data: { type: tranType },
            url: tranGetCategoriesUrl,
            success: function (data) {
                $('#CategoryId option').remove();
                $.each(data, function (id, option) {
                    var element = "<li><a data-id='" + option.CategoryId + "' href='#'><i class='fa fa-fw  " + option.IconId + "'></i> "
                        + option.Name + "</a></li>";
                    //$('#CategoryId').append("<option value=" + option.CategoryId + "><i class='fa fa-fw  " + option.IconId + "'></i>" + option.Name + "</option>");
                    $('.dropdown-category').append(element);
                })
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

    var FillSubCategoryOnCategoryChange = function () {
        var tranGetSubCategoriesUrl = '/Transactions/GetSubCategories';
        //var categoryId = $("#CategoryId").val();
        var categoryId = $("#CategoryId").attr('data-id');
        $.getJSON(tranGetSubCategoriesUrl, { categoryId: categoryId }, function (data) {
            $('#SubCategoryId option').remove();
            $.each(data, function (id, option) {
                $('#SubCategoryId').append("<option value=" + option.SubCategoryId + "><i class='fa fa-fw  " + option.IconId + "'></i>" + option.Name + "</option");
            });
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.log('Error getting subcategories!');
        });
    };

    var onCategoryChange = function () {
        $("#CategoryId").change(function () {
            FillSubCategoryOnCategoryChange();
        });
    };

    var FillTransferToOnAccountChange = function () {
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

    var onAccountChange = function () {
        $("#MoneyAccountId").change(function () {
            FillTransferToOnAccountChange();
        });
    };

    var onLoadCreateInit = function () {
        $.when(
            FillCategoryOnLoad(),
            FillTransferToOnAccountChange()
            ).done(function () {
                FillSubCategoryOnCategoryChange();
                onCategoryChange();
                onAccountChange();
            });

        $(document.body).on('click', '.dropdown-category li > a', function () {
                var element = $(this).html() + ' <span class="caret"></span>';
                var id = $(this).data('id');
                
                $("#CategoryId").html(element);
                $("#CategoryId").attr('data-id', id);
                FillSubCategoryOnCategoryChange();
        });
    };

    return {
        calculateIndexActiveElement: calculateIndexActiveElement,
        addIndexButtonEvents: addIndexButtonEvents,
        onLoadCreateInit: onLoadCreateInit,
    };

}();
