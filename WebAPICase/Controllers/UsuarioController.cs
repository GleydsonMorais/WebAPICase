using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPICase.Data;
using WebAPICase.Models;

namespace WebAPICase.Controllers
{
    public class UsuarioController : ApiController
    {
        DataContext dataContext = new DataContext();

        // GET: api/Usuario
        [HttpGet]
        public IEnumerable<Usuario> GetAllUsuario()
        {
            var listUsuario = dataContext.Usuario.AsQueryable();

            return listUsuario;
        }

        // GET: api/Usuario/5
        [HttpGet]
        public Usuario Get(int id)
        {
            return dataContext.Usuario.Where(u => u.idUsuario == id).FirstOrDefault();
        }

        // POST: api/Usuario
        [HttpPost]
        public void Post([FromBody]Usuario usuario)
        {
            dataContext.Usuario.Add(usuario);
            dataContext.SaveChanges();
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuario/5
        [HttpDelete]
        public void DeleteUsuario(int id)
        {
            var usuario = dataContext.Usuario.Where(u => u.idUsuario == id).FirstOrDefault();
            dataContext.Usuario.Remove(usuario);
            dataContext.SaveChanges();
        }
    }
}
