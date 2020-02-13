$(document).ready(function () {
    GetQuestiont();    
        var CatId = parseInt($('#ddlQuiz').val()) || 0;
        if (CatId == 0)
            $('.btnSubmit').prop("disabled", true);
        else
            $('.btnSubmit').prop("disabled", false);
   

    $("button[type=submit]").click(function () {
        var Question = $('#txtQuestion').val().trim().length;
        var ChoiceA = $('#txtChoiceA').val().trim().length;
        var ChoiceB = $('#txtChoiceB').val().trim().length;
        var isCheckedA = $("#chbIsAnswerA").is(":checked");
        var isCheckedB = $("#chbIsAnswerB").is(":checked");
        var isCheckedC = $("#chbIsAnswerC").is(":checked");
        var isCheckedD = $("#chbIsAnswerD").is(":checked");        
        if (!Question > 0 || !ChoiceA > 0 || !ChoiceB > 0 ) {
            $('#errorQuestionId').show();
            $('#errorChoiceIdA').show();
            $('#errorChoiceIdB').show();
            
            return false;
        }

        if (isCheckedA || isCheckedB || isCheckedC || isCheckedD) {

        } else {
            alert("Please checked One Correct Answer");
            return false;
        }
    });
});

function GetQuestiont() {
    $.ajax({
        url: "/Quiz/GetQuestion",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageQuestion').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'QuizName' },
                    { 'data': 'QuestionText' },
                    {
                        'data': 'AnswerText',
                        'render': function (data, type, full, meta) {
                            var AnswerText = full.AnswerText;
                            return '<span style="color:green">' + AnswerText + '</span>';
                        }
                    },
                    {
                        'data': 'ChoiceText',
                        'render': function (data, type, full, meta) {
                            var choice = full.ChoiceText.split('/');
                            var ch = "";
                            for (var i = 0; i < choice.length; i++) {
                                ch = ch + choice[i].replace('!X', '') + "<br>";                               
                            }
                            return '<span>' + ch + '</span>';
                        }
                    },
                    {
                        'data': 'QuestionID',
                        'sortable': false,
                        'visible': true,
                        'render': function (data, type, full, meta) {
                            return '<a title="Manage"  href="/Quiz/ManageQuestion?key=' + full.QuestionID + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
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
