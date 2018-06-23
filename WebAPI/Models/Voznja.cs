using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAPI.Models.Enums;

namespace WebAPI.Models
{
    public class Voznja
    {

        public int IdVoznje { get; set; }
        public DateTime VremePorudzbine { get; set; }
        public Lokacija LokacijaDolaskaTaksija { get; set; }
        public Tip Automobil { get; set; }
        public string Musterija { get; set; }
        public Lokacija Odrediste { get; set; }
        public string Dispecer { get; set; }
        public string Vozac { get; set; }
        public double Iznos { get; set; }
        public Komentar Komentar { get; set; }
        public Status Status { get; set; }

        public Voznja() { }

        public Voznja(int IdVoznje, string DatumVreme, string X, string Y, string UlicaBroj, string NaseljenoMesto, string PozivniBroj, string TipAutomobila, string IdMusterije, string XOdrediste, string YOdrediste, string UlicaBrojOdrediste, string NaseljenoMestoOdrediste, string PozivniBrojOdrediste, string IdDispecera, string IdVozaca, string Iznos, string OpisKomentara, string DatumKomentara, string KorisnickoImeOnogKoPraviKomentar, string Ocena, string StatusV)
        {
            this.IdVoznje = IdVoznje;
            this.VremePorudzbine = DateTime.Parse(DatumVreme);
            LokacijaDolaskaTaksija = new Lokacija(X, Y, UlicaBroj, NaseljenoMesto, PozivniBroj);

            if (TipAutomobila.ToLower().Equals("automobil"))
            {
                Automobil = Tip.Automobil;
            }
            else
            {
                Automobil = Tip.Kombi;
            }

            Musterija = IdMusterije;
            Odrediste = new Lokacija(XOdrediste, YOdrediste, UlicaBrojOdrediste, NaseljenoMestoOdrediste, PozivniBrojOdrediste);
            Dispecer = IdDispecera;
            Vozac = IdVozaca;
            this.Iznos = double.Parse(Iznos);
            Komentar = new Komentar(OpisKomentara, DatumKomentara, KorisnickoImeOnogKoPraviKomentar, IdVoznje, Ocena);

            if (StatusV.ToLower().Equals("kreirana_na_cekanju"))
            {
                this.Status = Status.Kreirana_Na_Cekanju;
            }
            else if (StatusV.ToLower().Equals("formirana"))
            {
                this.Status = Status.Formirana;
            }
            else if (StatusV.ToLower().Equals("obradjena"))
            {
                this.Status = Status.Obradjena;
            }
            else if (StatusV.ToLower().Equals("prihvacena"))
            {
                this.Status = Status.Prihvacena;
            }
            else if (StatusV.ToLower().Equals("otkazana"))
            {
                this.Status = Status.Otkazana;
            }
            else if (StatusV.ToLower().Equals("NEUSPESNA"))
            {
                this.Status = Status.Neuspesna;
            }
            else if (StatusV.ToLower().Equals("uspesna"))
            {
                this.Status = Status.Uspesna;
            }
            else if (StatusV.ToLower().Equals("u_toku"))
            {
                this.Status = Status.U_Toku;
            }
        }
    }
}