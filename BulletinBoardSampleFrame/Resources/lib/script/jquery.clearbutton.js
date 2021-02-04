
$(document).ready(function () {
	$('#btnClear').click(function () {
		if (confirm("Want to clear?")) {
			$('#form1 input[type="text"]').val('');
			$('#form1 #description').val('');
		}
	});
});