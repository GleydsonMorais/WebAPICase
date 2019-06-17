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

        [HttpGet]
        public IEnumerable<string[]> MediaCombustivelVendaPorMunicipio()
        {
            List<string[]> result = new List<string[]>();

            var listMunicipio = dataContext.Historico.ToList().OrderBy(h => h.municipio).Select(m => m.municipio).Distinct();

            foreach (var municipio in listMunicipio)
            {
                //Primeiro campor é o nome do municipio e o segundo é o valor de venda
                result.Add(new string[] { municipio, "R$ " + dataContext.Historico.Where(h => h.municipio == municipio).Average(m => m.valorVenda).ToString() });
            }

            return result;
        }

        [HttpGet]
        public IEnumerable<IEnumerable<Historico>> HistoricoPorRegiao()
        {
            var listHistorico = dataContext.Historico.GroupBy(h => h.regiao).Select(grp => grp.ToList()).ToList();

            return listHistorico;
        }

        [HttpGet]
        public IEnumerable<IEnumerable<Historico>> HistoricoPorDistribuidora()
        {
            var listHistorico = dataContext.Historico.GroupBy(h => h.revenda).Select(grp => grp.ToList()).ToList();

            return listHistorico;
        }

        [HttpGet]
        public IEnumerable<IEnumerable<Historico>> HistoricoPorDataColeta()
        {
            var listHistorico = dataContext.Historico.GroupBy(h => h.dataColeta).Select(grp => grp.ToList()).ToList();

            return listHistorico;
        }

        [HttpGet]
        public IEnumerable<string[]> MediaCombustivelCompravendaPorMunicipio()
        {
            List<string[]> result = new List<string[]>();

            var listMunicipio = dataContext.Historico.ToList().OrderBy(h => h.municipio).Select(m => m.municipio).Distinct();

            foreach (var municipio in listMunicipio)
            {
                //Primeiro campor é o nome do municipio, o segundo é o valor de compra e o terceiro é o valor de venda
                result.Add(new string[] { municipio, "R$ " + dataContext.Historico.Where(h => h.municipio == municipio).Average(m => m.valorCompra).ToString(), "R$ " + dataContext.Historico.Where(h => h.municipio == municipio).Average(m => m.valorVenda).ToString() });
            }

            return result;
        }

        [HttpGet]
        public IEnumerable<string[]> MediaCombustivelCompravendaPorBandeira()
        {
            List<string[]> result = new List<string[]>();

            var listBandeira = dataContext.Historico.ToList().OrderBy(h => h.bandeira).Select(m => m.bandeira).Distinct();

            foreach (var bandeira in listBandeira)
            {
                //Primeiro campor é o nome da bandeira, o segundo é o valor de compra e o terceiro é o valor de venda
                result.Add(new string[] { bandeira, "R$ " + dataContext.Historico.Where(h => h.bandeira == bandeira).Average(m => m.valorCompra).ToString(), "R$ " + dataContext.Historico.Where(h => h.bandeira == bandeira).Average(m => m.valorVenda).ToString() });
            }

            return result;
        }
    }
}
