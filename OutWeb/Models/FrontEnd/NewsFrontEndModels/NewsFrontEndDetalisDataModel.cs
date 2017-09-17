using OutWeb.Repositories;
using System;

namespace OutWeb.Models.FrontEnd.NewsFrontEndModels
{
    /// <summary>
    /// 最新消息列表資料模型
    /// </summary>
    public class NewsFrontEndDetalisDataModel
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
        /// 內文
        /// </summary>
        public string NewsContent { get; set; }
    }
}