function SubmitFormValidation() {
    var businessId = parseInt($('#ddlBusiness').val()) || 0;
    var sg29Id = $('#ddlSG29Approver').val() || 0;
    var transferLoc = $('#ddlRequestedTransferLocation').val();
    var requestType = parseInt($('#ddlRequestType').val()) || 0;
    var transferReason = $('#txtTransferReason').val().trim().length;
    var processLen = $('#txtTenureMonthsInProcess').val().trim().length;
    var locLen = $('#txtTenureMonthsInLocation').val().trim().length;
    var dateLen = $('#txtEffectiveTransferDate').val().trim().length;
    var allowanceLen = $("input[name=IsRelocationAllowance]:checked").length;
    var correctiveActionPlan = $("input[name=IsCorrectiveActionPlan]:checked").length;
    var salaryGrade = parseInt($('#ddlSalaryGrade').val()) || 0;
    var emailVal = $('#txtTransferApprovalEmail').val().trim().length;
    var hdEmployeeNameSessionValue = $('#hdEmployeeNameSessionValue').val();
    var hdEmployeeIdSessionValue = $('#hdEmployeeIdSessionValue').val();
    var hdEmployeeEmailSessionValue = $('#hdEmployeeEmailSessionValue').val();
    var hdEmployeeNTIDSessionVal = $('#hdEmployeeNTIDSessionVal').val();

    $('#hdRequestSubmiteeEmployeeName').val(hdEmployeeNameSessionValue);
    $('#hdRequestSubmiteeEmployeeId').val(hdEmployeeIdSessionValue);
    $('#hdRequestSubmiteeEmployeeEmail').val(hdEmployeeEmailSessionValue);
    $('#hdRequestSubmiteeEmployeeNTID').val(hdEmployeeNTIDSessionVal);
    var IsError = false;
    if (businessId === "" || businessId == 0) {
        $('#ddlBusiness').addClass("validation_failed");
        $('#errorBusinessSpan').show();
        IsError = true;
    }
    if (sg29Id === "" || sg29Id == 0) {
        $('#ddlSG29Approver').addClass("validation_failed");
        $('#errorSG29ApproverSpan').show();
        IsError = true;
    }
    if (transferLoc === "" || transferLoc == 0) {
        $('#ddlRequestedTransferLocation').addClass("validation_failed");
        $('#errorRequestedTransferSpan').show();
        IsError = true;
    }
    if (requestType === "" || requestType == 0) {
        $('#ddlRequestType').addClass("validation_failed");
        $('#errorRequestTypeSpan').show();
        IsError = true;
    }
    if (!transferReason > 0) {
        $('#txtTransferReason').addClass("validation_failed");
        $('#errorTransferReasonSpan').show();
        IsError = true;
    }
    if (!processLen > 0) {
        $('#txtTenureMonthsInProcess').addClass("validation_failed");
        $('#errorProcessSpan').show();
        IsError = true;
    }
    if (!locLen > 0) {
        $('#txtTenureMonthsInLocation').addClass("validation_failed");
        $('#errorUHGLocationSpan').show();
        IsError = true;
    }
    if (!dateLen > 0) {
        $('#txtEffectiveTransferDate').addClass("validation_failed");
        $('#errorEffectiveTransferSpan').show();
        IsError = true;
    }
    if (correctiveActionPlan === 0) {
        $('input[name=IsCorrectiveActionPlan]').addClass("validation_failed");
        $('#errorCorrectiveActionPlanSpan').show();
        IsError = true;
    }
    if (allowanceLen === 0) {
        $('input[name=IsRelocationAllowance]').addClass("validation_failed");
        $('#errorRelocationAllowanceSpan').show();
        IsError = true;
    }
    if (salaryGrade === "" || salaryGrade == 0) {
        $('#ddlSalaryGrade').addClass("validation_failed");
        $('#errorSalarySpan').show();
        IsError = true;
    }

    var transferLocZone = parseInt($('#hdRequestedLocationZone').val());
    var requestTypeVal = parseInt($('#ddlRequestType').val());
    var selectedRelocationVal = $("input[name=IsRelocationAllowance]:checked").val();

    if (transferLocZone === 2 && requestTypeVal === 1 && selectedRelocationVal === "True") {
        if ($('#hdRequestId').val() !== undefined) {
            if (!$('#hdTransferApprovalEmailName').val().length > 0 && !emailVal > 0) {
                $('#errorApprovalEmailSpan').show();
                $('#txtTransferApprovalEmail').addClass("validation_failed");
                IsError = true;
            }
        }
        else {
            if (!emailVal > 0) {
                $('#errorApprovalEmailSpan').show();
                $('#txtTransferApprovalEmail').addClass("validation_failed");
                IsError = true;
            }
        }
    }
    if (IsError) {
        if (businessId === "") {
            $('#ddlBusiness').focus();
        }
        else if (sg29Id === 0) {
            $('#ddlSG29Approver').focus();
        }
        else if (transferLoc === "") {
            $('#ddlRequestedTransferLocation').focus();
        }
        else if (requestType === "") {
            $('#ddlRequestType').focus();
        }
        else if (!transferReason > 0) {
            $('#txtTransferReason').focus();
        }
        else if (!processLen > 0) {
            $('#txtTenureMonthsInProcess').focus();
        }
        else if (!locLen > 0) {
            $('#txtTenureMonthsInLocation').focus();
        }
        else if (!dateLen > 0) {
            $('#txtEffectiveTransferDate').focus();
        }
        else if (allowanceLen === 0) {
            $('input[name=IsRelocationAllowance]').focus();
        }
        else if (salaryGrade === "") {
            $('#ddlSalaryGrade').focus();
        }
        else if (transferLocZone === 2 && requestTypeVal === 1 && selectedRelocationVal === "True") {
            if ($('#hdRequestId').val() !== undefined) {
                if (!$('#hdTransferApprovalEmailName').val().length > 0 && !emailVal > 0) {
                    $('#txtTransferApprovalEmail').focus();
                }
            }
            else {
                if (!emailVal > 0) {
                    $('#txtTransferApprovalEmail').focus();
                }
            }
        }
    }
    return IsError;
}

