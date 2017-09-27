using count.less.solutions.Models.Domain;
using FluentNHibernate.Mapping;

namespace count.less.solutions.Persistence
{
	public class CounterMap : ClassMap<Counter>
	{
		public CounterMap()
		{
            Id(c => c.Id);
            Map(c => c.Count);
		}
	}
}
