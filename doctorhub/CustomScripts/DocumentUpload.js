$(document).ready(function () {
    $('.ddlValidation').change(function () {
        var CatId = parseInt($('#ddlCAT').val()) || 0;
        if (CatId == 0)
            $('.btnSubmit').prop("disabled", true);
        else
            $('.btnSubmit').prop("disabled", false);
    });

});

$('#filemessage').keypress(function () {
    var maxLength = $(this).val().length;
    if (maxLength >= 45) {
        alert('You cannot enter more than ' + maxLength + ' chars');
        return false;
    }
});