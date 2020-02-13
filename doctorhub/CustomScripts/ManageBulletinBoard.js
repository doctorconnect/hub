$(document).ready(function () {
    GetBullentinBoard();

    $("button[type=submit]").click(function () {
        // debugger;
        var name = $('#txtName').val().trim().length;
        var title = $('#txtTitle').val().trim().length;
        var des = $('#txtDescription').val().trim().length;
        var art = $('#txtArticle').val().trim().length;

        if (!name > 0 && !title > 0 && !des > 0 && !art > 0) {
            return false;
        }
    });
});

function GetBullentinBoard() {
    $.ajax({
        url: "/Admin/GetBulletinBoard",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageBulletinBoard').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                responsive: true,
                data: data,
                columns: [
                    { 'data': 'Name' },
                    { 'data': 'Title' },
                    { 'data': 'Description' },
                    { 'data': 'Article' },
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
                            return '<a title="Manage"  href="/Admin/ManageBulletinBoard?key=' + full.Id + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
                        }
                    }
                ],
            });
        }
    });
}
