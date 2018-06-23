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
    public class VoznjaController : ApiController
    {
        [HttpGet]
        public List<Voznja> Get()
        {
            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            foreach (Korisnik k in users.korisnici)
            {
                if (user.KorisnickoIme != "" && user.KorisnickoIme != null && user.KorisnickoIme == k.KorisnickoIme)
                    return user.voznjeKorisnika;
            }

            return new List<Voznja>();
        }

        [HttpGet]
        [Route("api/voznja/getall")]
        public List<Voznja> GetAll()
        {
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            if (user.KorisnickoIme != "" && user.KorisnickoIme != null && user.Role == Enums.Uloga.Dispecer)
                return voznje.voznje;

            return new List<Voznja>();
        }


        [HttpGet]
        [Route("api/voznja/getdriversrides")]
        public List<Voznja> GetDriversRides()
        {
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;
            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;

            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            if (user.KorisnickoIme != "" && user.KorisnickoIme != null && user.Role == Enums.Uloga.Vozac)
            {
                foreach(Vozac rider in vozaci.vozaci)
                {
                    if (rider.KorisnickoIme == user.KorisnickoIme)
                    {
                        return rider.voznjeKorisnika;
                    }
                }
            }

            return new List<Voznja>();
        }

        public bool Post([FromBody] Voznja v)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }


            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;

            if (user.Role == Enums.Uloga.Musterija && v.Musterija == user.KorisnickoIme)
            {
                v.VremePorudzbine = DateTime.UtcNow;
                v.IdVoznje = voznje.voznje.Count;
                v.Musterija = user.KorisnickoIme;
                v.Komentar = new Komentar();
                v.Komentar.Opis = "";
                v.Komentar.KorisnickoIme = "";
                v.Odrediste = new Lokacija();
                v.Odrediste.X = "";
                v.Odrediste.Y = "";
                v.Odrediste.Adresa = new Adresa();
                v.Odrediste.Adresa.NaseljenoMesto = "";
                v.Odrediste.Adresa.PozivniBroj = "";
                v.Odrediste.Adresa.UlicaBroj = "";

                user.voznjeKorisnika.Add(v);
                voznje.voznje.Add(v);
                

                string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\voznje.txt";


                string line = String.Empty;
                    line = v.IdVoznje.ToString() + '|' + v.VremePorudzbine.ToString() + '|' + v.LokacijaDolaskaTaksija.X + '|' + v.LokacijaDolaskaTaksija.Y + '|' +
                    v.LokacijaDolaskaTaksija.Adresa.UlicaBroj + '|' + v.LokacijaDolaskaTaksija.Adresa.NaseljenoMesto + '|' + v.LokacijaDolaskaTaksija.Adresa.PozivniBroj + '|' + v.Automobil + '|' +
                    v.Musterija + '|' + v.Odrediste.X + '|' + v.Odrediste.Y + '|' + v.Odrediste.Adresa.UlicaBroj + '|' + v.Odrediste.Adresa.NaseljenoMesto + '|' + v.Odrediste.Adresa.PozivniBroj + '|' +
                    v.Dispecer + '|' + v.Vozac + '|' + v.Iznos + '|' + v.Komentar.Opis + '|' + v.Komentar.DatumObjave + '|' + v.Komentar.KorisnickoIme + '|' +
                    v.Komentar.OcenaVoznje + '|' + v.Status + '|' + Environment.NewLine;


                if (!File.Exists(path))
                {
                    File.WriteAllText(path, line);
                }
                else
                {
                    File.AppendAllText(path, line);
                }

                //voznje = new Voznje(@"~/App_Data/voznje.txt");
                HttpContext.Current.Application["voznje"] = voznje;
                HttpContext.Current.Application["korisnici"] = users;
                return true;
            }

            return false;
        }
        
        public bool Put(string id, [FromBody] Voznja v)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;

            foreach (Korisnik korisnik in users.korisnici)
            {
                if (korisnik.KorisnickoIme == user.KorisnickoIme)
                {
                    foreach (Voznja ride in korisnik.voznjeKorisnika)
                    {
                        if (ride.Status == Enums.Status.Kreirana_Na_Cekanju && ride.IdVoznje.ToString() == id)
                        {
                            ride.IdVoznje = Int32.Parse(id);
                            ride.VremePorudzbine = DateTime.UtcNow;
                            ride.Musterija = user.KorisnickoIme;
                            ride.Komentar = new Komentar();
                            ride.Komentar.Opis = "";
                            ride.Komentar.KorisnickoIme = "";
                            ride.Odrediste.X = "";
                            ride.Odrediste = new Lokacija();
                            ride.Odrediste.Y = "";
                            ride.Odrediste.Adresa = new Adresa();
                            ride.Odrediste.Adresa.NaseljenoMesto = "";
                            ride.Odrediste.Adresa.PozivniBroj = "";
                            ride.Odrediste.Adresa.UlicaBroj = "";
                            ride.LokacijaDolaskaTaksija.X = v.LokacijaDolaskaTaksija.X;
                            ride.LokacijaDolaskaTaksija.Y = v.LokacijaDolaskaTaksija.Y;
                            ride.LokacijaDolaskaTaksija.Adresa.NaseljenoMesto = v.LokacijaDolaskaTaksija.Adresa.NaseljenoMesto;
                            ride.LokacijaDolaskaTaksija.Adresa.PozivniBroj = v.LokacijaDolaskaTaksija.Adresa.PozivniBroj;
                            ride.LokacijaDolaskaTaksija.Adresa.UlicaBroj = v.LokacijaDolaskaTaksija.Adresa.UlicaBroj;

                            string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\voznje.txt";
                            string line = String.Empty;

                            line = ride.IdVoznje.ToString() + '|' + ride.VremePorudzbine.ToString() + '|' + ride.LokacijaDolaskaTaksija.X + '|' + ride.LokacijaDolaskaTaksija.Y + '|' +
                                ride.LokacijaDolaskaTaksija.Adresa.UlicaBroj + '|' + ride.LokacijaDolaskaTaksija.Adresa.NaseljenoMesto + '|' + ride.LokacijaDolaskaTaksija.Adresa.PozivniBroj + '|' + ride.Automobil + '|' +
                                ride.Musterija + '|' + ride.Odrediste.X + '|' + ride.Odrediste.Y + '|' + ride.Odrediste.Adresa.UlicaBroj + '|' + ride.Odrediste.Adresa.NaseljenoMesto + '|' + ride.Odrediste.Adresa.PozivniBroj + '|' +
                                ride.Dispecer + '|' + ride.Vozac + '|' + ride.Iznos + '|' + ride.Komentar.Opis + '|' + ride.Komentar.DatumObjave + '|' + ride.Komentar.KorisnickoIme + '|' +
                                ride.Komentar.OcenaVoznje + '|' + ride.Status + '|' + Environment.NewLine;

                            string[] arrLine = File.ReadAllLines(path);
                            arrLine[ride.IdVoznje] = line;
                            File.WriteAllLines(path, arrLine);
                            File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));
                        }
                    }
                }

                HttpContext.Current.Application["korisnici"] = users;

                return true;
            }

            return false;
        }
        

        [HttpDelete]
        public bool Delete(string id)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            if (user == null)
            {
                user = new Korisnik();
                HttpContext.Current.Session["user"] = user;
            }

            Korisnici users = HttpContext.Current.Application["korisnici"] as Korisnici;
            Voznje voznje = HttpContext.Current.Application["voznje"] as Voznje;
            
            foreach (Voznja ride in user.voznjeKorisnika)
            {
                if (ride.Status == Enums.Status.Kreirana_Na_Cekanju && ride.IdVoznje.ToString() == id)
                {
                    ride.Status = Enums.Status.Otkazana;

                    string path = @"C:\Users\Coa\Desktop\NovaVerzija\WebAPI\WebAPI\App_Data\voznje.txt";
                    string line = String.Empty;

                    line = ride.IdVoznje.ToString() + '|' + ride.VremePorudzbine.ToString() + '|' + ride.LokacijaDolaskaTaksija.X + '|' + ride.LokacijaDolaskaTaksija.Y + '|' +
                        ride.LokacijaDolaskaTaksija.Adresa.UlicaBroj + '|' + ride.LokacijaDolaskaTaksija.Adresa.NaseljenoMesto + '|' + ride.LokacijaDolaskaTaksija.Adresa.PozivniBroj + '|' + ride.Automobil + '|' +
                        ride.Musterija + '|' + ride.Odrediste.X + '|' + ride.Odrediste.Y + '|' + ride.Odrediste.Adresa.UlicaBroj + '|' + ride.Odrediste.Adresa.NaseljenoMesto + '|' + ride.Odrediste.Adresa.PozivniBroj + '|' +
                        ride.Dispecer + '|' + ride.Vozac + '|' + ride.Iznos + '|' + ride.Komentar.Opis + '|' + ride.Komentar.DatumObjave + '|' + ride.Komentar.KorisnickoIme + '|' +
                        ride.Komentar.OcenaVoznje + '|' + ride.Status + '|' + Environment.NewLine;

                        string[] arrLine = File.ReadAllLines(path);
                        arrLine[ride.IdVoznje] = line;
                        File.WriteAllLines(path, arrLine);
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                }
            }

            HttpContext.Current.Application["korisnici"] = users;

            return true;
        }
    }
}
