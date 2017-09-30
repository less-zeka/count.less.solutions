using System.Web.Mvc;
using count.less.solutions.Models.Domain;
using count.less.solutions.Models.ViewModels;
using NHibernate;

namespace count.less.solutions.Controllers
{
    public class HomeController : Controller
    {
		public ActionResult Index()
        {
			using (ISession session = Persistence.NHibertnateSession.OpenSession())
			{
                var model = new IndexViewModel();
                //TODO get a meaningful counter
                model.Counter = session.Get<Models.Domain.Counter>(1);
                return View(model);
			}
        }

		[HttpPost]
		[AllowAnonymous]
        public JsonResult Add(Counter model)
		{
			using (ISession session = Persistence.NHibertnateSession.OpenSession())
			{
			    using (ITransaction transaction = session.BeginTransaction())
			    {
			        var counterInDb = session.Get<Counter>(model.Id);
			        counterInDb.Add();
			        session.SaveOrUpdate(counterInDb);
			        transaction.Commit();
			        return Json(counterInDb);
                }
			}
		}

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Minus(Counter model)
        {
            using (ISession session = Persistence.NHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var counterInDb = session.Get<Counter>(model.Id);
                    counterInDb.Minus();
                    session.SaveOrUpdate(counterInDb);
                    transaction.Commit();
                    return Json(counterInDb);
                }
            }
        }

    }
}
