$(document).ready(function () {

    //Close the bootstrap alert
    $('#linkClose').click(function () {
        $('#divError').hide('fade');
    });

    // Save the new user details
    $('#btnRegister').click(function () {

        var gndr;
        if ($('#gndrMale').is(':checked')) {
            gndr = $('#gndrMale').val();
        }
        else {
            gndr = $('#gndrFemale').val();
        }

        $.ajax({
            url: '/api/Registration/PostKorisnika',
            method: 'POST',
            data: {
                KorisnickoIme: $('#txtUsername').val(),
                Email: $('#txtEmail').val(),
                Lozinka: $('#txtPassword').val(),
                confirmPassword: $('#txtConfirmPassword').val(),
                Ime: $('#txtFirstName').val(),
                Prezime: $('#txtLastName').val(),
                JMBG: $('#txtJmbg').val(),
                Telefon: $('#txtContactNumber').val(),
                Gender: gndr
            },
            success: function () {
                //$('#txtUsername').val("");
                //$('#txtEmail').val("");
                //$('#txtPassword').val("");
                //$('#txtConfirmPassword').val("");
                //$('#txtFirstName').val("");
                //$('#txtLastName').val("");
                //$('#txtJmbg').val("");
                //$('#txtContactNumber').val("");
                //$('#txtGndrMale').prop('checked', false);
                //$('#txtGndrFemale').prop('checked', false);
                //$("input:radio").attr("checked", false);
                //$("input:radio").removeAttr("checked");
                $('input[type="text"]').val("");
                $('input[type="password"]').val("");
                $('input[name="gender"]').prop('checked', false);
                $('#successModal').modal('show');
                //window.location.href = "Index.html";
            },
            error: function (jqXHR) {
                $('#divErrorText').text(jqXHR.responseText);
                $('#divError').show('fade');
            }
        });
    });
});