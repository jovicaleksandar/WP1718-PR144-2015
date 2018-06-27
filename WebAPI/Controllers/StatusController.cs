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
    public class StatusController : ApiController
    {
        [HttpPut]
        public bool Put(string id, [FromBody] Voznja v)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;

            foreach (Vozac rider in vozaci.vozaci)
            {
                if (rider.KorisnickoIme == user.KorisnickoIme && user.Role == Enums.Uloga.Vozac)
                {
                    if (v.Status == Enums.Status.Uspesna)
                        return true;
                }
            }

            return false;
        }

        [HttpPut]
        [Route("api/status/putvoznjauspesno/{id:int}")]
        public bool PutVoznjaUspesno(int id, [FromBody] Voznja v)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;

            foreach (Vozac item in vozaci.vozaci)
            {
                if (item.KorisnickoIme == user.KorisnickoIme && user.Role == Enums.Uloga.Vozac && item.KorisnickoIme == v.Vozac)
                {
                    item.voznjeKorisnika[id].Status = Enums.Status.Uspesna;
                    item.voznjeKorisnika[id].IdVoznje = v.IdVoznje;
                    item.voznjeKorisnika[id].Odrediste.X = v.Odrediste.X;
                    item.voznjeKorisnika[id].Odrediste.Y = v.Odrediste.Y;
                    item.voznjeKorisnika[id].Odrediste.Adresa.NaseljenoMesto = v.Odrediste.Adresa.NaseljenoMesto;
                    item.voznjeKorisnika[id].Odrediste.Adresa.PozivniBroj = v.Odrediste.Adresa.PozivniBroj;
                    item.voznjeKorisnika[id].Odrediste.Adresa.UlicaBroj = v.Odrediste.Adresa.UlicaBroj;
                    item.voznjeKorisnika[id].Iznos = v.Iznos;



                    //string pathVozac = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\vozaci.txt";
                    string pathVozac = "~/App_Data/vozaci.txt";
                    pathVozac = HostingEnvironment.MapPath(pathVozac);

                    string lineVozac = item.Id.ToString() + '|' + item.KorisnickoIme + '|' + item.Lozinka + '|' + item.Ime + '|' +
                    item.Prezime + '|' + item.Gender + '|' + item.JMBG + '|' + item.Telefon + '|' +
                            item.Email + '|' + item.Role + '|' + item.Lokacija.X + '|' + item.Lokacija.Y + '|' +
                            item.Lokacija.Adresa.UlicaBroj + '|' + item.Lokacija.Adresa.NaseljenoMesto + '|' +
                            item.Lokacija.Adresa.PozivniBroj + '|' + item.Automobil.Vozac + '|' + item.Automobil.GodisteAutomobila + '|' +
                            item.Automobil.BrojRegistarskeOznake + '|' + item.Automobil.BrojTaksiVozila + '|' + item.Automobil.Tip +
                            '|' + item.stanjeVozaca + Environment.NewLine;

                    string[] arrLine = File.ReadAllLines(pathVozac);
                    arrLine[item.Id] = lineVozac;
                    File.WriteAllLines(pathVozac, arrLine);
                    File.WriteAllLines(pathVozac, File.ReadAllLines(pathVozac).Where(l => !string.IsNullOrWhiteSpace(l)));


                    //string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\voznje.txt";
                    string path = "~/App_Data/voznje.txt";
                    path = HostingEnvironment.MapPath(path);


                    foreach (Voznja ride in item.voznjeKorisnika)
                    {
                        if (ride.IdVoznje == v.IdVoznje)
                        {
                            ride.Status = Enums.Status.Uspesna;
                            ride.Iznos = v.Iznos;
                            ride.Odrediste.X = v.Odrediste.X;
                            ride.Odrediste.Y = v.Odrediste.Y;
                            ride.Odrediste.Adresa.NaseljenoMesto = v.Odrediste.Adresa.NaseljenoMesto;
                            ride.Odrediste.Adresa.PozivniBroj = v.Odrediste.Adresa.PozivniBroj;
                            ride.Odrediste.Adresa.UlicaBroj = v.Odrediste.Adresa.UlicaBroj;

                            string line = String.Empty;
                            line = ride.IdVoznje.ToString() + '|' + ride.VremePorudzbine.ToString() + '|' + ride.LokacijaDolaskaTaksija.X + '|' + ride.LokacijaDolaskaTaksija.Y + '|' +
                            ride.LokacijaDolaskaTaksija.Adresa.UlicaBroj + '|' + ride.LokacijaDolaskaTaksija.Adresa.NaseljenoMesto + '|' + ride.LokacijaDolaskaTaksija.Adresa.PozivniBroj + '|' + ride.Automobil + '|' +
                            ride.Musterija + '|' + ride.Odrediste.X + '|' + ride.Odrediste.Y + '|' + ride.Odrediste.Adresa.UlicaBroj + '|' + ride.Odrediste.Adresa.NaseljenoMesto + '|' + ride.Odrediste.Adresa.PozivniBroj + '|' +
                            ride.Dispecer + '|' + ride.Vozac + '|' + ride.Iznos + '|' + ride.Komentar.Opis + '|' + ride.Komentar.DatumObjave + '|' + ride.Komentar.KorisnickoIme + '|' +
                            ride.Komentar.OcenaVoznje + '|' + ride.Status + '|' + Environment.NewLine;


                            string[] arrLine2 = File.ReadAllLines(path);
                            arrLine2[ride.IdVoznje] = line;
                            File.WriteAllLines(path, arrLine2);
                            File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));


                            Voznje voznje2 = new Voznje("~/App_Data/voznje.txt");
                            HttpContext.Current.Application["voznje"] = voznje2;
                            //Vozaci vozaci2 = new Vozaci(@"~/App_Data/vozaci.txt");
                            //HttpContext.Current.Application["vozaci"] = vozaci2;

                            return true;
                        }
                    }
                }
            }

            return false;
        }


        [HttpPut]
        [Route("api/status/putvoznjaneuspesno/{id:int}")]
        public bool PutVoznjaNeuspesno(int id, [FromBody] Komentar k)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;

            foreach (Vozac driver in vozaci.vozaci)
            {
                if (user.KorisnickoIme == driver.KorisnickoIme)
                {
                    foreach (Voznja ride in driver.voznjeKorisnika)
                    {
                        if (ride.IdVoznje == id)
                        {
                            ride.Status = Enums.Status.Neuspesna;
                            ride.Komentar.IdVoznje = id;
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


                            string[] arrLine2 = File.ReadAllLines(path);
                            arrLine2[ride.IdVoznje] = line;
                            File.WriteAllLines(path, arrLine2);
                            File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                            Voznje voznje2 = new Voznje("~/App_Data/voznje.txt");
                            HttpContext.Current.Application["voznje"] = voznje2;
                            Vozaci vozaci2 = new Vozaci(@"~/App_Data/vozaci.txt");
                            HttpContext.Current.Application["vozaci"] = vozaci2;

                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }
}
