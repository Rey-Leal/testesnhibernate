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
                    session.Save(new Produto { nome = "Arroz", preco = 18.00, quantidade = 2 });
                    session.Save(new Produto { nome = "Feijão", preco = 9.50, quantidade = 4 });
                    session.Save(new Produto { nome = "Óleo de Cozinha", preco = 7.80, quantidade = 2 });
                    session.Save(new Produto { nome = "Sal", preco = 2.00, quantidade = 1 });
                    session.Save(new Produto { nome = "Açúcar", preco = 5.50, quantidade = 3 });
                    session.Save(new Produto { nome = "Café", preco = 15.90, quantidade = 1 });
                    session.Save(new Produto { nome = "Leite", preco = 4.30, quantidade = 6 });
                    session.Save(new Produto { nome = "Farinha de Trigo", preco = 3.50, quantidade = 2 });
                    session.Save(new Produto { nome = "Macarrão", preco = 6.20, quantidade = 5 });
                    session.Save(new Produto { nome = "Molho de Tomate", preco = 3.20, quantidade = 3 });
                    session.Save(new Produto { nome = "Sabão em Pó", preco = 10.50, quantidade = 1 });
                    session.Save(new Produto { nome = "Detergente", preco = 2.50, quantidade = 2 });
                    session.Save(new Produto { nome = "Papel Higiênico", preco = 15.00, quantidade = 1 });
                    session.Save(new Produto { nome = "Carne Bovina", preco = 45.00, quantidade = 2 });
                    session.Save(new Produto { nome = "Frango", preco = 20.00, quantidade = 3 });
                    session.Save(new Produto { nome = "Ovos", preco = 12.00, quantidade = 2 });
                    session.Save(new Produto { nome = "Manteiga", preco = 5.00, quantidade = 1 });
                    session.Save(new Produto { nome = "Queijo", preco = 30.00, quantidade = 1 });
                    session.Save(new Produto { nome = "Batata", preco = 3.00, quantidade = 5 });
                    session.Save(new Produto { nome = "Banana", preco = 4.00, quantidade = 6 });

                    session.Save(new Usuario { nome = "Lucas", senha = "1A3B5C7D9E0F1A2B3C4D5E6F7A8B9C", dataCadastro = new DateTime(2015, 4, 15) });
                    session.Save(new Usuario { nome = "Julia", senha = "0F1A2B3C4D5E6F7A8B9C0D1A2B3C4D", dataCadastro = new DateTime(2012, 6, 27) });
                    session.Save(new Usuario { nome = "Carlos", senha = "9E8F7D6C5B4A3E2F1D0C9B8A7E6D5C", dataCadastro = new DateTime(2017, 3, 9) });
                    session.Save(new Usuario { nome = "Sofia", senha = "2F3E4D5C6B7A8E9F0C1D2B3A4C5D6E", dataCadastro = new DateTime(2020, 1, 23) });
                    session.Save(new Usuario { nome = "Pedro", senha = "4E5F6A7B8C9D0E1F2A3B4C5D6E7F8G", dataCadastro = new DateTime(2011, 8, 14) });
                    session.Save(new Usuario { nome = "Laura", senha = "7F8E9D0C1A2B3C4D5E6F7G8H9I0J1K", dataCadastro = new DateTime(2019, 1, 5) });
                    session.Save(new Usuario { nome = "Mateus", senha = "0C1D2E3F4A5B6C7D8E9F0A1B2C3D4E", dataCadastro = new DateTime(2016, 2, 11) });
                    session.Save(new Usuario { nome = "Fabia", senha = "3A4B5C6D7E8F9G0H1I2J3K4L5M6N7O", dataCadastro = new DateTime(2018, 5, 19) });
                    session.Save(new Usuario { nome = "Ricardo", senha = "6C7D8E9F0A1B2C3D4E5F6G7H8I9J0K", dataCadastro = new DateTime(2021, 9, 1) });
                    session.Save(new Usuario { nome = "Beatriz", senha = "9F0A1B2C3D4E5F6G7H8I9J0K1L2M3N", dataCadastro = new DateTime(2013, 7, 18) });


                    transaction.Commit();
                    Console.WriteLine($"Dados cadastrados com sucesso!");
                }

                //READ ALL
                Console.WriteLine("Todos produtos");
                var produtos = session.Query<Produto>().ToList();

                foreach (var p in produtos.ToList())
                {
                    Console.WriteLine($"{p.id,-4} {p.nome,-20} {p.preco,-7} {p.quantidade,-5}");
                }

                Console.WriteLine("Todos usuários");
                var usuarios = session.Query<Usuario>().ToList();

                foreach (var u in usuarios.ToList())
                {
                    Console.WriteLine($"{u.id,-4} {u.nome,-20} {u.senha,-35} {u.dataCadastro,-12}");
                }

                //READ ONE
                //Busca por id
                codigoBusca = 3;
                produto = session.Get<Produto>(codigoBusca);

                if (produto != null)
                {
                    Console.WriteLine($"Busca por id {codigoBusca}");
                    Console.WriteLine($"{produto.id,-4} {produto.nome,-20} {produto.preco,-7} {produto.quantidade,-5}");
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
                    Console.WriteLine($"{produto.id,-4} {produto.nome,-20} {produto.preco,-7} {produto.quantidade,-5}");
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
                    Console.WriteLine($"{p.id,-4} {p.nome,-20} {p.preco,-7} {p.quantidade,-5}");
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
                    codigoBusca = 2;
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
