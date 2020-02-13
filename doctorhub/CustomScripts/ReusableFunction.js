var viewModel = {};
var copyViewModel = {};
var str;

function AlertMessage(msg) {
    alert(msg)
}

function BlockScreen() {
    $.blockUI({
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .8,
            color: '#fff'
        }
    });
}

function UnblockScreen() {
    $.unblockUI();
}


function CheckboxDefaultProperty() {
    $("input[type=checkbox][Id=IsActive]").prop("checked", true).attr("disabled", true);
}

//  To disable/enable all typeof controls
function EnableDisableControls(CntrlId, propertyType, CntrlType) {
    if (propertyType == "disabled") {
        if (CntrlType == "T") {
            $('#' + CntrlId).attr("disabled", "disabled");
        }
        else if (CntrlType == "D") {
            $('#' + CntrlId).attr("disabled", "disabled");
        }
        else if (CntrlType == "B") {
            $('#' + CntrlId).attr("disabled", "disabled");
        }
        else if (CntrlType == "C") {
            $('input[Id=' + CntrlId + ']').attr("disabled", "disabled");
        }
    }
    else if (propertyType == "enabled") {
        if (CntrlType == "T") {
            $('#' + CntrlId).prop("disabled", false);
        }
        else if (CntrlType == "D") {
            $('#' + CntrlId).prop("disabled", false);
        }
        else if (CntrlType == "B") {
            $('#' + CntrlId).prop("disabled", false);
        }
        else if (CntrlType == "C") {
            $('input[Id=' + CntrlId + ']').prop("disabled", false);
        }
    }
}

//  To clear all typeof controls
function ClearControls(CntrlId, CntrlType) {
    if (CntrlType == "T") {
        $('#' + CntrlId).val("");
        //$('#' + CntrlId).removeClass();
    }
    else if (CntrlType == "D") {
        $('#' + CntrlId).val("");
        //$('#' + CntrlId).removeClass("disabled");
    }
    else if (CntrlType == "C") {
        $('input[Id=' + CntrlId + ']').removeAttr('checked');
    }
    else if (CntrlType == "R") {
        //$('#' + CntrlId).val("");
    }
}

function HideUpdateShowSave() {
    $("#btnUpdate").hide();
    $("#btnSave").show();
}

function HideSaveShowUpdate() {
    $("#btnUpdate").show();
    $("#btnSave").hide();
}

function InActiveConfirmation(CheckValue) {
    if (CheckValue == true) {
        $('input[type=checkbox][id=IsActive]').click(function () {
            if (!$('input[type=checkbox][id=IsActive]').prop("checked")) {
                if (confirm(arrMsg["CN004"])) {
                    $("input[type=checkbox][Id=IsActive]").prop("checked", false);
                }
                else {
                    $("input[type=checkbox][Id=IsActive]").prop("checked", true);
                }
            }
        });
    }
}

var alertMsg = '';
if (Type == "S") {
    alertMsg = arrMsg["CN001"];
}
else if (Type == "U") {
    alertMsg = arrMsg["CN002"];
}
if (confirm(alertMsg)) {
    ajaxindicatorstart(arrMsg["GEN01"]);
    jQuery.ajax({
        async: false,
        type: "POST",
        url: Action,
        data: ViewModel,
        contentType: "application/json",
        success: function (result) {
            if (result.StatusCode == 1) {
                AlertMessage(result.StatusMessage);
                RedirectToAction(RedirectTo);
            }
            else if (result.StatusCode == 0) {
                AlertMessage(result.StatusMessage);
            }
            ajaxindicatorstop();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            ajaxindicatorstop();
            AlertMessage(arrMsg["EX001"]);
        }
    });
}


function AjaxCallingWithConfirmMsg(Action, ViewModel, RedirectTo, alertMsg) {
    if (confirm(alertMsg)) {
        ajaxindicatorstart(arrMsg["GEN01"]);
        jQuery.ajax({
            async: false,
            type: "POST",
            url: Action,
            data: ViewModel,
            contentType: "application/json",
            success: function (result) {
                if (result.StatusCode == 1) {
                    AlertMessage(result.StatusMessage);
                    RedirectToAction(RedirectTo);
                }
                else if (result.StatusCode == 0) {
                    AlertMessage(result.StatusMessage);
                }
                ajaxindicatorstop();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ajaxindicatorstop();
                AlertMessage(arrMsg["EX001"]);
            }
        });
    }
}

function AjaxCallingWithoutConfirm(Action, ViewModel, RedirectTo, Type) {

    ajaxindicatorstart(arrMsg["GEN01"]);
    jQuery.ajax({
        async: false,
        type: "POST",
        url: Action,
        data: ViewModel,
        contentType: "application/json",
        success: function (result) {
            //debugger;
            if (result.StatusCode == 1) {
                AlertMessage(result.StatusMessage);
                RedirectToAction(RedirectTo);
            }
            else if (result.StatusCode == 0) {
                AlertMessage(result.StatusMessage);
            }
            ajaxindicatorstop();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            ajaxindicatorstop();
            AlertMessage(arrMsg["EX001"]);
        }
    });

}

