using System.Web.Mvc;

namespace DutyHours.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Duty Hours | Home";
            return View();
        }

        public ActionResult ResidentCalendar()
        {
            ViewBag.Title = "Duty Hours | Resident Calendar";
            return View("ResidentCalendar");
        }

        public ActionResult Analysis()
        {
            ViewBag.Title = "Duty Hours | Analysis";
            return View("Analysis");
        }
    }
}
