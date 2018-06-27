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
    public class RegistrationController : ApiController
    {
        [HttpGet]
        public IEnumerable<Korisnik> GetRegistred()
        {
            Korisnici korisnici = HttpContext.Current.Application["korisnici"] as Korisnici;

            return korisnici.korisnici;
        }

        [HttpPost]
        public bool PostKorisnika([FromBody] Korisnik korisnik)
        {
            Korisnici korisnici = HttpContext.Current.Application["korisnici"] as Korisnici;

            foreach (Korisnik k in korisnici.korisnici)
                if (k.KorisnickoIme == korisnik.KorisnickoIme)
                    return false;

            korisnik.Nalog = Enums.Account.Normalno;
            korisnik.Id = korisnici.korisnici.Count;
            korisnici.korisnici.Add(korisnik);

            //string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\korisnici.txt";
            string path = "~/App_Data/korisnici.txt";
            path = HostingEnvironment.MapPath(path);

            string line = String.Empty; 
            line = korisnik.Id.ToString() + '|' + korisnik.KorisnickoIme + '|' + korisnik.Lozinka + '|' + korisnik.Ime + '|' +
            korisnik.Prezime + '|' + korisnik.Gender + '|' + korisnik.JMBG + '|' + korisnik.Telefon + '|' +
                    korisnik.Email + '|' + korisnik.Role + '|' + korisnik.Nalog + Environment.NewLine;

            if (!File.Exists(path))
            {
                File.WriteAllText(path, line);
            }
            else
            {
                File.AppendAllText(path, line);
            }

            korisnici = new Korisnici(@"~/App_Data/korisnici.txt");
            HttpContext.Current.Application["korisnici"] = korisnici;
            
            //var request = Request.CreateResponse(HttpStatusCode.Moved);
            //request.Headers.Location = new Uri("http://localhost:10482/Html/Index.html");

            return true;
        }
    }
}
