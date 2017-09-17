using OutWeb.Enums;
using OutWeb.Repositories;
using System;

namespace OutWeb.Models.Manage.ManageBannerModels
{
    /// <summary>
    /// 最新消息列表資料模型
    /// </summary>
    public class BannerListDataModel
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
        /// 狀態
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 狀態描述
        /// </summary>
        public string StatusDescription
        {
            get
            {
                var eStatus = PublicMethodRepository.GetEnumByValue<StatusEnums>(this.StatusCode);
                return PublicMethodRepository.GetEnumDescription(eStatus);
            }
        }

        public DateTime? CreateDate { get; set; }

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