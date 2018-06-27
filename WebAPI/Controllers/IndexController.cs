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
    public class IndexController : ApiController
    {
        public Korisnik Post([FromBody]Korisnik korisnik)
        {
            Dispeceri dispeceri = (Dispeceri)HttpContext.Current.Application["dispeceri"];
            Korisnici users = (Korisnici)HttpContext.Current.Application["korisnici"];
            Vozaci vozaci = (Vozaci)HttpContext.Current.Application["vozaci"];
            Voznje voznje = (Voznje)HttpContext.Current.Application["voznje"];

            //********************************************************************************************************

            List<Voznja> search = HttpContext.Current.Session["search"] as List<Voznja>;

            if (search == null)
            {
                search = new List<Voznja>();
                HttpContext.Current.Session["search"] = search;
            }

            List<Korisnik> searchUsers = HttpContext.Current.Session["searchUsers"] as List<Korisnik>;

            if (searchUsers == null)
            {
                searchUsers = new List<Korisnik>();
                HttpContext.Current.Session["searchUsers"] = searchUsers;
            }

            //*****************************************************************************************************************

            foreach (Korisnik k in users.korisnici)
            {
                if (k.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    HttpContext.Current.Session["search"] = k.voznjeKorisnika;
                }
            }


            foreach (Dispecer d in dispeceri.dispecers)
            {
                if (d.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    List<Voznja> retVal = new List<Voznja>();

                    foreach (Voznja v in voznje.voznje)
                        retVal.Add(v);

                    HttpContext.Current.Session["search"] = retVal;
                }
            }


            foreach (Vozac v in vozaci.vozaci)
            {
                if (v.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    List<Voznja> retVal = new List<Voznja>();
                    retVal = v.voznjeKorisnika;
                    foreach (Voznja ride in voznje.voznje)
                    {
                        if (ride.Status == Enums.Status.Kreirana_Na_Cekanju)
                        {
                            retVal.Add(ride);
                        }
                    }

                    HttpContext.Current.Session["search"] = retVal;
                }
            }

            foreach (Dispecer d in dispeceri.dispecers)
            {
                if (d.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    List<Korisnik> retVal = new List<Korisnik>();
                    foreach (Korisnik k in users.korisnici)
                    {
                        retVal.Add(k);
                    }
                    foreach (Vozac v in vozaci.vozaci)
                    {
                        retVal.Add(v);
                    }

                    HttpContext.Current.Session["searchUsers"] = retVal;
                }
            }

            //*******************************************************************************************************************

            foreach (var item in users.korisnici)
            {
                if (korisnik.KorisnickoIme == item.KorisnickoIme)
                {
                    return item;
                }
            }

            foreach (var item in dispeceri.dispecers)
            {
                if (korisnik.KorisnickoIme == item.KorisnickoIme)
                {
                    return item;
                }
            }

            foreach (var item in vozaci.vozaci)
            {
                if (korisnik.KorisnickoIme == item.KorisnickoIme)
                    return item;
            }

            return null;
        }
    }
}
