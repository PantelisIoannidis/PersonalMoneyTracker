var categoriesUI = function () {
	function addIndexButtonEvents() {
	    $("#deleteCategoryBtn").click(function (e) {
	        e.preventDefault();
	        var deleteId = $("#selectedCategory").val();
	        if (!deleteId) return;
	        $("#categoryDeleteModal").modal("show");

	    });
	    $("#categoryDeleteConfirmButton").on('click', function (e) {
	        e.preventDefault();
	        var deleteId = $("#selectedCategory").val();
	        if (!deleteId) return;
	        var token = $('[name=__RequestVerificationToken]').val();
	        $("#categoryDeleteModal").modal("hide");
	        $.post("/Categories/Delete/", { __RequestVerificationToken: token, id: deleteId },
                function (retURL) { window.location.reload(true); });
	    });
	    $("#editCategoryBtn").click(function (e) {
	        e.preventDefault();
	        var editId = $("#selectedCategory").val();
	        if (!editId) return;
	        var newUrl = "/Categories/Edit/" + editId;
	        document.location.href = newUrl;

	    });
	};

	function calculateIndexActiveElement() {
	    var $currentLink = $(".categoryListItem, .subCategoryListItem");
	    $currentLink.click(function (e) {
	        $this = $(this);
	        $(".categoryListItem, .subCategoryListItem").removeClass('active');
	        $this.addClass('active');
	        var value = $this.find('a:first-child').data('target');
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
	    addIndexButtonEvents();
	    dropdownMenuToggle();
	    calculateIndexActiveElement();
	}


	return {
	    onLoadIndexInit: onLoadIndexInit

	};
}();