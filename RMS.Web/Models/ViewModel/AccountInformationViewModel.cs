using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class AccountInformationViewModel: AccountInformation
    {
        public List<AccountInformation> AccountInformationTreeViews{ get; set; }
        public List<AccountInformation> AccountInformation { get; set; }
        public List<CommonCode> AccountCategory { get; set; }
        public List<CommonCode> AccountStatus { get; set; }
        public AccountInformationViewModel()
        {
            AccountInformation = new List<AccountInformation>();
            AccountCategory = new List<CommonCode>();
            AccountStatus = new List<CommonCode>();
            AccountInformationTreeViews = new List<AccountInformation>();
        }
        public IEnumerable<SelectListItem> CategoryListItem
        {
            get { return new SelectList(AccountCategory, "DisplayCode", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> AccountStatusListItem
        {
            get { return new SelectList(AccountStatus, "DisplayCode", "TypeValue"); }
        }
    }
}