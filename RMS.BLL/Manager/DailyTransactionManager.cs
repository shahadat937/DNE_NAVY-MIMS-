using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class DailyTransactionManager:BaseManager,IDailyTransactionManager
    {
        private IDailyTransactionRepository _dailyTransactionRepository;
        private IuvGLTransactionRepository _uvGlTransactionRepository;
        public DailyTransactionManager()
        {
            _dailyTransactionRepository=new DailyTransactionRepository(Context);
            _uvGlTransactionRepository=new uvGLTransactionRepository(Context);

        }

        public ResponseModel SaveTrans(DailyTransaction model)
        {
            var dailyTransaction = new DailyTransaction();
            if (model.Debit > 0 || model.Credit > 0 && model.AccountCode!=null)
            {
                string transDate = model.TransDate.Date.ToString("dd MMM yyyy");
                dailyTransaction.VoucherNo = model.VoucherNo;
                dailyTransaction.TransDate = Convert.ToDateTime(transDate);
                dailyTransaction.TransType = model.TransNature;
                dailyTransaction.ModeOfTrans = model.ModeOfTrans;
                dailyTransaction.TransNature = model.TransNature;
                dailyTransaction.AccountCode = model.AccountCode;
                dailyTransaction.Description = model.Description;
                dailyTransaction.Debit = model.Debit;
                dailyTransaction.Credit = model.Credit;
                dailyTransaction.Currency = model.Currency;
                dailyTransaction.ExchangeRate = model.ExchangeRate;
                dailyTransaction.FDebit = model.ExchangeRate*model.Debit;
                dailyTransaction.FCredit = model.ExchangeRate*model.Credit;
                dailyTransaction.BankCode = PortalContext.CurrentUser.BankCode;
                dailyTransaction.BranchCode = PortalContext.CurrentUser.BranchCodeIdentity;
                dailyTransaction.UserAgentBranch = model.UserAgentBranch;
                dailyTransaction.Verified = false;
                dailyTransaction.UserId = PortalContext.CurrentUser.UserName;
                dailyTransaction.LastUpdate = DateTime.Now;
                if (model.Debit > 0)
                {
                    //var tr = _vwHoBalanceRepository.FindOne(x =>x.BankCode == PortalContext.CurrentUser.BankCode &&x.AgentBranchCode == model.UserAgentBranch);
                    decimal balance = 0;
                    if (model.Debit > balance)
                    {
                        ResponseModel.Message = "Balance must be greater than debit amount.";
                    }
                    else
                    {
                        ResponseModel.ResponsStatus = _dailyTransactionRepository.Save(dailyTransaction);
                        if (ResponseModel.ResponsStatus > 0)
                        {
                            ResponseModel.Data = _uvGlTransactionRepository.Filter(x => x.VoucherNo == model.VoucherNo && x.TransDate == dailyTransaction.TransDate && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
                            ResponseModel.Message = "Cover fund has been added. Please verify by another user.";
                        }
                        else
                        {
                            ResponseModel.Message = "Data is not saved";
                        }
                    }
                }
                else
                {
                    ResponseModel.ResponsStatus = _dailyTransactionRepository.Save(dailyTransaction);
                    if (ResponseModel.ResponsStatus > 0)
                    {
                        ResponseModel.Data = _uvGlTransactionRepository.Filter(x => x.VoucherNo == model.VoucherNo && x.TransDate == dailyTransaction.TransDate && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
                        ResponseModel.Message = "Cover fund has been added. Please verify by another user.";
                    }
                    else
                    {
                        ResponseModel.Message = "Data is not saved";
                    }
                }
            }
            return ResponseModel;
        }

        public List<DailyTransaction> GetUnverifiedTrans()
        {
            return
                _dailyTransactionRepository.Filter(x =>x.Verified == false && x.BankCode == PortalContext.CurrentUser.BankCode && x.UserId != PortalContext.CurrentUser.UserName).OrderByDescending(x=>x.IdentityNo).ToList();
        }

        public ResponseModel VerifyTransaction(List<string> noOfTransaction)
        {
            ResponseModel responseModel = new ResponseModel();
            foreach (var identityNo in noOfTransaction)
            {
                long identity = Convert.ToInt64(identityNo);
                var oldTransaction = _dailyTransactionRepository.FindOne(x => x.IdentityNo == identity);
                if (oldTransaction != null)
                {
                    if (oldTransaction.Debit > 0)
                    {
                        //var tr = _vwHoBalanceRepository.FindOne(x => x.BankCode == PortalContext.CurrentUser.BankCode && x.AgentBranchCode == oldTransaction.UserAgentBranch);
                        decimal balance = 0;
                        if (balance >= oldTransaction.Debit)
                        {
                            oldTransaction.Verified = true;
                            oldTransaction.Description = oldTransaction.Description + " verified by " +PortalContext.CurrentUser.UserName;
                            responseModel.ResponsStatus = _dailyTransactionRepository.Edit(oldTransaction);
                            ResponseModel.Message = "Selected transaction has been verified.";

                        }
                        else
                        {
                            ResponseModel.Message = "Balance must be greater than debit amount.";
                        }
                    }
                    else
                    {
                        oldTransaction.Verified = true;
                        oldTransaction.Description = oldTransaction.Description + " verified by " +PortalContext.CurrentUser.UserName;
                        ResponseModel.ResponsStatus = _dailyTransactionRepository.Edit(oldTransaction);
                        ResponseModel.Message = "Selected transaction has been verified.";
                    }
                }
            }
            return ResponseModel;
        }

        public ResponseModel ReverseTransaction(List<string> noOfTransaction)
        {
            ResponseModel responseModel = new ResponseModel();
            foreach (var identityNo in noOfTransaction)
            {
                long identity = Convert.ToInt64(identityNo);
                var oldTransaction = _dailyTransactionRepository.FindOne(x => x.IdentityNo == identity);
                if (oldTransaction != null)
                {
                  _dailyTransactionRepository.Delete(oldTransaction);
                    responseModel.ResponsStatus = 1;
                }
            }
            return responseModel;
        }

        public List<DailyTransaction> GetTransactionList()
        {
            return
              _dailyTransactionRepository.Filter(x =>x.Verified == false && x.BankCode == PortalContext.CurrentUser.BankCode && x.UserId==PortalContext.CurrentUser.UserName).OrderByDescending(x=>x.IdentityNo).ToList();

        }
        public List<DailyTransaction> GetVTransactionList()
        {
            return
              _dailyTransactionRepository.Filter(x => x.Verified == true && x.BankCode == PortalContext.CurrentUser.BankCode).OrderByDescending(x => x.IdentityNo).ToList();

        }
        public string GetLastVoucherNo()
        {
            string voucherNo = "";
            var isData = _dailyTransactionRepository.FindOne(x => x.BankCode == PortalContext.CurrentUser.BankCode);
            if (isData != null)
            {
                var rec =
                    _dailyTransactionRepository.Filter(x => x.BankCode == PortalContext.CurrentUser.BankCode)
                        .OrderByDescending(x => x.IdentityNo)
                        .ThenByDescending(x => x.VoucherNo)
                        .First();
                long vNo = Convert.ToInt64(rec.VoucherNo) + 1;
                voucherNo = vNo.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            }
            else
            {
                voucherNo = "0001";
            }
            return voucherNo;
        }
    }
}
