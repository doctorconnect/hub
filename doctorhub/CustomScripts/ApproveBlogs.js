$(document).ready(function () {
    GetPendingFeedback();
    GetCompletedFeedback();
    GetPagination();
    $("#chkParent").children().remove();
});


function GetPendingFeedback() {
    $.ajax({
        url: "/Admin/GetApproveBlosDetails",
        type: "POST",
        dataType: "JSON",
        data: { Status: '0' },
        async: false,
        success: function (data) {
            if (data.length > 0) {
                var trHTML = '';
                $.each(data, function (i, item) {
                    trHTML += '<tr><td style="width:2%;"><input type="checkbox"/></td><td style="width:28%;"><table class= "custum-grid-table" style="font-size:12px;"><tr><td><b>Blog By</b>: ' + data[i].BlogBy + '</td></tr><tr><td><b>Blog Title</b>: ' + data[i].Title  + '</td></tr></table></td><td style="width:33%;">' + data[i].Message + '</td><input type="hidden" id="hdDocId" value=' + data[i].BlogId + '><td><textarea style="width:100%"/></td></tr>';
                });
                $('#tblPendingRequest tbody').append(trHTML);
            }
            else {
                $('.btnUpdate').prop("disabled", true);
                $('#tblPendingRequest tbody').append('<tr><td></td><td style="color:red;font-size:15px;">No pending Blog is available For Approval.</td><td></td><td></td></tr>');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });
}

function GetCompletedFeedback() {
    $.ajax({
        url: "/Admin/GetApproveBlosDetails",
        type: "POST",
        dataType: "JSON",
        data: { Status: '1' },
        async: false,
        success: function (data) {
            if (data.UploadBy !== "") {
                var trHTML = '';
                $.each(data, function (i, item) {
                    trHTML += '<tr><td style="width:28%;"><table class= "custum-grid-table" style="font-size:12px;"><tr><td style=""><b>Blog By</b>: ' + data[i].BlogBy + '</td></tr><tr><td><b>Blog Title</b>: ' + data[i].Title +  '</td></tr></table></td><td>' + data[i].Message + '</td><td>' + data[i].Remarks + '</td></tr>';
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

function SubmitApproval() {
    var checkboxValues = [];

    $('#tblPendingRequest tbody').find('tr').each(function (i) {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            var id = row.find('input[type="hidden"]').val();
            var Remarks = row.find('textarea').val();
            if (Remarks === "") {
                alert("You must fill the text area!")
                return false;
            }
            var val = id + '^' + Remarks;
            checkboxValues.push(val)
        }
        return checkboxValues;
    });

    if (!$.isEmptyObject(checkboxValues)) {
        $.ajax({
            url: "/Admin/ApproveBlosStatus",
            type: "POST",
            dataType: "JSON",
            data: { Id: checkboxValues },
            success: function (data) {
                window.location.href = "/admin/ApproveBlogs"
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                window.location.href = "/Home/Error";
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
