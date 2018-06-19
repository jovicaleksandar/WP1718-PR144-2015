using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Dispecer : Korisnik
    {
        public Dispecer() : base() { }

        public Dispecer(int Id, string KorisnickoIme, string Lozinka, string Ime, string Prezime, string pol, string Jmbg, string Telefon, string email, string uloga) : base(Id, KorisnickoIme, Lozinka, Ime, Prezime, pol, Jmbg, Telefon, email, uloga)
        {

        }
    }
}