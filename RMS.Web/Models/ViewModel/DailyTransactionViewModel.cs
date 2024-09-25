using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class DailyTransactionViewModel:DailyTransaction
    {
        public string Message { get; set; }
        public string VoucherType { get; set; }
        [Range(0.0001, Double.MaxValue)]
        public decimal Amount { get; set; }
        public List<CommonCode> CurrencyList { get; set; }
        public List<CommonCode> TransactionNatureList { get; set; }
        public List<CommonCode> TransactionModeList { get; set; }
        public List<BranchInfo> ExchangeList { get; set; }
        public List<CommonCode> VoucherTypeList { get; set; } 
        public DailyTransaction DailyTransaction { get; set; }
        public List<DailyTransaction> DailyTransactions { get; set; }
        public IEnumerable AccountList { get; set; }
        public List<uvGLTransaction> uvGLTransactions { get; set; }
        public DailyTransactionViewModel()
        {
            CurrencyList=new List<CommonCode>();
            TransactionNatureList=new List<CommonCode>();
            TransactionModeList=new List<CommonCode>();
            ExchangeList = new List<BranchInfo>();
            AccountList= new List<AccountInformation>();
            VoucherTypeList = new List<CommonCode>();
            DailyTransactions = new List<DailyTransaction>();
            DailyTransaction = new DailyTransaction();
        }

        public IEnumerable<SelectListItem> CurrencySelectListItems
        {
            get { return new SelectList(CurrencyList,"Code","TypeValue");}
        }

        public IEnumerable<SelectListItem> TransactionNatureListItems
        {
            get { return new SelectList(TransactionNatureList,"Code","TypeValue");}
        }

        public IEnumerable<SelectListItem> TransactionModeListItems
        {
            get { return new SelectList(TransactionModeList,"Code", "TypeValue");}
        }
        public IEnumerable<SelectListItem> AccountSelectListItems
        {
            get { return new SelectList(AccountList, "AccountCode", "AccountHead"); }
        } 
        public IEnumerable<SelectListItem> ExchangeSelectListItems
        {
            get { return new SelectList(ExchangeList, "BranchCode", "BranchName"); }
        }

        public IEnumerable<SelectListItem> VoucherSelectListItems
        {
            get { return new SelectList(VoucherTypeList,"Code","TypeValue");}
        } 
    }
}