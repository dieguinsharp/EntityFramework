using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuSuperMercado.Models {
    class Produto {
        [Key]
        public int Id_Produto { get; set; }
        public string Nome { get; set; }


        public int Id_Tipo { get; set; }
        [ForeignKey("Id_Tipo")]
        public virtual Tipo Tipo { get; set; }


        public decimal Preco { get; set; }
        public DateTime? Validade { get; set; }
        public bool Ativo { get; set; }
    }
}
