$(document).ready(function () {
    GetAssessmentResult();
});

function GetAssessmentResult() {
    $.ajax({
        url: "/Admin/GetAssessmentResult",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblAssessmentResult').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                dom: 'Bfrtip',
                buttons: [
                    'copyHtml5', 'print', { extend: 'csvHtml5', filename: 'Assessment Result' },
                    { extend: 'excelHtml5', filename: 'Assessment Result' }
                ],
                columns: [
                    { 'data': 'UserName' },
                    { 'data': 'QuizName' },
                    { 'data': 'Marks' },
                    { 'data': 'status' }
                ],
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });
}

function GenerateResult() {
    var QuizText = document.getElementById("ddlQuiz");
    var QuizNameText = QuizText.options[QuizText.selectedIndex].text;
    GetSelectAssessmentResult(QuizNameText)
}

function GetSelectAssessmentResult(QuizNameText) {
    $.ajax({
        url: "/Admin/GetSelectAssessmentResult",
        method: 'post',
        dataType: 'json',
        data: { QuizNameText: QuizNameText },
        success: function (data) {
            var datatableInstance = $('#tblAssessmentResult').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                dom: 'Bfrtip',
                buttons: [
                    'copyHtml5', 'print', { extend: 'csvHtml5', filename: 'Assessment Result' },
                    { extend: 'excelHtml5', filename: 'Assessment Result' }
                ],
                columns: [
                    { 'data': 'UserName' },
                    { 'data': 'QuizName' },
                    { 'data': 'Marks' },
                    { 'data': 'status' }
                ],
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });
}
    