$(document).ready(function () {
    GetAnswer();

    $("button[type=submit]").click(function () {
        var BusinessSegment = $('#txtChoice').val().trim().length;
        if (!BusinessSegment > 0) {
            $('#errorChoiceId').show();
            return false;
        }
    });
});

function GetAnswer() {
    $.ajax({
        url: "/Quiz/GetQuizChoice",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageChoice').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'QuestionID' },
                    { 'data': 'QuestionText' },
                    { 'data': 'ChoiceText' },
                    {
                        'data': 'ChoiceID',
                        'sortable': false,
                        'visible': true,
                        'render': function (data, type, full, meta) {
                            return '<a title="Manage"  href="/Quiz/ManageQuizChoice?key=' + full.ChoiceID + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
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