function SubmitIJPFormValidation() {
    var businessId = parseInt($('#ddlBusiness').val()) || 0;
    var transferLoc = $('#ddlPreviousLocation').val();
    var transferReason = $('#txtTransferReason').val().trim().length;
    var transferDate = $('#txtDateOfTransfer').val().trim().length;
    var relocationDate = $('#txtRelocationAllowanceEffectiveDate').val().trim().length;
    var allowanceLen = $("input[name=IsRelocationAllowance]:checked").length;
    var salaryGrade = parseInt($('#ddlSalaryGrade').val()) || 0;
    var emailVal = $('#txtTransferApprovalEmail').val().trim().length;
    var hdEmployeeNameSessionValue = $('#hdEmployeeNameSessionValue').val();
    var hdEmployeeIdSessionValue = $('#hdEmployeeIdSessionValue').val();
    var hdEmployeeEmailSessionValue = $('#hdEmployeeEmailSessionValue').val();
    var hdEmployeeNTIDSessionVal = $('#hdEmployeeNTIDSessionVal').val();

    $('#hdRequestSubmiteeEmployeeName').val(hdEmployeeNameSessionValue);
    $('#hdRequestSubmiteeEmployeeId').val(hdEmployeeIdSessionValue);
    $('#hdRequestSubmiteeEmployeeEmail').val(hdEmployeeEmailSessionValue);
    $('#hdRequestSubmiteeEmployeeNTID').val(hdEmployeeNTIDSessionVal);
    var IsError = false;
    if (businessId == 0 || businessId == "") {
        $('#ddlBusiness').addClass("validation_failed");
        $('#errorBusinessSpan').show();
        IsError = true;
    }
    if (transferLoc === "" || transferLoc == 0) {
        $('#ddlRequestedTransferLocation').addClass("validation_failed");
        $('#errorLocationSpan').show();
        IsError = true;
    }
    if (!transferReason > 0) {
        $('#txtTransferReason').addClass("validation_failed");
        $('#errorTransferReasonSpan').show();
        IsError = true;
    }
    if (!transferDate > 0) {
        $('#txtDateOfTransfer').addClass("validation_failed");
        $('#errorDOTSpan').show();
        IsError = true;
    }
    if (!relocationDate > 0) {
        $('#txtRelocationAllowanceEffectiveDate').addClass("validation_failed");
        $('#errorRelocationSpan').show();
        IsError = true;
    }
    if (allowanceLen === 0) {
        $('#errorRelocationAllowanceSpan').show();
        IsError = true;
    }
    if (salaryGrade === "" || salaryGrade == 0) {
        $('#ddlSalaryGrade').addClass("validation_failed");
        $('#errorSalarySpan').show();
        IsError = true;
    }

    var transferLocZone = parseInt($('#hdPreviousLocationZone').val());
    var selectedRelocationVal = $("input[name=IsRelocationAllowance]:checked").val();

    if (transferLocZone === 2 && selectedRelocationVal === "True") {
        if ($('#hdRequestId').val() !== undefined) {
            if (!$('#hdTransferApprovalEmailName').val().length > 0 && !emailVal > 0) {
                $('#txtTransferApprovalEmail').addClass("validation_failed");
                $('#errorApprovalEmailSpan').show();
                IsError = true;
            }
        }
        else {
            if (!emailVal > 0) {
                $('#txtTransferApprovalEmail').addClass("validation_failed");
                $('#errorApprovalEmailSpan').show();
                IsError = true;
            }
        }
    }
    if (IsError) {
        if (businessId === "") {
            $('#ddlBusiness').focus();
        }
        else if (transferLoc === "") {
            $('#ddlRequestedTransferLocation').focus();
        }
        else if (!transferReason > 0) {
            $('#txtTransferReason').focus();
        }
        else if (!transferDate > 0) {
            $('#txtDateOfTransfer').focus();
        }
        else if (!relocationDate > 0) {
            $('#txtRelocationAllowanceEffectiveDate').focus();
        }
        else if (salaryGrade === "") {
            $('#ddlSalaryGrade').focus();
        }
        else if (transferLocZone === 2 && selectedRelocationVal === "True") {
            if ($('#hdRequestId').val() !== undefined) {
                if (!$('#hdTransferApprovalEmailName').val().length > 0 && !emailVal > 0) {
                    $('#txtTransferApprovalEmail').focus();
                }
            }
            else {
                if (!emailVal > 0) {
                    $('#txtTransferApprovalEmail').focus();
                }
            }
        }
    }
    return IsError;
}

