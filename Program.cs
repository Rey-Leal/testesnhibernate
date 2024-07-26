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
                //CREATE
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(new Produto { nome = "Arroz", preco = 25.56, quantidade = 1 });
                    session.Save(new Produto { nome = "Feijão", preco = 12.90, quantidade = 3 });
                    session.Save(new Produto { nome = "Café", preco = 16.33, quantidade = 4 });
                    session.Save(new Produto { nome = "Leite", preco = 4.99, quantidade = 7 });
                    transaction.Commit();
                }

                //READ ALL
                Console.WriteLine("Todos registros");
                var produtos = session.Query<Produto>().ToList();
                foreach (var p in produtos.ToList())
                {
                    Console.WriteLine($"{p.id}\t{p.nome}\t{p.preco}\t{p.quantidade}");
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

                //UPDATE
                using (ITransaction transaction = session.BeginTransaction())
                {
                    codigoBusca = 1;
                    produto = session.Get<Produto>(codigoBusca);

                    if (produto != null)
                    {
                        produto.nome = "Macarrão";
                        produto.preco = 9.99;
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
