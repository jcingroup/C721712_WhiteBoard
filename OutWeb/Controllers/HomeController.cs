using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.IsFirstPage = false;
        }

        // all 靜態
        public ActionResult Index()
        {
            ViewBag.IsFirstPage = true;
            return View();            
        }

        // 公司簡介
        public ActionResult AboutUs()
        {
            return View();
        }
    }
}