function DisclaimerValidation() {
    var isDisclaimerChecked = $('#chkDisclaimerITP').prop('checked');
    var actionVal = $('#hdActionValue').val();

    if (actionVal === undefined) {
        if (isDisclaimerChecked) {
            $('.btnITPAction').prop('disabled', false);
        }
        else {
            $('.btnITPAction').prop('disabled', true);
        }
    } else {
        if (isDisclaimerChecked && actionVal === "false") {
            $('.btnITPAction').prop('disabled', false);
        } else {
            $('.btnITPAction').prop('disabled', true);
        }
    }
}

function IJPDisclaimerValidation() {
    var isDisclaimerChecked = $('#chkDisclaimerIJP').prop('checked');
    var actionVal = $('#hdActionValue').val();

    if (actionVal === undefined) {
        if (isDisclaimerChecked) {
            $('.btnIJPAction').prop('disabled', false);
        }
        else {
            $('.btnIJPAction').prop('disabled', true);
        }
    } else {
        if (isDisclaimerChecked && actionVal === "false") {
            $('.btnIJPAction').prop('disabled', false);
        } else {
            $('.btnIJPAction').prop('disabled', true);
        }
    }
}

function DDLRequestedTransferLocation() {
    $('#ddlRequestType').val("");
    $('#errorRequestedTransferSpan').hide();
    var currentLocZone = parseInt($('#hdCurrentLocationZone').val());

    GetZoneOfTransferLocation($('#ddlRequestedTransferLocation').val());
    var transferLocZone = parseInt($('#hdRequestedLocationZone').val());

    if (currentLocZone === transferLocZone) {
        $('.emailTransferDiv').hide();
        $('#relocationInfoId').hide();
        $('#txtRelocationAmount').val(0);
        $('#hdRelocationAmount').val(0);
        $("input[name=IsRelocationAllowance]").prop('disabled', true);
        $('#rbRelocationAllowanceNo').prop({ 'checked': true, 'disabled': true });
        $('#hdRelocationAllowanceYes').val($("input[name=IsRelocationAllowance]:checked").val());
        $('#errorRelocationAllowanceSpan').hide();
    }
    else if ((currentLocZone === 1 || currentLocZone === 2) && (transferLocZone === 2 || transferLocZone === 1)) {
        $('#txtRelocationAmount').val("");
        $('#hdRelocationAmount').val("");
        $("input[name=IsRelocationAllowance]").prop({ 'disabled': false, 'checked': false });
    }
    else {
        $('#txtRelocationAmount').val("");
        $('#hdRelocationAmount').val("");
        $('.emailTransferDiv').hide();
        $('#relocationInfoId').hide();
        $("input[name=IsRelocationAllowance]").prop({ 'disabled': false, 'checked': false });
    }
}

function DDLRequestType() {
    $('#errorRequestTypeSpan').hide();
    var requestTypeVal = parseInt($('#ddlRequestType').val());
    var currentLocZone = parseInt($('#hdCurrentLocationZone').val());
    var transferLocZone = parseInt($('#hdRequestedLocationZone').val());
    if (currentLocZone == transferLocZone) {
        $('#txtRelocationAmount').val(0);
        $('#hdRelocationAmount').val(0);
        $('.emailTransferDiv').hide();
        $("input[name=IsRelocationAllowance]").prop('disabled', true);
        $('#rbRelocationAllowanceNo').prop({ 'checked': true, 'disabled': true });
        $('#hdRelocationAllowanceYes').val($("input[name=IsRelocationAllowance]:checked").val());
        $('#errorRelocationAllowanceSpan').hide();
        $('#relocationInfoId').hide();
    } else if ((currentLocZone === 1 || currentLocZone === 2) && (transferLocZone === 2 || transferLocZone === 1)) {
        if (requestTypeVal === 1) {
            $('#ddlSalaryGrade').val("");
            $('.emailTransferDiv').show();
            $('#relocationInfoId').show();
            $("input[name=IsRelocationAllowance]").prop({ 'disabled': false, 'checked': false });
        } else {
            $('#txtRelocationAmount').val(0);
            $('#hdRelocationAmount').val(0);
            $('.emailTransferDiv').hide();
            $("input[name=IsRelocationAllowance]").prop('disabled', true);
            $('#rbRelocationAllowanceNo').prop({ 'checked': true, 'disabled': true });
            $('#hdRelocationAllowanceYes').val($("input[name=IsRelocationAllowance]:checked").val());
            $('#errorRelocationAllowanceSpan').hide();
            $('#relocationInfoId').hide();
        }
    } else {
        if (requestTypeVal === 1) {
            $('#ddlSalaryGrade').val("");
            $('.emailTransferDiv').hide();
            $('#relocationInfoId').hide();
            $("input[name=IsRelocationAllowance]").prop({ 'disabled': true });
            $('#rbRelocationAllowanceYes').prop({ 'checked': true, 'disabled': true });
            $('#hdRelocationAllowanceYes').val($("input[name=IsRelocationAllowance]:checked").val());
        } else {
            $('#txtRelocationAmount').val(0);
            $('#hdRelocationAmount').val(0);
            $("input[name=IsRelocationAllowance]").prop({ 'disabled': true });
            $('#rbRelocationAllowanceNo').prop({ 'checked': true, 'disabled': true });
            $('#hdRelocationAllowanceYes').val($("input[name=IsRelocationAllowance]:checked").val())
        }
    }
}

