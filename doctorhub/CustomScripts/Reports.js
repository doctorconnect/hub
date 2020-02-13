$(function () {
    $('#txtDate').daterangepicker({
        "timePickerIncrement": 1,
        "opens": "left",
        "drops": "down",
        "buttonClasses": "btn btn-sm",
        "applyClass": "btn-success",
        "cancelClass": "btn-default",
        locale: {
            format: 'MM/DD/YYYY'
        }
    }, function (start, end, label) {
    });
    $("#tblTabularReport").hide();
    $("#myCharts").hide();
    if ($("#ddlReport").val() === "18") {
        $("#ddlCAP").attr('disabled', 'disabled');
    }
    else {
        $("#ddlCAP").removeAttr('disabled');
    }

    $("#ddlReport").change(function () {

        if ($(this).val() === "18") {

            $("#ddlCAP").attr('disabled', 'disabled');
        }
        else {
            $("#ddlCAP").removeAttr('disabled');
        }
    });
    $('.daterange-icon i').click(function () {
        $(this).parent().find('input').click();
    });
});

function GenerateReport() {
    var dateLen = $('#txtDate').val().trim().length;
    var ReportId = parseInt($('#ddlReport').val()) || 0;
    var IsError = false;
    if (!dateLen > 0) {
        $('#txtDate').addClass("validation_failed");
        $('#errorDateRangeSpan').show();
        IsError = true;
    }
    if (IsError) {
        if (!dateLen > 0) {
            $('#txtDate').focus();
        }
    }
    else {
        var selCriteria = document.getElementById("ddlFilterCriteria");
        var Criteria = selCriteria.options[selCriteria.selectedIndex].text;
        var DateRange = $('#txtDate').val();
        var Range = DateRange.split(" - ");
        var StartDate = Range[0];
        var EndDate = Range[1];
        var Capabilities = $("#ddlCAP").val();
        var CapabilitiesText = document.getElementById("ddlCAP");
        var CapText = CapabilitiesText.options[CapabilitiesText.selectedIndex].text;
        GetUtilizationReport(ReportId, StartDate, EndDate, Criteria, Capabilities, CapText);
    }
}

function GetUtilizationReport(ReportId, StartDate, EndDate, Criteria, Capabilities, CapText) {
    $("#barcanvas").remove();// removing previous canvas element
    $("#piecanvas").remove();// removing previous canvas element
    //change the data values or add new values for new graph
    $("#myChartPie").after("<canvas id='piecanvas'></canvas>");
    $("#myChartBar").after("<canvas id='barcanvas'></canvas>");
    $("#tblTabularReport").show();
    $("#myCharts").show();
    $.ajax({
        url: "/Admin/GetUtilizationReport",
        method: 'post',
        dataType: 'json',
        data: { ReportId: ReportId, StartDate: StartDate, EndDate: EndDate, Criteria: Criteria, Capabilities: Capabilities, CapText: CapText },
        success: function (data) {
            datatableInstance = $('#tblTabularReport').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                dom: 'Bfrtip',
                buttons: [
                    'copyHtml5', 'print', { extend: 'csvHtml5', filename: 'DateTime Report' },
                    { extend: 'excelHtml5', filename: 'DateTime Report' }
                ],
                columns: [
                    { 'data': 'Interval' },
                    { 'data': 'Utilization' }
                ]
            });
            var labels = []; var y = []; var color = [];
            var dynamicColors = function () {
                var r = Math.floor(Math.random() * 255);
                var g = Math.floor(Math.random() * 255);
                var b = Math.floor(Math.random() * 255);
                return "rgb(" + r + "," + g + "," + b + ")";
            };
            for (i = 0; i < data.length; i++) {
                labels.push(data[i].Interval);
                y.push(data[i].Utilization);
                color.push(dynamicColors());
            }

            var myBarChart = new Chart($("#barcanvas"), {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        backgroundColor: color,
                        data: y,
                        borderWidth: 1
                    }]
                },
                options: { legend: { display: false }, responsive: true, maintainAspectRatio: true }
            });
            myBarChart.render();

            var myPieChart = new Chart($("#piecanvas"), {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [{
                        backgroundColor: color,
                        data: y,
                        borderWidth: 1
                    }]
                },
                options: { legend: { display: true, position: 'bottom' }, responsive: true, maintainAspectRatio: true }
            });
            myPieChart.render();
            //var dataPoints = [];
            //for (var i = 0; i < data.length; i++) {
            //    dataPoints.push({
            //        x: new Date(data[i].Interval),
            //        y: data[i].Utilization
            //    });
            //}
            //var pieChart = new CanvasJS.Chart("pieChartContainer", {
            //    title: { text: "Pie Chart" },
            //    animationEnabled: true,
            //    data: [{
            //        type: "pie",
            //        dataPoints: dataPoints
            //    }]
            //});
            //pieChart.render();
            //var barChart = new CanvasJS.Chart("barChartContainer", {
            //    title: { text: "Bar Graph" },
            //    animationEnabled: true,
            //    data: [{
            //        type: "bar",
            //        dataPoints: dataPoints
            //    }]
            //});
            //barChart.render();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location.href = "/Home/Error";
        }
    });
}

