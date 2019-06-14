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
    public class HistoricoController : ApiController
    {
        DataContext dataContext = new DataContext();

        // GET: api/Historico
        public IEnumerable<Historico> Get()
        {
            return dataContext.Historico.AsQueryable();
        }

        // GET: api/Historico/5
        public Historico Get(int id)
        {
            return dataContext.Historico.Find(id);
        }

        // POST: api/Historico
        [HttpPost]
        public void Post([FromBody]Historico historico)
        {
            dataContext.Historico.Add(historico);
            dataContext.SaveChanges();
        }

        // PUT: api/Historico/5
        [HttpPut]
        public void Put(int id, [FromBody]Historico historico)
        {
            dataContext.Entry(historico).State = System.Data.Entity.EntityState.Modified;
            dataContext.SaveChanges();
        }

        // DELETE: api/Historico/5
        [HttpDelete]
        public void Delete(int id)
        {
            var historico = dataContext.Historico.Find(id);
            dataContext.Historico.Remove(historico);
            dataContext.SaveChanges();
        }
    }
}
