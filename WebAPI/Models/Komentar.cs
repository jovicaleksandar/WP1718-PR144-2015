using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Komentar
    {
        public string Opis { get; set; }
        public DateTime DatumObjave { get; set; }
        public string KorisnickoIme { get; set; }
        public int OcenaVoznje { get; set; }
        public int IdVoznje { get; set; }

        public Komentar() { }
        public Komentar(string Opis, string DatumObjave, string KorisnickoIme, int IdVoznje, string Ocena)
        {
            this.Opis = Opis;
            this.DatumObjave = DateTime.Parse(DatumObjave);
            this.KorisnickoIme = KorisnickoIme;
            this.IdVoznje = IdVoznje;
            OcenaVoznje = Int32.Parse(Ocena);
        }
    }
}