//function DownloadCharts() {
//    // get size of report page
//    var reportPageHeight = $('#myCharts').innerHeight();
//    var reportPageWidth = $('#myCharts').innerWidth();

//    // create a new canvas object that we will populate with all other canvas objects
//    var pdfCanvas = $('<canvas />').attr({
//        id: "canvaspdf",
//        width: reportPageWidth,
//        height: reportPageHeight
//    });

//    // keep track canvas position
//    var pdfctx = $(pdfCanvas)[0].getContext('2d');
//    var pdfctxX = 0;
//    var pdfctxY = 0;
//    var buffer = 100;

//    // for each chart.js chart
//    $("canvas").each(function (index) {
//        // get the chart height/width
//        var canvasHeight = $(this).innerHeight();
//        var canvasWidth = $(this).innerWidth();

//        // draw the chart into the new canvas
//        pdfctx.drawImage($(this)[0], pdfctxX, pdfctxY, canvasWidth, canvasHeight);
//        pdfctxX += canvasWidth + buffer;
//        // our report page is in a grid pattern so replicate that in the new canvas
//        if (index % 2 === 1) {
//            pdfctxX = 0;
//            pdfctxY += canvasHeight + buffer;
//        }
//    });

//    // create new pdf and add our new canvas as an image
//    var pdf = new jsPDF('l', 'pt', [reportPageWidth, reportPageHeight]);
//    pdf.addImage($(pdfCanvas)[0], 'PNG', 0, 0);

//    // download the pdf
//    pdf.save('filename.pdf');
//};

//donwload pdf from original canvas
function DownloadCharts() {
    var doc = new jsPDF('p', 'pt', 'a4');
    doc.text(270, 20, 'Bar Chart');
    var canvas = document.querySelector('#barcanvas');

    var w = canvas.width;
    var h = canvas.height;
    var context = canvas.getContext('2d');
    var data;
    //get the current ImageData for the canvas.
    data = context.getImageData(0, 0, w, h);
    //store the current globalCompositeOperation
    var compositeOperation = context.globalCompositeOperation;
    //set to draw behind current content
    context.globalCompositeOperation = "destination-over";
    //set background color
    context.fillStyle = "white";
    //draw background / rect on entire canvas
    context.fillRect(0, 0, w, h);

    var canvasImg = canvas.toDataURL("image/jpeg", 1.0, 0);

    //clear the canvas
    context.clearRect(0, 0, w, h);
    //restore it with original / cached ImageData
    context.putImageData(data, 0, 0);
    //reset the globalCompositeOperation to what it was
    context.globalCompositeOperation = compositeOperation;

    doc.addImage(canvasImg, 'JPEG', 20, 50, 560, 280);
    doc.addPage();
    doc.text(270, 20, 'Pie Chart');
    var piecanvas = document.querySelector('#piecanvas');

    w = piecanvas.width;
    h = piecanvas.height;
    var piecontext = piecanvas.getContext('2d');
    var piedata;
    //get the current ImageData for the canvas.
    piedata = piecontext.getImageData(0, 0, w, h);
    //store the current globalCompositeOperation
    compositeOperation = context.globalCompositeOperation;
    //set to draw behind current content
    piecontext.globalCompositeOperation = "destination-over";
    //set background color
    piecontext.fillStyle = "white";
    //draw background / rect on entire canvas
    piecontext.fillRect(0, 0, w, h);

    piecanvasImg = piecanvas.toDataURL("image/jpeg", 1.0, 0);

    //clear the canvas
    piecontext.clearRect(0, 0, w, h);
    //restore it with original / cached ImageData
    piecontext.putImageData(data, 0, 0);
    //reset the globalCompositeOperation to what it was
    piecontext.globalCompositeOperation = compositeOperation;

    doc.addImage(piecanvasImg, 'JPEG', 20, 50, 560, 280);
    doc.save('canvas.pdf');
}


//download pdf form hidden canvas
function downloadPDF2() {
    var newCanvas = document.querySelector('#supercool-canvas');

    //create image from dummy canvas
    var newCanvasImg = newCanvas.toDataURL("image/jpeg", 1.0);

    //creates PDF from img
    var doc = new jsPDF('landscape');
    doc.setFontSize(20);
    doc.text(15, 15, "Super Cool Chart");
    doc.addImage(newCanvasImg, 'JPEG', 10, 10, 280, 150);
    doc.save('new-canvas.pdf');
}
