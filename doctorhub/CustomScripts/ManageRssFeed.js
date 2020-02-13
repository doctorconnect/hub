$(document).ready(function () {
    GetRSSFeed();

    $("button[type=submit]").click(function () {
        var FaqQuestion = $('#txtTitle').val().trim().length;
        var FaqAnswer = $('#txtUrl').val().trim().length;
        if (!FaqQuestion > 0 && !FaqAnswer > 0) {
            $('#errortitleId').show();
            $('#errorUrlId').show();
            return false;
        }
    });
});

function GetRSSFeed() {
    $.ajax({
        url: "/Admin/GetRSS",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageRS').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [                   
                    { 'data': 'Title' },
                    { 'data': 'Url' },
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
                            return '<a title="Manage"  href="/Admin/ManageRSS?key=' + full.Id + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
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

