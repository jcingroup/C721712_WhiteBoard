using OutWeb.Enums;
using OutWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OutWeb.Models.FrontEnd.FeedbackFrontEndModels
{
    public class FeedbackFrontEndListDataModel : IPaginationModel
    {
        private IEnumerable<FeedbackFrontEndDataModel> m_data = new List<FeedbackFrontEndDataModel>();
        public IEnumerable<FeedbackFrontEndDataModel> Data { get { return this.m_data; } set { this.m_data = value; } }
        private PaginationResult m_pagination = new PaginationResult();

        public PaginationResult Pagination
        { get { return this.m_pagination; } set { this.m_pagination = value; } }
    }

    public class FeedbackFrontEndDataModel
    {
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
        /// 分類代碼
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 分類描述
        /// </summary>
        public string TypeDescription
        {
            get
            {
                var eStatus = PublicMethodRepository.GetEnumByValue<FeedbackType>(this.TypeCode);
                return PublicMethodRepository.GetEnumDescription(eStatus);
            }
        }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 發布日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 發布日期字串
        /// </summary>
        public string PublishDateStr
        {
            get
            {
                DateTime d = (DateTime)this.PublishDate;
                return d.ConvertDateTimeTo10CodeString();
            }
        }

        private string m_Content = string.Empty;

        /// <summary>
        /// 提示文 顯示284長度
        /// </summary>
        public string PartContent
        {
            get
            {
                int iWordLength = this.WordLength ?? this.m_Content.Length;
                string result = string.Empty;
                string htmlTagStr = Regex.Replace(this.m_Content, @"<[^>]+>|&nbsp;|&ldquo;|&rdquo;", "").Trim();
                result = htmlTagStr.Length >= iWordLength ? htmlTagStr.Substring(0, iWordLength) : htmlTagStr;
                return result;
            }
            set
            {
                this.m_Content = value;
            }
        }
    }
}