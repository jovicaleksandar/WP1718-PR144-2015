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
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("api/search/getfiltracija/{id}")]
        public List<Voznja> GetFiltracija(string id)
        {
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            foreach (Korisnik k in users.korisnici)
            {
                if (k.KorisnickoIme == user.KorisnickoIme)
                {
                    foreach (Voznja v in k.voznjeKorisnika)
                    {
                        if (v.Status.ToString() == id)
                        {
                            retVal.Add(v);
                        }
                    }
                    return retVal;
                }
            }

            return new List<Voznja>();
        }


        [HttpGet]
        [Route("api/search/getsearch/{from}/{to}")]
        public List<Voznja> GetSearch(DateTime from, DateTime to)
        {
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();
            int result1;
            int result2;

            foreach (Korisnik k in users.korisnici)
            {
                if (k.KorisnickoIme == user.KorisnickoIme)
                {
                    foreach (Voznja v in k.voznjeKorisnika)
                    {
                        result1 = DateTime.Compare(from, v.VremePorudzbine);
                        result2 = DateTime.Compare(to, v.VremePorudzbine);

                        if (result1 < 0 && result2 > 0)
                        {
                            retVal.Add(v);
                        }
                    }

                    return retVal;
                }
            }

            return new List<Voznja>();
        }


        [HttpGet]
        [Route("api/search/getsearchgrade/{from}/{to}")]
        public List<Voznja> GetSearchGrade(int from, int to)
        {
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            foreach (Korisnik k in users.korisnici)
            {
                if (k.KorisnickoIme == user.KorisnickoIme)
                {
                    foreach (Voznja v in k.voznjeKorisnika)
                    {
                        if (v.Komentar.OcenaVoznje >= from && v.Komentar.OcenaVoznje <= to)
                        {
                            retVal.Add(v);
                        }
                    }

                    return retVal;
                }
            }


            return new List<Voznja>();
        }


        [HttpGet]
        [Route("api/search/getsearchprice/{from}/{to}")]
        public List<Voznja> GetSearchPrice(int from, int to)
        {
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            foreach (Korisnik k in users.korisnici)
            {
                if (k.KorisnickoIme == user.KorisnickoIme)
                {
                    foreach (Voznja v in k.voznjeKorisnika)
                    {
                        if (v.Iznos >= from && v.Iznos <= to)
                        {
                            retVal.Add(v);
                        }
                    }

                    return retVal;
                }
            }


            return new List<Voznja>();
        }
    }
}
