$(document).ready(function () {
    GetAnswer();

    $("button[type=submit]").click(function () {
        var BusinessSegment = $('#txtAnswer').val().trim().length;
        if (!BusinessSegment > 0) {
            $('#errorAnswerId').show();
            return false;
        }
    });
});

function GetAnswer() {
    $.ajax({
        url: "/Quiz/GetAnswer",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageAnswer').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'AnswerID' },
                    { 'data': 'QuestionText' },
                    { 'data': 'AnswerText' },
                    {
                        'data': 'AnswerID',
                        'sortable': false,
                        'visible': true,
                        'render': function (data, type, full, meta) {
                            return '<a title="Manage"  href="/Quiz/ManageAnswer?key=' + full.AnswerID + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
                        }
                    }
                ],
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });
}