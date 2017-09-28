using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using count.less.solutions.Persistence;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace count.less.solutions
{
    public class Global : HttpApplication
    {
        //TODO don't think this is smart, but I want to get nhibernate going for now
        private static ISessionFactory _sessionFactory;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //TODO only once!
            //CreateDatabase();
        }

        private void CreateDatabase()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["countDbConnectionString"].ConnectionString;
            CreateDatabase(connectionString);
        }
        
		private void CreateDatabase(string connectionString)
		{
			var configuration = Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<CounterMap>())
				.BuildConfiguration();

			var exporter = new SchemaExport(configuration);
			exporter.Execute(true, true, false);

			_sessionFactory = configuration.BuildSessionFactory();
		}
    }
}
