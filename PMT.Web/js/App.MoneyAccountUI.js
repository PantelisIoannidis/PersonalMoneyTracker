var moneyAccountUI = function () {
	function addIndexButtonEvents() {
		$(".accountTableDeleteButton").click(function (e) {
			e.preventDefault();
			var id = $(this).siblings(".deleteAccountId").val();
			$("#deleteTarget").val(id);
			$("#accountDeleteModal").modal("show");
		});
		$("#accountTableDeleteConfirmButton").on('click', function (e) {
		    e.preventDefault();
		    var deleteid = $("#deleteTarget").val();
            if (!deleteid) return;
		    var token = $('[name=__RequestVerificationToken]').val();
            $("#accountDeleteModal").modal("hide");
            var url = pmt.rootPath + "moneyAccounts/Delete/";
            var form = $('<form/>', {action: url, method: 'POST'});
            form.append($('<input/>', {type: 'hidden',name: 'id',value: deleteid}));
            form.append($('<input/>', {type: 'hidden',name: '__RequestVerificationToken',value: token}));
            form.appendTo('body').submit();
		});
	};

	return {
		addIndexButtonEvents: addIndexButtonEvents,
	};
}();