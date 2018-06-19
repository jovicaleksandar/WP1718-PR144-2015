$(document).ready(function () {

    //Close the bootstrap alert
    $('#linkClose').click(function () {
        $('#divError').hide('fade');
    });

    // Save the new user details
    $('#btnAddDriver').click(function () {

        var gndr;
        if ($('#gndrMale').is(':checked')) {
            gndr = $('#gndrMale').val();
        }
        else {
            gndr = $('#gndrFemale').val();
        }

        $.ajax({
            url: '/api/Dispeceri/PostVozace',
            method: 'POST',
            data: {
                korisnickoIme: $('#txtUsername').val(),
                email: $('#txtEmail').val(),
                lozinka: $('#txtPassword').val(),
                confirmPassword: $('#txtConfirmPassword').val(),
                ime: $('#txtFirstName').val(),
                prezime: $('#txtLastName').val(),
                jmbg: $('#txtJmbg').val(),
                kontaktTelefon: $('#txtContactNumber').val(),
                pol: gndr
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