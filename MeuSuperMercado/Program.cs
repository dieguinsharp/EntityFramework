using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MeuSuperMercado.Models;
using MeuSuperMercado.ViewModels;
using System.Threading.Tasks;

namespace MeuSuperMercado {
    class Program {
        public static DAO db = new DAO();
        static void Main(string[] args) {
            while(true) {
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(" -- Bem vindo ao Supermercado Sharp! -- ");
                Console.WriteLine();

                Console.WriteLine(" -- Painel -- ");
                Console.WriteLine(" -- [1] Vender produtos --");
                Console.WriteLine(" -- [2] Ver Historico de compras dos clientes --");
                Console.WriteLine(" -- [3] Adicionar produtos a venda --");
                Console.WriteLine(" -- [4] Adicionar tipos de produtos  --");
                Console.WriteLine(" -- [5] Cadastrar clientes  --");
                Console.WriteLine(" -- [6] Ver produtos  --");
                Console.WriteLine();

                Console.Write("O que deseja: ");

                var r = Convert.ToInt32(Console.ReadLine());

                switch(r) {
                    case 1:

                        var idCliente = PegarIDCliente();

                        VenderProdutos(idCliente);

                        var soma = SomarProdutos();
                        Console.WriteLine("Calculando valores...");

                        Console.WriteLine("Valor total da sua compra: " + soma);

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Criando sua Nota Fiscal..");

                        using(var db = new DAO()) {

                            var nf = new NotaFiscal() {
                                Ativo = true, DataCompra = DateTime.Now, DataPagamento = DateTime.Now, Id_Cliente = idCliente, Valor = soma
                            };

                            db.NotaFiscal.Add(nf);
                            db.SaveChanges();

                            foreach(var i in ps) {
                                var associacao = new NF_Produtos() {
                                    Ativo = true, Id_Produto = i, Id_NF = nf.Id_NotaFiscal
                                };

                                db.NF_Produtos.Add(associacao);
                                db.SaveChanges();
                            }

                            var produtosNF = (from ass in db.NF_Produtos
                                              join p in db.Produto
                                              on ass.Id_NF equals nf.Id_NotaFiscal
                                              where p.Id_Produto == ass.Id_Produto
                                              select new {
                                                  p.Nome, p.Preco
                                              }).ToList();

                            Console.WriteLine("-- Supermercado Sharp XD --");
                            Console.WriteLine();

                            Console.WriteLine("-- Sua Nota Fiscal --");
                            Console.WriteLine("Cliente ID: " + idCliente);
                            Console.WriteLine("-- Produtos --");
                            foreach(var i in produtosNF) {
                                Console.WriteLine(i.Nome + " - " + "R$ " + i.Preco + ".00");
                            }
                            Console.WriteLine("Valor da nota:" + nf.Valor.ToString("C"));

                            Console.WriteLine();
                            Console.WriteLine("-- Volte sempre! --");

                        }

                            break;
                    case 2:

                        ExibirCompras();

                        break;
                    case 3:

                        AddProdutos();

                        break;
                    case 4:

                        AddTipoProduto();

                        break;
                    case 5:

                        CadastroCliente();

                        break;
                    case 6:

                        MostrarProdutos();

                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }

        static List<int> ps = new List<int>();
        public static void ExibirCompras() {

            Console.Write("Informe o nome do cliente: ");
            var nomeCliente = Console.ReadLine();

            var hs = (from p in db.Cliente
                        join nf in db.NotaFiscal
                        on p.Id_Cliente equals nf.Id_Cliente
                        where p.Nome == nomeCliente
                        select new {
                            nf.DataCompra, nf.DataPagamento, nf.Valor, p.Nome, Produtos = (
                            from produtos in db.Produto
                            join associacao in db.NF_Produtos
                            on produtos.Id_Produto equals associacao.Id_Produto
                            join tipo in db.Tipo
                            on produtos.Id_Tipo equals tipo.Id_Tipo

                            where associacao.Id_NF == nf.Id_NotaFiscal

                            select new {
                                produtos.Nome, TipoProduto = tipo.Nome
                            })
                        }).ToList();

            foreach(var h in hs) {
                Console.WriteLine("----------------");
                Console.WriteLine("Cliente: " + h.Nome);
                Console.WriteLine("Data da compra: " + h.DataCompra);

                Console.WriteLine();

                foreach(var ps in h.Produtos) {
                    Console.WriteLine(ps.Nome + " - " + ps.TipoProduto);
                }

                Console.WriteLine();

                Console.WriteLine("Data pagamento: " + h.DataPagamento);
                Console.WriteLine("Valor: " + h.Valor);
                Console.WriteLine("----------------");

                Console.WriteLine();
                Console.WriteLine();
            }
        }
        public static decimal SomarProdutos() {

            decimal d = 0;

            foreach(var i in ps) {
                var soma = (from p in db.Produto
                            where p.Id_Produto == i
                            select new {
                                p.Preco
                            }).FirstOrDefault();

                d += soma.Preco;
            }

            return d;
        }
        public static void VenderProdutos(int ID) {
            bool comprarMais = true;
            while(comprarMais) {

                Console.WriteLine("Cliente ID:" + ID);
                Console.WriteLine("Qtd de Produtos no carrinho:" + ps.Count);

                MostrarProdutos();

                Console.Write("Informe o ID do produto:");
                var s = Console.ReadLine();

                ps.Add(Convert.ToInt32(s));

                Console.Write("Deseja comprar mais? [S/N]");
                var r = Console.ReadLine();

                if(r == "N" || r == "n") {
                    comprarMais = false;
                }
            }
        }
        public static int PegarIDCliente() {

            Console.Write("Digite o nome completo do Cliente:");
            var r = Console.ReadLine();

            var cliente = (from c in db.Cliente
                           where c.Ativo && c.Nome == r
                           select new {
                               c.Id_Cliente
                           }).FirstOrDefault();

            return cliente.Id_Cliente;
        }
        public static void MostrarProdutos() {
            var produtos = (from e in db.Produto
                            join t in db.Tipo
                            on e.Id_Tipo equals t.Id_Tipo
                            where e.Ativo
                            select new {
                                e.Nome, e.Validade, e.Preco, e.Id_Produto, NomeTipo = t.Nome
                            }).ToList();

            Console.WriteLine("-- Lista de Produtos --");
            Console.WriteLine();

            foreach(var i in produtos) {
                Console.WriteLine();
                Console.WriteLine($"ID Produto: [{i.Id_Produto}]");
                Console.WriteLine($"Nome: {i.Nome}");
                Console.WriteLine($"Tipo: {i.NomeTipo}");
                Console.WriteLine($"Preço: {i.Preco}");
                Console.WriteLine($"Validade: - {i.Validade.Value.ToString("dd/MM/yyyy")}");
                Console.WriteLine();
            }
        }
        public static void CadastroCliente() {
            Console.Write("Informe o nome do cliente: ");
            var nomeCliente = Console.ReadLine();

            Console.Write("Informe o endereço do cliente: ");
            var endereco = Console.ReadLine();

            Console.Write("Informe o CPF do cliente: ");
            var cpf = Console.ReadLine();

            using(var db = new DAO()) {

                var c = new Cliente() {
                    Nome = nomeCliente, CPF = cpf, Ativo = true, Endereco = endereco
                };

                db.Cliente.Add(c);
                db.SaveChanges();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cliente criado com sucesso.");
            Console.WriteLine("Pressione para continuar...");
            Console.ReadKey();
        }
        public static void AddTipoProduto() {
            Console.Write("Informe o tipo de produto: ");
            var nomeTipo = Console.ReadLine();

            using(var db = new DAO()) {

                var t = new Tipo() {
                    Nome = nomeTipo, Ativo = true
                };

                db.Tipo.Add(t);
                db.SaveChanges();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tipo criado com sucesso.");
            Console.WriteLine("Pressione para continuar...");
            Console.ReadKey();
        }
        public static void AddProdutos() {
            Console.Write("Informe o nome do produto: ");
            var nomeProduto = Console.ReadLine();

            Console.Write("Informe o preço do produto: ");
            var precoProduto = Console.ReadLine();

            Console.Write("Informe a data de validade do produto: ");
            var dataProduto = Console.ReadLine();

            DateTime ValidadeProduto = Convert.ToDateTime(dataProduto);

            Console.WriteLine();

            var itens = (from e in db.Tipo
                         where e.Ativo
                         select new {
                             e.Id_Tipo,
                             e.Nome
                         }).ToList();

            foreach(var i in itens) {
                Console.WriteLine($"[{i.Id_Tipo}] - {i.Nome}");
            }

            Console.WriteLine();

            Console.Write("Informe o Tipo do produto: ");
            var tipoProduto = Convert.ToInt32(Console.ReadLine());

            using(var db = new DAO()) {

                var p = new Produto() {
                    Nome = nomeProduto, Id_Tipo = tipoProduto, Ativo = true, Preco = Convert.ToDecimal(precoProduto), Validade = ValidadeProduto
                };

                db.Produto.Add(p);
                db.SaveChanges();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Produto criado com sucesso.");
            Console.WriteLine("Pressione para continuar...");
            Console.ReadKey();
        }
    }
}