function RedirectToAction(RedirectTo) {
    window.location = RedirectTo;
}


function SetSelectInDdl() {
    $("#Category_SubCategoryId").append($("<option></option>").val("0").html("--Select--"));
    $("#Site_City_Cluster_ClusterId").append($("<option></option>").val("0").html("--Select--"));
    $("#Site_City_CityId").append($("<option></option>").val("0").html("--Select--"));
    $("#Site_SiteId").append($("<option></option>").val("0").html("--Select--"));
    $("#Vendor_VendorId").append($("<option></option>").val("0").html("--Select--"));
}


function BindDDLCommon(controlId, param, controller, action) {
    //debugger;
    $('#' + controlId).empty();
    $("#" + controlId).append($("<option></option>").val("0").html("--Select--"));

    $.ajax({
        async: false,
        url: "/" + controller + "/" + action,
        data: { Id: param },
        contentType: "application/json; charset=utf-8",
        cashe: false,
        success: function (data) {
            //debugger
            $.each(data, function (key, value) {
                $("#" + controlId).append($("<option></option>").val(value.Value).html(value.Text));
            });

        },
        error: function (a, b, c) {
        }
    });
}


function BindCountry(controlId, controller, action) {
    //debugger;
    $('#' + controlId).empty();
    $("#" + controlId).append($("<option></option>").val("0").html("--Select--"));

    EmptyDDL("ddlClusterId");
    EmptyDDL("ddlCityId");
    EmptyDDL("ddlSiteId");

    $.ajax({
        async: false,
        url: "/" + controller + "/" + action,
        //data: { Id: param },
        contentType: "application/json; charset=utf-8",
        cashe: false,
        success: function (data) {
            //debugger
            $.each(data, function (key, value) {
                $("#" + controlId).append($("<option></option>").val(value.Value).html(value.Text));
            });

        },
        error: function (a, b, c) {
        }
    });
}

function BindCategory(controlId, controller, action) {
    //debugger;
    $('#' + controlId).empty();
    $("#" + controlId).append($("<option></option>").val("0").html("--Select--"));

    $.ajax({
        async: false,
        url: "/" + controller + "/" + action,
        //data: { Id: param },
        contentType: "application/json; charset=utf-8",
        cashe: false,
        success: function (data) {
            //debugger
            $.each(data, function (key, value) {
                $("#" + controlId).append($("<option></option>").val(value.Value).html(value.Text));
            });

        },
        error: function (a, b, c) {
        }
    });
}

function BindTower(controlId, controller, action, SiteId) {
    //debugger;
    $('#' + controlId).empty();
    $("#" + controlId).append($("<option></option>").val("0").html("--Select--"));

    $.ajax({
        async: false,
        url: "/" + controller + "/" + action,
        data: { SiteId: SiteId },
        contentType: "application/json; charset=utf-8",
        cashe: false,
        success: function (data) {
            //debugger
            $.each(data, function (key, value) {
                $("#" + controlId).append($("<option></option>").val(value.Value).html(value.Text));
            });

        },
        error: function (a, b, c) {
        }
    });
}

function BindFloor(controlId, controller, action, TowerId) {
    //debugger;
    $('#' + controlId).empty();
    $("#" + controlId).append($("<option></option>").val("0").html("--Select--"));

    $.ajax({
        async: false,
        url: "/" + controller + "/" + action,
        data: { TowerId: TowerId },
        contentType: "application/json; charset=utf-8",
        cashe: false,
        success: function (data) {
            //debugger
            $.each(data, function (key, value) {
                $("#" + controlId).append($("<option></option>").val(value.Value).html(value.Text));
            });

        },
        error: function (a, b, c) {
        }
    });
}


function EmptyDDL(ddlId) {
    $('#' + ddlId).empty();
    $("#" + ddlId).append($("<option></option>").val("0").html("--Select--"));
}

function ClearPartialDDL() {
    EnableDisableControls("ddlCountryId", "enabled", "D");
    EnableDisableControls("ddlClusterId", "enabled", "D");
    EnableDisableControls("ddlCityId", "enabled", "D");
    EnableDisableControls("ddlSiteId", "enabled", "D");

    ClearControls("ddlCountryId", "D");
    EmptyDDL("ddlClusterId");
    EmptyDDL("ddlCityId");
    EmptyDDL("ddlSiteId");
}

function EnablePartialViewDDL() {
    EnableDisableControls("ddlCountryId", "enabled", "D");
    EnableDisableControls("ddlClusterId", "enabled", "D");
    EnableDisableControls("ddlCityId", "enabled", "D");
    EnableDisableControls("ddlSiteId", "enabled", "D");
}

function DisablePartialViewDDL() {
    EnableDisableControls("ddlCountryId", "disabled", "D");
    EnableDisableControls("ddlClusterId", "disabled", "D");
    EnableDisableControls("ddlCityId", "disabled", "D");
    EnableDisableControls("ddlSiteId", "disabled", "D");
}

