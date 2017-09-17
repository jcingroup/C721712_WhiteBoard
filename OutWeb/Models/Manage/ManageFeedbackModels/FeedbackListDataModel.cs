
using OutWeb.Enums;
using OutWeb.Repositories;
using System;

namespace GTOverseas.Models.Manage.ManageFeedbackModels
{
    /// <summary>
    /// 最新消息列表資料模型
    /// </summary>
    public class FeedbackListDataModel
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

        /// <summary>
        /// 分類
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