$(document).ready(function () {
    let korisnickoIme = localStorage.getItem("logged");
    let Korisnik = {
        KorisnickoIme: `${korisnickoIme}`
    };
    let check;
    let profil;

    var temp;

    $.ajax({
        url: '/api/korisnik',
        method: 'GET',
        success: function (data) {
            temp = data;
        }
    });

    $('#korisnik').show();
    $('#dispecer').hide();
    $('#vozac').hide();
    $('#korisnikPodaci').hide();
    $('#novaVoznjaKorisnik').hide();
    //$('#dispecerPodaci').hide();
    //$('#vozacPodaci').hide();
    //$('#dispecerNoviVozac').hide();
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
                $('#novaVoznjaKorisnik').hide();
                $('#dispecerNoviVozac').hide();
                $('#dispecer').hide();
                $('#vozac').hide();
                $('#home1').addClass("active");
                $('#home2').removeClass("active");
                $('#home3').removeClass("active");
            }
            else if (data.Role == 1) {
                $('#korisnik').hide();
                $('#korisnikPodaci').hide();
                $('#dispecerNoviVozac').hide();
                $('#dispecerPodaci').hide();
                $('#dispecer').show();
                $('#vozac').hide();
                $('#home2').addClass("active");
                $('#home1').removeClass("active");
                $('#home3').removeClass("active");
            }
            else {
                $('#vozac').show();
                $('#vozacLocation').hide();
                $('#vozacPodaci').hide();
                $('#korisnik').hide();
                $('#korisnikPodaci').hide();
                $('#dispecerNoviVozac').hide();
                $('#dispecerPodaci').hide();
                $('#dispecer').hide();
                $('#home3').addClass("active");
                $('#home1').removeClass("active");
                $('#home2').removeClass("active");
            }
        }
    });

    //**********************************************************************************************************************
    // Korisnik
    //**********************************************************************************************************************
    //**********************************************************************************************************************
    $('#profilKorisnik').click(function () {
        let korisnickoIme = localStorage.getItem("logged");
        let Korisnik = {
            KorisnickoIme: `${korisnickoIme}`
        };
        
        $('#jmbtrn1').fadeOut(300);
        $('#footer').fadeOut(300);
        $('#dispecerNoviVozac').fadeOut(300);
        $('#novaVoznjaKorisnik').fadeOut(300);
        $('#korisnikPodaci').delay(300).fadeIn(300);
        $('#home1').removeClass("active");
        $('#voznjaKorisnik').removeClass("active");
        $('#profilKorisnik').addClass("active");


        $('#txtUsernameKorisnik').val(profil.KorisnickoIme);
        $('#txtEmailKorisnik').val(profil.Email);
        $('#txtPasswordKorisnik').val(profil.Lozinka);
        $('#txtFirstNameKorisnik').val(profil.Ime);
        $('#txtLastNameKorisnik').val(profil.Prezime);
        $('#txtJmbgKorisnik').val(profil.JMBG);
        $('#txtContactNumberKorisnik').val(profil.Telefon);

        if (profil.Gender == 0) {
            $("#gndrMaleKorisnik").prop('checked', true);
            $("#gndrFemaleKorisnik").prop('checked', false);
        } else {
            $("#gndrFemaleKorisnik").prop('checked', true);
            $("#gndrMaleKorisnik").prop('checked', false);
        }
    });


    $('#home1').click(function () {
        $('#korisnikPodaci').fadeOut(300);
        $('#jmbtrn1').delay(300).fadeIn(300);
        $('#footer').delay(300).fadeIn(300);
        $('#profilKorisnik').removeClass("active");
        $('#voznjaKorisnik').removeClass("active");
        $('#dispecerNoviVozac').removeClass("active");
        $('#home1').addClass("active");
    });

    $('#voznjaKorisnik').click(function () {

        let korisnickoIme = localStorage.getItem("logged");
        let Korisnik = {
            KorisnickoIme: `${korisnickoIme}`
        };

        $('#jmbtrn1').fadeOut(300);
        $('#footer').fadeOut(300);
        $('#dispecerNoviVozac').fadeOut(300);
        $('#korisnikPodaci').fadeOut(300);
        $('#novaVoznjaKorisnik').delay(300).fadeIn(300);
        $('#home1').removeClass("active");
        $('#profilKorisnik').removeClass("active");
        $('#voznjaKorisnik').addClass("active");
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
                        $("#gndrMaleKorisnik").prop('checked', true);
                        $("#gndrFemaleKorisnik").prop('checked', false);
                    } else {
                        $("#gndrFemaleKorisnik").prop('checked', true);
                        $("#gndrMaleKorisnik").prop('checked', false);
                    }
                }
            }
        });
    });


    $("#carTypeNovaVoznja").click(function () {
        $("#miniVanTypeNovaVoznja").prop("checked", false);
        $('#carTypeNovaVoznja').prop("checked", true);
    });


    $("#miniVanTypeNovaVoznja").click(function () {
        $("#carTypeNovaVoznja").prop("checked", false);
        $('#miniVanTypeNovaVoznja').prop("checked", true);
    });


    $('#btnSaveNovaVoznja').click(function () {

        var type;
        if ($('#carTypeVozac').is(':checked')) {
            type = $('#carTypeVozac').val();
        }
        else {
            type = $('#miniVanTypeVozac').val();
        }

        let adresa = {
            UlicaBroj: $('#txtStreetNumNovaVoznja').val(),
            NaseljenoMesto: $('#txtCityNovaVoznja').val(),
            PozivniBroj: $('#txtZipCodeNovaVoznja').val()
        };

        let lokacija = {
            X: $('#txtCoordinateXNovaVoznja').val(),
            Y: $('#txtCoordinateYNovaVoznja').val(),
            Adresa: adresa
        };



        $.ajax({
            url: '/api/voznja',
            method: 'POST',
            data: {
                LokacijaDolaskaTaksija: lokacija,
                Automobil: type,
                Musterija: temp
            },
            success: function (data) {
                $('#txtStreetNumNovaVoznja').val("");
                $('#txtCityNovaVoznja').val("");
                $('#txtZipCodeNovaVoznja').val("");
                $('#txtCoordinateXNovaVoznja').val("");
                $('#txtCoordinateYNovaVoznja').val("");
                //Dodaj tip vozila
                window.location.href = "Index.html";
            }
        });
    });

    $('#btnOtkaziTrenutnuVoznju').click(function () {
        $.ajax({
            url: '/api/voznja/' + temp,
            method: 'PUT',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data) {
                    window.location.href = "Index.html";
                } else {
                    window.location.href = "Registration.html";
                }
            }
        });
    });


    $('#logOutKorisnik').click(function () {
        $.ajax({
            url: '/api/login',
            type: 'GET',
            success: function (data) {
                localStorage.setItem("logged", "");
                window.location.href = "Index.html";
            }
        });
    });

    $('#logInKorisnik').click(function () {
        let check = localStorage.getItem("logged");
        let korisnickoIme = localStorage.getItem("logged");
        let Korisnik = {
            KorisnickoIme: `${korisnickoIme}`
        };

        if (check != null && check != "") {
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
                $("#gndrMaleKorisnik").prop('checked', true);
                $("#gndrFemaleKorisnik").prop('checked', false);
            } else {
                $("#gndrFemaleKorisnik").prop('checked', true);
                $("#gndrMaleKorisnik").prop('checked', false);
            }
        } else {
            window.location.href = "Login.html";
        }
    });
    //**********************************************************************************************************************
    //**********************************************************************************************************************



    //**********************************************************************************************************************
    //Dispecer
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
        $('#dispecerNoviVozac').fadeOut(300);
        $('#home2').removeClass("active");
        $('#dodajVozaca').removeClass("active");
        $('#profilDispecer').addClass("active");


        $('#txtUsernameDispecer').val(profil.KorisnickoIme);
        $('#txtEmailDispecer').val(profil.Email);
        $('#txtPasswordDispecer').val(profil.Lozinka);
        $('#txtFirstNameDispecer').val(profil.Ime);
        $('#txtLastNameDispecer').val(profil.Prezime);
        $('#txtJmbgDispecer').val(profil.JMBG);
        $('#txtContactNumberDispecer').val(profil.Telefon);

        if (profil.Gender == 0) {
            $("#gndrMaleDispecer").prop('checked', true);
            $("#gndrFemaleDispecer").prop('checked', false);
        } else {
            $("#gndrFemaleDispecer").prop('checked', true);
            $("#gndrMaleDispecer").prop('checked', false);
        }
    });


    $('#home2').click(function () {
        $('#dispecerPodaci').fadeOut(300);
        $('#dispecerNoviVozac').fadeOut(300);
        $('#jmbtrn2').delay(300).fadeIn(300);
        $('#footer').delay(300).fadeIn(300);
        $('#profilDispecer').removeClass("active");
        $('#dodajVozaca').removeClass("active");
        $('#home2').addClass("active");
    });


    $('#logOutDispecer').click(function () {
        $.ajax({
            url: '/api/login',
            type: 'GET',
            success: function (data) {
                localStorage.setItem("logged", "");
                window.location.href = "Index.html";
            }
        });
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
            $('#dodajVozaca').removeClass("active");
            $('#profilDispecer').addClass("active");


            $('#txtUsernameDispecer').val(profil.KorisnickoIme);
            $('#txtEmailDispecer').val(profil.Email);
            $('#txtPasswordDispecer').val(profil.Lozinka);
            $('#txtFirstNameDispecer').val(profil.Ime);
            $('#txtLastNameDispecer').val(profil.Prezime);
            $('#txtJmbgDispecer').val(profil.JMBG);
            $('#txtContactNumberDispecer').val(profil.Telefon);

            if (profil.Gender == 0) {
                $("#gndrMaleDispecer").prop('checked', true);
                $("#gndrFemaleDispecer").prop('checked', false);
            } else {
                $("#gndrFemaleDispecer").prop('checked', true);
                $("#gndrMaleDispecer").prop('checked', false);
            }
        } else {
            window.location.href = "Login.html";
        }
    });


    $('#dodajVozaca').click(function () {
        $('#jmbtrn2').fadeOut(300);
        $('#footer').fadeOut(300);
        $('#dispecerPodaci').fadeOut(300);
        $('#dispecerNoviVozac').delay(300).fadeIn(300);
        $('#dodajVozaca').addClass("active");
        $('#home2').removeClass("active");
        $('#profilDispecer').removeClass("active");
    });


    $('#btnSaveDispecer').click(function () {
        var gndr;
        if ($('#gndrMaleDispecer').is(':checked')) {
            gndr = $('#gndrMaleDispecer').val();
        }
        else {
            gndr = $('#gndrFemaleDispecer').val();
        }

        $.ajax({
            url: '/api/korisnik/put',
            method: 'PUT',
            data: {
                KorisnickoIme: $('#txtUsernameDispecer').val(),
                Email: $('#txtEmailDispecer').val(),
                Lozinka: $('#txtPasswordDispecer').val(),
                confirmPassword: $('#txtConfirmPasswordDispecer').val(),
                Ime: $('#txtFirstNameDispecer').val(),
                Prezime: $('#txtLastNameDispecer').val(),
                JMBG: $('#txtJmbgDispecer').val(),
                Telefon: $('#txtContactNumberDispecer').val(),
                Gender: gndr
            },
            success: function (data) {
                if (data != null) {
                    $('#txtUsernameDispecer').val(data.KorisnickoIme);
                    $('#txtEmailDispecer').val(data.Email);
                    $('#txtPasswordDispecer').val(data.Lozinka);
                    $('#txtFirstNameDispecer').val(data.Ime);
                    $('#txtLastNameDispecer').val(data.Prezime);
                    $('#txtJmbgDispecer').val(data.JMBG);
                    $('#txtContactNumberDispecer').val(data.Telefon);

                    if (data.Gender == 0) {
                        $("#gndrMaleDispecer").prop('checked', true);
                        $("#gndrFemaleDispecer").prop('checked', false);
                    } else {
                        $("#gndrFemaleDispecer").prop('checked', true);
                        $("#gndrMaleDispecer").prop('checked', false);
                    }
                }
            }
        });
    });

    $('#btnAddNoviVozac').click(function () {
        let logged = localStorage.getItem("logged");
        if (logged == "") {
            window.location.href = "Login.html";
        }

        var gndr;
        if ($('#gndrMaleNoviVozac').is(':checked')) {
            gndr = $('#gndrMaleNoviVozac').val();
        }
        else {
            gndr = $('#gndrFemaleNoviVozac').val();
        }

        var type;
        if ($('#carTypeNoviVozac').is(':checked')) {
            type = $('#carTypeNoviVozac').val();
        }
        else {
            type = $('#miniVanTypeNoviVozac').val();
        }

        let automobil = {
            GodisteAutomobila: $('#txtYearOfManufactureNoviVozac').val(),
            BrojRegistarskeOznake: $('#txtLicencePlateNoviVozac').val(),
            BrojTaksiVozila: $('#txtCarIDNoviVozac').val(),
            Tip: type
        };

        let adresa = {
            UlicaBroj: $('#txtStreetNumNoviVozac').val(),
            NaseljenoMesto: $('#txtCityNoviVozac').val(),
            PozivniBroj: $('#txtZipCodeNoviVozac').val()
        };

        let lokacija = {
            X: $('#txtCoordinateXNoviVozac').val(),
            Y: $('#txtCoordinateYNoviVozac').val(),
            Adresa: adresa
        };

        $.ajax({
            url: '/api/dispecer/postvozac',
            method: 'POST',
            data: {
                KorisnickoIme: $('#txtUsernameNoviVozac').val(),
                Email: $('#txtEmailNoviVozac').val(),
                Lozinka: $('#txtPasswordNoviVozac').val(),
                Ime: $('#txtFirstNameNoviVozac').val(),
                Prezime: $('#txtLastNameNoviVozac').val(),
                JMBG: $('#txtJmbgNoviVozac').val(),
                Telefon: $('#txtContactNoviVozac').val(),
                Gender: gndr,
                Automobil: automobil,
                Lokacija: lokacija
                //X: $('#txtCoordinateXNoviVozac').val(),
                //Y: $('#txtCoordinateYNoviVozac').val(),
                //UlicaBroj: $('#txtStreetNumNoviVozac').val(),
                //NaseljenoMesto: $('#txtCityNoviVozac').val(),
                //PozivniBroj: $('#txtZipCodeNoviVozac').val(),
                //GodisteAutomobila: $('#txtYearOfManufactureNoviVozac').val(),
                //BrojRegistarskeOznake: $('#txtLicencePlateNoviVozac').val(),
                //BrojTaksiVozila: $('#txtCarIDNoviVozac').val(),
                //Tip: type
            },
            success: function (data) {
                $('#txtUsernameNoviVozac').val(""),
                $('#txtEmailNoviVozac').val(""),
                $('#txtPasswordNoviVozac').val(""),
                $('#txtFirstNameNoviVozac').val(""),
                $('#txtLastNameNoviVozac').val(""),
                $('#txtJmbgNoviVozac').val(""),
                $('#txtContactNumberNoviVozac').val(""),
                $('#txtCoordinateXNoviVozac').val(""),
                $('#txtCoordinateYNoviVozac').val(""),
                $('#txtStreetNumNoviVozac').val(""),
                $('#txtCityNoviVozac').val(""),
                $('#txtZipCodeNoviVozac').val(""),
                $('#txtYearOfManufactureNoviVozac').val(""),
                $('#txtLicencePlateNoviVozac').val(""),
                $('#txtCarIDNoviVozac').val(""),
                $('input[type="radio"]').prop('checked', false);
            }
        });
    });

    //**********************************************************************************************************************
    //**********************************************************************************************************************



    //**********************************************************************************************************************
    //Profil Vozac
    //**********************************************************************************************************************
    //**********************************************************************************************************************

    $('#profilVozac').click(function () {
        let korisnickoIme = localStorage.getItem("logged");
        let Korisnik = {
            KorisnickoIme: `${korisnickoIme}`
        };

        $('#jmbtrn3').fadeOut(300);
        $('#vozacLocation').fadeOut(300);
        $('#footer').fadeOut(300);
        $('#vozacPodaci').delay(300).fadeIn(300);
        $('#home3').removeClass("active");
        $('#changeLocationVozac').removeClass("active");
        $('#profilVozac').addClass("active");


        $('#txtUsernameVozac').val(profil.KorisnickoIme);
        $('#txtEmailVozac').val(profil.Email);
        $('#txtPasswordVozac').val(profil.Lozinka);
        $('#txtFirstNameVozac').val(profil.Ime);
        $('#txtLastNameVozac').val(profil.Prezime);
        $('#txtJmbgVozac').val(profil.JMBG);
        $('#txtContactNumberVozac').val(profil.Telefon);
        $('#txtCoordinateXVozac').val(profil.Lokacija.X);
        $('#txtCoordinateYVozac').val(profil.Lokacija.Y);
        $('#txtStreetNumVozac').val(profil.Lokacija.Adresa.UlicaBroj);
        $('#txtCityVozac').val(profil.Lokacija.Adresa.NaseljenoMesto);
        $('#txtZipCodeVozac').val(profil.Lokacija.Adresa.PozivniBroj);
        $('#txtYearOfManufactureVozac').val(profil.Automobil.GodisteAutomobila);
        $('#txtLicencePlateVozac').val(profil.Automobil.BrojRegistarskeOznake);
        $('#txtCarIDVozac').val(profil.Automobil.BrojTaksiVozila);


        if (profil.Gender == 0) {
            $("#gndrMaleVozac").prop('checked', true);
            $("#gndrFemaleVozac").prop('checked', false);
        } else {
            $("#gndrFemaleVozac").prop('checked', true);
            $("#gndrMaleVozac").prop('checked', false);
        }

        if (profil.Automobil.Tip == 0) {
            $("#carTypeVozac").prop('checked', true);
            $("#miniVanTypeVozac").prop('checked', false);
        } else {
            $("#miniVanTypeVozac").prop('checked', true);
            $("#carTypeVozac").prop('checked', false);
        }
    });


    $('#home3').click(function () {
        $('#dispecerPodaci').fadeOut(300);
        $('#dispecerNoviVozac').fadeOut(300);
        $('#vozacLocation').fadeOut(300);
        $('#vozacPodaci').fadeOut(300);
        $('#jmbtrn3').delay(300).fadeIn(300);
        $('#footer').delay(300).fadeIn(300);
        $('#profilVozac').removeClass("active");
        $('#home3').addClass("active");
    });




    $('#logOutVozac').click(function () {
        $.ajax({
            url: '/api/login',
            type: 'GET',
            success: function (data) {
                localStorage.setItem("logged", "");
                window.location.href = "Index.html";
            }
        });
    });

    $('#logInVozac').click(function () {
        let check = localStorage.getItem("logged");
        let korisnickoIme = localStorage.getItem("logged");
        let Korisnik = {
            KorisnickoIme: `${korisnickoIme}`
        };

        if (check != "") {
            $('#jmbtrn3').fadeOut(300);
            $('#vozacLocation').fadeOut(300);
            $('#footer').fadeOut(300);
            $('#vozacPodaci').delay(300).fadeIn(300);
            $('#home3').removeClass("active");
            $('#profilVozac').addClass("active");


            $('#txtUsernameVozac').val(profil.KorisnickoIme);
            $('#txtEmailVozac').val(profil.Email);
            $('#txtPasswordVozac').val(profil.Lozinka);
            $('#txtFirstNameVozac').val(profil.Ime);
            $('#txtLastNameVozac').val(profil.Prezime);
            $('#txtJmbgVozac').val(profil.JMBG);
            $('#txtContactNumberVozac').val(profil.Telefon);

            if (profil.Gender == 0) {
                $("#gndrMaleVozac").prop('checked', true);
                $("#gndrFemaleVozac").prop('checked', false);
            } else {
                $("#gndrFemaleVozac").prop('checked', true);
                $("#gndrMaleVozac").prop('checked', false);
            }
        } else {
            window.location.href = "Login.html";
        }
    });

    $('#changeLocationVozac').click(function () {
        $('#jmbtrn3').fadeOut(300);
        $('#footer').fadeOut(300);
        $('#vozacPodaci').fadeOut(300);
        $('#vozacLocation').delay(300).fadeIn(300);
        $('#profilVozac').removeClass("active");
        $('#home3').removeClass("active");
        $('#changeLocationVozac').addClass("active");
    });


    $("#gndrMaleVozac").click(function () {
        $("#gndrFemaleVozac").prop("checked", false);
        $('#gndrMaleVozac').prop("checked", true);
    });


    $("#gndrFemaleVozac").click(function () {
        $("#gndrMaleVozac").prop("checked", false);
        $('#gndrFemaleVozac').prop("checked", true);
    });


    $("#carTypeVozac").click(function () {
        $("#miniVanTypeVozac").prop("checked", false);
        $('#carTypeVozac').prop("checked", true);
    });


    $("#miniVanTypeVozac").click(function () {
        $("#carTypeVozac").prop("checked", false);
        $('#miniVanTypeVozac').prop("checked", true);
    });


    $('#btnSaveVozac').click(function () {
        let logged = localStorage.getItem("logged");
        if (logged == "") {
            window.location.href = "Login.html";
        }


        var gndr;
        if ($('#gndrMaleVozac').is(':checked')) {
            gndr = $('#gndrMaleVozac').val();
        }
        else {
            gndr = $('#gndrFemaleVozac').val();
        }

        var type;
        if ($('#carTypeVozac').is(':checked')) {
            type = $('#carTypeVozac').val();
        }
        else {
            type = $('#miniVanTypeVozac').val();
        }

        let automobil = {
            GodisteAutomobila: $('#txtYearOfManufactureVozac').val(),
            BrojRegistarskeOznake: $('#txtLicencePlateVozac').val(),
            BrojTaksiVozila: $('#txtCarIDVozac').val(),
            Tip: type
        };

        let adresa = {
            UlicaBroj: $('#txtStreetNumVozac').val(),
            NaseljenoMesto: $('#txtCityVozac').val(),
            PozivniBroj: $('#txtZipCodeVozac').val()
        };

        let lokacija = {
            X: $('#txtCoordinateXVozac').val(),
            Y: $('#txtCoordinateYVozac').val(),
            Adresa: adresa
        };

        $.ajax({
            url: '/api/vozac/putvozac',
            method: 'PUT',
            data: {
                KorisnickoIme: $('#txtUsernameVozac').val(),
                Email: $('#txtEmailVozac').val(),
                Lozinka: $('#txtPasswordVozac').val(),
                Ime: $('#txtFirstNameVozac').val(),
                Prezime: $('#txtLastNameVozac').val(),
                JMBG: $('#txtJmbgVozac').val(),
                Telefon: $('#txtContactNumberVozac').val(),
                Gender: gndr,
                Automobil: automobil,
                Lokacija: lokacija
                //X: $('#txtCoordinateXNoviVozac').val(),
                //Y: $('#txtCoordinateYNoviVozac').val(),
                //UlicaBroj: $('#txtStreetNumNoviVozac').val(),
                //NaseljenoMesto: $('#txtCityNoviVozac').val(),
                //PozivniBroj: $('#txtZipCodeNoviVozac').val(),
                //GodisteAutomobila: $('#txtYearOfManufactureNoviVozac').val(),
                //BrojRegistarskeOznake: $('#txtLicencePlateNoviVozac').val(),
                //BrojTaksiVozila: $('#txtCarIDNoviVozac').val(),
                //Tip: type
            },
            success: function (data) {

            }
        });
    });




    $('#btnSaveVozacLocation').click(function () {
        let adresa = {
            UlicaBroj: $('#txtStreetNumVozacLocation').val(),
            NaseljenoMesto: $('#txtCityVozacLocation').val(),
            PozivniBroj: $('#txtZipCodeVozacLocation').val()
        };

        let lokacija = {
            X: $('#txtCoordinateXVozacLocation').val(),
            Y: $('#txtCoordinateYVozacLocation').val(),
            Adresa: adresa
        };



        $.ajax({
            url: '/api/location',
            method: 'PUT',
            data: {
                KorisnickoIme: profil.KorisnickoIme,
                Lokacija: lokacija
            },
            success: function (data) {
                $('#txtStreetNumVozacLocation').val(data.Lokacija.Adresa.UlicaBroj);
                $('#txtCityVozacLocation').val(data.Lokacija.Adresa.NaseljenoMesto);
                $('#txtZipCodeVozacLocation').val(data.Lokacija.Adresa.PozivniBroj);
                $('#txtCoordinateXVozacLocation').val(data.Lokacija.X);
                $('#txtCoordinateYVozacLocation').val(data.Lokacija.Y);
            }
        });

    });


    //**********************************************************************************************************************
    //**********************************************************************************************************************
});