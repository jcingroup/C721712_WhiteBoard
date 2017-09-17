using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTOverseas.Models.Manage.ManageFeedbackModels
{
    public class FeedbackListViewModel 
    {

        FeedbackListFilterModel m_filter = new FeedbackListFilterModel();
        FeedbackListResultModel m_result = new FeedbackListResultModel();

        public FeedbackListFilterModel Filter { get { return this.m_filter; } set { this.m_filter = value; } }
        public FeedbackListResultModel Result { get { return this.m_result; } set { this.m_result = value; } }

    }
}