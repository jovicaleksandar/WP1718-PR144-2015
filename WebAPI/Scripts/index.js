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
    $('#izmeniVoznjuKorisnik').hide();
    $('#modifikacijaVoznjeKorisnik').hide();
    $('#otkazKomentar').hide();
    $('#changeDrive').hide();
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
                $('#modifikujVoznjuKorisnik').removeClass("active");
                $('#modifikacijaVoznjeKorisnik').hide();
                $('#izmeniVoznjuKorisnik').hide();
                $('#otkazKomentar').hide();
            }
            else if (data.Role == 1) {
                $('#korisnik').hide();
                $('#korisnikPodaci').hide();
                $('#dispecerNoviVozac').hide();
                $('#dispecerPodaci').hide();
                $('#voznjeTrenutnogDispecera').hide();
                $('#dodajNovuVoznjuDispecer').hide();
                $('#obradiVoznjuDispecer').hide();
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
                $('#neuspesnaVoznjaKomentar').hide();
                $('#vozacOdrediste').hide();
                $('#korisnik').hide();
                $('#korisnikPodaci').hide();
                $('#dispecerNoviVozac').hide();
                $('#vozacStatus').fadeOut(300);
                $('#neprihvaceneVoznje').hide();
                $('#dispecerPodaci').hide();
                $('#dispecer').hide();
                $('#home3').addClass("active");
                $('#home1').removeClass("active");
                $('#home2').removeClass("active");
                $('#slobodneVoznje').removeClass("active");
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
        $('#modifikujVoznjuKorisnik').removeClass("active");
        $('#modifikacijaVoznjeKorisnik').hide();
        $('#izmeniVoznjuKorisnik').hide();
        $('#otkazKomentar').hide();


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
        $('#modifikujVoznjuKorisnik').removeClass("active");
        $('#novaVoznjaKorisnik').fadeOut(300);
        $('#modifikacijaVoznjeKorisnik').hide();
        $('#izmeniVoznjuKorisnik').hide();
        $('#otkazKomentar').hide();
    });

    //var username;
    //$.ajax({
    //    url: '/api/korisnik',
    //    method: 'GET',
    //    success: function (data) {
    //        username = data;
    //    }
    //});

    //var user = {
    //    KorisnickoIme: `${username}`
    //}

    $('#modifikujVoznjuKorisnik').click(function () {
        //let korisnickoIme = localStorage.getItem("logged");

        $('#modifikacijaVoznjeKorisnik').hide();
        $('#tabelaZaIzmenu').hide();
        $('#korisnikPodaci').fadeOut(300);
        $('#novaVoznjaKorisnik').fadeOut(300);
        $('#jmbtrn1').fadeOut(300);
        $('#izmeniVoznjuKorisnik').delay(300).fadeIn(300);
        $('#profilKorisnik').removeClass("active");
        $('#voznjaKorisnik').removeClass("active");
        $('#dispecerNoviVozac').removeClass("active");
        $('#home1').removeClass("active");
        $('#modifikujVoznjuKorisnik').addClass("active");
        $('#modifikujVoznjuKorisnik').removeClass("active");
        $('#otkazKomentar').hide();

        
        

        $.ajax({
            url: '/api/voznja',
            type: 'GET',
            success: function (data) {
                var voznje = data;
                //$('#tabelaZaIzmenu').hide();
                $('#changeDrive').hide();
                var table = `<thead><tr class="success"><th colspan="8" style="text-align:center">Rides</th></tr></thead>`;
                table += `<tbody><tr><th>ID</th><th>Street and number</th><th>Status</th><th>Otkazi</th><th>Izmeni</th><th>Korisnicko ime</th><th>Opis</th><th>Ocena</th>`;
                var row;
                //for (i = 0; i < data.Count; i++) {
                $(data).each(function (index) {
                    //var row = $('<tr>').addClass('success').text(data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj);
                    //table.append(row);

                    var id = data[index].IdVoznje;
                    var status;
                    if (data[index].Status == 0) {
                        status = "Kreirana na cekanju";
                    } else if (data[index].Status == 1) {
                        status = "Formirana";
                    } else if (data[index].Status == 2) {
                        status = "Obradjena";
                    } else if (data[index].Status == 3) {
                        status = "Prihvacena";
                    } else if (data[index].Status == 4) {
                        status = "Otkazana";
                    } else if (data[index].Status == 5) {
                        status = "Neuspesna";
                    } else if (data[index].Status == 6) {
                        status = "Uspesna";
                    } else if (data[index].Status == 7) {
                        status = "U toku";
                    } else {
                        status = "Nepoznato";
                    }

                    table += `<tr><td>${data[index].IdVoznje}</td><td> ${data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj} </td><td> ${status} </td>`;
                    table += `<td><input id="btnOtkaziVoznju${index}" class="btn btn-success" type="button" value="Otkazi" /></td>`;
                    table += `<td><input id="btnIzmeniVoznju${index}" class="btn btn-success" type="button" value="Izmeni" /></td>`;
                    table += `<td>${data[index].Komentar.KorisnickoIme}</td><td>${data[index].Komentar.Opis}</td><td>${data[index].Komentar.OcenaVoznje}</td></tr>`
                });

                $("#tabelaVoznji").html(table);

                //var tabela = `<thead><tr class="success"><th colspan="4" style="text-align:center">Modify Ride</th></tr></thead><tbody>`;

                //tabela += `<tr><td>Coordinate X</td><td><input type="text" class="form-control" id="changeCoordinateX" placeholder="Coordinate X" /></td></tr>`;


                //tabela += `</tbody>`;
                //$("#tabelaZaIzmenu").html(tabela);


                $(data).each(function (index) {
                    var id = data[index].IdVoznje;
                    $('#btnOtkaziVoznju' + index).click(function ()
                    {
                        var num = index;
                        var idvoznje = `${data[index].IdVoznje}`;
                        $.ajax({
                            url: `/api/voznja/` + data[index].IdVoznje,
                            type: 'DELETE',
                            data: {
                                id: num
                            },
                            success: function (data) {
                                $('#izmeniVoznjuKorisnik').delay(300).fadeOut(300);
                                $('#otkazKomentar').delay(300).fadeIn(300);


                                $('#btnSaveComment').click(function () {
                                    let opis = $('#txtCommentDescription').val();
                                    let ocena = $('#txtCommentGrade').val();
                                    let komentar = {
                                        Opis: `${opis}`,
                                        OcenaVoznje: `${ocena}`,
                                        IdVoznje: idvoznje
                                    };

                                    $.ajax({
                                        url: '/api/komentar',
                                        type: 'POST',
                                        data: JSON.stringify(komentar),
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        success: function (data) {
                                            $('#txtCommentDescription').val("");
                                            $('#txtCommentGrade').val("");
                                            window.location.href = "Index.html";
                                        }
                                    });
                                })
                            }
                        });
                    })

                    $('#btnIzmeniVoznju' + index).click(function () {
                        //alert("Izmena");
                        //$('#changeDrive').show();
                        $('#izmeniVoznjuKorisnik').hide();
                        $('#modifikujVoznjuKorisnik').addClass("active");
                        $('#modifikacijaVoznjeKorisnik').show();

                        
                        //$('#changeDrive').show();


                        var num = index;

                        $('#btnChange').click(function () {

                            var type;
                            if ($('#carTypeVozac').is(':checked')) {
                                type = $('#carTypeChange').val();
                            }
                            else {
                                type = $('#miniVanTypeChange').val();
                            }

                            let adresa = {
                                UlicaBroj: $('#txtStreetNumChange').val(),
                                NaseljenoMesto: $('#txtCityChange').val(),
                                PozivniBroj: $('#txtZipCodeChange').val()
                            };

                            let lokacija = {
                                X: $('#txtCoordinateXChange').val(),
                                Y: $('#txtCoordinateYChange').val(),
                                Adresa: adresa
                            };
                            
                            $.ajax({
                                url: `/api/voznja/` + id,
                                type: 'PUT',
                                data: {
                                    LokacijaDolaskaTaksija: lokacija,
                                    Automobil: type,
                                    Musterija: temp
                                },
                                success: function (data) {

                                }
                            });
                        })
                    })
                });
            }
        });


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
        $('#modifikujVoznjuKorisnik').removeClass("active");
        $('#izmeniVoznjuKorisnik').hide();
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


    /*$('#izmeniVoznjuKorisnik').click(function () {
        $('#modifikujVoznjuKorisnik').addClass("active");

    });*/


    $('#btnSaveNovaVoznjaDispecer').click(function () {
        var type;
        if ($('#carTypeNovaVoznjaDispecer').is(':checked')) {
            type = $('#carTypeNovaVoznjaDispecer').val();
        }
        else {
            type = $('#miniVanTypeNovaVoznjaDispecer').val();
        }

        let adresa = {
            UlicaBroj: $('#txtStreetNumNovaVoznjaDispecer').val(),
            NaseljenoMesto: $('#txtCityNovaVoznjaDispecer').val(),
            PozivniBroj: $('#txtZipCodeNovaVoznjaDispecer').val()
        };

        let lokacija = {
            X: $('#txtCoordinateXNovaVoznjaDispecer').val(),
            Y: $('#txtCoordinateYNovaVoznjaDispecer').val(),
            Adresa: adresa
        };



        $.ajax({
            url: '/api/dispecer/postvoznja',
            method: 'POST',
            data: {
                LokacijaDolaskaTaksija: lokacija,
                Automobil: type,
                Dispecer: temp
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

    $('#modifikujVoznjuDispecer').click(function () {
        //let korisnickoIme = localStorage.getItem("logged");

        $('#korisnik').hide();
        //$('#modifikacijaVoznjeKorisnik').hide();
        $('#dispecerNoviVozac').hide();
        $('#dodajNovuVoznjuDispecer').hide();
        $('#voznjeTrenutnogDispecera').hide();
        $('#novaVoznjaKorisnik').fadeOut(300);
        $('#jmbtrn2').fadeOut(300);
        $('#obradiVoznjuDispecer').delay(300).fadeIn(300);
        $('#profilDispecer').removeClass("active");
        $('#voznjaDispecer').removeClass("active");
        $('#dispecerNoviVozac').removeClass("active");
        $('#dispeceroveVoznje').removeClass("active");
        $('#home2').removeClass("active");
        



        $.ajax({
            url: '/api/voznja/getall',
            type: 'GET',
            success: function (data) {
                var voznje = data;

                var table = `<thead><tr class="success"><th colspan="8" style="text-align:center">Rides</th></tr></thead>`;
                table += `<tbody><tr><th>ID</th><th>Street and number</th><th>Status</th><th>Obradi</th><th>Korisnicko ime</th><th>Opis</th><th>Ocena</th>`;
                var row;
                //for (i = 0; i < data.Count; i++) {
                $(data).each(function (index) {
                    //var row = $('<tr>').addClass('success').text(data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj);
                    //table.append(row);

                    var status;
                    if (data[index].Status == 0) {
                        status = "Kreirana na cekanju";
                    } else if (data[index].Status == 1) {
                        status = "Formirana";
                    } else if (data[index].Status == 2) {
                        status = "Obradjena";
                    } else if (data[index].Status == 3) {
                        status = "Prihvacena";
                    } else if (data[index].Status == 4) {
                        status = "Otkazana";
                    } else if (data[index].Status == 5) {
                        status = "Neuspesna";
                    } else if (data[index].Status == 6) {
                        status = "Uspesna";
                    } else if (data[index].Status == 7) {
                        status = "U toku";
                    } else {
                        status = "Nepoznato";
                    }

                    table += `<tr><td>${data[index].IdVoznje}</td><td> ${data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj} </td><td> ${status} </td>`;
                    //table += `<td><input id="btnOtkaziVoznjuDispecer${index}" class="btn btn-success" type="button" value="Otkazi" /></td>`;
                    table += `<td><input id="btnObradiVoznjuDispecer${index}" class="btn btn-success" type="button" value="Obradi" /></td>`;
                    table += `<td>${data[index].Komentar.KorisnickoIme}</td><td>${data[index].Komentar.Opis}</td><td>${data[index].Komentar.OcenaVoznje}</td></tr>`
                });

                $("#tabelaVoznjiDispecer").html(table);



                $(data).each(function (index) {
                    $('#btnObradiVoznjuDispecer' + index).click(function () {
                        var num = index;
                        $.ajax({
                            url: `/api/dispecer/` + index,
                            type: 'PUT',
                            data: {
                                id: num
                            },
                            success: function (data) {
                                if (data) {
                                    alert("Odradio");
                                    window.location.href = "Index.html";
                                } else {
                                    alert("Nema slobodnih vozaca");
                                }
                            }
                        });
                    });
                });
            }
        });
    });

    /*$('#btnOtkaziTrenutnuVoznju').click(function () {

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
            url: '/api/voznja/' + temp,
            method: 'PUT',
            data: {
                LokacijaDolaskaTaksija: lokacija,
                Automobil: type,
                Musterija: temp
            },
            success: function (data) {
                if (data) {
                    window.location.href = "Index.html";
                } else {
                    window.location.href = "Registration.html";
                }
            }
        });
    });*/


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
        $('#dispeceroveVoznje').removeClass("active");
        $('#dodajVozaca').removeClass("active");
        $('#voznjeTrenutnogDispecera').hide();
        $('#obradiVoznjuDispecer').hide();
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

    $('#dispeceroveVoznje').click(function () {
        $('#dispecerPodaci').fadeOut(300);
        $('#dispecerNoviVozac').fadeOut(300);
        $('#jmbtrn2').fadeOut(300);
        $('#obradiVoznjuDispecer').hide();
        $('#footer').fadeOut(300);
        $('#profilDispecer').removeClass("active");
        $('#dodajVozaca').removeClass("active");
        $('#home2').removeClass("active");
        $('#modifikujVoznjuDispecer').removeClass("active");
        $('#dispeceroveVoznje').addClass("active");

        $('#voznjeTrenutnogDispecera').delay(300).fadeIn(300);


        $.ajax({
            url: '/api/voznja/getdispecerovevoznje',
            type: 'GET',
            success: function (data) {
                var voznje = data;

                var table = `<thead><tr class="success"><th colspan="8" style="text-align:center">Rides</th></tr></thead>`;
                table += `<tbody><tr><th>ID</th><th>Street and number</th><th>Status</th><th>Obradi</th><th>Korisnicko ime</th><th>Opis</th><th>Ocena</th>`;
                var row;
                //for (i = 0; i < data.Count; i++) {
                $(data).each(function (index) {
                    //var row = $('<tr>').addClass('success').text(data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj);
                    //table.append(row);

                    var status;
                    if (data[index].Status == 0) {
                        status = "Kreirana na cekanju";
                    } else if (data[index].Status == 1) {
                        status = "Formirana";
                    } else if (data[index].Status == 2) {
                        status = "Obradjena";
                    } else if (data[index].Status == 3) {
                        status = "Prihvacena";
                    } else if (data[index].Status == 4) {
                        status = "Otkazana";
                    } else if (data[index].Status == 5) {
                        status = "Neuspesna";
                    } else if (data[index].Status == 6) {
                        status = "Uspesna";
                    } else if (data[index].Status == 7) {
                        status = "U toku";
                    } else {
                        status = "Nepoznato";
                    }

                    table += `<tr><td>${data[index].IdVoznje}</td><td> ${data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj} </td><td> ${status} </td>`;
                    //table += `<td><input id="btnOtkaziVoznjuDispecer${index}" class="btn btn-success" type="button" value="Otkazi" /></td>`;
                    table += `<td><input id="btnObradiMojeVoznje${index}" class="btn btn-success" type="button" value="Obradi" /></td>`;
                    table += `<td>${data[index].Komentar.KorisnickoIme}</td><td>${data[index].Komentar.Opis}</td><td>${data[index].Komentar.OcenaVoznje}</td></tr>`
                });

                $("#tabelaVoznjiTrenutnogDispecera").html(table);



                $(data).each(function (index) {
                    $('#btnObradiMojeVoznje' + index).click(function () {
                        var num = index;
                        $.ajax({
                            url: `/api/dispecer/` + index,
                            type: 'PUT',
                            data: {
                                id: num
                            },
                            success: function (data) {
                                if (data) {
                                    alert("Odradio");
                                    window.location.href = "Index.html";
                                } else {
                                    alert("Nema slobodnih vozaca");
                                }
                            }
                        });
                    });
                });
            }
        });
    });











    //});

    $('#home2').click(function () {
        $('#dispecerPodaci').fadeOut(300);
        $('#dispecerNoviVozac').fadeOut(300);
        $('#jmbtrn2').delay(300).fadeIn(300);
        $('#voznjeTrenutnogDispecera').hide();
        $('#obradiVoznjuDispecer').hide();
        $('#footer').delay(300).fadeIn(300);
        $('#profilDispecer').removeClass("active");
        $('#dispeceroveVoznje').removeClass("active");
        $('#dodajVozaca').removeClass("active");
        $('#home2').addClass("active");
    });

    $('#voznjaDispecer').click(function () {
        $('#dispecerPodaci').fadeOut(300);
        $('#dispecerNoviVozac').fadeOut(300);
        $('#dispeceroveVoznje').removeClass("active");
        $('#obradiVoznjuDispecer').hide();
        $('#voznjeTrenutnogDispecera').hide();
        $('#jmbtrn2').fadeOut(300);
        $('#profilDispecer').fadeOut(300);
        $('#dodajNovuVoznjuDispecer').delay(300).fadeIn(300);
        $('#profilDispecer').removeClass("active");
        $('#dodajVozaca').removeClass("active");
        $('#home2').removeClass("active");
        $('#voznjaDispecer').addClass("active");
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
            $('#voznjeTrenutnogDispecera').hide();
            $('#footer').fadeOut(300);
            $('#dispecerPodaci').delay(300).fadeIn(300);
            $('#home2').removeClass("active");
            $('#dodajVozaca').removeClass("active");
            $('#obradiVoznjuDispecer').hide();
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
        $('#voznjeTrenutnogDispecera').hide();
        $('#obradiVoznjuDispecer').hide();
        $('#dodajVozaca').addClass("active");
        $('#home2').removeClass("active");
        $('#dispeceroveVoznje').removeClass("active");
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
        $('#neprihvaceneVoznje').hide();
        $('#vozacStatus').fadeOut(300);
        $('#neuspesnaVoznjaKomentar').hide();
        $('#vozacOdrediste').hide();
        $('#vozacLocation').fadeOut(300);
        $('#footer').fadeOut(300);
        $('#vozacPodaci').delay(300).fadeIn(300);
        $('#home3').removeClass("active");
        $('#changeLocationVozac').removeClass("active");
        $('#slobodneVoznje').removeClass("active");
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
        $('#vozacStatus').fadeOut(300);
        $('#vozacOdrediste').hide();
        $('#neuspesnaVoznjaKomentar').hide();
        $('#neprihvaceneVoznje').hide();
        $('#dispecerPodaci').fadeOut(300);
        $('#dispecerNoviVozac').fadeOut(300);
        $('#vozacLocation').fadeOut(300);
        $('#vozacPodaci').fadeOut(300);
        $('#jmbtrn3').delay(300).fadeIn(300);
        $('#footer').delay(300).fadeIn(300);
        $('#slobodneVoznje').removeClass("active");
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
            $('#vozacStatus').fadeOut(300);
            $('#jmbtrn3').fadeOut(300);
            $('#neprihvaceneVoznje').hide();
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
        $('#vozacStatus').fadeOut(300);
        $('#neprihvaceneVoznje').hide();
        $('#neuspesnaVoznjaKomentar').hide();
        $('#vozacOdrediste').hide();
        $('#jmbtrn3').fadeOut(300);
        $('#footer').fadeOut(300);
        $('#vozacPodaci').fadeOut(300);
        $('#vozacLocation').delay(300).fadeIn(300);
        $('#slobodneVoznje').removeClass("active");
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
                window.location.href = "Index.html";
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

    $('#slobodneVoznje').click(function () {
        $('#jmbtrn3').fadeOut(300);
        $('#neuspesnaVoznjaKomentar').hide();
        $('#vozacOdrediste').hide();
        $('#footer').fadeOut(300);
        $('#vozacPodaci').fadeOut(300);
        $('#vozacLocation').fadeOut(300);
        $('#vozacStatus').fadeOut(300);
        $('#slobodneVoznje').addClass("active");
        $('#profilVozac').removeClass("active");
        $('#home3').removeClass("active");
        $('#changeLocationVozac').removeClass("active");
        $('#changeStatusVozac').removeClass("active");

        $('#neprihvaceneVoznje').delay(300).fadeIn(300);







        $.ajax({
            url: '/api/voznja/getunclaimedrides',
            type: 'GET',
            success: function (data) {
                alert("Upao");
                var voznje = data;

                var table = `<thead><tr class="success"><th colspan="8" style="text-align:center">Rides</th></tr></thead>`;
                table += `<tbody><tr><th>ID</th><th>Street and number</th><th>Status</th><th>Obradi</th><th>Korisnicko ime</th><th>Opis</th><th>Ocena</th>`;
                var row;
                //for (i = 0; i < data.Count; i++) {
                $(data).each(function (index) {
                    //var row = $('<tr>').addClass('success').text(data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj);
                    //table.append(row);

                    var status;
                    if (data[index].Status == 0) {
                        status = "Kreirana na cekanju";
                    } else if (data[index].Status == 1) {
                        status = "Formirana";
                    } else if (data[index].Status == 2) {
                        status = "Obradjena";
                    } else if (data[index].Status == 3) {
                        status = "Prihvacena";
                    } else if (data[index].Status == 4) {
                        status = "Otkazana";
                    } else if (data[index].Status == 5) {
                        status = "Neuspesna";
                    } else if (data[index].Status == 6) {
                        status = "Uspesna";
                    } else if (data[index].Status == 7) {
                        status = "U toku";
                    } else {
                        status = "Nepoznato";
                    }

                    table += `<tr><td>${data[index].IdVoznje}</td><td> ${data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj} </td><td> ${status} </td>`;

                    table += `<td><input id="btnObradiNeprihvacenuVoznju${index}" class="btn btn-success" type="button" value="Obradi" /></td>`;

                    table += `<td>${data[index].Komentar.KorisnickoIme}</td><td>${data[index].Komentar.Opis}</td><td>${data[index].Komentar.OcenaVoznje}</td></tr>`
                });

                $("#tabelaSlobodnihVoznji").html(table);


                $(data).each(function (index) {
                    $('#btnObradiNeprihvacenuVoznju' + index).click(function () {

                        var id = `${data[index].IdVoznje}`;
                        var status = `${$('#cmbStatus' + index).val()}`;
                        var vozac = `${data[index].Vozac}`;

                        var voznja = {
                            IdVoznje: id,
                            Vozac: vozac,
                            Status: status
                        }

                        $.ajax({
                            url: `/api/neprihvacenevoznje/` + id,
                            type: 'PUT',
                            data: JSON.stringify(voznja),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (data) {
                                alert("Ipao u neprihvacene");
                            }
                        });
                    });
                });
            }
        });

    });


    $('#changeStatusVozac').click(function () {
        $('#jmbtrn3').fadeOut(300);
        $('#neuspesnaVoznjaKomentar').hide();
        $('#vozacOdrediste').hide();
        $('#footer').fadeOut(300);
        $('#neprihvaceneVoznje').hide();
        $('#vozacPodaci').fadeOut(300);
        $('#vozacLocation').fadeOut(300);
        $('#vozacStatus').delay(300).fadeIn(300);
        $('#slobodneVoznje').removeClass("active");
        $('#profilVozac').removeClass("active");
        $('#home3').removeClass("active");
        $('#changeLocationVozac').removeClass("active");
        $('#changeStatusVozac').addClass("active");

        $.ajax({
            url: '/api/voznja/getdriversrides',
            type: 'GET',
            success: function (data) {
                alert("Upao");
                var voznje = data;

                var table = `<thead><tr class="success"><th colspan="8" style="text-align:center">Rides</th></tr></thead>`;
                table += `<tbody><tr><th>ID</th><th>Street and number</th><th>Status</th><th>Uspesno/Neuspesno</th><th>Obradi</th><th>Korisnicko ime</th><th>Opis</th><th>Ocena</th>`;
                var row;
                //for (i = 0; i < data.Count; i++) {
                $(data).each(function (index) {
                    //var row = $('<tr>').addClass('success').text(data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj);
                    //table.append(row);

                    var status;
                    if (data[index].Status == 0) {
                        status = "Kreirana na cekanju";
                    } else if (data[index].Status == 1) {
                        status = "Formirana";
                    } else if (data[index].Status == 2) {
                        status = "Obradjena";
                    } else if (data[index].Status == 3) {
                        status = "Prihvacena";
                    } else if (data[index].Status == 4) {
                        status = "Otkazana";
                    } else if (data[index].Status == 5) {
                        status = "Neuspesna";
                    } else if (data[index].Status == 6) {
                        status = "Uspesna";
                    } else if (data[index].Status == 7) {
                        status = "U toku";
                    } else {
                        status = "Nepoznato";
                    }

                    table += `<tr><td>${data[index].IdVoznje}</td><td> ${data[index].LokacijaDolaskaTaksija.Adresa.UlicaBroj} </td><td> ${status} </td>`;


                    table += `<td><select id="cmbStatus${index}"><option value="Uspesna">Successful</option><option value="Neuspesna">Unsuccessful</option></select></td>`;


                    table += `<td><input id="btnObradiVoznjuVozac${index}" class="btn btn-success" type="button" value="Obradi" /></td>`;

                    table += `<td>${data[index].Komentar.KorisnickoIme}</td><td>${data[index].Komentar.Opis}</td><td>${data[index].Komentar.OcenaVoznje}</td></tr>`
                });

                $("#tabelaVoznjiVozac").html(table);

                $(data).each(function (index) {
                    $('#btnObradiVoznjuVozac' + index).click(function () {
                        var num = index;
                        var id = `${data[index].IdVoznje}`;
                        var status = `${$('#cmbStatus' + index).val()}`;
                        var vozac = `${data[index].Vozac}`;
                        var voznja = {
                            IdVoznje: id,
                            Vozac: vozac,
                            Status: status
                        }
                        $.ajax({
                            url: `/api/status/` + id,
                            type: 'PUT',
                            data: JSON.stringify(voznja),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (data) {
                                if (data) {
                                    alert("Odradio apdejt voznje");
                                    $('#vozacStatus').hide();
                                    $('#neuspesnaVoznjaKomentar').hide();
                                    $('#vozacOdrediste').show();


                                    $.ajax({
                                        url: '/api/voznja/getstatus/' + id,
                                        type: 'GET',
                                        success: function (data) {
                                            if (!data) {
                                                alert("Ova voznja je vec obradjena");
                                                window.location.href = "Index.html";
                                            }
                                        }
                                    });

                                    $('#btnSaveDestination').click(function () {
                                        let adresa = {
                                            UlicaBroj: `${$('#txtStreetNumDestination').val()}`,
                                            NaseljenoMesto: `${$('#txtCityDestination').val()}`,
                                            PozivniBroj: `${$('#txtZipCodeDestination').val()}`
                                        }

                                        let lokacija = {
                                            X: `${$('#txtCoordinateXDestination').val()}`,
                                            Y: `${$('#txtCoordinateYDestination').val()}`,
                                            Adresa: adresa
                                        }

                                        var ride = {
                                            IdVoznje: id,
                                            Vozac: vozac,
                                            Odrediste: lokacija,
                                            Iznos: `${$('#txtAmountDestination').val()}`
                                        }

                                        $.ajax({
                                            url: '/api/status/PutVoznjauspesno/' + index,
                                            type: 'PUT',
                                            data: JSON.stringify(ride),
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            success: function (data) {
                                                window.locat.href = "Index.html";
                                            }
                                        });

                                    });

                                } else {

                                    $.ajax({
                                        url: '/api/voznja/getstatus/' + id,
                                        type: 'GET',
                                        success: function (data) {
                                            if (!data) {
                                                alert("Ova voznja je vec obradjena");
                                                window.location.href = "Index.html";
                                            }
                                        }
                                    });

                                    $('#vozacStatus').hide();
                                    $('#vozacOdrediste').hide();
                                    $('#neuspesnaVoznjaKomentar').show();

                                    $('#btnSaveNeuspesnaVoznjaComment').click(function () {
                                        let opis = $('#txtCommentNeuspesnaVoznjaDescription').val();
                                        let ocena = $('#txtCommentNeuspesnaVoznjaGrade').val();
                                        let komentar = {
                                            Opis: `${opis}`,
                                            OcenaVoznje: `${ocena}`,
                                            IdVoznje: `${index}`
                                        };

                                        $.ajax({
                                            url: '/api/status/PutVoznjaNeuspesno/' + index,
                                            type: 'PUT',
                                            data: JSON.stringify(komentar),
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            success: function (data) {
                                                $('#txtCommentNeuspesnaVoznjaDescription').val("");
                                                $('#txtCommentNeuspesnaVoznjaGrade').val("");
                                                window.location.href = "Index.html";
                                            }
                                        });
                                    })
                                }
                            }
                        });
                    });
                });
            }
        });
    });


    //**********************************************************************************************************************
    //**********************************************************************************************************************
});