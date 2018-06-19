$(document).ready(function () {

    //Close the bootstrap alert
    $('#linkClose').click(function () {
        $('#divError').hide('fade');
    });

    // Save the new user details
    $('#btnLogin').click(function () {

        let korisnik = {
            KorisnickoIme: `${$('#txtUsername').val()}`,
            Lozinka: `${$('#txtPassword').val()}`,
        }


        $.ajax({
            url: '/api/Login',
            method: 'POST',
            data: JSON.stringify(korisnik),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',

            success: function (data) {
                $('input[type="text"]').val("");
                $('input[type="password"]').val("");
                if (data) {
                    $('#successModal').modal('show');
                    //window.location.href = "Index.html";
                    localStorage.setItem("logged", korisnik.KorisnickoIme);
                }
                else {
                    window.location.href = "Registration.html";
                }
            },
            error: function (jqXHR) {
                $('#divErrorText').text(jqXHR.responseText);
                $('#divError').show('fade');
            }
        });
    });
});