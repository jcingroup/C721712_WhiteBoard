using OutWeb.Enums;
using OutWeb.Models.Manage.ImgModels;
using OutWeb.Models.Manage.ProductKindModels;
using OutWeb.Repositories;
using System.Collections.Generic;

namespace OutWeb.Models.FrontEnd.ProductFrontEndModels
{
    /// <summary>
    /// 產品列表資料模型
    /// </summary>
    public class ProductFrontEndDetalisDataModel
    {
        private MemberViewModel m_representImage = new MemberViewModel();

        private List<MemberViewModel> m_otherImages = new List<MemberViewModel>();

        /// <summary>
        /// 代表圖片
        /// </summary>
        public MemberViewModel RepresentImage { get { return this.m_representImage; } set { this.m_representImage = value; } }

        /// <summary>
        /// 其他圖片
        /// </summary>
        public List<MemberViewModel> OtherImages { get { return this.m_otherImages; } set { this.m_otherImages = value; } }

        /// <summary>
        /// 主索引
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 產品分類模型
        /// </summary>
        public ProductKindDetailsDataModel ProductType = new ProductKindDetailsDataModel();

        /// <summary>
        /// 產品名稱
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 所屬分類索引
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// 成立日期
        /// </summary>

        public string ProductBulidDate { get; set; }

        /// <summary>
        /// 產品地點
        /// </summary>
        public string ProductLocation { get; set; }

        /// <summary>
        /// 教學特色
        /// </summary>
        public string TeachingFeatures { get; set; }

        /// <summary>
        /// 學生國籍比例
        /// </summary>
        public string StudentNationalityRatio { get; set; }

        /// <summary>
        /// 發布狀態
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 產品簡介
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        /// 發布狀態描述
        /// </summary>
        public string StatusDescription
        {
            get
            {
                var eStatus = PublicMethodRepository.GetEnumByValue<StatusEnums>(this.StatusCode);
                return PublicMethodRepository.GetEnumDescription(eStatus);
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string Content { get; set; }
    }
}