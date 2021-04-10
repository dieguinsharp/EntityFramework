using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MeuSuperMercado.Models {
    class DAO : DbContext {
        public DAO() : base("name=ModeloEntidades") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<NotaFiscal> NotaFiscal { get; set; }
        public virtual DbSet<NF_Produtos> NF_Produtos { get; set; }
    }
}
