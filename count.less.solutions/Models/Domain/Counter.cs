using System;

namespace count.less.solutions.Models.Domain
{
    public class Counter : IHaveAuditInformation
    {
        public Counter()
        {
        }

        public static Counter GetUserCounter(){
            
            return new Counter();
        }

        public virtual int Id { get; set; }

		public virtual int Count
		{
			get;
		    protected set;
		}

        public virtual void Add(){
            Count++;
        }

        public virtual void Minus()
        {
            Count--;
        }

        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
