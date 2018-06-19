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
                $('#korisnikPodaci').hide();
                $('#dispecer').hide();
                $('#vozac').hide();
                $('#home1').addClass("active");
                $('#home2').removeClass("active");
                $('#home3').removeClass("active");
            }
            else if (data.Role == 1) {
                $('#korisnik').hide();
                $('#korisnikPodaci').hide();
                $('#dispecerPodaci').hide();
                $('#dispecer').show();
                $('#vozac').hide();
                $('#home2').addClass("active");
                $('#home1').removeClass("active");
                $('#home3').removeClass("active");
            }
            else {
                $('#korisnik').hide();
                $('#korisnikPodaci').hide();
                $('#dispecerPodaci').hide();
                $('#dispecer').hide();
                $('#vozac').show();
            }
        }
    });

    //**********************************************************************************************************************
    // Profil Korisnik
    //**********************************************************************************************************************
    //**********************************************************************************************************************
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


        $('#txtUsernameKorisnik').val(profil.KorisnickoIme);
        $('#txtEmailKorisnik').val(profil.Email);
        $('#txtPasswordKorisnik').val(profil.Lozinka);
        $('#txtFirstNameKorisnik').val(profil.Ime);
        $('#txtLastNameKorisnik').val(profil.Prezime);
        $('#txtJmbgKorisnik').val(profil.JMBG);
        $('#txtContactNumberKorisnik').val(profil.Telefon);

        if (profil.Gender == 0) {
            $("#gndrMaleKorisnik").prop('checked', true)
            $("#gndrFemaleKorisnik").prop('checked', false)
        } else {
            $("#gndrFemaleKorisnik").prop('checked', true)
            $("#gndrMaleKorisnik").prop('checked', false)
        }
    });


    $('#home1').click(function () {
        $('#korisnikPodaci').fadeOut(300);
        $('#jmbtrn1').delay(300).fadeIn(300);
        $('#footer').delay(300).fadeIn(300);
        $('#profilKorisnik').removeClass("active");
        $('#home1').addClass("active");
    });


    $('#btnSaveKorisnik').click(function () {
        var gndr;
        if ($('#gndrMaleKorisnik').is(':checked')) {
            gndr = $('#gndrMaleKorisnik').val();
        }
        else {
            gndr = $('#gndrFemaleKorisnik').val();
        }

        $.ajax({
            url: '/api/korisnik/put',
            method: 'PUT',
            data: {
                KorisnickoIme: $('#txtUsernameKorisnik').val(),
                Email: $('#txtEmailKorisnik').val(),
                Lozinka: $('#txtPasswordKorisnik').val(),
                confirmPassword: $('#txtConfirmPasswordKorisnik').val(),
                Ime: $('#txtFirstNameKorisnik').val(),
                Prezime: $('#txtLastNameKorisnik').val(),
                JMBG: $('#txtJmbgKorisnik').val(),
                Telefon: $('#txtContactNumberKorisnik').val(),
                Gender: gndr
            },
            success: function (data) {
                if (data != null) {
                    $('#txtUsernameKorisnik').val(data.KorisnickoIme);
                    $('#txtEmailKorisnik').val(data.Email);
                    $('#txtPasswordKorisnik').val(data.Lozinka);
                    $('#txtFirstNameKorisnik').val(data.Ime);
                    $('#txtLastNameKorisnik').val(data.Prezime);
                    $('#txtJmbgKorisnik').val(data.JMBG);
                    $('#txtContactNumberKorisnik').val(data.Telefon);

                    if (data.Gender == 0) {
                        $("#gndrMaleKorisnik").prop('checked', true)
                        $("#gndrFemaleKorisnik").prop('checked', false)
                    } else {
                        $("#gndrFemaleKorisnik").prop('checked', true)
                        $("#gndrMaleKorisnik").prop('checked', false)
                    }
                }
            }
        });
    });


    $('#logOutKorisnik').click(function () {
        localStorage.setItem("logged", "");
        window.location.href = "Index.html";
    });

    $('#logInKorisnik').click(function () {
        let check = localStorage.getItem("logged");
        let korisnickoIme = localStorage.getItem("logged");
        let Korisnik = {
            KorisnickoIme: `${korisnickoIme}`
        };

        if (check != "") {
            $('#jmbtrn1').fadeOut(300);
            $('#footer').fadeOut(300);
            $('#korisnikPodaci').delay(300).fadeIn(300);
            $('#home1').removeClass("active");
            $('#profilKorisnik').addClass("active");


            $('#txtUsernameKorisnik').val(profil.KorisnickoIme);
            $('#txtEmailKorisnik').val(profil.Email);
            $('#txtPasswordKorisnik').val(profil.Lozinka);
            $('#txtFirstNameKorisnik').val(profil.Ime);
            $('#txtLastNameKorisnik').val(profil.Prezime);
            $('#txtJmbgKorisnik').val(profil.JMBG);
            $('#txtContactNumberKorisnik').val(profil.Telefon);

            if (profil.Gender == 0) {
                $("#gndrMaleKorisnik").prop('checked', true)
                $("#gndrFemaleKorisnik").prop('checked', false)
            } else {
                $("#gndrFemaleKorisnik").prop('checked', true)
                $("#gndrMaleKorisnik").prop('checked', false)
            }
        } else {
            window.location.href = "Login.html"
        }
    });
    //**********************************************************************************************************************
    //**********************************************************************************************************************



    //**********************************************************************************************************************
    //Profil Dispecer
    //**********************************************************************************************************************
    //**********************************************************************************************************************
    $('#profilDispecer').click(function () {
        let korisnickoIme = localStorage.getItem("logged");
        let Korisnik = {
            KorisnickoIme: `${korisnickoIme}`
        };

        $('#jmbtrn2').fadeOut(300);
        $('#footer').fadeOut(300);
        $('#dispecerPodaci').delay(300).fadeIn(300);
        $('#home2').removeClass("active");
        $('#profilDispecer').addClass("active");


        $('#txtUsernameDispecer').val(profil.KorisnickoIme);
        $('#txtEmailDispecer').val(profil.Email);
        $('#txtPasswordDispecer').val(profil.Lozinka);
        $('#txtFirstNameDispecer').val(profil.Ime);
        $('#txtLastNameDispecer').val(profil.Prezime);
        $('#txtJmbgDispecer').val(profil.JMBG);
        $('#txtContactNumberDispecer').val(profil.Telefon);

        if (profil.Gender == 0) {
            $("#gndrMaleDispecer").prop('checked', true)
            $("#gndrFemaleDispecer").prop('checked', false)
        } else {
            $("#gndrFemaleDispecer").prop('checked', true)
            $("#gndrMaleDispecer").prop('checked', false)
        }
    });


    $('#home2').click(function () {
        $('#dispecerPodaci').fadeOut(300);
        $('#jmbtrn2').delay(300).fadeIn(300);
        $('#footer').delay(300).fadeIn(300);
        $('#profilDispecer').removeClass("active");
        $('#home2').addClass("active");
    });


    $('#logOutDispecer').click(function () {
        localStorage.setItem("logged", "");
        window.location.href = "Index.html";
    });

    $('#logInDispecer').click(function () {
        let check = localStorage.getItem("logged");
        let korisnickoIme = localStorage.getItem("logged");
        let Korisnik = {
            KorisnickoIme: `${korisnickoIme}`
        };

        if (check != "") {
            $('#jmbtrn2').fadeOut(300);
            $('#footer').fadeOut(300);
            $('#dispecerPodaci').delay(300).fadeIn(300);
            $('#home2').removeClass("active");
            $('#profilDispecer').addClass("active");


            $('#txtUsernameDispecer').val(profil.KorisnickoIme);
            $('#txtEmailDispecer').val(profil.Email);
            $('#txtPasswordDispecer').val(profil.Lozinka);
            $('#txtFirstNameDispecer').val(profil.Ime);
            $('#txtLastNameDispecer').val(profil.Prezime);
            $('#txtJmbgDispecer').val(profil.JMBG);
            $('#txtContactNumberDispecer').val(profil.Telefon);

            if (profil.Gender == 0) {
                $("#gndrMaleDispecer").prop('checked', true)
                $("#gndrFemaleDispecer").prop('checked', false)
            } else {
                $("#gndrFemaleDispecer").prop('checked', true)
                $("#gndrMaleDispecer").prop('checked', false)
            }
        } else {
            window.location.href = "Login.html"
        }
    });
    //**********************************************************************************************************************
    //**********************************************************************************************************************



    //**********************************************************************************************************************
    //Profil Vozac
    //**********************************************************************************************************************
    //**********************************************************************************************************************



    //**********************************************************************************************************************
    //**********************************************************************************************************************
});