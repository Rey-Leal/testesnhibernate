using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Reflection;
using TestesNHibernate.Models;
using NHibernate;
using NHibernate.Criterion;

namespace TestesNHibernate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Conexao
            string connStr = "Data Source=REINALDO-PC\\SQLEXPRESS;Initial Catalog=Testes;User Id=;Password=;";
            var config = new Configuration();
            config.DataBaseIntegration(d =>
            {
                d.ConnectionString = connStr;
                d.Dialect<MsSql2012Dialect>();
                d.Driver<SqlClientDriver>();
            });

            config.AddAssembly(Assembly.GetExecutingAssembly());
            var sessionFactory = config.BuildSessionFactory();

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
                    session.Save(new Produto { nome = "Grão", preco = 25.56, quantidade = 1 });
                    session.Save(new Produto { nome = "Feijão", preco = 12.90, quantidade = 3 });
                    session.Save(new Produto { nome = "Café", preco = 16.33, quantidade = 4 });
                    session.Save(new Produto { nome = "Leite", preco = 4.99, quantidade = 7 });
                    session.Save(new Produto { nome = "Macarrão", preco = 6.62, quantidade = 9 });

                    session.Save(new Usuario { nome = "Rey", senha = "D2E6A94B8DAB5A9EBA64F71294EAF59D", dataCadastro = DateTime.Now });
                    session.Save(new Usuario { nome = "Ana", senha = "0D5CC8A8A6A9F85F0BAE5EBA92F1959E", dataCadastro = DateTime.Now });
                    session.Save(new Usuario { nome = "João", senha = "EC50A6C5EBFA8D3FF2BBA645F60A1B39", dataCadastro = DateTime.Now });
                    session.Save(new Usuario { nome = "Maria", senha = "A1A09D5BFDFFC3C2B7A0A5914C30D842", dataCadastro = DateTime.Now });

                    transaction.Commit();
                    Console.WriteLine($"Dados cadastrados com sucesso!");
                }

                //READ ALL
                Console.WriteLine("Todos produtos");
                var produtos = session.Query<Produto>().ToList();

                foreach (var p in produtos.ToList())
                {
                    Console.WriteLine($"{p.id}\t{p.nome}\t{p.preco}\t{p.quantidade}");
                }

                Console.WriteLine("Todos usuários");
                var usuarios = session.Query<Usuario>().ToList();

                foreach (var u in usuarios.ToList())
                {
                    Console.WriteLine($"{u.id}\t{u.nome}\t{u.senha}\t{u.dataCadastro}");
                }

                //READ ONE
                //Busca por id
                codigoBusca = 3;
                produto = session.Get<Produto>(codigoBusca);

                if (produto != null)
                {
                    Console.WriteLine($"Busca por id {codigoBusca}");
                    Console.WriteLine($"{produto.id}\t{produto.nome}\t{produto.preco}\t{produto.quantidade}");
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
                    Console.WriteLine($"{produto.id}\t{produto.nome}\t{produto.preco}\t{produto.quantidade}");
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
                    Console.WriteLine($"{p.id}\t{p.nome}\t{p.preco}\t{p.quantidade}");
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
