using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebAPI.Models
{
    public class Voznje
    {
        public List<Voznja> voznje { get; set; }
        public Voznje() { }

        public Voznje(string path)
        {
            Korisnici korisnici = HttpContext.Current.Application["korisnici"] as Korisnici;
            Vozaci vozaci = HttpContext.Current.Application["vozaci"] as Vozaci;
            Dispeceri dispeceri = HttpContext.Current.Application["dispeceri"] as Dispeceri;

            foreach (Korisnik k in korisnici.korisnici)
                k.voznjeKorisnika = new List<Voznja>();
            foreach (Dispecer k in dispeceri.dispecers)
                k.voznjeKorisnika = new List<Voznja>();
            foreach (Vozac k in vozaci.vozaci)
                k.voznjeKorisnika = new List<Voznja>();

            path = HostingEnvironment.MapPath(path);
            voznje = new List<Voznja>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line == null || line == "")
                    break;

                string[] tokens = line.Split('|');
                Voznja d = new Voznja(Int32.Parse(tokens[0]), tokens[1], tokens[2], tokens[3], tokens[4], tokens[5], tokens[6], tokens[7], tokens[8], tokens[9], tokens[10], tokens[11], tokens[12], tokens[13], tokens[14], tokens[15], tokens[16], tokens[17], tokens[18], tokens[19], tokens[20], tokens[21]);
                voznje.Add(d);
                
                //tokens[8]
                foreach (Korisnik k in korisnici.korisnici)
                {
                    if (k.KorisnickoIme == tokens[8])
                    {
                        k.voznjeKorisnika.Add(d);
                    }
                }
                
                //tokens[15]
                foreach (Vozac vozac in vozaci.vozaci)
                {
                    if (tokens[15] == vozac.KorisnickoIme)
                    {
                        vozac.voznjeKorisnika.Add(d);
                    }
                }
                
                //tokens[14]
                foreach (Dispecer dispecer in dispeceri.dispecers)
                {
                    if (tokens[14] == dispecer.KorisnickoIme)
                    {
                        dispecer.voznjeKorisnika.Add(d);
                    }
                }

            }
            sr.Close();
            stream.Close();
        }
    }
}