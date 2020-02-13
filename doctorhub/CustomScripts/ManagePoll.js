$(document).ready(function () {
    $("#txtFromDate").attr('readOnly', 'true');
    $("#txtToDate").attr('readOnly', 'true');
    $("#txtFromDate").datepicker({
        minDate:'0',
        onSelect: function (selectedDate) {
            var dt = new Date(selectedDate);
            dt.setDate(dt.getDate())
            $("#txtToDate").datepicker("option", "minDate", dt)
        }
    });
    $("#txtToDate").datepicker({
    });
    $("#chbIsActive").prop('checked', true);
    var PollID = $("#PollID").val();
    if (PollID.length > 0) {
        $("#txtFromDate").attr('disabled', 'disabled');
        $("#txtToDate").attr('disabled', 'disabled');
    }
    GetManagePoll();
});
function ValidateManagePoll() {
    var _Question = $("#txtQuestion").val();
    var _Options = $("#txtOptions").val();
    var _FromDate = $("#txtFromDate").val();
    var _ToDate = $("#txtToDate").val();
    var _ChkActive = $('#IsActive').is(':checked');
    if (_Question == "" || _Options == "" || _FromDate == "" || _ToDate == "") {
        alert("All fields are required.");
        return false;
    }
}

function GetManagePoll() {
    $.ajax({
        url: "/Admin/ManagePollDetails",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            datatableInstance = $('#tblManagePoll').DataTable({
                destroy: true,
                paging: true,
                sort: true,
                searching: true,
                data: data,
                columns: [
                    { 'data': 'Questions' },
                    { 'data': 'Options' },
                    {
                        'data': 'FromDate',
                        'render': function (jsonDate) {
                            var date = new Date(parseInt(jsonDate.substr(6)));
                            var month = date.getMonth() + 1;
                            var fulldate = month + "/" + date.getDate() + "/" + date.getFullYear();
                            fulldate = ChangeDateFormatToName(fulldate);

                            return fulldate;
                        }
                    },
                    {
                        'data': 'ToDate',
                        'render': function (jsonDate) {
                            var date = new Date(parseInt(jsonDate.substr(6)));
                            var month = date.getMonth() + 1;
                            var fulldate = month + "/" + date.getDate() + "/" + date.getFullYear();
                            fulldate = ChangeDateFormatToName(fulldate);

                            return fulldate;
                        }
                    },
                    { 'data': 'IsActive' },
                    {
                        'data': 'PollID',
                        'sortable': false,
                        'visible': true,
                        'render': function (data, type, full, meta) {
                            return '<a title="Manage" href="/Admin/ManagePoll?key=' + full.PollID + '"><i class="fa fa-edit font-20px"></i>&nbsp;Manage</a>';
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

function ChangeDateFormatToName(inputDate) {
    const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    var date = inputDate.split('/');
    date = date[1] + "-" + (monthNames[date[0] - 1]) + "-" + date[2];

    return date;
}
