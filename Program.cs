using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TestesNHibernate.Models;
using TestesNHibernate.Database;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Criterion;

namespace TestesNHibernate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Conexao            
            var sessionFactory = Conexao.ConectarSessionFactory();

            Produto produto = new Produto();
            int codigoBusca;
            string campoBusca;

            using (var session = sessionFactory.OpenSession())
            {
                //QUERYS - Execucao direta de querys
                //(Nao recomendado, usado apenas para operacoes nao comtempladas pelo core NHibernate)                
                using (var transaction = session.BeginTransaction())
                {
                    session.CreateSQLQuery("DELETE FROM Produto").ExecuteUpdate();
                    session.CreateSQLQuery("DBCC CHECKIDENT ('Produto', RESEED, 0)").ExecuteUpdate();
                    session.CreateSQLQuery("DELETE FROM Usuario").ExecuteUpdate();
                    session.CreateSQLQuery("DBCC CHECKIDENT ('Usuario', RESEED, 0)").ExecuteUpdate();
                    transaction.Commit();
                    Console.WriteLine($"Dados limpos e ids resetados!");
                }

                //CRUD
                //CREATE
                using (ITransaction transaction = session.BeginTransaction())
                {
                    //Produtos
                    session.Save(new Produto { grupo = "013548", nome = "Arroz", especificacao = "Tipo 1, Branco", unidade = "kg", preco = 18.00, quantidade = 2 });
                    session.Save(new Produto { grupo = "089341", nome = "Feijão", especificacao = "Preto, Tipo 1", unidade = "kg", preco = 9.50, quantidade = 4 });
                    session.Save(new Produto { grupo = "024763", nome = "Óleo de Cozinha", especificacao = "Soja, 900ml", unidade = "litro", preco = 7.80, quantidade = 2 });
                    session.Save(new Produto { grupo = "056278", nome = "Sal", especificacao = "Refinado", unidade = "kg", preco = 2.00, quantidade = 1 });
                    session.Save(new Produto { grupo = "078123", nome = "Açúcar", especificacao = "Refinado", unidade = "kg", preco = 5.50, quantidade = 3 });
                    session.Save(new Produto { grupo = "009284", nome = "Café", especificacao = "Torrado e Moído", unidade = "kg", preco = 15.90, quantidade = 1 });
                    session.Save(new Produto { grupo = "064812", nome = "Leite", especificacao = "Integral, UHT", unidade = "litro", preco = 4.30, quantidade = 6 });
                    session.Save(new Produto { grupo = "015937", nome = "Farinha de Trigo", especificacao = "Tipo 1", unidade = "kg", preco = 3.50, quantidade = 2 });
                    session.Save(new Produto { grupo = "072540", nome = "Macarrão", especificacao = "Espaguete", unidade = "pacote", preco = 6.20, quantidade = 5 });
                    session.Save(new Produto { grupo = "039182", nome = "Molho de Tomate", especificacao = "Tradicional", unidade = "frasco", preco = 3.20, quantidade = 3 });
                    session.Save(new Produto { grupo = "083561", nome = "Sabão em Pó", especificacao = "2 kg", unidade = "pacote", preco = 10.50, quantidade = 1 });
                    session.Save(new Produto { grupo = "021345", nome = "Detergente", especificacao = "Neutro, 500ml", unidade = "unidade", preco = 2.50, quantidade = 2 });
                    session.Save(new Produto { grupo = "075923", nome = "Papel Higiênico", especificacao = "Folha dupla", unidade = "pacote", preco = 15.00, quantidade = 1 });
                    session.Save(new Produto { grupo = "098214", nome = "Carne Bovina", especificacao = "Corte Músculo", unidade = "kg", preco = 45.00, quantidade = 2 });
                    session.Save(new Produto { grupo = "032589", nome = "Frango", especificacao = "Corte Peito", unidade = "kg", preco = 20.00, quantidade = 3 });
                    session.Save(new Produto { grupo = "047832", nome = "Ovos", especificacao = "Brancos, dúzia", unidade = "dúzia", preco = 12.00, quantidade = 2 });
                    session.Save(new Produto { grupo = "062541", nome = "Manteiga", especificacao = "Com Sal", unidade = "kg", preco = 5.00, quantidade = 1 });
                    session.Save(new Produto { grupo = "029374", nome = "Queijo", especificacao = "Mussarela", unidade = "kg", preco = 30.00, quantidade = 1 });
                    session.Save(new Produto { grupo = "048213", nome = "Batata", especificacao = "Inglesa", unidade = "kg", preco = 3.00, quantidade = 5 });
                    session.Save(new Produto { grupo = "019283", nome = "Banana", especificacao = "Nanica", unidade = "kg", preco = 4.00, quantidade = 6 });

                    //Usuarios
                    session.Save(new Usuario { nome = "Lucas", senha = "1A3B5C7D9E0F1A2B3C4D5E6F7A8B9C", dataCadastro = new DateTime(2015, 4, 15), email = "lucas@example.com" });
                    session.Save(new Usuario { nome = "Julia", senha = "0F1A2B3C4D5E6F7A8B9C0D1A2B3C4D", dataCadastro = new DateTime(2012, 6, 27), email = "julia@example.com" });
                    session.Save(new Usuario { nome = "Carlos", senha = "9E8F7D6C5B4A3E2F1D0C9B8A7E6D5C", dataCadastro = new DateTime(2017, 3, 9), email = "carlos@example.com" });
                    session.Save(new Usuario { nome = "Sofia", senha = "2F3E4D5C6B7A8E9F0C1D2B3A4C5D6E", dataCadastro = new DateTime(2020, 1, 23), email = "sofia@example.com" });
                    session.Save(new Usuario { nome = "Pedro", senha = "4E5F6A7B8C9D0E1F2A3B4C5D6E7F8G", dataCadastro = new DateTime(2011, 8, 14), email = "pedro@example.com" });
                    session.Save(new Usuario { nome = "Laura", senha = "7F8E9D0C1A2B3C4D5E6F7G8H9I0J1K", dataCadastro = new DateTime(2019, 1, 5), email = "laura@example.com" });
                    session.Save(new Usuario { nome = "Mateus", senha = "0C1D2E3F4A5B6C7D8E9F0A1B2C3D4E", dataCadastro = new DateTime(2016, 2, 11), email = "mateus@example.com" });
                    session.Save(new Usuario { nome = "Fabia", senha = "3A4B5C6D7E8F9G0H1I2J3K4L5M6N7O", dataCadastro = new DateTime(2018, 5, 19), email = "fabia@example.com" });
                    session.Save(new Usuario { nome = "Ricardo", senha = "6C7D8E9F0A1B2C3D4E5F6G7H8I9J0K", dataCadastro = new DateTime(2021, 9, 1), email = "ricardo@example.com" });
                    session.Save(new Usuario { nome = "Beatriz", senha = "9F0A1B2C3D4E5F6G7H8I9J0K1L2M3N", dataCadastro = new DateTime(2013, 7, 18), email = "beatriz@example.com" });

                    transaction.Commit();
                    Console.WriteLine($"Dados cadastrados com sucesso!");
                }

                //READ ALL
                Console.WriteLine("Todos produtos");
                var produtos = session.Query<Produto>().ToList();

                foreach (var p in produtos.ToList())
                {
                    Console.WriteLine($"{p.id,-4} {p.grupo,-7} {p.nome,-20} {p.especificacao,-20} {p.unidade,-10} {p.preco,-7} {p.quantidade,-5}");
                }

                Console.WriteLine("Todos usuários");
                var usuarios = session.Query<Usuario>().ToList();

                foreach (var u in usuarios.ToList())
                {
                    Console.WriteLine($"{u.id,-4} {u.nome,-20} {u.senha,-32} {u.email,-20} {u.dataCadastro,-12}");
                }

                //READ ONE
                //Busca por id
                codigoBusca = 3;
                produto = session.Get<Produto>(codigoBusca);

                if (produto != null)
                {
                    Console.WriteLine($"Busca por id {codigoBusca}");
                    Console.WriteLine($"{produto.id,-4} {produto.grupo,-7} {produto.nome,-20} {produto.especificacao,-20} {produto.unidade,-10} {produto.preco,-7} {produto.quantidade,-5}");
                }
                else
                {
                    Console.WriteLine($"Produto {codigoBusca} não encontrado!");
                }

                //Busca por campo
                campoBusca = "Arroz";
                produto = session.Query<Produto>().FirstOrDefault(p => p.nome == campoBusca);

                if (produto != null)
                {
                    Console.WriteLine($"Busca por campo {campoBusca}");
                    Console.WriteLine($"{produto.id,-4} {produto.grupo,-7} {produto.nome,-20} {produto.especificacao,-20} {produto.unidade,-10} {produto.preco,-7} {produto.quantidade,-5}");
                }
                else
                {
                    Console.WriteLine($"Produto {campoBusca} não encontrado!");
                }

                //Busca por criteria (like, and, or)
                ICriteria criteria = session.CreateCriteria<Produto>();
                criteria.Add(
                    Restrictions.Or(
                        Restrictions.Like("nome", "f%"),
                        Restrictions.Like("nome", "%ão")
                    )
                );
                IList<Produto> listaDeProdutos = criteria.List<Produto>();

                Console.WriteLine($"Busca por criteria");

                foreach (var p in listaDeProdutos)
                {
                    Console.WriteLine($"{produto.id,-4} {produto.grupo,-7} {produto.nome,-20} {produto.especificacao,-20} {produto.unidade,-10} {produto.preco,-7} {produto.quantidade,-5}");
                }

                //UPDATE
                using (ITransaction transaction = session.BeginTransaction())
                {
                    codigoBusca = 1;
                    produto = session.Get<Produto>(codigoBusca);

                    if (produto != null)
                    {
                        produto.nome = "Arroz";
                        produto.preco = 29.99;
                        produto.quantidade = 50;

                        session.Update(produto);
                        transaction.Commit();
                        Console.WriteLine($"Produto {codigoBusca} atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine($"Produto {codigoBusca} não encontrado.");
                    }
                }

                //DELETE
                using (ITransaction transaction = session.BeginTransaction())
                {
                    codigoBusca = 99;
                    produto = session.Get<Produto>(codigoBusca);

                    if (produto != null)
                    {
                        session.Delete(produto);
                        transaction.Commit();
                        Console.WriteLine($"Produto {codigoBusca} deletado!");
                    }
                    else
                    {
                        Console.WriteLine($"Produto {codigoBusca} não encontrado.");
                    }
                }

                session.Close();
                Console.ReadLine();
            }
        }
    }
}
