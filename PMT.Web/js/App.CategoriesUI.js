var categoriesUI = function () {

    function ajaxCall(_id,_url) {
        var token = $('[name=__RequestVerificationToken]').val();
        $.ajax({
            type: 'POST',
            data: { __RequestVerificationToken: token, id: _id },
            url: _url,
            success: function (data) {
                $('#categoriesPanel').load(pmt.rootPath + "Categories/LoadIndexPanelPartial/",
                    function (responseTxt, statusTxt, xhr) {
                        calculateIndexActiveElement();
                        dropdownMenuToggle();
                    }
                );
                commonUI.showSuccessNotifications(data.message);
            },
            fail: function (jqXHR, textStatus, errorThrown) {
                commonUI.showWarningNotifications(Resources.ActionWasNotCompleted);
            }
        });
    }

    function indexEvents() {

	    $("#deleteCategoryBtn").click(function (e) {
	        e.preventDefault();
	        var deleteId = $("#selectedCategory").val();
            if (!deleteId) {
                commonUI.showWarningNotifications(Resources.PleaseSelectACategoryOrSubcategory);
                return;
            }
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
            if (!deleteId) {
                commonUI.showWarningNotifications(Resources.PleaseSelectACategory);
                return;
            }
            $("#categoryDeleteModal").modal("hide");
            ajaxCall(deleteId, pmt.rootPath + "Categories/Delete/",)
        });

	    $("#subCategoryDeleteConfirmButton").on('click', function (e) {
	        e.preventDefault();
	        var deleteId = $("#selectedCategory").val();
            if (!deleteId) {
                commonUI.showWarningNotifications(Resources.PleaseSelectASubcategory);
                return;
            }
            $("#subCategoryDeleteModal").modal("hide");
            ajaxCall(deleteId, pmt.rootPath + "Categories/Delete/", )
        });

	    $("#editCategoryBtn").click(function (e) {
	        e.preventDefault();
	        var editId = $("#selectedCategory").val();
            if (!editId) {
                commonUI.showWarningNotifications(Resources.PleaseSelectACategoryOrSubcategory);
                return;
            };
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
	    $('ul.categoryList > li > a').on("click",function () {
	        $this = $(this);
            $this.find("i.facaret").toggleClass("fa-caret-right fa-caret-down");
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
        onLoadIndexInit: onLoadIndexInit

	};
}();