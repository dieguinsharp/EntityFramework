using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MeuSuperMercado.Models {
    class Cliente {

        [Key]
        public int Id_Cliente { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }
        public bool Ativo { get; set; }
    }
}
