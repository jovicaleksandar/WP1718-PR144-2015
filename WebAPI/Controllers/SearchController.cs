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
        [Route("api/search/search/{id}")]
        public List<Voznja> GetSearch(string id)
        {
        }
}
