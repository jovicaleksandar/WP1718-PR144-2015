using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class KorisnikController : ApiController
    {
        public Korisnik Put([FromBody] Korisnik k)
        {
            return k;
        }
    }
}
