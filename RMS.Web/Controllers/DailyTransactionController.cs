using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize(Roles = "CoverFundManagement")]

    public class DailyTransactionController : Controller
    {
        private ICommonCodeManager _commonCodeManager;
        private IDailyTransactionManager _dailyTransactionManager;
        private IBranchInfoManager _branchInfoManager;
        private IAccountInformationManager _accountInformationManager;
        private IEventLogManager _eventLogManager;
        string LocalMachine;
        string PhysicalAddress;
        string LocalIPAddress;
        string MotherBoardId;
        string ProcessorId;
        public DailyTransactionController(ICommonCodeManager commonCodeManager,
            IDailyTransactionManager dailyTransactionManager,IBranchInfoManager branchInfoManager,IAccountInformationManager accountInformationManager, IEventLogManager eventLogManager)
        {
            _commonCodeManager = commonCodeManager;
            _dailyTransactionManager = dailyTransactionManager;
            _branchInfoManager = branchInfoManager;
            _accountInformationManager = accountInformationManager;
            _eventLogManager = eventLogManager;
        }
        public ActionResult Index(DailyTransactionViewModel model)
        {
            try
            {
                ModelState.Clear();
                model.CurrencyList = _commonCodeManager.GetCurrencyList();
                model.VoucherTypeList = _commonCodeManager.GetVoucherTypeList();
                model.TransactionModeList = _commonCodeManager.GetTransactionModeList();
                model.TransactionNatureList = _commonCodeManager.GetTransactionNatureList();
                model.AccountList = _accountInformationManager.GetAccountList();
                model.ExchangeList = _branchInfoManager.GetIssuingBranchList();
                model.DailyTransactions = _dailyTransactionManager.GetTransactionList();
                model.VoucherNo = _dailyTransactionManager.GetLastVoucherNo();
                model.TransDate = DateTime.Now.Date;
            }
            catch (Exception exception)
            {

                Errorlog.WriteLog(exception);  
            }
            return View(model);
        }

        public ActionResult Save(DailyTransactionViewModel model)
        {
            try
            {
               ResponseModel responseModel = new ResponseModel();
                string voucherType = model.VoucherType;
                if (voucherType == "VT001")
                {
                    model.Debit = model.Amount;
                    model.Credit = 0;
                }
                else
                {
                    model.Credit = model.Amount;
                    model.Debit = 0;
                }
                responseModel = _dailyTransactionManager.SaveTrans(model);
                model.Message = responseModel.Message;
            }
            catch (Exception exception)
            {
                
                Errorlog.WriteLog(exception);
            }
            return RedirectToAction("index",model);
        }
        
        public ActionResult Verify(List<string> noOfTransaction, DailyTransactionViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            if (noOfTransaction != null)
            {
                responseModel = _dailyTransactionManager.VerifyTransaction(noOfTransaction);
                model.Message = responseModel.Message;
                return RedirectToAction("Verify", model);
            }
            model.DailyTransactions = _dailyTransactionManager.GetUnverifiedTrans();
            return View(model);
        }
        public ActionResult Reverse(List<string> noOfTransaction, DailyTransactionViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            if (noOfTransaction != null)
            {
                responseModel = _dailyTransactionManager.ReverseTransaction(noOfTransaction);
                if (responseModel.ResponsStatus > 0)
                {
                    model.Message = "Selected transaction has been removed.";
                    return RedirectToAction("Reverse",model);
                }
                else
                {
                    model.Message = "Selected transaction is not remove.";
                    return RedirectToAction("Reverse", model);
                }
            }
            model.DailyTransactions = _dailyTransactionManager.GetUnverifiedTrans();
            return View(model);
        }
    }
}