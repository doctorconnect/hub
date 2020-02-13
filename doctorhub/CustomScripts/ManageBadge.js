$(document).ready(function () {
    GetBadge();
    $("button[type=submit]").click(function () {
        var BadgeName = $('#txtBadgeName').val().trim().length;
        var BadgePoint = $('#txtBadgePoint').val().trim().length;
        var BadgePointTo = $('#txtBadgePointTo').val().trim().length;
        var fileExt = $('#fileUpload').val().split('.').pop().toLowerCase();
        if ($.inArray(fileExt, ['jpeg', 'jpg', 'png']) === -1) {
            $('#errorBannerSpan').show();
            return false;
        }
        if (!BadgeName > 0 && !BadgePoint > 0 && !BadgePointTo > 0) {
            $('#errorBadgeNameId').show();
            $('#errorBadgePointId').show();
            $('#errorBadgePointIdTo').show();
            return false;
        }
    });
});

function GetBadge() {
    $.ajax({
        url: "/Admin/GetBadge",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageBadge').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'Id' },
                    {
                        "data": "BImage",
                        "render": function (data) {
                            return '<img src="data:image/png;base64,' + data + '" />';
                        }
                    },
                    { 'data': 'BadgeName' },
                    {
                        'data': 'BadgePoint',
                        'render': function (data, type, full, meta) {
                            return '<span>' + full.BadgePoint+'-'+ full.BadgePointTo+'</span>'
                        }
                    },
                    { 'data': 'IsActive' },
                    { 'data': 'CreatedBy' },
                    {
                        'data': 'CreatedOn',
                        'render': function (jsonDate) {
                            var date = new Date(parseInt(jsonDate.substr(6)));
                            var month = date.getMonth() + 1;
                            return month + "/" + date.getDate() + "/" + date.getFullYear();
                        }
                    },
                    {
                        'data': 'Id',
                        'sortable': false,
                        'visible': true,
                        'render': function (data, type, full, meta) {
                            return '<a title="Manage"  href="/Admin/ManageBadge?key=' + full.Id + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
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

-----------------------ManageAnswer.js---------------------
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
    