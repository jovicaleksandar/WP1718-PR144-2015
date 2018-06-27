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
    public class BanController : ApiController
    {
        [HttpPut]
        [Route("api/ban/putban")]
        public bool PutBan([FromBody] Korisnik k)
        {
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            foreach (Korisnik korisnik in users.korisnici)
            {
                if (korisnik.KorisnickoIme == k.KorisnickoIme)
                {
                    if (korisnik.Nalog == Enums.Account.Normalno)
                    {
                        korisnik.Nalog = Enums.Account.Banovan;
                        string path = "~/App_Data/korisnici.txt";
                        path = HostingEnvironment.MapPath(path);

                        string line = String.Empty;
                        line = korisnik.Id.ToString() + '|' + korisnik.KorisnickoIme + '|' + korisnik.Lozinka + '|' + korisnik.Ime + '|' +
                        korisnik.Prezime + '|' + korisnik.Gender + '|' + korisnik.JMBG + '|' + korisnik.Telefon + '|' +
                                korisnik.Email + '|' + korisnik.Role + '|' + korisnik.Nalog + Environment.NewLine;


                        string[] arrLine = File.ReadAllLines(path);
                        arrLine[korisnik.Id] = line;
                        File.WriteAllLines(path, arrLine);
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                        users = new Korisnici(@"~/App_Data/korisnici.txt");
                        HttpContext.Current.Application["korisnici"] = users;

                        return true;
                    }
                }
            }

            return false;
        }


        [HttpPut]
        [Route("api/ban/putunban")]
        public bool PutUnban([FromBody] Korisnik k)
        {
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            foreach (Korisnik korisnik in users.korisnici)
            {
                if (korisnik.KorisnickoIme == k.KorisnickoIme)
                {
                    if (korisnik.Nalog == Enums.Account.Banovan)
                    {
                        korisnik.Nalog = Enums.Account.Normalno;
                        string path = "~/App_Data/korisnici.txt";
                        path = HostingEnvironment.MapPath(path);

                        string line = String.Empty;
                        line = korisnik.Id.ToString() + '|' + korisnik.KorisnickoIme + '|' + korisnik.Lozinka + '|' + korisnik.Ime + '|' +
                        korisnik.Prezime + '|' + korisnik.Gender + '|' + korisnik.JMBG + '|' + korisnik.Telefon + '|' +
                                korisnik.Email + '|' + korisnik.Role + '|' + korisnik.Nalog + Environment.NewLine;


                        string[] arrLine = File.ReadAllLines(path);
                        arrLine[korisnik.Id] = line;
                        File.WriteAllLines(path, arrLine);
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                        users = new Korisnici(@"~/App_Data/korisnici.txt");
                        HttpContext.Current.Application["korisnici"] = users;

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
