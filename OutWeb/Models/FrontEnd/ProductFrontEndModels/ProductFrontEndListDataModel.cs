using OutWeb.Models.Manage.ImgModels;
using OutWeb.Models.Manage.ProductKindModels;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OutWeb.Models.FrontEnd.ProductFrontEndModels
{
    public class ProductFrontEndListDataModel : IPaginationModel
    {
        private IEnumerable<ProductFrontEndDataModel> m_data;

        public IEnumerable<ProductFrontEndDataModel> Data { get { return this.m_data; } set { this.m_data = value; } }
        private PaginationResult m_pagination = new PaginationResult();

        public PaginationResult Pagination
        { get { return this.m_pagination; } set { this.m_pagination = value; } }
    }

    public class ProductFrontEndDataModel
    {
        private MemberViewModel m_representImage = new MemberViewModel();

        /// <summary>
        /// 代表圖片
        /// </summary>
        public MemberViewModel RepresentImage { get { return this.m_representImage; } set { this.m_representImage = value; } }

        /// <summary>
        /// 內文字數
        /// </summary>
        public int? WordLength;

        /// <summary>
        /// 主索引
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 排序欄位
        /// </summary>
        public int? SortIndex { get; set; }

        /// <summary>
        /// 狀態代碼
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 產品名稱
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 所屬分類索引
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// 所屬分類
        /// </summary>

        public ProductKindDetailsDataModel ProductType { get; set; }

        private string m_dscription = string.Empty;

        /// <summary>
        /// 產品簡介 顯示284長度
        /// </summary>
        public string ProductDscription
        {
            get
            {
                int iWordLength = this.WordLength ?? this.m_dscription.Length;
                string result = string.Empty;
                string htmlTagStr = Regex.Replace(this.m_dscription, @"<[^>]+>|&nbsp;|&ldquo;|&rdquo;", "").Trim();
                result = htmlTagStr.Length >= iWordLength ? htmlTagStr.Substring(0, iWordLength) : htmlTagStr;
                return result;
            }
            set
            {
                this.m_dscription = value;
            }
        }

        public DateTime PublishDate { get; set; }
    }
}