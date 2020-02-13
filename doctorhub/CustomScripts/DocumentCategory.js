$(document).ready(function () {
    GetBusinessSegment();

    $("#btnSave").click(function () {
        var BusinessSegment = $('#txtDocumentCategory').val().trim().length;
        if (!BusinessSegment > 0) {
            $('#errorDocumentCategoryId').show();
            return false;
        }
    });
});

function GetBusinessSegment() {
    $.ajax({
        url: "/Admin/GetDocumentCategory",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblDocumentCategory').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'Name' },
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
                        'data': 'ID',
                        'sortable': false,
                        'visible': true,
                        'render': function (data, type, full, meta) {
                            return '<a title="Manage"  href="/Admin/ManageDocumentCategory?key=' + full.ID + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
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
