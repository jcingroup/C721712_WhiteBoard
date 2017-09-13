using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class AgentsController : Controller
    {
        // GET: Agents
        public ActionResult Index()
        {
            return View();
        }

        // 套程式-代理商
        public ActionResult Agents()
        {
            return View();
        }
    }
}