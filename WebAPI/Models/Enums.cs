using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Enums
    {
        public enum Uloga { Musterija, Dispecer, Vozac }

        public enum Pol { Muskarac, Zena }

        public enum Tip { Automobil, Kombi }

        public enum Status { Kreirana_Na_Cekanju, Formirana, Obradjena, Prihvacena, Otkazana, Neuspesna, Uspesna, U_Toku }

        public enum Stanje { Slobodan, Zauzet }
    }
}