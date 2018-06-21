using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {

        public bool Post([FromBody]Korisnik korisnik)
        {

            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Korisnici users = (Korisnici)HttpContext.Current.Application["korisnici"];
            Dispeceri dispeceri = (Dispeceri)HttpContext.Current.Application["dispeceri"];
            Vozaci vozaci = (Vozaci)HttpContext.Current.Application["vozaci"];

            foreach (var item in users.korisnici)
            {
                if (item.KorisnickoIme == korisnik.KorisnickoIme && item.Lozinka == korisnik.Lozinka)
                {
                    HttpContext.Current.Session["user"] = item as Korisnik;
                    return true;
                }
            }

            foreach (var item in dispeceri.dispecers)
            {
                if (item.KorisnickoIme == korisnik.KorisnickoIme && item.Lozinka == korisnik.Lozinka)
                {
                    HttpContext.Current.Session["user"] = item as Dispecer;
                    return true;
                }
            }

            foreach (var item in vozaci.vozaci)
            {
                if (item.KorisnickoIme == korisnik.KorisnickoIme && item.Lozinka == korisnik.Lozinka)
                {
                    HttpContext.Current.Session["user"] = item as Vozac;
                    return true;
                }
            }
            return false;
        }

        public void Get()
        {
            HttpContext.Current.Session.Abandon();

            Korisnik user = new Korisnik();
            HttpContext.Current.Session["user"] = user;
        }
    }
}
