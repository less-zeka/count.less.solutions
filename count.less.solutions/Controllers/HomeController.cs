using System.Web.Mvc;
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
                model.Counter = session.Get<Models.Domain.Counter>(0);
                return View(model);
			}
        }

		[HttpPost]
		[AllowAnonymous]
        public JsonResult Add(IndexViewModel model)
		{
			using (ISession session = Persistence.NHibertnateSession.OpenSession())
			{
                model.Counter.Add();
                session.Merge(model);
				return Json(model);
			}

		} 
    }
}
