using System;
using System.ComponentModel.DataAnnotations;

namespace RMS.Model
{
    
    public partial class vwMesageInfo
    {
        public string OrgName { get; set; }
        public string OfficeName { get; set; }
        public string ReportName { get; set; }
        public long MessageInfoIdentity { get; set; }
        public string MessageId { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> MessageDate { get; set; }
        public string MsgFrom { get; set; }
        public string MsgTo { get; set; }
        [DataType(DataType.Html)]
        public string Subject { get; set; }
        public string Message { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }
    
    }
}
