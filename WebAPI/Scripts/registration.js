//$(document).ready(function () {



function validateRegister() {
    $("#regForm").validate({
        rules: {
            username: {
                required: true,
                minlength: 4
            },
            email: {
                email: true
            },
            password: {
                required: true,
                minlength: 4
            },
            confirmPassword: {
                required: true,
                equalTo: '#txtPassword',
            },
            firstName: {
                required: true
            },
            lastName: {
                required: true
            },
            JMBG: {
                required: true,
                number: true,
                minlength: 13,
                maxlength: 13
            },
            contactNumber: {
                number: true
            }
        },
        messages: {
            username: {
                required: "Morate uneti ovo polje",
                minlength: "Korisnicko ime mora biti minimum 4 slova dugacak"
            },
            email: {
                email: "Morate uneti validnu e-mail adresu."
            },
            password: {
                required: "Morate uneti ovo polje",
                minlength: "Lozinka mora biti minimum 5 slova dugacak"
            },
            confirmPassword: {
                required: "Morate uneti ovo polje",
                equalTo: "Password i Confirm Password polja se ne poklapaju"
            },
            firstName: {
                required: "Morate uneti ovo polje"
            },
            lastName: {
                required: "Morate uneti ovo polje"
            },
            JMBG: {
                required: "Morate uneti ovo polje",
                number: "Ovo polje mora biti broj",
                minlength: "JMBG mora imati 13 cifara",
                maxlength: "JMBG mora imati 13 cifara"
            },
            contactNumber: {
                required: "Morate uneti ovo polje",
                number: "Ovo polje mora biti broj"
            }
        },

        submitHandler: function (form) { doRegistrationSubmit() }

    });
}






    //Close the bootstrap alert
    $('#linkClose').click(function () {
        $('#divError').hide('fade');
    });

    // Save the new user details
    //$('#btnRegister').click(function () {
    function doRegistrationSubmit() {
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
                $('input[type="email"]').val("");
                $('input[type="number"]').val("");
                $('input[name="gender"]').prop('checked', false);
                $('#successModal').modal('show');
                //window.location.href = "Index.html";
            },
            error: function (jqXHR) {
                $('#divErrorText').text(jqXHR.responseText);
                $('#divError').show('fade');
            }
        });
    }
//});