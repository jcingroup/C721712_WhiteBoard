//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OutWeb.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class WBWORKS
    {
        public int ID { get; set; }
        public int BUD_USRID { get; set; }
        public System.DateTime BUD_DT { get; set; }
        public int UPD_USRID { get; set; }
        public System.DateTime UPD_DT { get; set; }
        public string WKS_TITLE { get; set; }
        public string WKS_CONTENT { get; set; }
        public string LANG_CD { get; set; }
        public bool DIS_FRONT_ST { get; set; }
        public int SR_SQ { get; set; }
        public string PUB_DT { get; set; }
    }
}