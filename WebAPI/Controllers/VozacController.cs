using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using WebAPI.Models;
using static WebAPI.Models.Enums;

namespace WebAPI.Controllers
{
    public class VozacController : ApiController
    {
        [HttpPut]
        public Vozac PutVozac([FromBody] Vozac v)
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
                foreach (var item in vozaci.vozaci)
                {
                    if (v.KorisnickoIme == item.KorisnickoIme)
                    {
                        item.Email = v.Email;
                        item.Lozinka = v.Lozinka;
                        item.Ime = v.Ime;
                        item.Prezime = v.Prezime;
                        item.JMBG = v.JMBG;
                        item.Telefon = v.Telefon;
                        item.Gender = v.Gender;
                        item.Role = Uloga.Vozac;
                        item.Automobil.Vozac = v.Ime + " " + v.Prezime;
                        item.Automobil.BrojRegistarskeOznake = v.Automobil.BrojRegistarskeOznake;
                        item.Automobil.BrojTaksiVozila = v.Automobil.BrojTaksiVozila;
                        item.Automobil.GodisteAutomobila = v.Automobil.GodisteAutomobila;
                        item.Automobil.Tip = v.Automobil.Tip;
                        item.Lokacija.X = v.Lokacija.X;
                        item.Lokacija.Y = v.Lokacija.Y;
                        item.Lokacija.Adresa.NaseljenoMesto = v.Lokacija.Adresa.NaseljenoMesto;
                        item.Lokacija.Adresa.PozivniBroj = v.Lokacija.Adresa.PozivniBroj;
                        item.Lokacija.Adresa.UlicaBroj = v.Lokacija.Adresa.UlicaBroj;

                        //string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\vozaci.txt";
                        string path = "~/App_Data/vozaci.txt";
                        path = HostingEnvironment.MapPath(path);

                        string line = item.Id.ToString() + '|' + item.KorisnickoIme + '|' + item.Lozinka + '|' + item.Ime + '|' +
                        item.Prezime + '|' + item.Gender + '|' + item.JMBG + '|' + item.Telefon + '|' +
                                item.Email + '|' + item.Role + '|' + item.Lokacija.X + '|' + item.Lokacija.Y + '|' +
                                item.Lokacija.Adresa.UlicaBroj + '|' + item.Lokacija.Adresa.NaseljenoMesto + '|' +
                                item.Lokacija.Adresa.PozivniBroj + '|' + item.Automobil.Vozac + '|' + item.Automobil.GodisteAutomobila + '|' +
                                item.Automobil.BrojRegistarskeOznake + '|' + item.Automobil.BrojTaksiVozila + '|' + item.Automobil.Tip +
                                '|' + item.Nalog + Environment.NewLine;

                        string[] arrLine = File.ReadAllLines(path);
                        arrLine[item.Id] = line;
                        File.WriteAllLines(path, arrLine);
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                        Vozaci vozaci2 = new Vozaci(@"~/App_Data/vozaci.txt");
                        HttpContext.Current.Application["vozaci"] = vozaci2;


                        return v;
                    }
                }
            }
            return null;
        }

        [HttpGet]
        public List<Voznja> Get()
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;

            if (user.Role == Uloga.Vozac)
            {
                foreach (Vozac v in vozaci.vozaci)
                {
                    if (user.KorisnickoIme == v.KorisnickoIme)
                    {
                        return v.voznjeKorisnika;
                    }
                }
            }


            return new List<Voznja>();
        }



        [HttpGet]
        [Route("api/vozac/getslobodne")]
        public List<Vozac> GetSlobodne()
        {
            Vozaci users = HttpContext.Current.Application["vozaci"] as Vozaci;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Vozac> retVal = new List<Vozac>();

            foreach (Vozac k in users.vozaci)
            {
                if (k.stanjeVozaca == Stanje.Slobodan)
                    retVal.Add(k);
            }

            return retVal;

        }
    }
}
