using RMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class CommonCodeViewModel : CommonCode
    {
        public List<CommonCode> CommonCode { get; set; }
        public List<CommonCode> CommonType { get; set; }
        public List<CommonCode> CommonCodeTreeViews { get; set; }
        public List<CommonCode> GetCommonCodeByType { get; set; }
        public List<CommonCode> CommonCodeByType { get; set; }
        public string Message { get; set; }
        [DefaultValue(false)]
        public bool SearchStatus { get; set; }
        public CommonCodeViewModel()
        {
            CommonCodeTreeViews = new List<CommonCode>();
            CommonCode = new List<CommonCode>();
            CommonType = new List<CommonCode>();
            GetCommonCodeByType = new List<CommonCode>();
            CommonCodeByType = new List<CommonCode>();

        }
        public IEnumerable<SelectListItem> CommonCodeListItem
        {
            get { return new SelectList(GetCommonCodeByType, "Type", "Type"); }
        }
        public IEnumerable<SelectListItem> CommonCodeTypeListItem
        {
            get { return new SelectList(CommonCodeByType, "Type", "Type"); }
        }
    }
}