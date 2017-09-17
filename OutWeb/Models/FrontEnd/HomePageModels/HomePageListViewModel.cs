using OutWeb.Models.FrontEnd.NewsFrontEndModels;
using OutWeb.Models.FrontEnd.ProductFrontEndModels;
using System.Collections.Generic;

namespace OutWeb.Models.FrontEnd.HomePageModels
{
    public class HomePageListViewModel
    {
        private List<BannerModel> m_banner = new List<BannerModel>();
        public List<BannerModel> Banner { get { return this.m_banner; } set { this.m_banner = value; } }
        private IEnumerable<NewsFrontEndDataModel> m_news;
        public IEnumerable<NewsFrontEndDataModel> News { get { return this.m_news; } set { this.m_news = value; } }

        private IEnumerable<ProductFrontEndDataModel> m_schoolData;
        public IEnumerable<ProductFrontEndDataModel> SchoolData { get { return this.m_schoolData; } set { this.m_schoolData = value; } }
    }
}