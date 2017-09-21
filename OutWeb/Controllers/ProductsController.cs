using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ProductsController()
        {
            ViewBag.IsFirstPage = false;
        }

        public ActionResult Index()
        {
            return View("List");
        }

        // 套程式-產品介紹
        // 列表
        public ActionResult List()
        {
            return View();
        }

        // 內容
        public ActionResult Content()
        {
            return View();
        }
    }
}