﻿using System.Web.Configuration;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Event;

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
                //.ExposeConfiguration(c => c.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { new AuditEventListener() })
			    .ExposeConfiguration(val =>
			    {
			        val.AppendListeners(ListenerType.PreUpdate, new IPreUpdateEventListener[] { new AuditEventListener() });
			        val.AppendListeners(ListenerType.PreInsert, new IPreInsertEventListener[] { new AuditEventListener() });
			    })
                .BuildConfiguration();

			ISessionFactory sessionFactory = configuration.BuildSessionFactory();
			return sessionFactory.OpenSession();
		}
	}
}
