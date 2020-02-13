function ajaxindicatorstart(text) {
      if ($('body').find('#resultLoading').attr('id') != 'resultLoading') {
        $('body').append('<div id="resultLoading" style="display:none"><div><img src="../../images/Loader.gif" alt="" /><div>' + text + '</div></div><div class="bg"></div></div>');
    }

    $('#resultLoading').css({
        'width': '100%',
        'height': '100%',
        'position': 'fixed',
        'z-index': '10000000',
        'top': '0',
        'left': '0',
        'right': '0',
        'bottom': '0',
        'margin': 'auto'
    });

    $('#resultLoading .bg').css({
        'background': '#000000',
        'opacity': '0.7',
        'width': '100%',
        'height': '100%',
        'position': 'absolute',
        'top': '0'
    });

    $('#resultLoading>div:first').css({
        'width': '250px',
        'height': '75px',
        'text-align': 'center',
        'position': 'fixed',
        'top': '0',
        'left': '0',
        'right': '0',
        'bottom': '0',
        'margin': 'auto',
        'font-size': '16px',
        'z-index': '10',
        'color': '#ffffff'
            
    });

    $('#resultLoading .bg').height('100%');
    $('#resultLoading').fadeIn(0);
    $('body').css('cursor', 'wait');
}

function ajaxindicatorstop() {
    $('#resultLoading .bg').height('100%');
    $('#resultLoading').fadeOut(300);
    $('body').css('cursor', 'default');
}

$(document).ajaxStart(function (e) {
    //show ajax indicator
         ajaxindicatorstart('Processing, please wait...');
}).ajaxStop(function () {
        //hide ajax indicator
    ajaxindicatorstop();
});

//on form submit
$('form').on("submit", function () {
      //ShowProgress();
    ajaxindicatorstart('Processing, please wait...');
});
