using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace TestesNHibernate.Database
{
    public class Conexao
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory ConectarSessionFactory()
        {
            if (_sessionFactory == null)
            {
                _sessionFactory = CriarSessionFactory();
            }

            return _sessionFactory;
        }

        private static ISessionFactory CriarSessionFactory()
        {
            var connStr = "Data Source=REINALDO-PC\\SQLEXPRESS;Initial Catalog=Testes;User Id=;Password=;Integrated Security=SSPI;TrustServerCertificate=True;";
            var config = new Configuration();
            config.DataBaseIntegration(d =>
            {
                d.ConnectionString = connStr;
                d.Dialect<MsSql2012Dialect>();
                d.Driver<SqlClientDriver>();
            });

            config.AddAssembly(Assembly.GetExecutingAssembly());

            return config.BuildSessionFactory();
        }
    }
}
