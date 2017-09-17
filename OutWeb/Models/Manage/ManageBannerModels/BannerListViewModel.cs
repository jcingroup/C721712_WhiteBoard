using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Models.Manage.ManageBannerModels
{
    public class BannerListViewModel 
    {

        BannerListFilterModel m_filter = new BannerListFilterModel();
        BannerListResultModel m_result = new BannerListResultModel();

        public BannerListFilterModel Filter { get { return this.m_filter; } set { this.m_filter = value; } }
        public BannerListResultModel Result { get { return this.m_result; } set { this.m_result = value; } }

    }
}