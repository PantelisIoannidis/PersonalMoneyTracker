var categoriesUI = function () {
	function addIndexButtonEvents() {

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
	}


	return {
	    onLoadIndexInit, onLoadIndexInit
	};
}();