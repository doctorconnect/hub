$(document).ready(function () {
    GetPendingFeedback();
    GetPagination();
    $("#chkParent").children().remove();
});

function GetPendingFeedback() {
    $.ajax({
        url: "/Admin/GetApprovePostDetails",
        type: "POST",
        dataType: "JSON",
        data: { Status: '0' },
        async: false,
        success: function (data) {
            if (data.length > 0) {
                var trHTML = '';
                $.each(data, function (i, item) {
                    //trHTML += '<tr><td style="width:2%;"><input type="checkbox"/></td><td style="width:28%;"><table class= "custum-grid-table" style="font-size:12px;"><tr><td><b>Flag Count</b>: ' + data[i].FlagCount + '</td></tr><tr><td><b>Post By</b>: ' + data[i].PostedByName + '</td></tr><tr><td><b>Time</b>: ' + data[i].PostedDate + '</td></tr></table></td><td style="width:33%;">' + data[i].Message + '</td><input type="hidden" id="hdDocId" value=' + data[i].PostId + '><td><textarea style="width:100%"/></td></tr>';
                    trHTML += '<tr><td style="width:2%;"><input type="checkbox"/></td><td style="width:28%;"><table class= "custum-grid-table" style="font-size:12px;"><tr><td><b>Flag Count</b>: ' + data[i].FlagCount + '</td></tr><tr><td><b>Post By</b>: ' + data[i].PostedByName + '</td></tr><tr><td><b>Time</b>: ' + data[i].PostedDate + '</td></tr></table></td><td style="width:33%;">' + data[i].Message + '</td><input type="hidden" id="hdDocId" value=' + data[i].PostId + '></tr>';
                });
                $('#tblPendingRequest tbody').append(trHTML);
            }
            else {
                $('.btnUpdate').prop("disabled", true);
                $('#tblPendingRequest tbody').append('<tr><td></td><td style="color:green;font-size:15px;">No Flagged post.</td><td></td><td></td></tr>');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });
}

function GetCompletedFeedback() {
    $.ajax({
        url: "/Admin/GetApprovePostDetails",
        type: "POST",
        dataType: "JSON",
        data: { Status: '1' },
        async: false,
        success: function (data) {
            if (data.UploadBy !== "") {
                var trHTML = '';
                $.each(data, function (i, item) {
                    trHTML += '<tr><td style="width:28%;"><table class= "custum-grid-table" style="font-size:12px;"><tr><td><b>Flag Count</b>: ' + data[i].FlagCount + '</td></tr><tr><td style=""><b>Post By</b>: ' + data[i].PostedByName + '</td></tr><tr><td><b>Blog Title</b>: ' + data[i].PostedDate + '</td></tr></table></td><td>' + data[i].Message + '</td><td>' + data[i].Remarks + '</td></tr>';
                });
                $('#tblApprovedRequest tbody').append(trHTML);
            }
            else {
                alert("Invalid input or record doesn't exist");
                return false;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });
}

function UnFlagPost() {
    var checkboxValues = [];

    $('#tblPendingRequest tbody').find('tr').each(function (i) {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            var id = row.find('input[type="hidden"]').val();
            var Identifier = "UnFlag";
            var val = id + '^' + Identifier;
            checkboxValues.push(val)
        }
        return checkboxValues;
    });

    if (!$.isEmptyObject(checkboxValues)) {
        $.confirm({
            title: '',
            type: 'green',
            content: '<p style="font-style:italic;">Are you sure you want to Unflag the post?</p>',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: "/Admin/UnFlagPost",
                        type: "POST",
                        dataType: "JSON",
                        data: { Id: checkboxValues },
                        success: function (data) {
                            window.location.href = "/admin/ApprovePost"
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
}

function DeletePost() {
    var checkboxValues = [];

    $('#tblPendingRequest tbody').find('tr').each(function (i) {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            var id = row.find('input[type="hidden"]').val();
            var Identifier = "POST";
            var val = id + '^' + Identifier;
            checkboxValues.push(val)
        }
        return checkboxValues;
    });

    if (!$.isEmptyObject(checkboxValues)) {
        $.confirm({
            title: '',
            type: 'red',
            content: '<p style="font-style:italic;">Are you sure you want to Delete Selected Post?</p>',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: "/Admin/DeleteFlagPost",
                        type: "POST",
                        dataType: "JSON",
                        data: { Id: checkboxValues },
                        success: function (data) {
                            window.location.href = "/admin/ApprovePost"
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
}

function GetPagination() {
    var myTable = "#tblApprovedRequest";
    var myTableBody = myTable + " tbody";
    var myTableRows = myTableBody + " tr";
    var myTableColumn = myTable + " th";

    function initTable() {
        $(myTableBody).attr("data-pageSize", 50);
        $(myTableBody).attr("data-firstRecord", 0);
        $('#previous').hide();
        $('#next').show();

        paginate(parseInt($(myTableBody).attr("data-firstRecord"), 10),
            parseInt($(myTableBody).attr("data-pageSize"), 10));
    }
    $(myTableColumn).click(function () {
        paginate(parseInt($(myTableBody).attr("data-firstRecord"), 10),
            parseInt($(myTableBody).attr("data-pageSize"), 10));
    });

    $("a.paginate").click(function (e) {
        e.preventDefault();
        var tableRows = $(myTableRows);
        var tmpRec = parseInt($(myTableBody).attr("data-firstRecord"), 10);
        var pageSize = parseInt($(myTableBody).attr("data-pageSize"), 10);

        if ($(this).attr("id") == "next") {
            tmpRec += pageSize;
        } else {
            tmpRec -= pageSize;
        }
        if (tmpRec < 0 || tmpRec > tableRows.length) return
        $(myTableBody).attr("data-firstRecord", tmpRec);

        paginate(tmpRec, pageSize);
    });

    var paginate = function (start, size) {
        var tableRows = $(myTableRows);
        var end = start + size;
        tableRows.hide();
        tableRows.slice(start, end).show();
        $(".paginate").show();
        if (tableRows.eq(0).is(":visible")) $('#previous').hide();
        if (tableRows.eq(tableRows.length - 1).is(":visible")) $('#next').hide();
    }
    initTable();
}
