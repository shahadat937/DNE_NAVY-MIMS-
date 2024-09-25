using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class AccountInformationManager:BaseManager,IAccountInformationManager
    {
        private IAccountInformationRepository _accountInformationRepository;

        public AccountInformationManager()
        {
            _accountInformationRepository=new AccountInformationRepository(Context);
        }

        public IEnumerable GetAccountList()
        {
            return _accountInformationRepository.Filter(x => x.BankCode == PortalContext.CurrentUser.BankCode ).Select(x=>new
            {
                AccountCode= x.AccountCode,
                AccountHead = "[" + x.AccountCode + "]" + x.AccountHead
            }).ToList();
        }
        public AccountInformation GetAccountInfoByCode(string AccountCode)
        {
            return _accountInformationRepository.FindOne(x => x.AccountCode == AccountCode);
        }
        public List<AccountInformation> GetAccountInfoTeeView(string BankCode)
        {
            var firstLevel = new List<AccountInformation>();
            var secondLevel = new List<AccountInformation>();
            var thirdLevel = new List<AccountInformation>();
            var fourthLevel = new List<AccountInformation>();

            var accountInfoTrees = _accountInformationRepository.Filter(x => x.BankCode==BankCode);

            foreach (AccountInformation accountInfoTree in accountInfoTrees)
            {
                if (accountInfoTree.AccountLevel == "1")
                {
                    firstLevel.Add(accountInfoTree);
                }
                if (accountInfoTree.AccountLevel == "2")
                {
                    secondLevel.Add(accountInfoTree);

                }
                if (accountInfoTree.AccountLevel == "3")
                {
                    thirdLevel.Add(accountInfoTree);
                }
                if (accountInfoTree.AccountLevel == "4")
                {
                    fourthLevel.Add(accountInfoTree);
                }
            }
            foreach (AccountInformation accountInfo in firstLevel)
            {
               accountInfo.AccountInfoTrees = GetFirstChaild(accountInfo.AccountCode, secondLevel, thirdLevel, fourthLevel).ToList();
            }
            return firstLevel;
        }
        private IEnumerable<AccountInformation> GetFirstChaild(string AccountCode, List<AccountInformation> secondLevel, List<AccountInformation> thirdLevel, List<AccountInformation> fourthLevel)
        {
            //BranchCode = BranchCode.Trim();
            secondLevel = secondLevel.Where(x => x.FirstLevel == AccountCode).ToList();
            foreach (var accountInfo in secondLevel)
            {
               accountInfo.AccountInfoTrees = GetSecondChaild(accountInfo.AccountCode, thirdLevel, fourthLevel).ToList();
                yield return accountInfo;
            }
        }
        private IEnumerable<AccountInformation> GetSecondChaild(string AccountCode, List<AccountInformation> thirdLevel, List<AccountInformation> fourthLevel)
        {
            thirdLevel = thirdLevel.Where(x => x.SecondLevel == AccountCode).ToList();
            foreach (var accountInfo in thirdLevel)
            {
                accountInfo.AccountInfoTrees = GetThirdChaild(accountInfo.AccountCode, fourthLevel).ToList();
                yield return accountInfo;
            }
            //return thirdLevel;
        }
        private IEnumerable<AccountInformation> GetThirdChaild(string AccountCode, List<AccountInformation> fourthLevel)
        {
            fourthLevel = fourthLevel.Where(x => x.ThirdLevel == AccountCode).ToList();
            return fourthLevel;
        }

        public ResponseModel SaveAccountInfo(AccountInformation model)
        {

            var level = _accountInformationRepository.GetAccountInfoByCode(x => x.AccountCode == model.FirstLevel && x.BankCode==PortalContext.CurrentUser.BankCode).ToList();
            if (level.Count() < 1)
            {
                model.AccountLevel = "1";
                model.FirstLevel = model.AccountCode;
                model.SecondLevel = "";
                model.ThirdLevel = "";
                model.FourthLevel = "";
                model.FifthLevel = "";
            }
            else
            {
                int BLevel = Convert.ToInt32(level[0].AccountLevel) + 1;
                //model.BranchLevel = level[0].BranchLevel;
                if (level[0].AccountLevel == "1")
                {
                    model.AccountLevel = BLevel.ToString();
                    model.FirstLevel = level[0].FirstLevel;
                    model.SecondLevel = model.AccountCode;
                    model.ThirdLevel = "";
                    model.FourthLevel = "";
                    model.FifthLevel = "";
                }
                if (level[0].AccountLevel == "2")
                {
                    model.AccountLevel = BLevel.ToString();
                    model.FirstLevel = level[0].FirstLevel;
                    model.SecondLevel = level[0].SecondLevel;
                    model.ThirdLevel = model.AccountCode;
                    model.FourthLevel = "";
                    model.FifthLevel = "";
                }
                if (level[0].AccountLevel == "3")
                {
                    model.AccountLevel = BLevel.ToString();
                    model.FirstLevel = level[0].FirstLevel;
                    model.SecondLevel = level[0].SecondLevel;
                    model.ThirdLevel = level[0].ThirdLevel;
                    model.FourthLevel = model.AccountCode;
                    model.FifthLevel = "";
                }
                if (level[0].AccountLevel == "4")
                {
                    model.AccountLevel = BLevel.ToString();
                    model.FirstLevel = level[0].FirstLevel;
                    model.SecondLevel = level[0].SecondLevel;
                    model.ThirdLevel = level[0].ThirdLevel;
                    model.FourthLevel = level[0].FourthLevel;
                    model.FifthLevel = model.AccountCode;
                }

            }

            var rec = _accountInformationRepository.Filter(x => x.AccountCode == model.AccountCode && x.BankCode==PortalContext.CurrentUser.BankCode).ToList();
            if (rec.Count <= 0)
            {
                var AInfo = new AccountInformation
                {
                    BankCode=PortalContext.CurrentUser.BankCode,
                    AccountCode = model.AccountCode.Trim(),
                    AccountHead = model.AccountHead,
                    Category = model.Category,
                    AccountStatusCode = model.AccountStatusCode,
                    AccountLevel = model.AccountLevel,
                    FirstLevel = model.FirstLevel.Trim(),
                    SecondLevel = model.SecondLevel.Trim(),
                    ThirdLevel = model.ThirdLevel.Trim(),
                    FourthLevel = model.FourthLevel.Trim(),
                    FifthLevel = model.FifthLevel.Trim()
                };

                ResponseModel.ResponsStatus = _accountInformationRepository.Save(AInfo);
                ResponseModel.Message = "Chart of Accounts Saved successfully";
            }
            else
            {
                var recInfo = rec.First();
                    //recInfo.BankCode = BankCode;
                    //recInfo.AccountCode = model.AccountCode;
                    recInfo.AccountHead = model.AccountHead;
                    recInfo.Category = model.Category;
                    recInfo.AccountStatusCode = model.AccountStatusCode;
                    //recInfo.AccountLevel = model.AccountLevel;
                    //recInfo.FirstLevel = model.FirstLevel;
                    //recInfo.SecondLevel = model.SecondLevel;
                    //recInfo.ThirdLevel = model.ThirdLevel;
                    //recInfo.FourthLevel = model.FourthLevel;
                    //recInfo.FifthLevel = model.FifthLevel;
                ResponseModel.ResponsStatus = _accountInformationRepository.Edit(recInfo);
                ResponseModel.Message = "Chart of Accounts Updated successfully";

            }
            return ResponseModel;

        }

    }
}
