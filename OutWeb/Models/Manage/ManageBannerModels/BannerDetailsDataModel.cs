
using OutWeb.Models.Manage.ImgModels;

namespace OutWeb.Models.Manage.ManageBannerModels
{
    public class BannerDetailsDataModel
    {
        private MemberViewModel m_imageInformation = new MemberViewModel();
        public MemberViewModel ImageInformation
        { get { return this.m_imageInformation; } set { this.m_imageInformation = value; } }
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImgLink { get; set; }
        public string Status { get; set; }
        public int? SortIndex { get; set; }
    }
}