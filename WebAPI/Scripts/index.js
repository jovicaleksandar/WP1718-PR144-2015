$(document).ready(function () {
    let korisnickoIme = localStorage.getItem("logged");
    let Korisnik = {
        KorisnickoIme: `${korisnickoIme}`
    };
    let check;
    let profil;

    $('#korisnik').show();
    $('#dispecer').hide();
    $('#vozac').hide();
    $('#korisnikPodaci').hide();
    $('#home1').addClass("active");

    $.ajax({
        url: '/api/Index',
        type: 'POST',
        data: JSON.stringify(Korisnik),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            profil = data;
            if (data.Role == 0) {
                $('#korisnik').show();
                $('#korisnikPodaci').hide();
                $('#dispecer').hide();
                $('#vozac').hide();
            }
            else if (data.Role == 1) {
                $('#korisnik').hide();
                $('#korisnikPodaci').hide();
                $('#dispecer').show();
                $('#vozac').hide();
            }
            else {
                $('#korisnik').hide();
                $('#korisnikPodaci').hide();
                $('#dispecer').hide();
                $('#vozac').show();
            }
        }
    });


    $('#profilKorisnik').click(function () {
        let korisnickoIme = localStorage.getItem("logged");
        let Korisnik = {
            KorisnickoIme: `${korisnickoIme}`
        };

        $('#jmbtrn1').fadeOut(300);
        $('#footer').fadeOut(300);
        $('#korisnikPodaci').delay(300).fadeIn(300);
        $('#home1').removeClass("active");
        $('#profilKorisnik').addClass("active");


        $('#txtUsername').val(profil.KorisnickoIme);
        $('#txtEmail').val(profil.Email);
        $('#txtPassword').val(profil.Lozinka);
        $('#txtFirstName').val(profil.Ime);
        $('#txtLastName').val(profil.Prezime);
        $('#txtJmbg').val(profil.JMBG);
        $('#txtContactNumber').val(profil.Telefon);

        if (profil.Gender == 0) {
            $("#gndrMale").prop('checked', true)
            $("#gndrFemale").prop('checked', false)
        } else {
            $("#gndrFemale").prop('checked', true)
            $("#gndrMale").prop('checked', false)
        }
    });


    $('#home1').click(function () {
        $('#korisnikPodaci').fadeOut(300);
        $('#jmbtrn1').delay(300).fadeIn(300);
        $('#footer').delay(300).fadeIn(300);
        $('#profilKorisnik').removeClass("active");
        $('#home1').addClass("active");
    });


    $('#btnSave').click(function () {
        $.ajax({
            url: '/api/Korisnik',
            method: 'PUT',
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
            success: function (data) {

            }
        });
    });
});