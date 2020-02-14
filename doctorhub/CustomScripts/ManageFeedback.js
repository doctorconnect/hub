$(document).ready(function () {
    GetPendingFeedback();
    GetCompletedFeedback();
    GetPagination();
    $("#chkParent").children().remove();
});

//$('#chkParent').click(function (e) {
//    $(this).closest('table').find('td input:checkbox').prop('checked', this.checked);
//});

function GetPendingFeedback() {
    $.ajax({
        url: "/Admin/GetUserFeedbackDetails",
        type: "POST",
        dataType: "JSON",
        data: { Status: 'Pending' },
        async: false,
        success: function (data) {
            if (data.length > 0) {
                var trHTML = '';
                $.each(data, function (i, item) {
                    trHTML += '<tr><td style="width:2%;"><input type="checkbox"/></td><td style="width:28%;"><table class= "custum-grid-table" style="font-size:12px;"><tr><td><b>Code</b>: ' + data[i].UserCode + '</td></tr><tr><td><b>Name</b>: ' + data[i].UserName + '</td></tr><tr><td><b>Email</b>: ' + data[i].UserEmail + '</td></tr><tr></tr></table></td><td style="width:33%;">' + data[i].FeedbackQuestion + '</td><input type="hidden" id="hdFeedBackId" value=' + data[i].FeedBackId + '><td><textarea style="width:100%"/></td><input type="hidden" id="hdUserId" value=' + data[i].UserCode + ' /></tr>';
                });
                $('#tblPendingRequest tbody').append(trHTML);
            }
            else {
                $('.btnUpdate').prop("disabled", true);
                $('#tblPendingRequest tbody').append('<tr><td></td><td style="color:red;font-size:15px;">No pending feedback is available.</td><td></td><td></td></tr>');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });
}

function GetCompletedFeedback() {
    $.ajax({
        url: "/Admin/GetUserFeedbackDetails",
        type: "POST",
        dataType: "JSON",
        data: { Status: 'Completed' },
        async: false,
        success: function (data) {
            if (data.UserCode !== "") {
                var trHTML = '';
                $.each(data, function (i, item) {
                    trHTML += '<tr><td style="width:28%;"><table class= "custum-grid-table" style="font-size:12px;"><tr><td style=""><b>Code</b>: ' + data[i].UserCode + '</td></tr><tr><td><b>Name</b>: ' + data[i].UserName + '</td></tr><tr><td><b>Email</b>: ' + data[i].UserEmail + '</td></tr><tr></tr></table></td><td>' + data[i].FeedbackQuestion + '</td><td>' + data[i].AdminReply + '</td></tr>';
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

function SubmitAdminReply() {
    var checkboxValues = [];

    $('#tblPendingRequest tbody').find('tr').each(function (i) {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            var feedbackId = row.find('input[type="hidden"]').val();
            var adminreply = row.find('textarea').val();
            var userId = row.find('input[id=hdUserId]').val();
            if (adminreply === "") {
                alert("You must fill the text area!")
                return false;
            }
            var val = feedbackId + '^' + adminreply + '^' + userId;
            checkboxValues.push(val)
        }
        return checkboxValues;
    });

    if (!$.isEmptyObject(checkboxValues)) {
        $.ajax({
            url: "/Admin/SubmitAdminReply",
            type: "POST",
            dataType: "JSON",
            data: { feedbackIds: checkboxValues },
            success: function (data) {
                window.location.href = "/admin/managefeedback"
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

