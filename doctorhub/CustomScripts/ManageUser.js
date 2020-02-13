$(document).ready(function () {
    var datatableInstance;
    CheckKeyId();
    GetUser();
    $("#chkAll").children().remove();
});

function CheckKeyId() {
    var keyValue = parseInt($('#hdKeyId').val()) || 0;

    if (keyValue > 0)
        $('.usersection-div').show();
    else
        $('.usersection-div').hide();
}

function GetUser() {
    $.ajax({
        url: "/Admin/GetManageUser",
        method: 'post',
        dataType: 'json',
        async: false,
        success: function (data) {
            datatableInstance = $('#tblManageUser').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'Id' },
                    {
                        'data': 'UserName',
                        'render': function (data, type, full, meta) {
                            return '<span>' + full.UserName + '(' + full.UserCode + ')' + '</span>'
                        }
                    },
                    { 'data': 'ManagerName' },
                    { 'data': 'RoleName' },
                    { 'data': 'BusinessSegmentName' },
                    { 'data': 'CapabilitiesName' },
                    { 'data': 'LOBName' },
                    { 'data': 'StatusType' },
                    {
                        'data': 'Id',
                        'sortable': false,
                        'visible': true,
                        'render': function (data, type, full, meta) {
                            if (full.Status === 5)
                                return '<a title="Manage" href="/Admin/ManageUser?key=' + full.Id + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a> &nbsp;&nbsp;<a onclick=ApproveStatus(' + full.Id + ');> <i class="glyphicon glyphicon-ok"></i> Approve</a>&nbsp;&nbsp;<a onclick=RejectStatus(' + full.Id + ');><i class="glyphicon glyphicon-remove"></i> Reject</a>';
                            else if (full.Status === 6)
                                return '<a title="Manage" href="/Admin/ManageUser?key=' + full.Id + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a> &nbsp;&nbsp;<a onclick=RejectStatus(' + full.Id + ');><i class="glyphicon glyphicon-remove"></i> Reject</a>';
                            else if (full.Status === 7)
                                return '';
                        }
                    }
                ],
                "columnDefs": [
                    {
                        "searchable": false,
                        'targets': 0,
                        'checkboxes': {
                            'selectRow': true
                        }
                    }],
                'select': {
                    'style': 'multi; margin-left:7px;'
                },
                'order': [[1, 'asc']]
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });
}

function ApproveStatus(Id) {
    var status = "Approve";

    $.confirm({
        title: '',
        type: 'green',
        content: '<p style="font-style:italic;">Are you sure, you want to Approve user request?</p>',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Admin/UpdateUserRequestStatus",
                    method: 'post',
                    dataType: 'json',
                    data: { UserId: Id, Status: status },
                    success: function (data) {
                        window.location.href = '/Admin/ManageUser';
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        window.location.href = "/Home/Error";
                    }
                });
            },
            cancel: function () {
            }
        }
    });
}

function RejectStatus(Id) {
    var status = "Reject";
    $.confirm({
        title: '',
        type: 'red',
        content: '<p style="font-style:italic;">Are you sure, you want to Reject user request?</p>',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Admin/UpdateUserRequestStatus",
                    method: 'post',
                    dataType: 'json',
                    data: { UserId: Id, Status: status },
                    success: function (data) {
                        window.location.href = '/Admin/ManageUser';
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        window.location.href = "/Home/Error";
                    }
                });
            },
            cancel: function () {
            }
        }
    });
}

function ApproveAllRequests() {
    var checkboxValues = [];

    var rows_selected = datatableInstance.column(0).checkboxes.selected();
    if (rows_selected.length > 0) {
        $.each(rows_selected, function (index, rowId) {
            checkboxValues.push(rowId);
        });
    } else {
        alert("Please select the checkbox(s)");
        return false;
    }

    $.ajax({
        url: "/Admin/UpdateBulkRequestStatus",
        method: 'post',
        dataType: 'json',
        data: { RequestIds: checkboxValues },
        success: function (data) {
            window.location.href = '/Admin/ManageUser';
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });

}

function RejectAllRequests() {
    var checkboxValues = [];

    var rows_selected = datatableInstance.column(0).checkboxes.selected();
    if (rows_selected.length > 0) {
        $.each(rows_selected, function (index, rowId) {
            checkboxValues.push(rowId);
        });
    } else {
        alert("Please select the checkbox(s)");
        return false;
    }

    $.ajax({
        url: "/Admin/UpdateBulkRequestStatusReject",
        method: 'post',
        dataType: 'json',
        data: { RequestIds: checkboxValues },
        success: function (data) {
            window.location.href = '/Admin/ManageUser';
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });

}
    