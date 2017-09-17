namespace GTOverseas.Models.Manage.ManageFeedbackModels
{
    /// <summary>
    /// 心得分享列表資過濾條件模型
    /// </summary>
    public class FeedbackListFilterModel 
    {
        /// <summary>
        /// 選取頁面
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 查詢分類
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 查詢狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 查詢關鍵字
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// 排序條件
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// 查詢開始日期
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 查詢結束時間
        /// </summary>
        public string EndDate { get; set; }
    }
}