function ClearDDL(viewModel) {
    //debugger;
    //Super Admin 
    if (viewModel.LoggedInUser.RoleId() == 1) {
        EnableDisableControls("ddlCountryId", "enabled", "D");
        EnableDisableControls("ddlClusterId", "enabled", "D");
        EnableDisableControls("ddlCityId", "enabled", "D");
        EnableDisableControls("ddlSiteId", "enabled", "D");
        BindCountry("ddlCountryId", "DDLCommon", "GetCountry");
        EmptyDDL("ddlClusterId");
        EmptyDDL("ddlCityId");
        EmptyDDL("ddlSiteId");
    }
        //Admin
    else if (viewModel.LoggedInUser.RoleId() == 2) {
        EnableDisableControls("ddlCountryId", "disabled", "D");
        EnableDisableControls("ddlClusterId", "disabled", "D");
        EnableDisableControls("ddlCityId", "enabled", "D");
        EnableDisableControls("ddlSiteId", "enabled", "D");
        BindCountry("ddlCountryId", "DDLCommon", "GetCountry");
        $("#ddlCountryId > [value=" + viewModel.LoggedInUser.CountryId() + "]").prop("selected", "true");
        BindDDLCommon("ddlClusterId", viewModel.LoggedInUser.CountryId(), "DDLCommon", "GetCluster");
        $("#ddlClusterId > [value=" + viewModel.LoggedInUser.ClusterId() + "]").prop("selected", "true");
        BindDDLCommon("ddlCityId", viewModel.LoggedInUser.ClusterId(), "DDLCommon", "GetCity");
        EmptyDDL("ddlSiteId");
    }
        //Supervisor
    else if (viewModel.LoggedInUser.RoleId() == 3) {
        BindCountry("ddlCountryId", "DDLCommon", "GetCountry");
        $("#ddlCountryId > [value=" + viewModel.LoggedInUser.CountryId() + "]").prop("selected", "true");
        BindDDLCommon("ddlClusterId", viewModel.LoggedInUser.CountryId(), "DDLCommon", "GetCluster");
        $("#ddlClusterId > [value=" + viewModel.LoggedInUser.ClusterId() + "]").prop("selected", "true");
        BindDDLCommon("ddlCityId", viewModel.LoggedInUser.ClusterId(), "DDLCommon", "GetCity");
        $("#ddlCityId > [value=" + viewModel.LoggedInUser.CityId() + "]").prop("selected", "true");
        BindDDLCommon("ddlSiteId", viewModel.LoggedInUser.CityId(), "DDLCommon", "GetSite");
        $("#ddlSiteId > [value=" + viewModel.LoggedInUser.SiteId() + "]").prop("selected", "true");
        DisablePartialViewDDL();
    }

}

// to remove space
function myTrim(x) {
    // debugger;
    return x.replace(/^\s+|\s+$/gm, '');
}

// Kushal Srivastava
function HideShowControls(CntrlId, Status) {
    if (Status == "Hide") {
        $('#' + CntrlId).hide();
    }
    else if (Status == 'Show') {
        $('#' + CntrlId).show();
    }

}

function onlyDecimal(element, decimals) {
    $(element).keypress(function (event) {
        num = $(this).val();
        num = isNaN(num) || num === '' || num === null ? 0.00 : num;
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();

        }
        if ($(this).val() == parseFloat(num).toFixed(decimals)) {
            event.preventDefault();
        }
    });
}

function showMessage(errMsg, type, lines, autoSlideDown) {
    $('#errMsg').html(errMsg);
    var top = '92%';
    //$('#errImg').attr('src', '../Images/' + type + '.png');
    if (lines > 2) {
        top = '85%';
    }
    if (lines > 4) {
        top = '80%';
    }
    $('#errorSlider').animate({ 'top': top }, "6000", "linear");
    if (autoSlideDown) {
        setTimeout(function () { closeError() }, 6000)
    }
}
function closeError() {
    $('#errorSlider').animate({ 'top': '100%' }, "6000", "linear");
    $('#errMsg').html('');
}
function HideShowControl(CntrlArray, Action) {
    //debugger;
    if (Action == "H") {
        for (var i = 0; i < CntrlArray.length; i++) {
            $('#' + CntrlArray[i]).hide();
        }
    }
    else if (Action == "S") {
        for (var i = 0; i < CntrlArray.length; i++) {
            $('#' + CntrlArray[i]).show();
        }
    }
}
function ReadOnly(CtrlID, Value) {
    $('#' + CtrlID).prop("readonly", Value);
}

function ScrollTop() {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scrollToTop').fadeIn();
        } else {
            $('.scrollToTop').fadeOut();
        }
    });
    $('.scrollToTop').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });
}
function ScrollToTopInEdit() {
    $('html, body').animate({ scrollTop: 0 }, 800);
}

function IsANumber(inputVal) {
    //var intRegex = /^\d+$/;   
    var intRegex = /^[+-]?\d+(\.\d+)?([eE][+-]?\d+)?$/;
    if (intRegex.test(inputVal)) {
        return true;
    }
    else {
        return false
    }
}
