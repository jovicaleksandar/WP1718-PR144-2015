using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol Gender { get; set; }
        public string JMBG { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public Uloga Role { get; set; }
        //public string Voznja { get; set; }

        public Korisnik(int id, string korisnickoIme, string lozinka, string ime, string prezime, string gender, string jMBG, string telefon, string email, string role)
        {
            Id = id;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;

            if (gender.ToLower() == "muskarac")
                Gender = Pol.Muskarac;
            else
                Gender = Pol.Zena;

            JMBG = jMBG;
            Telefon = telefon;
            Email = email;

            if (role.ToLower() == "dispecer")
                Role = Uloga.Dispecer;
            else if (role.ToLower() == "vozac")
                Role = Uloga.Vozac;
            else
                Role = Uloga.Musterija;
        }
        //Dodati listu voznji

        public Korisnik()
        {

        }

    }



        public enum Uloga
        {
            Musterija,
            Dispecer,
            Vozac
        }

        public enum Pol
        {
            Muskarac,
            Zena
        }
}