function DDLSalaryGrade() {
    var IsNCR;
    $('#errorSalarySpan').hide();
    var transferLocZone = parseInt($('#hdRequestedLocationZone').val());
    var grade = $('#ddlSalaryGrade option:selected').text().trim();
    var currentLocZone = parseInt($('#hdCurrentLocationZone').val());
    var requestTypeVal = parseInt($('#ddlRequestType').val());
    var selectedRelocationVal = $("input[name=IsRelocationAllowance]:checked").val();
    //var relocationAmountVal = parseInt($('#hdRelocationAmount').val());

    if (transferLocZone === "") {
        $('#errorRequestedTransferSpan').show();
        return false;
    }

    if (transferLocZone === 1 || transferLocZone === 2) {
        IsNCR = 'true';
    } else {
        IsNCR = 'false';
    }

    //if (relocationAmountVal !== 0)
    if ((currentLocZone != transferLocZone) && (requestTypeVal != 2) && selectedRelocationVal != "False")
        FillRelocationAmount(grade, IsNCR);
}

function RBRelocationAllowance() {
    $('#errorRelocationAllowanceSpan').hide();
    var selectedRelocationVal = $("input[name=IsRelocationAllowance]:checked").val();

    if (selectedRelocationVal === "True") {
        $('#txtRelocationAmount').val("");
        $('#hdRelocationAmount').val("");
        $('#ddlSalaryGrade').val("");
        $('.emailTransferDiv').show();
        $('#hdRelocationAllowanceYes').val(selectedRelocationVal);
    }
    else {
        $('#txtRelocationAmount').val(0);
        $('#hdRelocationAmount').val(0);
        $('.emailTransferDiv').hide();
        $('#hdRelocationAllowanceYes').val(selectedRelocationVal);
    }
}

function RBCorrectiveActionPlan() {
    var selectedCAPVal = $("input[name=IsCorrectiveActionPlan]:checked").val();

    if (selectedCAPVal === "True")
        $('.lblCAP').addClass("cap-select");
    else
        $('.lblCAP').removeClass("cap-select");
}

function CheckITPFormValidation() {
    var transferLocZone = parseInt($('#hdRequestedLocationZone').val());
    var selectedRelocationVal = $("input[name=IsRelocationAllowance]:checked").val();
    $('#hdRelocationAllowanceYes').val(selectedRelocationVal);

    if (transferLocZone != 2) {
        $("input[name=IsRelocationAllowance]").prop('disabled', true);
        $('#relocationInfoId').hide();
    } else {
        $("input[name=IsRelocationAllowance]").prop('disabled', false);
        $('#relocationInfoId').show();
    }
    SetEffectiveTransferDateFormat();
    RBCorrectiveActionPlan();
}

function SetEffectiveTransferDateFormat() {
    var TenureMonthsInProcess = $('#hdTenureMonthsInProcess').val().split(' ')[0]
    TenureMonthsInProcess = ChangeDateFormatToName(TenureMonthsInProcess)
    var TenureMonthsInLocation = $('#hdTenureMonthsInLocation').val().split(' ')[0]
    TenureMonthsInLocation = ChangeDateFormatToName(TenureMonthsInLocation)
    var EffectiveTransferDate = $('#hdEffectiveTransferDate').val().split(' ')[0]
    EffectiveTransferDate = ChangeDateFormatToName(EffectiveTransferDate)

    $('#txtTenureMonthsInProcess').val(TenureMonthsInProcess);
    $('#txtTenureMonthsInLocation').val(TenureMonthsInLocation);
    $('#txtEffectiveTransferDate').val(EffectiveTransferDate);

    var transferLocZone = parseInt($('#hdRequestedLocationZone').val());
    var currentLocZone = $('#hdCurrentLocationZone').val();
    var requestTypeVal = parseInt($('#ddlRequestType').val());
    var selectedRelocationVal = $("input[name=IsRelocationAllowance]:checked").val();

    if (transferLocZone === 2 && requestTypeVal === 1 && selectedRelocationVal === "True") {
        $('.emailTransferDiv').show();
    } else {
        $('.emailTransferDiv').hide();
    }
}

