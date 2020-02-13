$(document).ready(function () {
    $("#txtFromDate").attr('readOnly', 'true');
    $("#txtToDate").attr('readOnly', 'true');
    $("#txtFromDate").datepicker({
        minDate : new Date(),
        onSelect: function (selectedDate) {
            $("#txtToDate").val(selectedDate);
            $("#txtToDate").datepicker("option", { minDate: new Date(selectedDate) })
        }
    });

    $("#txtToDate").datepicker({
        onSelect: function (selectedDate) {
            toDate = new Date(selectedDate);
        }
    });

    $("#chbIsActive").prop('checked', true);
    GetQuiz();

    $("button[type=submit]").click(function () {
        var txtQuiz = $('#txtQuiz').val().trim().length;
        var FromDate = $('#txtFromDate').val().trim().length;
        var ToDate = $('#txtToDate').val().trim().length;
        if (!txtQuiz > 0 || !FromDate > 0 || !ToDate > 0) {
            $('#errorAssessmentId').show();
            $('#errorfromdateId').show();
            $('#errortodateId').show();
            return false;
        }
    });

});

function GetQuiz() {
    $.ajax({
        url: "/Quiz/GetQuiz",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageQuiz').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'QuizName' },
                    {
                        'data': 'FromDate',
                        'render': function (jsonDate) {
                            var date = new Date(parseInt(jsonDate.substr(6)));
                            var month = date.getMonth() + 1;
                            var fulldate = month + "/" + date.getDate() + "/" + date.getFullYear();
                            fulldate = ChangeDateFormatToName(fulldate);

                            return fulldate;
                        }
                    },
                    {
                        'data': 'ToDate',
                        'render': function (jsonDate) {
                            var date = new Date(parseInt(jsonDate.substr(6)));
                            var month = date.getMonth() + 1;
                            var fulldate = month + "/" + date.getDate() + "/" + date.getFullYear();
                            fulldate = ChangeDateFormatToName(fulldate);

                            return fulldate;
                        }
                    },
                    { 'data': 'IsActive' },
                    { 'data': 'CreatedBy' },
                    {
                        'data': 'QuizID',
                        'sortable': false,
                        'visible': true,
                        'render': function (data, type, full, meta) {
                            return '<a title="Manage"  href="/Quiz/ManageQuiz?key=' + full.QuizID + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
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

function ChangeDateFormatToName(inputDate) {
    const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    var date = inputDate.split('/');
    date = date[1] + "-" + (monthNames[date[0] - 1]) + "-" + date[2];

    return date;
}

