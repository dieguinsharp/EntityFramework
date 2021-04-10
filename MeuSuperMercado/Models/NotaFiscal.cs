using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuSuperMercado.Models {
    class NotaFiscal {
        [Key]
        public int Id_NotaFiscal { get; set; }


        public int Id_Cliente { get; set; }
        [ForeignKey("Id_Cliente")]
        public virtual Cliente Cliente { get; set; }


        public decimal Valor { get; set; }
        public DateTime? DataCompra { get; set; }
        public DateTime? DataPagamento { get; set; }
        public bool Ativo { get; set; }
    }
}
