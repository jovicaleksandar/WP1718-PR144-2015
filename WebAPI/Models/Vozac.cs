using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Vozac : Korisnik
    {
        public Lokacija Lokacija { get; set; }
        public Automobil Automobil { get; set; }

        public Vozac() { }

        public Vozac(int id, string KorisnickoIme, string Lozinka, string Ime, string Prezime, string pol, string Jmbg, string Telefon, string email, string uloga, string x, string y, string ulicaBroj, string naseljenoMesto, string pozivniBroj, string vozac,
            string godisteAutomobila, string brojRegOznake, string brojVozila, string tip) : base(id, KorisnickoIme, Lozinka, Ime, Prezime, pol, Jmbg, Telefon, email, uloga)
        {
            Lokacija = new Lokacija(x, y, ulicaBroj, naseljenoMesto, pozivniBroj);
            Automobil = new Automobil(KorisnickoIme, godisteAutomobila, brojRegOznake, brojVozila, tip);
        }
    }
}