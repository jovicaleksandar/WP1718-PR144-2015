﻿using System;
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
    public class DispecerController : ApiController
    {
        [HttpGet]
        public IEnumerable<Dispecer> GetDispecere()
        {
            Dispeceri dispecers = HttpContext.Current.Application["dispeceri"] as Dispeceri;

            return dispecers.dispecers;
        }

        [HttpPost]
        [Route("api/dispecer/postvozac")]
        public bool PostVozac([FromBody] Vozac v)
        {
            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;
            Korisnik user = HttpContext.Current.Session["user"] as Korisnik;

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            if (user.Role == Uloga.Dispecer)
            {
                foreach (Vozac vozac in vozaci.vozaci)
                {
                    if (v.KorisnickoIme == vozac.KorisnickoIme)
                        return false;
                }

                v.Id = vozaci.vozaci.Count;
                v.Role = Uloga.Vozac;
                v.Automobil.Vozac = v.KorisnickoIme;
                v.stanjeVozaca = Stanje.Slobodan;

                vozaci.vozaci.Add(v);

                //string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\vozaci.txt";
                string path = "~/App_Data/vozaci.txt";
                path = HostingEnvironment.MapPath(path);

                string line = String.Empty;
                line = v.Id.ToString() + '|' + v.KorisnickoIme + '|' + v.Lozinka + '|' + v.Ime + '|' +
                v.Prezime + '|' + v.Gender + '|' + v.JMBG + '|' + v.Telefon + '|' + v.Email + '|' + v.Role +
                '|' + v.Lokacija.X + '|' + v.Lokacija.Y + '|' + v.Lokacija.Adresa.UlicaBroj + '|' + v.Lokacija.Adresa.NaseljenoMesto +
                '|' + v.Lokacija.Adresa.PozivniBroj + '|' + v.Automobil.Vozac + '|' + v.Automobil.GodisteAutomobila + '|' + v.Automobil.BrojRegistarskeOznake +
                '|' + v.Automobil.BrojTaksiVozila + '|' + v.Automobil.Tip + '|' + v.stanjeVozaca + Environment.NewLine;

                if (!File.Exists(path))
                {
                    File.WriteAllText(path, line);
                }
                else
                {
                    File.AppendAllText(path, line);
                }

                Vozaci vozaci2 = new Vozaci(@"~/App_Data/vozaci.txt");
                HttpContext.Current.Application["vozaci"] = vozaci2;

                return true;
            }
            else
                return false;
        }

        [HttpPost]
        [Route("api/dispecer/postvoznja")]
        public bool PostVoznja([FromBody] Voznja v)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }


            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;
            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;

            if (user.Role == Enums.Uloga.Dispecer && v.Dispecer == user.KorisnickoIme)
            {
                foreach (Vozac item in vozaci.vozaci)
                {
                    if (item.stanjeVozaca == Stanje.Slobodan)
                    {
                        item.stanjeVozaca = Stanje.Zauzet;
                        v.Status = Status.Prihvacena;
                        v.Vozac = item.KorisnickoIme;
                        v.VremePorudzbine = DateTime.UtcNow;
                        v.Dispecer = user.KorisnickoIme;
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

                        v.IdVoznje = user.voznjeKorisnika.Count;
                        user.voznjeKorisnika.Add(v);

                        v.IdVoznje = item.voznjeKorisnika.Count;
                        item.voznjeKorisnika.Add(v);

                        vozaci.vozaci[item.Id].voznjeKorisnika.Add(v);

                        v.IdVoznje = voznje.voznje.Count;
                        voznje.voznje.Add(v);

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


                        /*foreach (Korisnik k in users.korisnici)
                        {
                            if (k.KorisnickoIme == user.KorisnickoIme)
                            {
                                k.voznjeKorisnika.Add(v);
                            }
                        }*/

                        //string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\voznje.txt";
                        string path = "~/App_Data/voznje.txt";
                        path = HostingEnvironment.MapPath(path);


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

                        HttpContext.Current.Application["vozaci"] = vozaci;
                        Voznje voznje2 = new Voznje("~/App_Data/voznje.txt");
                        HttpContext.Current.Application["voznje"] = voznje2;
                        //Vozaci vozaci2 = new Vozaci(@"~/App_Data/vozaci.txt");
                        //HttpContext.Current.Application["vozaci"] = vozaci2;

                        return true;
                    }
                }
            }

            return false;
        }

        public bool PutVoznja(string id, [FromBody] Voznja v)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }


            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;
            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;

            if (user.Role == Uloga.Dispecer)
            {
                foreach (Vozac item in vozaci.vozaci)
                {
                    if (item.stanjeVozaca == Stanje.Slobodan && item.KorisnickoIme == v.Vozac && v.Status == Status.Kreirana_Na_Cekanju)
                    {
                        item.stanjeVozaca = Stanje.Zauzet;

                        voznje.voznje[Int32.Parse(id)].Vozac = item.KorisnickoIme;
                        voznje.voznje[Int32.Parse(id)].Dispecer = user.KorisnickoIme;
                        voznje.voznje[Int32.Parse(id)].Status = Status.Prihvacena;
                        item.voznjeKorisnika.Add(voznje.voznje[Int32.Parse(id)]);

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


                        string line = String.Empty;
                        line = voznje.voznje[Int32.Parse(id)].IdVoznje.ToString() + '|' + voznje.voznje[Int32.Parse(id)].VremePorudzbine.ToString() + '|' + voznje.voznje[Int32.Parse(id)].LokacijaDolaskaTaksija.X + '|' + voznje.voznje[Int32.Parse(id)].LokacijaDolaskaTaksija.Y + '|' +
                        voznje.voznje[Int32.Parse(id)].LokacijaDolaskaTaksija.Adresa.UlicaBroj + '|' + voznje.voznje[Int32.Parse(id)].LokacijaDolaskaTaksija.Adresa.NaseljenoMesto + '|' + voznje.voznje[Int32.Parse(id)].LokacijaDolaskaTaksija.Adresa.PozivniBroj + '|' + voznje.voznje[Int32.Parse(id)].Automobil + '|' +
                        voznje.voznje[Int32.Parse(id)].Musterija + '|' + voznje.voznje[Int32.Parse(id)].Odrediste.X + '|' + voznje.voznje[Int32.Parse(id)].Odrediste.Y + '|' + voznje.voznje[Int32.Parse(id)].Odrediste.Adresa.UlicaBroj + '|' + voznje.voznje[Int32.Parse(id)].Odrediste.Adresa.NaseljenoMesto + '|' + voznje.voznje[Int32.Parse(id)].Odrediste.Adresa.PozivniBroj + '|' +
                        voznje.voznje[Int32.Parse(id)].Dispecer + '|' + voznje.voznje[Int32.Parse(id)].Vozac + '|' + voznje.voznje[Int32.Parse(id)].Iznos + '|' + voznje.voznje[Int32.Parse(id)].Komentar.Opis + '|' + voznje.voznje[Int32.Parse(id)].Komentar.DatumObjave + '|' + voznje.voznje[Int32.Parse(id)].Komentar.KorisnickoIme + '|' +
                        voznje.voznje[Int32.Parse(id)].Komentar.OcenaVoznje + '|' + voznje.voznje[Int32.Parse(id)].Status + '|' + Environment.NewLine;

                        string[] arrLine2 = File.ReadAllLines(path);
                        arrLine2[Int32.Parse(id)] = line;
                        File.WriteAllLines(path, arrLine2);
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                        Voznje voznje2 = new Voznje("~/App_Data/voznje.txt");
                        HttpContext.Current.Application["voznje"] = voznje2;
                        HttpContext.Current.Application["vozaci"] = vozaci;


                        return true;
                    }
                }
            }


            return false;
        }
    }
}
