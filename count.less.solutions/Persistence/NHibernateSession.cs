using System.Web.Configuration;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace count.less.solutions.Persistence
{
    public class NHibertnateSession
	{
		public static ISession OpenSession()
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["countDbConnectionString"].ConnectionString;

			var configuration = FluentNHibernate.Cfg.Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<CounterMap>())
				.BuildConfiguration();

			ISessionFactory sessionFactory = configuration.BuildSessionFactory();
			return sessionFactory.OpenSession();
		}
	}
}
