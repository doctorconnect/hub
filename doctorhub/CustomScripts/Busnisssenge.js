$(document).ready(function () {
    GetBusinessSegment();

    $("button[type=submit]").click(function () {
        var BusinessSegment = $('#txtBusinessSegment').val().trim().length;
        if (!BusinessSegment > 0) {
            $('#errorBusinessSegmentId').show();
            return false;
        }
    });
});

function GetBusinessSegment() {
    $.ajax({
        url: "/Admin/GetBusinessSegment",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageBusinessSegment').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'Id' },
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
                        'data': 'Id',
                        'sortable': false,
                        'visible': true,
                        'render': function (data, type, full, meta) {
                            return '<a title="Manage"  href="/Admin/ManageBusinessSegment?key=' + full.Id + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
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
