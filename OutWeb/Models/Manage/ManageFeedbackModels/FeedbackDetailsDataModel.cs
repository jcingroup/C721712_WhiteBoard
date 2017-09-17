using OutWeb.Repositories;
using System;

namespace GTOverseas.Models.Manage.ManageFeedbackModels
{
    public class FeedbackDetailsDataModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }


        public string PublishDateStr
        {
            get
            {
                string dateStr = PublishDate == null ? DateTime.UtcNow.AddHours(8).ConvertDateTimeTo10CodeString() : PublicMethodRepository.ConvertDateTimeTo10CodeString((DateTime)this.PublishDate);
                return dateStr;
            }
        }

        public string Status { get; set; }
        public string Type { get; set; }
        public int? SortIndex { get; set; }
        public string Content { get; set; }
    }
}