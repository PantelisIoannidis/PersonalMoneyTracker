var categoriesCreateUI = function () {

    var caretHtml = '<span class="caret"></span>';

    function fillCategory() {
        var tranGetCategoriesUrl = '/Transactions/GetCategories';
        var tranType = $('#Type').val();
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
                    if (categoryId == "" || categoryId == "0")
                        categoryId = firstcategoryBtn;
                    if (option.CategoryId == categoryId) {
                        var element = "<span><i class='fa fa-fw  " + option.IconId + "'></i> "
                        + option.Name + '</span>' + caretHtml;

                        $("#categoryBtn").html(element);
                        $('#CategoryId').val(categoryId);
                        $('#SubCategoryId').val("");
                    }
                });
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

    function onCategoryChange() {
        $(document.body).on('click', '.dropdown-category li > a', function () {
            var newElement = $(this).html() + caretHtml;
            var id = $(this).data('id');

            $("#categoryBtn").html(newElement);
            $('#CategoryId').val(id);
        });
    };

    function onCategoryTypeChange() {
        $(document.body).on('click', '#CategoryType > option', function () {
            var id = $(this).val();
            $("#Type").val(id);
            $('#CategoryId').val("");
            fillCategory();
        });
    };

    

    function createEvents() {


	    $("#html5picker").change(function () {
	        var color = $("#html5picker").get(0).value;
	        $("#Color").val(color);
	    });

		$("#chooseIconBtn").click(function (e) {
			e.preventDefault();
			$("#iconSelectionModal").modal("show");
			loadIcons();

		});
		$(document.body).on('click', ".selectedIconBtn", function (e) {
		    $this = $(this);
		    $chooseIconBtn = $("#chooseIconBtn");
		    e.preventDefault();
		    var iconId = $this.data('id');

		    $("#IconId").val(iconId);
		    $chooseIconBtn.children("i").removeClass();
		    $chooseIconBtn.children("i").addClass("fa fa-fw " + iconId);
			$("#iconSelectionModal").modal("hide");
		});
	}

	function loadIcons() {
	    var url = "/Categories/GetAllIcons";
	    $.getJSON(url, null, function (data) {
	        var template = Handlebars.compile($('#iconCollectionTemplate').html());
	        var temp = template(data);
	        $("#iconCollection").html(temp);
	    }).fail(function (jqXHR, textStatus, errorThrown) {
	        console.log('Error getting icons!');
	    });;
	    
	}

	function onLoadCreateInit() {
	    fillCategory();
	    onCategoryChange();
	    onCategoryTypeChange();
	    createEvents();
	    
	
	};

return {
	    onLoadCreateInit: onLoadCreateInit
	};
}();