$(document).ready(function () {
    GetBusinessSegment();

    $("button[type=submit]").click(function () {
        var BusinessSegment = $('#txtLob').val().trim().length;
        if (!BusinessSegment > 0) {
            $('#errorLobId').show();
            return false;
        }
    });
});

function GetBusinessSegment() {
    $.ajax({
        url: "/Admin/GetLineofBusiness",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageLineofBusiness').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'Id' },
                    { 'data': 'BsName' },
                    { 'data': 'CapName' },
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
                            return '<a title="Manage"  href="/Admin/ManageLineofBusiness?key=' + full.Id + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
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
