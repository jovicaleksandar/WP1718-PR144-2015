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
    public class KorisnikController : ApiController
    {
        [HttpPut]
        public Korisnik Put([FromBody] Korisnik korisnik)
        {
            Dispeceri dispeceri = HttpContext.Current.Application["dispeceri"] as Dispeceri;
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;

            foreach (var item in users.korisnici)
            {
                if (korisnik.KorisnickoIme == item.KorisnickoIme)
                {
                    item.Id = korisnik.Id;
                    item.Email = korisnik.Email;
                    item.Lozinka = korisnik.Lozinka;
                    item.Ime = korisnik.Ime;
                    item.Prezime = korisnik.Prezime;
                    item.JMBG = korisnik.JMBG;
                    item.Telefon = korisnik.Telefon;
                    item.Gender = korisnik.Gender;
                    item.Role = Uloga.Musterija;

                    string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\korisnici.txt";
                    users.korisnici.Add(korisnik);
                    string line = korisnik.Id.ToString() + '|' +  korisnik.KorisnickoIme + '|' + korisnik.Lozinka + '|' + korisnik.Ime + '|' +
                    korisnik.Prezime + '|' + korisnik.Gender + '|' + korisnik.JMBG + '|' + korisnik.Telefon + '|' +
                            korisnik.Email + '|' + korisnik.Role + Environment.NewLine;

                    string[] arrLine = File.ReadAllLines(path);
                    arrLine[item.Id] = line;
                    File.WriteAllLines(path, arrLine);
                    File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                    HttpContext.Current.Application["korisnici"] = users;

                    return korisnik;
                }
            }

            foreach (var item in dispeceri.dispecers)
            {
                if (korisnik.KorisnickoIme == item.KorisnickoIme)
                {
                    item.Id = korisnik.Id;
                    item.Email = korisnik.Email;
                    item.Lozinka = korisnik.Lozinka;
                    item.Ime = korisnik.Ime;
                    item.Prezime = korisnik.Prezime;
                    item.JMBG = korisnik.JMBG;
                    item.Telefon = korisnik.Telefon;
                    item.Gender = korisnik.Gender;
                    item.Role = Uloga.Dispecer;

                    string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\dispeceri.txt";
                    dispeceri.dispecers.Add(korisnik as Dispecer);
                    string line = korisnik.Id.ToString() + '|' + korisnik.KorisnickoIme + '|' + korisnik.Lozinka + '|' + korisnik.Ime + '|' +
                    korisnik.Prezime + '|' + korisnik.Gender + '|' + korisnik.JMBG + '|' + korisnik.Telefon + '|' +
                            korisnik.Email + '|' + korisnik.Role + Environment.NewLine;

                    string[] arrLine = File.ReadAllLines(path);
                    arrLine[item.Id] = line;
                    File.WriteAllLines(path, arrLine);
                    File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));


                    HttpContext.Current.Application["dispeceri"] = dispeceri;

                    return korisnik;
                }
            }


            return null;
        }
    }
}
