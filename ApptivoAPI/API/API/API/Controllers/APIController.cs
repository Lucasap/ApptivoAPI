using API.Models;
using Apptivo.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    public class APIController : ApiController
    {
        // GET: api/API
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/API/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/API/Login/{strEmail}/{strPassword}")]
        [ResponseType(typeof(Persona))]
        public IHttpActionResult Login(string strEmail, string strPassword)
        {
            
            Usuario usr  = new Usuario();
            usr.ObtenerPorMail(strEmail, strPassword);

            if(usr != null)
            {
                return Ok(usr);
            }
            else
            {
                return null;
            }
        }

        // POST: api/API
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/API/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/API/5
        public void Delete(int id)
        {
        }
    }
}
