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

        public Vozac(int id, string KorisnickoIme, string Lozinka, string Ime, string Prezime, string pol, string Jmbg, string Telefon, string email, string uloga, string x, string y, string ulica_broj, string naseljeno_mjesto, string pozivni_broj, string vozac,
            string godiste_automobila, string broj_reg_oznake, string broj_tvozila, string tip) : base(id, KorisnickoIme, Lozinka, Ime, Prezime, pol, Jmbg, Telefon, email, uloga)
        {
            Lokacija = new Lokacija(x, y, ulica_broj, naseljeno_mjesto, pozivni_broj);
            Automobil = new Automobil(KorisnickoIme, godiste_automobila, broj_reg_oznake, broj_tvozila, tip);
        }
    }
}