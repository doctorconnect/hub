$(function () {
    $('span.noti').click(function (e) {
        e.stopPropagation();
        showNotificationDetails();
    })

    function showNotificationDetails() {
        $('#notiContent').empty();
        $('#notiContent').append($('<li>Loading...</li>'));
        $.ajax({
            type: 'POST',
            url: '/Home/UpdateNotificationCount',
            success: function (result) {
                $('#notiContent').empty();
                if ((result.data).length > 0) {
                    //$.each(result.data, function (index, value) {
                    //    if (result.loggedRoleId == "1" || result.loggedRoleId == "10")
                    //        $('#notiContent').append($('<li><div class="col-md-3 col-sm-3 col-xs-3"><div class="notify-img"><img src="../images/avatar2.png" alt=""></div></div> <div class="col-md-9 col-sm-9 col-xs-9 pd-l0">' + value.AdminDescripation + '</div></li>'))

                    //    else
                    //        $('#notiContent').append($('<li><div class="col-md-3 col-sm-3 col-xs-3"><div class="notify-img"><img src="../images/avatar2.png" alt=""></div></div> <div class="col-md-9 col-sm-9 col-xs-9 pd-l0">' + value.UserDescripation + '</div></li>'))
                    //});
                    $.each(result.data, function (index, value) {
                        if (result.loggedRoleId == "1" || result.loggedRoleId == "10")
                            $('#notiContent').append($('<li><div class="col-md-12 col-sm-12 col-xs-12">' + value.AdminDescripation + '</div></li>'));

                        else
                            $('#notiContent').append($('<li><div class="col-md-12 col-sm-12 col-xs-12">' + value.UserDescripation + '</div></li>'));
                    });
                    $('.notify-drop').show();
                    $('.notilbl').hide();
                } else {
                    $('.notify-drop').hide();
                    alert('Not found any notification');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                window.location.href = "/Home/Error";
            }
        })
    }
})

