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
    public class KorisnikController : ApiController
    {
        [HttpPut]
        public Korisnik Put([FromBody] Korisnik korisnik)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Dispeceri dispeceri = HttpContext.Current.Application["dispeceri"] as Dispeceri;
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;

            if (user.Role == Uloga.Musterija && user.KorisnickoIme == korisnik.KorisnickoIme)
            {
                foreach (var item in users.korisnici)
                {
                    if (korisnik.KorisnickoIme == item.KorisnickoIme)
                    {
                        item.Email = korisnik.Email;
                        item.Lozinka = korisnik.Lozinka;
                        item.Ime = korisnik.Ime;
                        item.Prezime = korisnik.Prezime;
                        item.JMBG = korisnik.JMBG;
                        item.Telefon = korisnik.Telefon;
                        item.Gender = korisnik.Gender;
                        item.Role = Uloga.Musterija;

                        string path = "~/App_Data/korisnici.txt";
                        path = HostingEnvironment.MapPath(path);

                        //users.korisnici.Add(korisnik);
                        string line = korisnik.Id.ToString() + '|' + korisnik.KorisnickoIme + '|' + korisnik.Lozinka + '|' + korisnik.Ime + '|' +
                        korisnik.Prezime + '|' + korisnik.Gender + '|' + korisnik.JMBG + '|' + korisnik.Telefon + '|' +
                                korisnik.Email + '|' + korisnik.Role + '|' + korisnik.Nalog + Environment.NewLine;

                        string[] arrLine = File.ReadAllLines(path);
                        arrLine[item.Id] = line;
                        File.WriteAllLines(path, arrLine);
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                        Korisnici korisnici2 = new Korisnici("~/App_Data/korisnici.txt");
                        HttpContext.Current.Application["korisnici"] = korisnici2;
                        //HttpContext.Current.Application["korisnici"] = users;

                        return korisnik;
                    }
                }
            }

            if (user.Role == Uloga.Dispecer && user.KorisnickoIme == korisnik.KorisnickoIme)
            {
                foreach (var item in dispeceri.dispecers)
                {
                    if (korisnik.KorisnickoIme == item.KorisnickoIme)
                    {
                        item.Email = korisnik.Email;
                        item.Lozinka = korisnik.Lozinka;
                        item.Ime = korisnik.Ime;
                        item.Prezime = korisnik.Prezime;
                        item.JMBG = korisnik.JMBG;
                        item.Telefon = korisnik.Telefon;
                        item.Gender = korisnik.Gender;
                        item.Role = Uloga.Dispecer;

                        //string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\dispeceri.txt";
                        string path = "~/App_Data/dispeceri.txt";
                        path = HostingEnvironment.MapPath(path);

                        //dispeceri.dispecers.Add(korisnik as Dispecer);
                        string line = korisnik.Id.ToString() + '|' + korisnik.KorisnickoIme + '|' + korisnik.Lozinka + '|' + korisnik.Ime + '|' +
                        korisnik.Prezime + '|' + korisnik.Gender + '|' + korisnik.JMBG + '|' + korisnik.Telefon + '|' +
                                korisnik.Email + '|' + korisnik.Role + '|' + korisnik.Nalog + Environment.NewLine;

                        string[] arrLine = File.ReadAllLines(path);
                        arrLine[item.Id] = line;
                        File.WriteAllLines(path, arrLine);
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));


                        Dispeceri dispeceri2 = new Dispeceri("~/App_Data/dispeceri.txt");
                        HttpContext.Current.Application["dispeceri"] = dispeceri2;

                        return korisnik;
                    }
                }
            }

            return null;
        }

        public string Get()
        {
            Korisnik user = HttpContext.Current.Session["user"] as Korisnik;

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            return user.KorisnickoIme;
        }
    }
}
