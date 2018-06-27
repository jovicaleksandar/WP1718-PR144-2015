using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using static WebAPI.Models.Enums;

namespace WebAPI.Controllers
{
    public class LocationController : ApiController
    {
        [HttpPut]
        public Vozac Put([FromBody] Vozac v)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;

            if (user.Role == Uloga.Vozac && user.KorisnickoIme == v.KorisnickoIme)
            {
                foreach (Vozac vozac in vozaci.vozaci)
                {
                    if (v.KorisnickoIme == vozac.KorisnickoIme)
                    {
                        vozac.Lokacija.X = v.Lokacija.X;
                        vozac.Lokacija.Y = v.Lokacija.Y;
                        vozac.Lokacija.Adresa.NaseljenoMesto = v.Lokacija.Adresa.NaseljenoMesto;
                        vozac.Lokacija.Adresa.PozivniBroj = v.Lokacija.Adresa.PozivniBroj;
                        vozac.Lokacija.Adresa.UlicaBroj = v.Lokacija.Adresa.UlicaBroj;

                        string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\vozaci.txt";
                        vozaci.vozaci.Add(vozac);

                        string line = String.Empty;
                        line = vozac.Id.ToString() + '|' + vozac.KorisnickoIme + '|' + vozac.Lozinka + '|' + vozac.Ime + '|' +
                        vozac.Prezime + '|' + vozac.Gender + '|' + vozac.JMBG + '|' + vozac.Telefon + '|' + vozac.Email + '|' + vozac.Role +
                        '|' + vozac.Lokacija.X + '|' + vozac.Lokacija.Y + '|' + vozac.Lokacija.Adresa.UlicaBroj + '|' + vozac.Lokacija.Adresa.NaseljenoMesto +
                        '|' + vozac.Lokacija.Adresa.PozivniBroj + '|' + vozac.Automobil.Vozac + '|' + vozac.Automobil.GodisteAutomobila + '|' + vozac.Automobil.BrojRegistarskeOznake +
                        '|' + vozac.Automobil.BrojTaksiVozila + '|' + vozac.Automobil.Tip + '|' + vozac.stanjeVozaca + Environment.NewLine;


                        string[] arrLine = File.ReadAllLines(path);
                        arrLine[vozac.Id] = line;
                        File.WriteAllLines(path, arrLine);
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));


                        Vozaci vozaci2 = new Vozaci("~/App_Data/vozaci.txt");
                        HttpContext.Current.Application["vozaci"] = vozaci2;


                        return vozac;
                    }
                }
            }

            return null;
        }
    }
}
