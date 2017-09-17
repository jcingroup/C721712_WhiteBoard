using Newtonsoft.Json;
using OutWeb.ActionFilter;
using OutWeb.Authorize;
using OutWeb.Enums;
using OutWeb.Models.Manage.ImgModels;
using OutWeb.Modules.Manage;
using OutWeb.Repositories;
using OutWeb.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    [ErrorHandler]
    public class ImgHandlerController : Controller
    {
        public void CreateSchoolData(ImagesModel vm, List<HttpPostedFileBase> images, int schoolTypeValue)
        {
            FormCollection form = new FormCollection();
            form.Add("type", schoolTypeValue.ToString());
            form.Add("status", "Y");
            string langCode = PublicMethodRepository.CurrentLanguageCode;
            Language language = PublicMethodRepository.GetLanguageEnumByCode(langCode);
            ListModuleService module = ListFactoryService.Create(ListMethodType.NEWS);
            int identityId = module.DoSaveData(form, language);
            vm.ID = identityId;
        }

        public void CreateBannerData(ImagesModel vm, List<HttpPostedFileBase> images)
        {
            FormCollection form = new FormCollection();
            string langCode = PublicMethodRepository.CurrentLanguageCode;
            Language language = PublicMethodRepository.GetLanguageEnumByCode(langCode);
            ListModuleService module = ListFactoryService.Create(ListMethodType.BANNER);
            int identityId = module.DoSaveData(form, language);
            vm.ID = identityId;
        }

        /// <summary>
        /// 表單提交
        /// </summary>
        /// <param name="act"></param>
        /// <param name="vm"></param>
        /// <param name="myFile"></param>
        /// <returns></returns>
        [CheckFolder]
        [HttpPost]
        public string Index(string act, ImagesModel vm, List<HttpPostedFileBase> images)
        {
            this.UploadPhoto(act, vm, images);
            if (act == "post")
                this.SaveImagesToDB(vm);

            var jsonStrModel = JsonConvert.SerializeObject(vm);
            return jsonStrModel;
        }

        /// <summary>
        /// 寫Log查看表單post的結果
        /// </summary>
        /// <param name="vm"></param>
        private void SaveImagesToDB(ImagesModel vm)
        {
            ImgModule imgModule = new ImgModule();
            imgModule.SaveImgs(vm);
        }

        /// <summary>
        /// 上傳照片
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="myFile"></param>
        private void UploadPhoto(string uploadType, ImagesModel vm, List<HttpPostedFileBase> images)
        {
            string serverMapPath = string.Empty;

            if (uploadType == "upload")
                serverMapPath = "~/Content/Upload/Manage/Images/Temp/";
            else
                serverMapPath = "~/Content/Upload/Manage/Images/";

            foreach (var m in vm.MemberData)
                m.FilePath = Server.MapPath(serverMapPath + m.FileName);
            int imgMaxWidth = 0;
            int imgMaxHeight = 0;
            if (vm.ActionName.StartsWith("News"))
            {
                imgMaxWidth = 400;
                imgMaxHeight = 300;
            }
            else if (vm.ActionName.StartsWith("Banner"))
            {
                imgMaxWidth = 1920;
                imgMaxHeight = 400;
            }
            if (images != null && images.Count > 0)
            {
                for (int i = 0; i < images.Count; i++)
                {
                    string strFileName = Guid.NewGuid().ToString() + Path.GetExtension(images[i].FileName);
                    string strFilePath = Server.MapPath(serverMapPath + strFileName);
                    if (uploadType == "post")
                    {
                        //reize
                        ImageRepository imageRepository = new ImageRepository(imgMaxWidth, imgMaxHeight, strFilePath);
                        Image img = Image.FromStream(images[i].InputStream);
                        imageRepository.SaveAdjustImageSize((Bitmap)img);
                    }
                    else
                    {
                        images[i].SaveAs(strFilePath);
                    }
                    vm.MemberData.Add(new MemberViewModel()
                    {
                        FilePath = strFilePath,
                        FileName = strFileName,
                        FileUrl = serverMapPath.Substring(2, serverMapPath.Length - 2) + strFileName,
                    });
                }
            }
        }

        /// <summary>
        /// 刪除圖檔
        /// </summary>
        /// <param name="imgID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteImg(int imgID)
        {
            bool success = true;
            string messages = string.Empty;
            JsonResult resultJson = new JsonResult();
            ImgModule imgModule = new ImgModule();
            try
            {
                imgModule.DeleteImg(imgID);
            }
            catch (Exception ex)
            {
                messages = string.Format("圖片刪除失敗 ：{0}", ex.Message);
                resultJson = Json(new { success = success, messages = messages });
                return resultJson;
            }
            resultJson = Json(new { success = success, messages = "success" });
            return resultJson;
        }
    }
}