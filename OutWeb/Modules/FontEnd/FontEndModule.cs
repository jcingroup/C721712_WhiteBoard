//using OutWeb.Entities;
//using OutWeb.Enums;
//using OutWeb.Models;
//using OutWeb.Models.FrontEnd.HomePageModels;
//using OutWeb.Models.FrontEnd.NewsFrontEndModels;
//using OutWeb.Models.FrontEnd.ProductFrontEndModels;
//using OutWeb.Models.Manage.ProductKindModels;
//using OutWeb.Modules.Manage;
//using OutWeb.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;

//namespace GTOverseas.Modules.FontEnd
//{
//    public class FontEndModule : IDisposable
//    {
//        private WBDBEntities m_DB = new WBDBEntities();

//        private WBDBEntities DB
//        { get { return this.m_DB; } set { this.m_DB = value; } }

//        #region 模組化功能

//        /// <summary>
//        /// [前台] 列表分頁處理
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="page"></param>
//        /// <returns></returns>
//        public dynamic ListPageFrontEnd<T>(IEnumerable<T> data, int page, int pageSize, ListMethodType methodType)
//        {
//            int startRow = 0;
//            PaginationResult paginationResult = null;
//            if (pageSize > 0)
//            {
//                //分頁
//                startRow = (page - 1) * pageSize;
//                paginationResult = new PaginationResult()
//                {
//                    CurrentPage = page,
//                    DataCount = data.Count(),
//                    PageSize = pageSize,
//                    FirstPage = 1,
//                    LastPage = data.Count() == 0 ? 1 : Convert.ToInt32(Math.Ceiling((decimal)data.Count() / pageSize))
//                };
//            }

//            dynamic result = null;
//            switch (methodType)
//            {
//                case ListMethodType.NotSet:
//                    break;

//                case ListMethodType.NEWS:
//                    result = new NewsFrontEndListDataModel();
//                    result.Data = data.Skip(startRow).Take(pageSize).ToList();
//                    break;

//                //case ListMethodType.FEEDBACK:
//                //    result = new FeedbackFrontEndListDataModel();
//                //    result.Data = data.Skip(startRow).Take(pageSize).ToList();
//                //    break;

//                case ListMethodType.PRODUCT:
//                    result = new ProductFrontEndListDataModel();
//                    result.Data = data.ToList();
//                    break;

//                default:
//                    break;
//            }
//            result.Pagination = paginationResult;
//            return result;
//        }

//        #endregion 模組化功能

//        #region 前台

//        #region 最新消息

//        /// <summary>
//        /// [前台]取得最新消息列表
//        /// </summary>
//        /// <param name="filterModel"></param>
//        /// <returns></returns>
//        public IEnumerable<NewsFrontEndDataModel> GetNewsListFrontEnd(bool isHomePagel)
//        {
//            int configSetWordLength90 = Convert.ToInt32(PublicMethodRepository.GetConfigAppSetting("DefaultContentWordLength90"));
//            int configSetWordLength284 = Convert.ToInt32(PublicMethodRepository.GetConfigAppSetting("DefaultContentWordLength284"));
//            int configSetPageSize03 = (int)PageSizeConfig.SIZE03;
//            IEnumerable<NewsFrontEndDataModel> data;
//            try
//            {
//                int wordLength = (isHomePagel) ? configSetWordLength90 : configSetWordLength284;
//                data = DB.WBNEWS.Select(s => new NewsFrontEndDataModel()
//                {
//                    ID = s.ID,
//                    SortIndex = s.SR_SQ,
//                    Title = s.NEWS_TITLE,
//                    PartContent = s.NEWS_CONTENT,
//                    PublishDate = s.PUB_DT,
//                    DisplayForFront = (bool)s.DIS_FRONT_ST,
//                    DisplayForHomePage = (bool)s.DIS_HOME_ST,
//                    WordLength = wordLength
//                })
//                 .ToList();
//                //篩選+排序
//                if (isHomePagel)
//                {
//                    data = data
//                   //.Where(s => s.StatusCode == "Y")
//                   .OrderByDescending(o => o.SortIndex)
//                   .ThenByDescending(o => o.PublishDate)
//                   .ToList()
//                   .Take(configSetPageSize03);
//                }
//                else
//                {
//                    data = data
//                         //.Where(s => s.StatusCode == "Y")
//                         .OrderByDescending(o => o.SortIndex)
//                         .ThenByDescending(o => o.PublishDate)
//                         .ToList();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return data;
//        }

