using OutWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OutWeb.Models.FrontEnd.NewsFrontEndModels
{
    public class NewsFrontEndListDataModel : IPaginationModel
    {
        private IEnumerable<NewsFrontEndDataModel> m_data;
        public IEnumerable<NewsFrontEndDataModel> Data { get { return this.m_data; } set { this.m_data = value; } }

        private PaginationResult m_pagination = new PaginationResult();

        public PaginationResult Pagination
        { get { return this.m_pagination; } set { this.m_pagination = value; } }
    }

    public class NewsFrontEndDataModel
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
        //public string StatusCode { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 前台顯示
        /// </summary>
        public bool DisplayForFront { get; set; }

        public string DisplayForFrontStr
        {
            get
            {
                string str = string.Empty;
                if (this.DisplayForFront)
                    str = "顯示";
                else
                    str = "隱藏";
                return str;
            }
        }

        /// <summary>
        /// 首頁顯示
        /// </summary>
        public bool DisplayForHomePage { get; set; }

        public string DisplayForHomePageStr
        {
            get
            {
                string str = string.Empty;
                if (this.DisplayForHomePage)
                    str = "顯示";
                else
                    str = "隱藏";
                return str;
            }
        }

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