function CheckRequestValidation(isManager) {
    var userId = $('#hdEmpNTID').val();

    $.ajax({
        url: "/Request/GetCountOfRaisedRequest",
        method: 'post',
        dataType: 'json',
        data: { UserId: userId },
        async: false,
        success: function (data) {
            if (!isManager) { // Call while employee logged in 
                $('#EmployeeAccessHiddenValue').val(data);
                if (data == 1001)
                    EmployeeNotAuthroizedMessage();
                else
                    if (data > 0)
                        EmployeeAlertMessage();
            }
            else { // Call while manager logged in 
                $('#ManagerAccessHiddenValue').val(data);
                if (data == 1001)
                    ManagerNotAuthroizedMessage();
                else
                    if (data > 0)
                        ManagerAlertMessage();
            }
        }
    });
}

function CheckIJPFormValidation() {
    var transferLocZone = parseInt($('#hdPreviousLocationZone').val());

    if (transferLocZone !== 2) {
        $("input[name=IsRelocationAllowance]").prop('disabled', true);
        $('#relocationInfoId').hide();
    } else {
        $("input[name=IsRelocationAllowance]").prop('disabled', false);
        $('#relocationInfoId').show();
    }
    SetIJPEffectiveTransferDateFormat();
}

function SetIJPEffectiveTransferDateFormat() {
    var transferLocZone = parseInt($('#hdPreviousLocationZone').val());
    var selectVal = $("input[name=IsRelocationAllowance]:checked").val();

    var DateOfTransfer = $('#hdDateOfTransfer').val().split(' ')[0];
    DateOfTransfer = ChangeDateFormatToName(DateOfTransfer);
    var RelocationAllowanceEffectiveDate = $('#hdRelocationAllowanceEffectiveDate').val().split(' ')[0];
    RelocationAllowanceEffectiveDate = ChangeDateFormatToName(RelocationAllowanceEffectiveDate);

    $('#txtDateOfTransfer').val(DateOfTransfer);
    $('#txtRelocationAllowanceEffectiveDate').val(RelocationAllowanceEffectiveDate);

    if (transferLocZone === 2 && selectVal === "True") {
        $('.emailTransferDiv').show();
    } else {
        $('.emailTransferDiv').hide();
    }
}

function EmployeeAlertMessage() {
    $.alert({
        title: 'Alert!',
        content: 'Transfer request is already raised,Please check current status in myrequest details.',
        buttons: {
            Ok: function () {
                window.location.href = '/Home/Index';
            },
        }
    });
}

function ManagerAlertMessage() {
    $.alert({
        title: 'Alert!',
        content: 'Your request is already raised,Please check current status in my request details.',
        buttons: {
            Ok: function () { },
        }
    });
    ClearFormDetails();
}

function EmployeeNotAuthroizedMessage() {
    $.alert({
        title: 'Alert!',
        content: 'Internal transfer policy is not applicable for contractual employees.',
        buttons: {
            Ok: function () {
                window.location.href = '/Home/Index';
            },
        }
    });
}

function ManagerNotAuthroizedMessage() {
    $.alert({
        title: 'Alert!',
        content: 'Internal transfer policy is not applicable for contractual employees.',
        buttons: {
            Ok: function () { },
        }
    });
}

function DateConversion(jsonDate) {
    var date = new Date(parseInt(jsonDate.substr(6)));
    var month = date.getMonth() + 1;
    var fulldate = month + "/" + date.getDate() + "/" + date.getFullYear();
    fulldate = ChangeDateFormatToName(fulldate);

    return fulldate;
}

function ChangeDateFormatToName(inputDate) {
    const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    var date = inputDate.split('/');
    date = date[1] + "-" + (monthNames[date[0] - 1]) + "-" + date[2];

    return date;
}

function GetRequestId(d) {
    var id = d.getAttribute("data-id");
    $("#hiddenRequestId").val(id);
}

function ClearFormDetails() {
    $('#hdEmpName').val("");
    $('#EmployeeName').val("");
    $('#hdEmpNTID').val("");
    $('#EmployeeNTID').val("");
    $('#hdEmpEmail').val("");
    $('#EmployeeEmail').val("");
    $('#hdEmpEmail').val("");
    $('#EmployeeId').val("");
    $('#hdEmpID').val("");
    $('#hdSupervisorId').val("");
    $('#SupervisorId').val("");
    $('#hdSupervisorEmail').val("");
    $('#SupervisorEmail').val("");
    $('#hdSupervisorName').val("");
    $('#SupervisorName').val("");
    $('#hdSupervisorNTID').val("");
    $('#SupervisorNTID').val("");
    $('#hdJobTitle').val("");
    $('#JobTitle').val("");
    $('#hdCurrentLocation').val("");
    $('#CurrentLocation').val("");
}

