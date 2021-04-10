using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuSuperMercado.Models {
    class NF_Produtos {
        [Key]
        public int Id_NFProdutos { get; set; }

        public int Id_NF { get; set; }
        [ForeignKey("Id_NF")]
        public virtual NotaFiscal NotaFiscal { get; set; }

        public int Id_Produto { get; set; }
        [ForeignKey("Id_Produto")]
        public virtual Produto Produto { get; set; }

        public bool Ativo { get; set; }
    }
}
