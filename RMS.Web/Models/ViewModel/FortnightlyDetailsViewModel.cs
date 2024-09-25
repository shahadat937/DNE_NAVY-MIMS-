using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class FortnightlyDetailsViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
        public FortnightlyDetail FortnightlyDetail { get; set; }
        public List<FortnightlyDetail> FortnightlyDetails { get; set; }
        public List<FortnightlyInfo> FortnightlyInfos { get; set; }
        public List<CommonCode> CommonCodes { get; set; } 
        public FortnightlyDetailsViewModel()
        {
            FortnightlyDetail = new FortnightlyDetail();
            FortnightlyDetails = new List<FortnightlyDetail>();
            FortnightlyInfos = new List<FortnightlyInfo>();
            CommonCodes=new List<CommonCode>();
        }
        public IEnumerable<SelectListItem> FortnightlyInfoListItem
        {
            get { return new SelectList(FortnightlyInfos, "FortnightlyId", "CommonCodeID"); }

        }
        public IEnumerable<SelectListItem> SelectedEmployment
        {
            get { return new SelectList(CommonCodes, "CommonCodeId", "TypeValue"); }
        }
        public string SearchKey { get; set; }
    }
}