function ChangeDateFormatToNumber(inputDate) {
    const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    var date = inputDate.split('-');
    date = monthNames.indexOf(date[1]) + "/" + date[0] + "/" + date[2];

    return date;
}

function SelectPicker() {
    $('.selectpicker').selectpicker({
        liveSearch: true,
    });
}

function MonthDiff(startDate, effectiveEndDate) {
    var months;
    months = (effectiveEndDate.split('/')[2] - startDate.split('/')[2]) * 12;
    months -= parseInt(startDate.split('/')[0]) + 1;
    months += parseInt(effectiveEndDate.split('/')[0]);

    return months <= 0 ? 0 : months;
}

function GetZoneOfTransferLocation(transferLoc) {
    $.ajax({
        url: "/Request/GetZoneOfTransferLocation",
        type: "POST",
        dataType: "JSON",
        data: { location: transferLoc },
        async: false,
        success: function (data) {
            if (data.LocationId !== 0) {
                $('#hdRequestedLocationZone').val(data.ZoneId);
            }
            else {
                alert("Invalid input or record doesn't exist");
                return false;
            }
        }
    })
}

function FillDropDownForSG29ApproverDetails(businessId) {
    var selectedVal = $('#hdSG29Approver').val();

    $.ajax({
        url: "/Request/GetSG29ApproverDetails",
        type: "POST",
        dataType: "JSON",
        data: { BusinessId: businessId },
        success: function (data) {
            $('#ddlSG29Approver').html("");
            if (data.ApproverNTID !== null) {
                $("#ddlSG29Approver").append("<option value='0'>--- Select SG29+ approver ---</option>");
                if (selectedVal === undefined) {
                    $.each(data, function (i) {
                        optionhtml = '<option value="' +
                            data[i].ApproverNTID + '">' + data[i].ApproverName + " (" + data[i].ApproverEmail + ")" + '</option>';
                        $('#ddlSG29Approver').append(optionhtml);
                    });
                    $('.selectpicker').selectpicker('refresh');
                }
                else {
                    $.each(data, function (i) {
                        optionhtml = '<option value="' +
                            data[i].ApproverNTID.split('\\')[1] + '">' + data[i].ApproverName + " (" + data[i].ApproverEmail + ")" + '</option>';
                        $('#ddlSG29Approver').append(optionhtml);

                        $("#ddlSG29Approver option[value='" + selectedVal.split('\\')[1] + "']").prop('selected', 'selected');
                    });
                    $('.selectpicker').selectpicker('refresh');
                }
            }
            else {
                alert("Invalid input or record doesn't exist");
                return false;
            }
        }
    })
}

function FillITPTransferLocationDetails(locationCode) {
    $.ajax({
        url: "/Request/GetLocationDetails",
        type: "GET",
        dataType: "JSON",
        success: function (data) {
            $('#ddlRequestedTransferLocation').html("");
            if (data.GLCode !== null) {
                $("#ddlRequestedTransferLocation").append("<option value='0'>--- Select transfer location ---</option>");
                $.each(data, function (i) {
                    if (data[i].GLCode != locationCode) {
                        optionhtml = '<option value="' +
                            data[i].GLCode + '">' + data[i].LocationName + " (" + data[i].GLCode + ")" + '</option>';
                        $('#ddlRequestedTransferLocation').append(optionhtml);
                    }
                });
            }
            else {
                alert("Invalid input or record doesn't exist");
                return false;
            }
        }
    })
}

function FillIJPPreviousLocationDetails(locationCode) {
    $.ajax({
        url: "/Request/GetLocationDetails",
        type: "GET",
        dataType: "JSON",
        success: function (data) {
            $('#ddlPreviousLocation').html("");
            if (data.GLCode !== null) {
                $("#ddlPreviousLocation").append("<option value='0'>--- Select transfer location ---</option>");
                $.each(data, function (i) {
                    if (data[i].GLCode != locationCode) {
                        optionhtml = '<option value="' +
                            data[i].GLCode + '">' + data[i].LocationName + " (" + data[i].GLCode + ")" + '</option>';
                        $('#ddlPreviousLocation').append(optionhtml);
                    }
                });
            }
            else {
                alert("Invalid input or record doesn't exist");
                return false;
            }
        }
    })
}

