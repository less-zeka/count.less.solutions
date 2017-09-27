using System;
namespace count.less.solutions.Models.Domain
{
    public class Counter
    {
        protected Counter()
        {
            Count = 0;
        }

        public static Counter GetUserCounter(){
            
            return new Counter();
        }

        public virtual Guid Id { get; set; }

		public virtual int Count
		{
			get;
			set;
		}

        public virtual void Add(){
            Count++;
        }

        public virtual void Reset(){
            Count = 0;
        }
    }
}
