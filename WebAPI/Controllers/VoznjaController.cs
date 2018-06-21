using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class VoznjaController : ApiController
    {
        public bool Post([FromBody] Voznja v)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }


            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;

            if (user.Role == Enums.Uloga.Musterija)
            {
                v.IdVoznje = voznje.voznje.Count;
                v.Musterija = user.KorisnickoIme;
                v.Komentar = new Komentar();
                v.Komentar.Opis = "";
                v.Komentar.KorisnickoIme = "";
                v.Odrediste = new Lokacija();
                v.Odrediste.X = "";
                v.Odrediste.Y = "";
                v.Odrediste.Adresa = new Adresa();
                v.Odrediste.Adresa.NaseljenoMesto = "";
                v.Odrediste.Adresa.PozivniBroj = "";
                v.Odrediste.Adresa.UlicaBroj = "";

                foreach (Korisnik item in users.korisnici)
                {
                    if (item.KorisnickoIme == user.KorisnickoIme)
                        item.voznjeKorisnika.Add(v);
                }

                voznje.voznje.Add(v);

                string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\voznje.txt";


                string line = String.Empty;
                line = v.IdVoznje.ToString() + '|' + v.VremePorudzbine.ToString() + '|' + v.LokacijaDolaskaTaksija.X + '|' + v.LokacijaDolaskaTaksija.Y + '|' +
                    v.LokacijaDolaskaTaksija.Adresa.UlicaBroj + '|' + v.LokacijaDolaskaTaksija.Adresa.NaseljenoMesto + '|' + v.LokacijaDolaskaTaksija.Adresa.PozivniBroj + '|' + v.Automobil + '|' +
                    v.Musterija + '|' + v.Odrediste.X + '|' + v.Odrediste.Y + '|' + v.Odrediste.Adresa.UlicaBroj + '|' + v.Odrediste.Adresa.NaseljenoMesto + '|' + v.Odrediste.Adresa.PozivniBroj + '|' +
                    v.Dispecer + '|' + v.Vozac + '|' + v.Iznos + '|' + v.Komentar.Opis + '|' + v.Komentar.DatumObjave + '|' + v.Komentar.KorisnickoIme + '|' + 
                    v.Komentar.OcenaVoznje + '|' + v.Status + '|' + Environment.NewLine;


                if (!File.Exists(path))
                {
                    File.WriteAllText(path, line);
                }
                else
                {
                    File.AppendAllText(path, line);
                }

                voznje = new Voznje(@"~/App_Data/voznje.txt");
                HttpContext.Current.Application["voznje"] = voznje;

                return true;
            }

            return false;
        }
    }
}
