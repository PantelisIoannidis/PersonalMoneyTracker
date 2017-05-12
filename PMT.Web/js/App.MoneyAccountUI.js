var moneyAccountUI = function () {
	function addIndexButtonEvents() {
		$(".accountTableDeleteButton").click(function (e) {
			e.preventDefault();
			var id = $(this).siblings(".deleteAccountId").val();
			$("#deleteTarget").val(id);
			$("#accountDeleteModal").modal("show");
		});
	};
	$("#accountTableDeleteConfirmButton").on('click', function (e) {
		e.preventDefault();
		var id = $("#deleteTarget").val();
		if (!id) return;
		var token = $('[name=__RequestVerificationToken]').val();
		$("#accountDeleteModal").modal("hide");
		$.post("/moneyAccounts/Delete/", { __RequestVerificationToken: token, id: id },
			function (retURL) { window.location.reload(true); });
	});
	return {
		addIndexButtonEvents: addIndexButtonEvents,
	};
}();