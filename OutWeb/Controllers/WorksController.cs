using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class WorksController : Controller
    {
        public WorksController()
        {
            ViewBag.IsFirstPage = false;
        }

        public ActionResult Index()
        {
            return View("List");
        }

        // 套程式-案例分享
        // 列表頁
        public ActionResult List()
        {
            return View();
        }

        // 內容頁
        public ActionResult Content()
        {
            return View();
        }
    }
}