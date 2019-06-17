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

        Usuario usuario = new Usuario();

        // GET: api/Usuario
        [HttpGet]
        public IEnumerable<Usuario> Get()
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
        [HttpPut]
        public void Put(int id, [FromBody]Usuario usuario)
        {
            dataContext.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            dataContext.SaveChanges();
        }

        // DELETE: api/Usuario/5
        [HttpDelete]
        public void Delete(int id)
        {
            var usuario = dataContext.Usuario.Where(u => u.idUsuario == id).FirstOrDefault();
            dataContext.Usuario.Remove(usuario);
            dataContext.SaveChanges();
        }
    }
}
