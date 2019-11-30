function func(x) {
    var name = document.getElementById("name").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    $.ajax({
        url: '@Url.Content("~/Login/UserRegistration/")',
        type: 'POST',
        dataType: 'application/json',
        data: { 'name': name, 'email': email, 'password': password },
        success: function (response) {
            alert(responseText.text);
        }
    });

}