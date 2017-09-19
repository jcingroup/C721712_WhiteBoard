﻿using OutWeb.Authorize;
using OutWeb.Entities;
using OutWeb.Enums;
using OutWeb.Exceptions;
using OutWeb.Models.Manage.ManageNewsModels;
using OutWeb.Models.Manage.ProductKindModels;
using OutWeb.Models.Manage.ProductModels;
using OutWeb.Modules.Manage;
using OutWeb.Repositories;
using OutWeb.Service;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    [Auth]
    [ErrorHandler]
    public class _SysAdmController : Controller
    {

        public _SysAdmController()
        {
            ViewBag.IsFirstPage = false;
        }


        #region 產品管理 分類
        /// <summary>
        /// 產品分類若停用判斷是否已有產品使用該分類
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckProductStatusHideHasOnUsed(int? ID)
        {
            bool success = true;
            JsonResult resultJson = new JsonResult();
            string messages = string.Empty;

            WBDBEntities Db = new WBDBEntities();
            int count = Db.WBPRODUCT.Where(o => o.MAP_PRODUCT_TP_ID == ID && o.DIS_FRONT_ST).Count();
            if (count > 0)
            {
                success = false;
                messages = "「尚有產品被歸類在此分類且狀態為已顯示於前台，故無法停用。」";
            }
            else
                success = true;
            resultJson = Json(new { success = success, messages = messages });
            return resultJson;
        }



        public ActionResult ProductKindList(int? page, string qry, string sort, string status)

        {
            Language language = PublicMethodRepository.CurrentLanguageEnum;
            ProductKindListViewModel model = new ProductKindListViewModel();
            model.Filter.CurrentPage = page ?? 1;
            model.Filter.QueryString = qry ?? string.Empty;
            model.Filter.SortColumn = sort ?? string.Empty;
            model.Filter.Status = status ?? string.Empty;
            ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.PRODUCTKIND);
            model.Result = (module.DoGetList(model.Filter, language) as ProductKindListResultModel);
            return View(model);
        }

        [HttpGet]
        public ActionResult ProductKindAdd()
        {
            return View(new ProductKindDetailsDataModel());
        }

        [HttpPost]
        public ActionResult ProductKindAdd(FormCollection form)
        {
            string langCode = form["lang"] ?? PublicMethodRepository.CurrentLanguageCode;
            Language language = PublicMethodRepository.GetLanguageEnumByCode(langCode);
            ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.PRODUCTKIND);
            int identityId = module.DoSaveData(form, language);
            return RedirectToAction("ProductKindEdit", "_SysAdm", new { ID = identityId });
        }

        [HttpGet]
        public ActionResult ProductKindEdit(int? ID)
        {
            if (!ID.HasValue)
                return RedirectToAction("ProductKind");
            ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.PRODUCTKIND);
            ProductKindDetailsDataModel model = (module.DoGetDetailsByID((int)ID) as ProductKindDetailsDataModel);
            if (model == null)
                return RedirectToAction("Login", "SignIn");
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ProductKindEdit(FormCollection form)
        {
            string langCode = form["lang"] ?? PublicMethodRepository.CurrentLanguageCode;
            Language language = PublicMethodRepository.GetLanguageEnumByCode(langCode);
            int? ID = Convert.ToInt32(form["ProductKindID"]);
            ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.PRODUCTKIND);
            int identityId = module.DoSaveData(form, language, ID);
            ProductKindDetailsDataModel model = (module.DoGetDetailsByID((int)ID) as ProductKindDetailsDataModel);
            return View(model);
        }

        [HttpPost]
        public JsonResult ProductKindDelete(int? ID)
        {
            bool success = true;
            JsonResult resultJson = new JsonResult();
            string messages = string.Empty;
            try
            {
                ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.PRODUCTKIND);
                module.DoDeleteByID((int)ID);
                messages = "刪除成功";
                resultJson = Json(new { success = success, messages = messages });
            }
            catch (ProductKindRelationExcption exRe)
            {
                success = false;
                resultJson = Json(new { success = success, messages = exRe.Message });
            }
            catch (Exception ex)
            {
                success = false;
                messages = ex.Message;
            }
            return resultJson;
        }

        #endregion 產品管理 分類

        #region 產品資料

        /// 代理語言產品資料
        public ActionResult ProductList(int? page, string qry, string sort, int? type, string status, string lang)
        {
            Language language = PublicMethodRepository.CurrentLanguageEnum;
            ProductListViewModel model = new ProductListViewModel();
            model.Filter.CurrentPage = page ?? 1;
            model.Filter.QueryString = qry ?? string.Empty;
            model.Filter.SortColumn = sort ?? string.Empty;
            model.Filter.TypeID = type;
            model.Filter.Status = status ?? string.Empty;
            ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.PRODUCT);
            model.Result = (module.DoGetList(model.Filter, language) as ProductListResultModel);
            ProductKindModule typeModule = new ProductKindModule();
            //產品分類下拉選單
            ViewBag.TypeList = typeModule.CreateProductKindDropList(model.Filter.TypeID);
            return View(model);
        }

        [HttpGet]
        public ActionResult ProductDataAdd()
        {
            ProductKindModule typeModule = new ProductKindModule();
            //產品分類下拉選單
            SelectList typeList = typeModule.CreateProductKindDropList(null, false);
            ViewBag.TypeList = typeList;
            return View(new ProductDetailsDataModel());
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ProductDataAdd(FormCollection form)
        {
            string langCode = form["lang"] ?? PublicMethodRepository.CurrentLanguageCode;
            Language language = PublicMethodRepository.GetLanguageEnumByCode(langCode);
            ListModuleService module = ListFactoryService.Create(ListMethodType.PRODUCT);
            int identityId = module.DoSaveData(form, language);
            return RedirectToAction("ProductDataEdit", "_SysAdm", new { ID = identityId });
        }

        [HttpGet]
        public ActionResult ProductDataEdit(int? ID)
        {
            if (!ID.HasValue)
                return RedirectToAction("ProductList");
            ListModuleService module = ListFactoryService.Create(ListMethodType.PRODUCT);
            ProductDetailsDataModel model = (module.DoGetDetailsByID((int)ID) as ProductDetailsDataModel);
            if (model == null)
                return RedirectToAction("Login", "SignIn");
            //取圖檔
            ImgModule imgModule = new ImgModule();
            model.ImagesData = imgModule.GetPreviewImg(model.ID, "Product", "R").FirstOrDefault();
            model.OtherImagesData = imgModule.GetPreviewImg(model.ID, "Product", "O");
            //產品分類下拉選單
            ProductKindModule typeModule = new ProductKindModule();
            SelectList typeList = typeModule.CreateProductKindDropList(model.TypeID, false);
            ViewBag.TypeList = typeList;
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ProductDataEdit(FormCollection form)
        {
            string langCode = form["lang"] ?? PublicMethodRepository.CurrentLanguageCode;
            Language language = PublicMethodRepository.GetLanguageEnumByCode(langCode);
            int? ID = Convert.ToInt32(form["ProductID"]);
            ListModuleService module = ListFactoryService.Create(ListMethodType.PRODUCT);
            int identityId = module.DoSaveData(form, language, ID);
            ProductDetailsDataModel model = (module.DoGetDetailsByID((int)ID) as ProductDetailsDataModel);
            //取圖檔
            ImgModule imgModule = new ImgModule();
            model.ImagesData = imgModule.GetPreviewImg(model.ID, "Product", "R").FirstOrDefault();
            model.OtherImagesData = imgModule.GetPreviewImg(model.ID, "Product", "O");
            //產品分類下拉選單
            ProductKindModule typeModule = new ProductKindModule();
            SelectList typeList = typeModule.CreateProductKindDropList(model.TypeID, false);
            ViewBag.TypeList = typeList;
            return View(model);
        }

        [HttpPost]
        public JsonResult ProductDataEditDelete(int? ID)
        {
            bool success = true;
            string messages = string.Empty;
            try
            {
                ListModuleService module = ListFactoryService.Create(ListMethodType.PRODUCT);
                module.DoDeleteByID((int)ID);
                messages = "刪除成功";
            }
            catch (Exception ex)
            {
                success = false;
                messages = ex.Message;
            }
            var resultJson = Json(new { success = success, messages = messages });
            return resultJson;
        }

        #endregion 產品資料

        #region 最新消息=消息報報

        [HttpGet]
        public ActionResult NewsList(int? page, string qry, string sort, string fSt, string hSt, string pDate, string lang)
        {
            Language language = PublicMethodRepository.CurrentLanguageEnum;
            NewsListViewModel model = new NewsListViewModel();
            model.Filter.CurrentPage = page ?? 1;
            model.Filter.QueryString = qry ?? string.Empty;
            model.Filter.SortColumn = sort ?? string.Empty;
            model.Filter.DisplayForFrontEnd = fSt ?? string.Empty;
            model.Filter.DisplayForHomePage = hSt ?? string.Empty;
            model.Filter.PublishDate = pDate;
            ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.NEWS);
            model.Result = (module.DoGetList(model.Filter, language) as NewsListResultModel);
            return View(model);
        }

        [HttpGet]
        public ActionResult NewsAdd()
        {
            return View(new NewsDetailsDataModel());
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult NewsAdd(FormCollection form)
        {
            string langCode = form["lang"] ?? PublicMethodRepository.CurrentLanguageCode;
            Language language = PublicMethodRepository.GetLanguageEnumByCode(langCode);
            ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.NEWS);
            int identityId = module.DoSaveData(form, language);
            return RedirectToAction("NewsEdit", "_SysAdm", new { ID = identityId });
        }

        [HttpGet]
        public ActionResult NewsEdit(int? ID)
        {
            if (!ID.HasValue)
                return RedirectToAction("News");
            ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.NEWS);
            NewsDetailsDataModel model = (module.DoGetDetailsByID((int)ID) as NewsDetailsDataModel);
            if (model == null)
                return RedirectToAction("Login", "SignIn");
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult NewsEdit(FormCollection form)
        {
            string langCode = form["lang"] ?? PublicMethodRepository.CurrentLanguageCode;
            Language language = PublicMethodRepository.GetLanguageEnumByCode(langCode);
            int? ID = Convert.ToInt32(form["newsID"]);
            ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.NEWS);
            int identityId = module.DoSaveData(form, language, ID);
            NewsDetailsDataModel model = (module.DoGetDetailsByID((int)ID) as NewsDetailsDataModel);
            return View(model);
        }

        [HttpPost]
        public JsonResult NewsDelete(int? ID)
        {
            bool success = true;
            string messages = string.Empty;
            try
            {
                ListModuleService module = ListFactoryService.Create(Enums.ListMethodType.NEWS);
                module.DoDeleteByID((int)ID);
                messages = "刪除成功";
            }
            catch (Exception ex)
            {
                success = false;
                messages = ex.Message;
            }
            var resultJson = Json(new { success = success, messages = messages });
            return resultJson;
        }

        #endregion 最新消息=消息報報

        #region 案例分享

        public ActionResult WorksList()
        {
            return View();
        }

        public ActionResult WorksData()
        {
            return View();
        }

        #endregion 案例分享

        #region 代理商

        public ActionResult Agents()
        {
            return View();
        }

        #endregion 代理商

        #region 修改密碼

        public ActionResult ChangePW()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePW(string now_pwd, string new_pwd, string chk_new_pwd)
        {
            return View();
        }

        #endregion 修改密碼
    }
}