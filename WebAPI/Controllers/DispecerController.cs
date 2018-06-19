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
    public class DispecerController : ApiController
    {
        [HttpGet]
        public IEnumerable<Dispecer> GetDispecere()
        {
            Dispeceri dispecers = HttpContext.Current.Application["dispeceri"] as Dispeceri;

            return dispecers.dispecers;
        }
    }
}
