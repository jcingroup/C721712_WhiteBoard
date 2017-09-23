using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Models.Manage.ImgModels
{
    public class ImageModel
    {
        public string act { get; set; }
        public string actionName { get; set; }
        public string ID { get; set; }

        public HttpPostedFileBase image { get; set; }
        public List<HttpPostedFileBase> images { get; set; }
    }


}