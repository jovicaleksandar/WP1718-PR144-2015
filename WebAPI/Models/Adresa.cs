using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Adresa
    {
        public Adresa() { }
        public Adresa(string ub, string nm, string pb)
        {
            UlicaBroj = ub;
            NaseljenoMesto = nm;
            PozivniBroj = pb;
        }
        public string UlicaBroj { get; set; }
        public string NaseljenoMesto { get; set; }
        public string PozivniBroj { get; set; }
    }
}