using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    using EfSearchModel.Model;
    using Models;
    using EfSearchModel;
    public class HomeController : Controller
    {
        public ActionResult Index(QueryModel model)
        {
            using(var db=new DbEntities())
            {
                var list = db.Users.Where(model).ToList();
                return View(list);
            }
        }



        public ActionResult About()
        {
            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var key in filterContext.HttpContext.Request.Form.AllKeys)
            {
                var key2 = key.Split(")]}".ToArray()).Last();
                if (!ViewData.ContainsKey(key2))
                {
                    ViewData.Add(key2, filterContext.HttpContext.Request.Form[key]);
                }
            }
        }
    }
}