//        /// <summary>
//        /// [前台]取得最新消息內文
//        /// </summary>
//        /// <param name="ID"></param>
//        /// <returns></returns>
//        public NewsFrontEndDetalisDataModel GetNewsByIDFrontEnd(int ID)
//        {
//            NewsFrontEndDetalisDataModel data = DB.WBNEWS.Select(s => new NewsFrontEndDetalisDataModel
//            {
//                ID = s.ID,
//                Title = s.NEWS_TITLE,
//                PublishDate = (DateTime)s.PUB_DT,
//                NewsContent = s.NEWS_CONTENT
//            }).Where(w => w.ID == ID).FirstOrDefault();
//            return data;
//        }

//        #endregion 最新消息

//        #region 心得分享

//        /// <summary>
//        /// 依索引值取出對應的心得分享分類描述
//        /// </summary>
//        /// <param name="num"></param>
//        /// <returns></returns>
//        public string GetFeedbackDescriptionByNumber(int num)
//        {
//            FeedbackType feedbackType = FeedbackType.NotSet;
//            switch (num)
//            {
//                case 1:
//                    feedbackType = FeedbackType.B;
//                    break;

//                case 2:
//                    feedbackType = FeedbackType.S;
//                    break;

//                case 3:
//                    feedbackType = FeedbackType.C;
//                    break;

//                default:
//                    feedbackType = FeedbackType.B;
//                    break;
//            }
//            return PublicMethodRepository.GetEnumDescription(feedbackType);
//        }

//        /// <summary>
//        /// [前台]取得心得分享列表
//        /// </summary>
//        /// <param name="filterModel"></param>
//        /// <returns></returns>
//        //public IEnumerable<FeedbackFrontEndDataModel> GetFeedbackListFrontEnd(int id)
//        //{
//        //    FeedbackType feedbackType = FeedbackType.NotSet;
//        //    switch (id)
//        //    {
//        //        case 1:
//        //            feedbackType = FeedbackType.B;
//        //            break;

//        //        case 2:
//        //            feedbackType = FeedbackType.S;
//        //            break;

//        //        case 3:
//        //            feedbackType = FeedbackType.C;
//        //            break;

//        //        default:
//        //            feedbackType = FeedbackType.B;
//        //            break;
//        //    }
//        //    IEnumerable<FeedbackFrontEndDataModel> data;
//        //    try
//        //    {
//        //        int wordLength = Convert.ToInt32(PublicMethodRepository.GetConfigAppSetting("DefaultContentWordLength284"));
//        //        data = DB.GTFBK.Select(s => new FeedbackFrontEndDataModel()
//        //        {
//        //            ID = s.ID,
//        //            SortIndex = s.SR_SQ,
//        //            Title = s.FBK_TITLE,
//        //            TypeCode = s.FBK_TP,
//        //            PartContent = s.FBK_CONTENT,
//        //            PublishDate = s.PUB_DT,
//        //            StatusCode = s.FBK_ST,
//        //            WordLength = wordLength
//        //        })
//        //                 .ToList();
//        //        //篩選+排序
//        //        data = data
//        //            .Where(o => o.StatusCode == "Y" &&
//        //                         o.TypeCode == feedbackType.ToString())
//        //                    .OrderByDescending(s => s.SortIndex)
//        //                    .ThenByDescending(d => d.PublishDate)
//        //                    .ToList();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //    return data;
//        //}

