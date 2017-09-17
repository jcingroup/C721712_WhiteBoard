using OutWeb.Models;
using System.Collections.Generic;

namespace GTOverseas.Models.Manage.ManageFeedbackModels
{
    /// <summary>
    /// 最新消息列表資回傳模型
    /// </summary>
    public class FeedbackListResultModel : IPaginationModel
    {
        private List<FeedbackListDataModel> m_data = new List<FeedbackListDataModel>();
        public List<FeedbackListDataModel> Data { get { return this.m_data; } set { this.m_data = value; } }

        /// <summary>
        /// 分頁模型
        /// </summary>
        public PaginationResult m_pagination = new PaginationResult();

        public PaginationResult Pagination { get { return this.m_pagination; } set { this.m_pagination = value; } }
    }
}