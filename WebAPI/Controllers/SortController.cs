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
    public class SortController : ApiController
    {
        [HttpGet]
        [Route("api/sort/getsort/{id}")]
        public List<Voznja> GetSort(string id)
        {
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            if (id == "Datum")
            {
                foreach (Korisnik k in users.korisnici)
                {
                    if (k.KorisnickoIme == user.KorisnickoIme)
                    {
                        foreach (Voznja v in k.voznjeKorisnika)
                        {
                            retVal.Add(v);
                        }
                        retVal = retVal.OrderByDescending(x => x.VremePorudzbine).ToList();
                        return retVal;
                    }
                }
            }
            else if (id == "Ocena")
            {
                foreach (Korisnik k in users.korisnici)
                {
                    if (k.KorisnickoIme == user.KorisnickoIme)
                    {
                        foreach (Voznja v in k.voznjeKorisnika)
                        {
                            retVal.Add(v);
                        }
                        retVal = retVal.OrderByDescending(x => x.Komentar.OcenaVoznje).ToList();
                        return retVal;
                    }
                }

            }

            return new List<Voznja>();
        }



        [HttpGet]
        [Route("api/sort/getsortdispecer/{id}")]
        public List<Voznja> GetSortDispecer(string id)
        {
            Dispeceri users = HttpContext.Current.Application["dispeceri"] as Dispeceri;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            if (id == "Datum")
            {
                foreach (Dispecer k in users.dispecers)
                {
                    if (k.KorisnickoIme == user.KorisnickoIme)
                    {
                        foreach (Voznja v in k.voznjeKorisnika)
                        {
                            retVal.Add(v);
                        }
                        retVal = retVal.OrderByDescending(x => x.VremePorudzbine).ToList();
                        return retVal;
                    }
                }
            }
            else if (id == "Ocena")
            {
                foreach (Dispecer k in users.dispecers)
                {
                    if (k.KorisnickoIme == user.KorisnickoIme)
                    {
                        foreach (Voznja v in k.voznjeKorisnika)
                        {
                            retVal.Add(v);
                        }
                        retVal = retVal.OrderByDescending(x => x.Komentar.OcenaVoznje).ToList();
                        return retVal;
                    }
                }

            }

            return new List<Voznja>();
        }

    }
}
