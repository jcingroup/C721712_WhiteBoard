using System.Collections.Generic;

namespace OutWeb.Models.Manage.ManageBannerModels
{
    /// <summary>
    /// 最新消息列表資回傳模型
    /// </summary>
    public class BannerListResultModel : IPaginationModel
    {
        private List<BannerListDataModel> m_data = new List<BannerListDataModel>();
        public List<BannerListDataModel> Data { get { return this.m_data; } set { this.m_data = value; } }

        /// <summary>
        /// 分頁模型
        /// </summary>
        public PaginationResult m_pagination = new PaginationResult();

        public PaginationResult Pagination { get { return this.m_pagination; } set { this.m_pagination = value; } }
    }
}