//        /// <summary>
//        /// [前台]取得心得分享內文
//        /// </summary>
//        /// <param name="ID"></param>
//        /// <returns></returns>
//        //public FeedbackFrontEndDetalisDataModel GetFondbackByIDFrontEnd(int ID)
//        //{
//        //    FeedbackFrontEndDetalisDataModel data = DB.GTFBK.Select(s => new FeedbackFrontEndDetalisDataModel
//        //    {
//        //        ID = s.ID,
//        //        Title = s.FBK_TITLE,
//        //        PublishDate = (DateTime)s.PUB_DT,
//        //        NewsContent = s.FBK_CONTENT
//        //    }).Where(w => w.ID == ID).FirstOrDefault();
//        //    return data;
//        //}

//        #endregion 心得分享

//        #region 代理產品

//        /// <summary>
//        /// [前台]取得產品分類列表
//        /// </summary>
//        /// <param name="filterModel"></param>
//        /// <returns></returns>
//        public IEnumerable<ProductFrontEndDataModel> GetProductListFrontEnd(int? id)
//        {
//            IEnumerable<ProductFrontEndDataModel> data;
//            try
//            {
//                int wordLength = Convert.ToInt32(PublicMethodRepository.GetConfigAppSetting("DefaultContentWordLength90"));
//                data = DB.WBPRODUCT.Select(s => new ProductFrontEndDataModel()
//                {
//                    ID = s.ID,
//                    SortIndex = s.SR_SQ,
//                    ProductName = s.PRD_NM,
//                    TypeID = (int)s.MAP_PRODUCT_TP_ID,
//                    WordLength = wordLength
//                })
//                         .ToList();
//                //篩選+排序
//                data = data
//                    .Where(o => o.StatusCode == "Y")
//                            .OrderByDescending(s => s.SortIndex)
//                            .ThenByDescending(d => d.PublishDate)
//                            .ToList();

//                if (id.HasValue)
//                    data = data.Where(o => o.TypeID == id).ToList();

//                foreach (var d in data)
//                {
//                    ProductKindModule schoolTypeModule = new ProductKindModule();
//                    d.ProductType = (schoolTypeModule.DoGetDetailsByID(d.TypeID) as ProductKindDetailsDataModel);
//                    ImgModule imgModule = new ImgModule();
//                    d.RepresentImage = imgModule.GetPreviewImg(d.ID, "Product", "R").FirstOrDefault();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return data;
//        }

//        /// <summary>
//        /// [前台]取得產品分類內文
//        /// </summary>
//        /// <param name="ID"></param>
//        /// <returns></returns>
//        public ProductFrontEndDetalisDataModel GetProductByIDFrontEnd(int ID)
//        {
//            ProductFrontEndDetalisDataModel data = DB.WBPRODUCT.Select(s => new ProductFrontEndDetalisDataModel
//            {
//                ID = s.ID,
//                ProductName = s.PRD_NM,
//                TeachingFeatures = s.PRD_FEAT,
//                Content = s.PRD_CONTENT,
//                TypeID = (int)s.MAP_PRODUCT_TP_ID,
//                Sort = s.SR_SQ,
//            }).Where(w => w.ID == ID).FirstOrDefault();
//            ProductKindModule productTypeModule = new ProductKindModule();
//            data.ProductType = (productTypeModule.DoGetDetailsByID(data.TypeID) as ProductKindDetailsDataModel);
//            return data;
//        }

//        #endregion 代理產品

//        #region Banner

//        public List<BannerModel> GetBannerImg()
//        {
//            var banner = this.DB.WBBANNER
//                .OrderByDescending(o => o.SR_SQ)
//                .ThenByDescending(o => o.BUD_DT)
//                .Select(s => new BannerModel()
//                {
//                    ID = s.ID,
//                    LinkHref = s.BANNER_IMG_LINK,
//                })
//                .Take(3)
//                .ToList();
//            foreach (var b in banner)
//            {
//                var pic = this.DB.WBPIC.Where(o => o.MAP_NEWS_ID == b.ID).FirstOrDefault();
//                b.ImgSrc = pic == null ? "" : pic.IMG_URL;
//            }
//            return banner;
//        }

//        #endregion Banner

//        #endregion 前台

//        public void Dispose()
//        {
//            this.Dispose();
//        }
//    }
//}