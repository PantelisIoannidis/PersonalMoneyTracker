var categoriesUI = function () {

    function ShowWarningModal(message) {
        if (message === "null") {

        } else
            if (message === "notFound") {

            }
    }

    function indexEvents() {

	    $("#deleteCategoryBtn").click(function (e) {
	        e.preventDefault();
	        var deleteId = $("#selectedCategory").val();
	        if (!deleteId) return;
	        var name = $("#" + deleteId).data("name");
	        $(".categoryNameToDelete").text(name);
            if(deleteId.indexOf("sub")===-1)
                $("#categoryDeleteModal").modal("show");
	        else
                $("#subCategoryDeleteModal").modal("show");

	    });
	    $("#categoryDeleteConfirmButton").on('click', function (e) {
	        e.preventDefault();
	        var deleteId = $("#selectedCategory").val();
	        if (!deleteId) return;
	        var token = $('[name=__RequestVerificationToken]').val();
	        $("#categoryDeleteModal").modal("hide");
	        $.post(pmt.rootPath + "Categories/Delete/", { __RequestVerificationToken: token, categoryId: deleteId },
                function (retURL) { window.location.reload(true); });
	    });
	    $("#subCategoryDeleteConfirmButton").on('click', function (e) {
	        e.preventDefault();
	        var deleteId = $("#selectedCategory").val();
	        if (!deleteId) return;
	        var token = $('[name=__RequestVerificationToken]').val();
	        $("#categoryDeleteModal").modal("hide");
	        $.post(pmt.rootPath + "Categories/Delete/", { __RequestVerificationToken: token, categoryId: deleteId },
                function (retURL) { window.location.reload(true); });
	    });
	    $("#editCategoryBtn").click(function (e) {
	        e.preventDefault();
	        var editId = $("#selectedCategory").val();
	        if (!editId) return;
	        var newUrl = pmt.rootPath+"Categories/Edit/" + editId;
	        document.location.href = newUrl;

	    });
	    $("#newCategoryBtn").click(function (e) {
	        e.preventDefault();
	        var newUrl = pmt.rootPath + "Categories/NewCategory/";
	        document.location.href = newUrl;

	    });

	    $("#newSubCategoryBtn").click(function (e) {
	        e.preventDefault();
	        var newUrl = pmt.rootPath + "Categories/NewSubCategory/";
	        document.location.href = newUrl;

	    });
	};

	function calculateIndexActiveElement() {
	    var $currentLink = $(".categoryListItem, .subCategoryListItem");
	    $currentLink.click(function (e) {
	        $this = $(this);
	        $(".categoryListItem, .subCategoryListItem").removeClass('active');
	        $this.addClass('active');
	        var value = $this.data('target');
	        $("#selectedCategory").val(value);
	        return false;
	    });
	};

	function dropdownMenuToggle() {
	    $('ul.categoryList > li > a').click(function () {
	        $this = $(this);
            $this.find("i").toggleClass("glyphicon-chevron-right glyphicon-chevron-down");
            var $target = $this.data("target");
            $('#' + $target).toggleClass("in");
	    });
	}

	function onLoadIndexInit() {
	    indexEvents();
	    dropdownMenuToggle();
	    calculateIndexActiveElement();
	}


	return {
        onLoadIndexInit: onLoadIndexInit,
        CallBackFromController: CallBackFromController

	};
}();