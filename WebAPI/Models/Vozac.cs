using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static WebAPI.Models.Enums;

namespace WebAPI.Models
{
    public class Vozac : Korisnik
    {
        public Lokacija Lokacija { get; set; }
        public Automobil Automobil { get; set; }
        public Stanje stanjeVozaca { get; set; }

        public Vozac() { }

        public Vozac(int id, string KorisnickoIme, string Lozinka, string Ime, string Prezime, string pol, string Jmbg, string Telefon, string email, string uloga, string x, string y, string ulicaBroj, string naseljenoMesto, string pozivniBroj, string vozac,
            string godisteAutomobila, string brojRegOznake, string brojVozila, string tip, string stanje, string nalog) : base(id, KorisnickoIme, Lozinka, Ime, Prezime, pol, Jmbg, Telefon, email, uloga, nalog)
        {
            Lokacija = new Lokacija(x, y, ulicaBroj, naseljenoMesto, pozivniBroj);
            Automobil = new Automobil(KorisnickoIme, godisteAutomobila, brojRegOznake, brojVozila, tip);

            if (stanje.ToLower().Equals("zauzet"))
                stanjeVozaca = Stanje.Zauzet;
            else
                stanjeVozaca = Stanje.Slobodan;
        }
    }
}