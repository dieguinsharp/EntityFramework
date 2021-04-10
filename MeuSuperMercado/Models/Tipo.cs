using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MeuSuperMercado.Models {
    class Tipo {

        [Key]
        public int Id_Tipo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
