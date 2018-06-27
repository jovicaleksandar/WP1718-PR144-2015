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
    public class NeprihvaceneVoznjeController : ApiController
    {
        [HttpPut]
        public bool Put(int id, [FromBody] Voznja v)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }


            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;
            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;

            if (user.Role == Enums.Uloga.Vozac)
            {
                foreach (Vozac item in vozaci.vozaci)
                {
                    if (user.Id == item.Id)
                    {
                        voznje.voznje[id].Status = Enums.Status.Prihvacena;
                        voznje.voznje[id].Vozac = user.KorisnickoIme;
                        vozaci.vozaci[item.Id].voznjeKorisnika.Add(voznje.voznje[id]);
                        vozaci.vozaci[item.Id].stanjeVozaca = Enums.Stanje.Zauzet;
                        
                        //user.voznjeKorisnika.Add(voznje.voznje[id]);
                        
                        item.voznjeKorisnika.Add(voznje.voznje[id]);

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


                        HttpContext.Current.Application["vozaci"] = vozaci;
                    }
                }
            }

            if (user.Role == Enums.Uloga.Vozac)
            {
                foreach (Voznja ride in voznje.voznje)
                {
                    if (ride.IdVoznje == id)
                    {
                        voznje.voznje[id].Status = Enums.Status.Prihvacena;

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

                        
                        Voznje voznje2 = new Voznje("~/App_Data/voznje.txt");
                        HttpContext.Current.Application["voznje"] = voznje2;

                        return true;
                    }
                }
            }
            return false;
        }
    }
}