function FillRequestFormDetails(data) {
    $('#ddlRequesteeEmployeeName option:selected').text(data.EmployeeName);
    $('.selectpicker').selectpicker('refresh');
    $('#hdEmpName').val(data.EmployeeName);
    $('#EmployeeName').val(data.EmployeeName);
    $('#hdEmpNTID').val(data.EmployeeNTID);
    $('#EmployeeNTID').val(data.EmployeeNTID);
    $('#hdEmpEmail').val(data.EmployeeEmail);
    $('#EmployeeEmail').val(data.EmployeeEmail);
    $('#hdEmpEmail').val(data.EmployeeEmail);
    $('#EmployeeId').val(data.EmployeeCode);
    $('#hdEmpID').val(data.EmployeeCode);
    $('#hdSupervisorId').val(data.ManagerCode);
    $('#SupervisorId').val(data.ManagerCode);
    $('#hdSupervisorEmail').val(data.ManagerEmail);
    $('#SupervisorEmail').val(data.ManagerEmail);
    $('#hdSupervisorName').val(data.ManagerName);
    $('#SupervisorName').val(data.ManagerName);
    $('#hdSupervisorNTID').val(data.ManagerNTID);
    $('#SupervisorNTID').val(data.ManagerNTID);
    $('#hdJobTitle').val(data.JobTitle);
    $('#JobTitle').val(data.JobTitle);
    $('#hdCurrentLocation').val(data.LocationCode);
    $('#CurrentLocation').val(data.EmployeeLocation + " (" + data.LocationCode + ")");
    $('#hdCurrentLocationZone').val(data.CurrentLocationZone);
    $('#hdBusinessUnit').val(data.BusinessUnit);
    $('#hdDepartmentId').val(data.DepartmentId);
    $('#hdOperatingUnit').val(data.OperatingUnit);
}

function FillRelocationAmount(grade, IsNCR) {
    $.ajax({
        url: "/Request/GetRelocationAmountDetail",
        type: "POST",
        dataType: "JSON",
        data: { grade: grade },
        success: function (data) {
            if (data.Id !== null) {
                if (IsNCR == "true") {
                    $('#txtRelocationAmount').val(data.InSideNCR);
                    $('#hdRelocationAmount').val(data.InSideNCR);
                } else {
                    $('#txtRelocationAmount').val(data.OutSideNCR);
                    $('#hdRelocationAmount').val(data.OutSideNCR);
                }
            }
            else {
                alert("Invalid input or record doesn't exist");
                return false;
            }
        }
    })
}

function DDLPreviousLocation() {
    $('#errorLocationSpan').hide();
    var currentLocZone = parseInt($('#hdCurrentLocationZone').val());

    GetZoneOfIJPTransferLocation($('#ddlPreviousLocation').val());
    var transferLocZone = parseInt($('#hdPreviousLocationZone').val());
    if (currentLocZone === transferLocZone) {
        $('.emailTransferDiv').hide();
        $('#relocationInfoId').hide();
        $('#txtRelocationAmount').val(0);
        $('#hdRelocationAmount').val(0);
        $("input[name=IsRelocationAllowance]").prop('disabled', true);
        $("#rbRelocationAllowanceNo").prop({ 'checked': true, 'disabled': true });
        $('#hdRelocationAllowanceYes').val($("input[name=IsRelocationAllowance]:checked").val());
    }
    else if ((currentLocZone === 1 || currentLocZone === 2) && (transferLocZone === 2 || transferLocZone === 1)) {
        $('#ddlSalaryGrade').val("");
        $('#txtRelocationAmount').val("");
        $('#hdRelocationAmount').val("");
        $("input[name=IsRelocationAllowance]").prop({ 'disabled': false, 'checked': false });
        $('#relocationInfoId').show();
    }
    else {
        $('#ddlSalaryGrade').val("");
        $('#txtRelocationAmount').val("");
        $('#hdRelocationAmount').val("");
        $('.emailTransferDiv').hide();
        $('#relocationInfoId').hide();
        $("input[name=IsRelocationAllowance]").prop({ 'disabled': true });
        $("#rbRelocationAllowanceYes").prop({ 'checked': true, 'disabled': true });
        $('#hdRelocationAllowanceYes').val($("input[name=IsRelocationAllowance]:checked").val());
    }
}

function DDLIJPSalaryGrade() {
    var IsNCR;

    $('#errorSalarySpan').hide();
    var transferLocZone = parseInt($('#hdPreviousLocationZone').val().trim());
    var grade = $('#ddlSalaryGrade option:selected').text().trim();
    var currentLocZone = parseInt($('#hdCurrentLocationZone').val());
    var selectedRelocationVal = $("input[name=IsRelocationAllowance]:checked").val();
    //var relocationAmountVal = parseInt($('#hdRelocationAmount').val());

    if (transferLocZone === "") {
        $('#errorLocationSpan').show();
        return false;
    }
    if (transferLocZone === 1 || transferLocZone === 2) {
        IsNCR = 'true';
    } else {
        IsNCR = 'false';
    }
    //if (relocationAmountVal !== 0)
    if ((currentLocZone != transferLocZone) && selectedRelocationVal != "False")
        FillRelocationAmount(grade, IsNCR);
}

