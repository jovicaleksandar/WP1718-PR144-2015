using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAPI.Models.Enums;

namespace WebAPI.Models
{
    public class Automobil
    {
        public Automobil() { }
        public Automobil(string v, string g, string b, string bt, string t)
        {
            Vozac = v;
            GodisteAutomobila = g;
            BrojRegistarskeOznake = b;
            BrojTaksiVozila = bt;

            if(t.ToLower() == "automobil")
            {
                this.Tip = Tip.Automobil;
            }
            else
            {
                this.Tip = Tip.Kombi;
            }
        }
        public string Vozac { get; set; }
        public string GodisteAutomobila { get; set; }
        public string BrojRegistarskeOznake { get; set; }
        public string BrojTaksiVozila { get; set; }
        public Tip Tip { get; set; }
    }

}