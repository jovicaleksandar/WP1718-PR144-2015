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
        [Route("api/search/getfiltracijadispecer/{id}")]
        public List<Voznja> GetFiltracijaDispecer(string id)
        {
            Dispeceri users = HttpContext.Current.Application["dispeceri"] as Dispeceri;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            foreach (Dispecer k in users.dispecers)
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
        [Route("api/search/getfiltracijavozac/{id}")]
        public List<Voznja> GetFiltracijaVozac(string id)
        {
            Vozaci users = HttpContext.Current.Application["vozaci"] as Vozaci;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            foreach (Vozac k in users.vozaci)
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
        [Route("api/search/getsearchdispecer/{from}/{to}")]
        public List<Voznja> GetSearchDispecer(DateTime from, DateTime to)
        {
            Dispeceri users = HttpContext.Current.Application["dispeceri"] as Dispeceri;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();
            int result1;
            int result2;

            foreach (Dispecer k in users.dispecers)
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
        [Route("api/search/getsearchvozac/{from}/{to}")]
        public List<Voznja> GetSearchVozac(DateTime from, DateTime to)
        {
            Vozaci users = HttpContext.Current.Application["vozaci"] as Vozaci;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();
            int result1;
            int result2;

            foreach (Vozac k in users.vozaci)
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
        [Route("api/search/getsearchgradedispecer/{from}/{to}")]
        public List<Voznja> GetSearchGradeDispecer(int from, int to)
        {
            Dispeceri users = HttpContext.Current.Application["dispeceri"] as Dispeceri;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            foreach (Dispecer k in users.dispecers)
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
        [Route("api/search/getsearchgradevozac/{from}/{to}")]
        public List<Voznja> GetSearchGradeVozac(int from, int to)
        {
            Vozaci users = HttpContext.Current.Application["vozaci"] as Vozaci;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            foreach (Vozac k in users.vozaci)
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




        [HttpGet]
        [Route("api/search/getsearchpricedispecer/{from}/{to}")]
        public List<Voznja> GetSearchPriceDispecer(int from, int to)
        {
            Dispeceri users = HttpContext.Current.Application["dispeceri"] as Dispeceri;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            foreach (Korisnik k in users.dispecers)
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






        [HttpGet]
        [Route("api/search/getsearchpricevozac/{from}/{to}")]
        public List<Voznja> GetSearchPriceVozac(int from, int to)
        {
            Vozaci users = HttpContext.Current.Application["vozaci"] as Vozaci;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Voznja> retVal = new List<Voznja>();

            foreach (Vozac k in users.vozaci)
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



        [HttpGet]
        [Route("api/search/getsearchbyname/{fname}/{lname}")]
        public List<Korisnik> GetSearchByName(string fname, string lname)
        {
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Dispeceri dispeceri = HttpContext.Current.Application["dispeceri"] as Dispeceri;
            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;

            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            List<Korisnik> retVal = new List<Korisnik>();

            bool flag1 = true;
            bool flag2 = true;

            if (fname.Equals("nevalidan_unos"))
                flag1 = false;

            if (lname.Equals("nevalidan_unos"))
                flag2 = false;

            if (user.Role == Enums.Uloga.Dispecer)
            {
                foreach (Korisnik k in users.korisnici)
                {
                    if (!flag1)
                        fname = k.Ime;

                    if (!flag2)
                        lname = k.Prezime;

                    if (k.Ime.ToLower().Equals(fname.ToLower()) && k.Prezime.ToLower().Equals(lname.ToLower()))
                        retVal.Add(k);
                }

                foreach(Vozac v in vozaci.vozaci)
                {
                    if (!flag1)
                        fname = v.Ime;

                    if (!flag2)
                        lname = v.Prezime;

                    if (v.Ime.ToLower().Equals(fname.ToLower()) && v.Prezime.ToLower().Equals(lname.ToLower()))
                        retVal.Add(v);
                }

                return retVal;
            }

            return new List<Korisnik>();
        }




    }
}
