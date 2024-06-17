using System.Web.Mvc;
using ClassLibrary;

namespace Framework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["language"] = "uk/eng";
            Session["crumbs"] = new SessionDemoModel
            {
                IntSessionItem = 411,
                StringSessionItem = "Hello, World!"
            };
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}