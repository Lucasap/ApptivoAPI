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
        [HttpGet]
        public Usuario Login(string strEmail, string strPassword)
        {

            Usuario usr = new Usuario();
            usr = usr.ObtenerPorMail(strEmail, strPassword);


            if (usr != null)
            {
                return usr;
            }
            else
            {
                return null;
            }
        }
        [Route("api/API/Registrar/{strNombre}/{strApellido}/{strEmail}/{strPassword}")]
        [HttpGet]
        public string Registrar(string strNombre, string strApellido, string strEmail, string strPassword)
        {
            string TodoOk = "Ok";
            Usuario nusuario = new Usuario();
            Usuario usr = new Usuario();
            usr.Nombre = strNombre;
            usr.Apellido = strApellido;
            usr.Sexo = "M";
            usr.Email = strEmail;
            usr.Contraseña = strPassword;
            nusuario.Insert(usr);
            return TodoOk;
        }
        [Route("api/API/CoordenadasxLinea/{strLinea}")]
        [HttpGet]
        public List<LatLng> CoordenadasxLinea(string strLinea)
        {
            LatLng p = new LatLng();
            Usuario usu = new Usuario();
            List<LatLng> lista = new List<LatLng>();
            lista = usu.ObtenerPorLinea(strLinea);
            return lista;
        }
        [Route("api/API/Coordenadas/{strMail}/{strLinea}/{strLat}/{strLng}")]
        [HttpGet]
        public string Coordenadas(string strMail, string strLinea, string strLat, string strLng)
        {
            string TodoOk = "Good";
            float nLat = float.Parse(strLat);
            float nLng = float.Parse(strLng);
            Usuario nusuario = new Usuario();
            nusuario.InsertarCoordenadas(strMail, strLinea, nLat, nLng);
            return TodoOk;
        }
        [Route("api/API/SeBajo/{strMail}")]
        [HttpGet]
        public string SeBajo(string strMail)
        {
            string TodoOk = "Good";
            Usuario nusuario = new Usuario();
            nusuario.ActualizarSeBajo(strMail);
            return TodoOk;
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
