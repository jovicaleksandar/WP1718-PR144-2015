﻿using System;
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

                vozaci.vozaci.Add(v);

                string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\vozaci.txt";

                string line = String.Empty;
                line = v.Id.ToString() + '|' + v.KorisnickoIme + '|' + v.Lozinka + '|' + v.Ime + '|' +
                v.Prezime + '|' + v.Gender + '|' + v.JMBG + '|' + v.Telefon + '|' + v.Email + '|' + v.Role +
                '|' + v.Lokacija.X + '|' + v.Lokacija.Y + '|' + v.Lokacija.Adresa.UlicaBroj + '|' + v.Lokacija.Adresa.NaseljenoMesto +
                '|' + v.Lokacija.Adresa.PozivniBroj + '|' + v.Automobil.Vozac + '|' + v.Automobil.GodisteAutomobila + '|' + v.Automobil.BrojRegistarskeOznake +
                '|' + v.Automobil.BrojTaksiVozila + '|' + v.Automobil.Tip + Environment.NewLine;

                if (!File.Exists(path))
                {
                    File.WriteAllText(path, line);
                }
                else
                {
                    File.AppendAllText(path, line);
                }

                vozaci = new Vozaci(@"~/App_Data/vozaci.txt");
                HttpContext.Current.Application["vozaci"] = vozaci;

                return true;
            }
            else
                return false;
        }

        [HttpPost]
        [Route("api/dispecer/postvoznja")]
        public bool PostVoznja([FromBody] Voznja v)
        {

            return false;
        }
    }
}
