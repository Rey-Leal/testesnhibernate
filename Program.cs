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

namespace TestesNHibernate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //String de conexao com o banco de dados
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

            using (var session = sessionFactory.OpenSession())
            {
                //CREATE
                var produto = new Produto { nome = "Arroz", preco = 25.56, quantidade = 1 };
                session.Save(produto);
                produto = new Produto { nome = "Feijão", preco = 12.90, quantidade = 3 };
                session.Save(produto);
                produto = new Produto { nome = "Café", preco = 16.33, quantidade = 4 };
                session.Save(produto);
                produto = new Produto { nome = "Leite", preco = 4.99, quantidade = 7 };
                session.Save(produto);

                //READ
                var produtos = session.Query<Produto>().ToList();
                foreach (var p in produtos.ToList())
                {
                    Console.WriteLine(p.nome + "\t" + p.preco + "\t" + p.quantidade);
                }

                session.Close();
                Console.ReadLine();
            }
        }
    }
}
