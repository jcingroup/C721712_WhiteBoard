using OutWeb.Entities;
using OutWeb.Models.Manage.ImgModels;
using OutWeb.Provider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OutWeb.Modules.Manage
{
    public class ImgModule
    {
        private WBDBEntities m_DB = new WBDBEntities();

        private WBDBEntities DB
        { get { return this.m_DB; } set { this.m_DB = value; } }

        /// <summary>
        /// 儲存圖片
        /// </summary>
        /// <param name="model"></param>
        public void SaveImgs(ImagesModel model)
        {
            var otherImgs = this.DB.WBPIC.Where(o => o.MAP_AC_NM == model.ActionName && o.UP_MODE == model.UploadType.ToString()).ToList();

            if (model.ActionName.StartsWith("School"))
            {
                //刪除舊圖
                foreach (var img in otherImgs)
                {
                    var delImg = this.DB.WBPIC.Where(o => o.MAP_NEWS_ID == model.ID && o.UP_MODE == model.UploadType).FirstOrDefault();
                    if (delImg != null)
                        this.DB.WBPIC.Remove(delImg);
                    this.DB.SaveChanges();
                }
            }
            else
            {
                //刪除舊圖(Banner)
                foreach (var img in otherImgs)
                {
                    var delBannerImg = this.DB.WBPIC.Where(o => o.MAP_NEWS_ID == model.ID).FirstOrDefault();
                    if (delBannerImg != null)
                        this.DB.WBPIC.Remove(delBannerImg);
                    this.DB.SaveChanges();
                }
            }
            //存檔
            foreach (var img in model.MemberData)
            {
                int sq = model.MemberData.IndexOf(img);
                WBPIC pic = new WBPIC()
                {
                    IMG_NM = img.FileName,
                    MAP_AC_NM = model.ActionName,
                    UP_MODE = model.UploadType ?? "",
                    IMG_URL = img.FileUrl,
                    IMG_LINK = img.FileUrl,
                    FILE_PATH = img.FilePath,
                    SR_SQ = sq,
                    UP_DT = DateTime.UtcNow.AddHours(8),
                    UP_USR_ID = UserProvider.Instance.User.ID
                };

                if (model.ActionName.StartsWith("School"))
                    pic.MAP_NEWS_ID = model.ID;
                else
                    pic.MAP_NEWS_ID = model.ID;

                this.DB.WBPIC.Add(pic);
                this.DB.SaveChanges();
                img.ID = pic.ID;
            }
        }

        /// <summary>
        /// 取得圖片
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="actionName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<MemberViewModel> GetPreviewImg(int ID, string actionName, string actionMode)
        {
            ImagesModel imgModel = new ImagesModel();
            imgModel.ID = ID;
            imgModel.ActionName = actionName;
            imgModel.UploadType = actionMode;

            List<MemberViewModel> imgList = new List<MemberViewModel>();
            if (actionName.StartsWith("School"))
            {
                imgList = this.DB.WBPIC
                        .Where(o => o.MAP_NEWS_ID == ID && o.MAP_AC_NM.StartsWith("School") && o.UP_MODE == actionMode)
                        .Select(s => new MemberViewModel()
                        {
                            ID = s.ID,
                            FileName = s.IMG_NM,
                            FileUrl = s.IMG_LINK,
                            FilePath = s.FILE_PATH
                        })
                        .ToList();
            }
            else
            {
                imgList = this.DB.WBPIC
                                   .Where(o => o.MAP_NEWS_ID == ID && o.MAP_AC_NM.StartsWith("Banner"))
                                   .Select(s => new MemberViewModel()
                                   {
                                       ID = s.ID,
                                       FileName = s.IMG_NM,
                                       FileUrl = s.IMG_LINK,
                                       FilePath = s.FILE_PATH
                                   })
                                   .ToList();
            }
            return imgList;
        }

        /// <summary>
        /// 刪除圖片
        /// </summary>
        /// <param name="imgID"></param>
        public void DeleteImg(int imgID)
        {
            var img = this.DB.WBPIC.Where(o => o.ID == imgID).First();
            this.DB.WBPIC.Remove(img);
            this.DB.SaveChanges();
        }
    }
}