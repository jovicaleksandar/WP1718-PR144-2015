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

namespace WebAPI.Controllers
{
    public class KomentarController : ApiController
    {
        public bool Post([FromBody] Komentar k)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;

            foreach (Korisnik korisnik in users.korisnici)
            { 
                if (korisnik.KorisnickoIme == user.KorisnickoIme)
                {
                    foreach (Voznja ride in korisnik.voznjeKorisnika)
                    {
                        if (ride.IdVoznje == k.IdVoznje)
                        {
                            ride.Komentar.IdVoznje = k.IdVoznje;
                            ride.Komentar.DatumObjave = DateTime.UtcNow;
                            ride.Komentar.KorisnickoIme = user.KorisnickoIme;
                            ride.Komentar.OcenaVoznje = k.OcenaVoznje;
                            ride.Komentar.Opis = k.Opis;

                            //string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\voznje.txt";
                            string path = "~/App_Data/voznje.txt";
                            path = HostingEnvironment.MapPath(path);

                            string line = String.Empty;

                            line = ride.IdVoznje.ToString() + '|' + ride.VremePorudzbine.ToString() + '|' + ride.LokacijaDolaskaTaksija.X + '|' + ride.LokacijaDolaskaTaksija.Y + '|' +
                                ride.LokacijaDolaskaTaksija.Adresa.UlicaBroj + '|' + ride.LokacijaDolaskaTaksija.Adresa.NaseljenoMesto + '|' + ride.LokacijaDolaskaTaksija.Adresa.PozivniBroj + '|' + ride.Automobil + '|' +
                                ride.Musterija + '|' + ride.Odrediste.X + '|' + ride.Odrediste.Y + '|' + ride.Odrediste.Adresa.UlicaBroj + '|' + ride.Odrediste.Adresa.NaseljenoMesto + '|' + ride.Odrediste.Adresa.PozivniBroj + '|' +
                                ride.Dispecer + '|' + ride.Vozac + '|' + ride.Iznos + '|' + ride.Komentar.Opis + '|' + ride.Komentar.DatumObjave + '|' + ride.Komentar.KorisnickoIme + '|' +
                                ride.Komentar.OcenaVoznje + '|' + ride.Status + '|' + Environment.NewLine;

                            string[] arrLine = File.ReadAllLines(path);
                            arrLine[ride.IdVoznje] = line;
                            File.WriteAllLines(path, arrLine);
                            File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                            foreach (Korisnik kor in users.korisnici)
                            {
                                if (kor.KorisnickoIme == ride.Musterija)
                                {
                                    foreach (Voznja voznja in kor.voznjeKorisnika)
                                    {
                                        if (voznja.IdVoznje == ride.IdVoznje)
                                        {
                                            voznja.Komentar = ride.Komentar;
                                        }
                                    }
                                }
                            }

                            Voznje voznje2 = new Voznje("~/App_Data/voznje.txt");
                            HttpContext.Current.Application["voznje"] = voznje2;

                            HttpContext.Current.Application["korisnici"] = users;

                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
