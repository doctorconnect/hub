
$(document).ready(function () {
    GetFAQ();

    $("button[type=submit]").click(function () {
        var FaqQuestion = $('#txtQuestion').val().trim().length;
        var FaqAnswer = $('#txtAnswer').val().trim().length;
        if (!FaqQuestion > 0 && !FaqAnswer > 0) {
            $('#errorFaqQueId').show();
            $('#errorFaqAnsId').show();
            return false;
        }
    });
});



function GetFAQ() {
    $.ajax({
        url: "/Admin/GetFAQ",
        method: 'post',
        dataType: 'json',
        success: function (data) {
            var datatableInstance = $('#tblManageFAQ').DataTable({
                destroy: true,
                paging: true,
                sort: false,
                searching: true,
                data: data,
                autoWidth: false,
                columns: [
                   { 'data': 'Id' },
                   { 'data': 'FaqQuestion' },
                   { 'data': 'FaqAnswer' },
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
                           return '<a title="Manage"  href="/Admin/ManageFaq?key=' + full.Id + '"><i class="glyphicon glyphicon-edit"></i>&nbsp;Manage</a>';
                       }
                   }
                ],
                columnDefs: [
                    {
                        render: function (data, type, full, meta) {
                            return "<div class='text-wrap width-200'>" + data + "</div>";
                        },
                        targets: 3
                    }
                ]
            });
        }
    });
}
