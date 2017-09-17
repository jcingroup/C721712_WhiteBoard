﻿using OutWeb.Repositories;
using System;

namespace OutWeb.Models.Manage.ManageNewsModels
{
    /// <summary>
    /// 最新消息列表資料模型
    /// </summary>
    public class NewsListDataModel
    {
        /// <summary>
        /// 主索引
        /// </summary>
        public int ID { get; set; }

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

        public string PublishDateStr
        {
            get
            {
                DateTime d = (DateTime)this.PublishDate;
                return d.ConvertDateTimeTo10CodeString();
            }
        }

        ///// <summary>
        ///// 狀態
        ///// </summary>
        //public string StatusCode { get; set; }

        ///// <summary>
        ///// 狀態描述
        ///// </summary>
        //public string StatusDescription
        //{
        //    get
        //    {
        //        var eStatus = PublicMethodRepository.GetEnumByValue<StatusEnums>(this.StatusCode);
        //        return PublicMethodRepository.GetEnumDescription(eStatus);
        //    }
        //}

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 語系
        /// </summary>
        public string Language { get; set; }
    }
}