function RBIJPRelocationAllowance() {
    $('#errorRelocationAllowanceSpan').hide();
    var selectedRelocationVal = $("input[name=IsRelocationAllowance]:checked").val();
    var transferLocZone = $('#hdPreviousLocationZone').val();

    if (selectedRelocationVal === "True") {
        $('#txtRelocationAmount').val("");
        $('#hdRelocationAmount').val("");
        $('#ddlSalaryGrade').val("");
        //$('#relocationInfoId').show();
        $('.emailTransferDiv').show();
        $('#hdRelocationAllowanceYes').val(selectedRelocationVal);
    }
    else {
        $('#txtRelocationAmount').val(0);
        $('#hdRelocationAmount').val(0);
        $('.emailTransferDiv').hide();
        //$('#relocationInfoId').hide();
        $('#hdRelocationAllowanceYes').val(selectedRelocationVal);
    }
}

function FillEmployeeDDL(data, IsSLM) {
    var optionhtml = "";
    $('#ddlRequesteeEmployeeName').html("");
    $("#ddlRequesteeEmployeeName").append("<option value='0'>--- Select employee ---</option>");

    if (!IsSLM) { // Fill Employee's name dropdown list
        for (var i = 0; i < data.ListOfDirectReportToManager.length; i++) {
            optionhtml += '<option value="' + data.ListOfDirectReportToManager[i].EmployeeNTID + '">' +
                data.ListOfDirectReportToManager[i].EmployeeName + '</option>';
        }
    }
    else { // Fill Manager's name dropdown list
        for (i = 0; i < data.ListOfDirectReportToSLM.length; i++) {
            optionhtml += '<option value="' + data.ListOfDirectReportToSLM[i].EmployeeNTID + '">' +
                data.ListOfDirectReportToSLM[i].EmployeeName + '</option>';
        }
    }
    $('#ddlRequesteeEmployeeName').append(optionhtml);
    $('.selectpicker').selectpicker('refresh');
}

function FillEmployeeManagerDDL(data) {
    var optionhtml = "";
    $('#ddlRequesteeEmployeeName').html("");
    $("#ddlRequesteeEmployeeName").append("<option value='0'>--- Select employee ---</option>");

    for (var i = 0; i < data.ListOfDirectReportToManager.length; i++) {
        optionhtml += '<option value="' + data.ListOfDirectReportToManager[i].EmployeeNTID + '">' +
            data.ListOfDirectReportToManager[i].EmployeeName + '</option>';
    }

    for (i = 0; i < data.ListOfDirectReportToSLM.length; i++) {
        optionhtml += '<option value="' + data.ListOfDirectReportToSLM[i].EmployeeNTID + '">' +
            data.ListOfDirectReportToSLM[i].EmployeeName + '</option>';
    }

    $('#ddlRequesteeEmployeeName').append(optionhtml);
    $('.selectpicker').selectpicker('refresh');
}

function FillManagerDDL(data) {
    var optionhtml = "";

    $('#ddlRequesteeManager').html("");
    $("#ddlRequesteeManager").append("<option value='0'>--- Select manager ---</option>");

    for (var i = 0; i < data.ListOfDirectReportToSLM.length; i++) {
        optionhtml += '<option value="' + data.ListOfDirectReportToSLM[i].EmployeeNTID + '">' +
            data.ListOfDirectReportToSLM[i].EmployeeName + '</option>';
    }
    $('#ddlRequesteeManager').append(optionhtml);
    $('.selectpicker').selectpicker('refresh');
}

function FillDropDownForEmployeeDetails(requesterVal) {
    $.ajax({
        url: "/Request/GetEmployeeListByManager",
        type: "POST",
        dataType: "JSON",
        data: { searchParameter: requesterVal },
        success: function (data) {
            ClearFormDetails();
            $('#ddlRequesteeEmployeeName').html("");
            $("#ddlRequesteeEmployeeName").append("<option value='0'>--- Select employee ---</option>");

            if (data.ListOfDirectReportToManager.length > 0) {
                var optionhtml = "";
                for (var i = 0; i < data.ListOfDirectReportToManager.length; i++) {
                    optionhtml += '<option value="' + data.ListOfDirectReportToManager[i].EmployeeNTID + '">' + data.ListOfDirectReportToManager[i].EmployeeName + '</option>';
                }
                $('#ddlRequesteeEmployeeName').append(optionhtml);
                $('.selectpicker').selectpicker('refresh');
            }
            else {
                $('.selectpicker').selectpicker('refresh');
                alert("This tool can be use for OGS India employees only.");
                return false;
            }
        }
    });
}

function GetZoneOfIJPTransferLocation(transferLoc) {
    $.ajax({
        url: "/Request/GetZoneOfTransferLocation",
        type: "POST",
        dataType: "JSON",
        data: { location: transferLoc },
        async: false,
        success: function (data) {
            if (data.LocationId !== 0) {
                $('#hdPreviousLocationZone').val(data.ZoneId);
            }
            else {
                alert("Invalid input or record doesn't exist");
                return false;
            }
        }
    });
}
