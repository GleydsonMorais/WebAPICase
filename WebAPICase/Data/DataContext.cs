using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPICase.Data.Mapping;
using WebAPICase.Models;

namespace WebAPICase.Data
{
    public class DataContext : DbContext
    {
        //HOST-INFOPUBLIC - (APAGAR)
        static private string stringConnection = "Data Source=HOST-INFOPUBLIC;Initial Catalog=DBEntidade;User ID=Infopublic;Password=#info8116*;Connect Timeout=15;Encrypt=False;Packet Size=8000;MultipleActiveResultSets=True;Application Name=EntityFramework;Integrated Security=False";

        //HOST Casa
        //static private string stringConnection = "Data Source=HOST-INFOPUBLIC;Initial Catalog=DBEntidade;User ID=Infopublic;Password=#info8116*;Connect Timeout=15;Encrypt=False;Packet Size=8000;MultipleActiveResultSets=True;Application Name=EntityFramework;Integrated Security=False";

        static DataContext()
        {
            Database.SetInitializer<DataContext>(null);
        }

        public DataContext()
            : base(stringConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}