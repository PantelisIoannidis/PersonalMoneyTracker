var categoriesCreateUI = function () {
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
	    createEvents();
	
	};

return {
	    onLoadCreateInit: onLoadCreateInit
	};
}();