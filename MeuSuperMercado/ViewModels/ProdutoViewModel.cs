using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuSuperMercado.ViewModels {
    class ProdutoViewModel {
        public int Id_Produto { get; set; }
        public string Nome { get; set; }

        public int Id_Tipo { get; set; }

        public string Preco { get; set; }
        public DateTime? Validade { get; set; }
        public bool Ativo { get; set; }
    }
}
