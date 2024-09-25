using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RMS.BLL.DashBoardTreeView.UpdateOperationState;
using RMS.BLL.IManager;
using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class UpdateOplStateManager:BaseManager,IUpdateOplStateManager
    {
        private IUpdateOplStateRepository _updateOplStateRepository;
        RM_AGBEntities ds = new RM_AGBEntities();
        public UpdateOplStateManager()
        {
            _updateOplStateRepository = new UpdateOplStateRepository(Instance.Context);
        }

        //public IEnumerable GetOplList()
        //{
            //return _updateOplStateRepository.Filter(x => x.BankCode == PortalContext.CurrentUser.BankCode).Select(x => new
            //{
            //    AccountCode = x.AccountCode,
            //    AccountHead = "[" + x.AccountCode + "]" + x.AccountHead
            //}).ToList();

            
        //}
        public UpdateOPlState GetOplByCode(long accountCode)
        {

            return _updateOplStateRepository.FindOne(x => x.Id == accountCode);
        }

        public List<UpdateOPlState> GetOplTeeView(long BankCode)
        {
            throw new NotImplementedException();
        }

        //public List<UpdateOPlState> GetOplTeeView(long BankCode)
        //{
        //    var firstLevel = new List<UpdateOPlState>();
        //    var secondLevel = new List<UpdateOPlState>();
        //    var thirdLevel = new List<UpdateOPlState>();
        //    //var fourthLevel = new List<AccountInformation>();


        //    List<UpdateOPlState> accountInfoTrees = _updateOplStateRepository.All().ToList();

        //    foreach (UpdateOPlState accountInfoTree in accountInfoTrees)
        //    {
        //        if (accountInfoTree.LEVEL == 1)
        //        {
        //            firstLevel.Add(accountInfoTree);
        //        }
        //        if (accountInfoTree.LEVEL == 2)
        //        {
        //            secondLevel.Add(accountInfoTree);

        //        }
        //        if (accountInfoTree.LEVEL == 3)
        //        {
        //            thirdLevel.Add(accountInfoTree);
        //        }

        //    }
        //    foreach (UpdateOPlState accountInfo in firstLevel)
        //    {

        //        accountInfo.AccountInfoTrees = GetFirstChaild(accountInfo.id, secondLevel, thirdLevel).ToList();
        //    }
        //    return firstLevel;
        //}

        public List<UpdateChartsOfAccount> GetAll()
        {
            //var controlAccounts = _updateOplStateRepository.All().ToList();
            List<UpdateOPlState> controlAccounts = ds.UpdateOPlStates.Where(x =>x.ParentCode != null ).ToList();
            IUpdateTarget chTarget = new UpdateChartofAccountAdapter(controlAccounts);
            var client = new UpdateTreeViewBuilder(chTarget);
            return client.GetChartofAccount();
        }

        //private IEnumerable<UpdateOPlState> GetFirstChaild(long AccountCode, List<UpdateOPlState> secondLevel, List<UpdateOPlState> thirdLevel)
        //{
        //    //BranchCode = BranchCode.Trim();
        //    //var s = Int32.Parse(AccountCode);
        //    secondLevel = secondLevel.Where(x => x.firstLevel == AccountCode).ToList();
        //    foreach (var accountInfo in secondLevel)
        //    {
                
        //       accountInfo.AccountInfoTrees = GetSecondChaild(accountInfo.id,thirdLevel).ToList();
        //        yield return accountInfo;
        //    }
        //}
        //private IEnumerable<UpdateOPlState> GetSecondChaild(long AccountCode, List<UpdateOPlState> thirdLevel)
        //{
        //    //var t = Int32.Parse(AccountCode);
        //    thirdLevel = thirdLevel.Where(x => x.SecendLevel == AccountCode).ToList();
        //    //foreach (var accountInfo in thirdLevel)
        //    //{
        //    //    accountInfo.AccountInfoTrees = GetThirdChaild(accountInfo.id).ToList();
        //    //    yield return accountInfo;
        //    //}
        //    return thirdLevel;
        //}
        //private IEnumerable<UpdateOPlState> GetThirdChaild(long AccountCode)
        //{
        //    fourthLevel = fourthLevel.Where(x => x.ThirdLevel == AccountCode).ToList();
        //    return fourthLevel;
        //}

        //public ResponseModel SaveAccountInfo(AccountInformation model)
        //{

        //    var level = _accountInformationRepository.GetAccountInfoByCode(x => x.AccountCode == model.FirstLevel && x.BankCode==PortalContext.CurrentUser.BankCode).ToList();
        //    if (level.Count() < 1)
        //    {
        //        model.AccountLevel = "1";
        //        model.FirstLevel = model.AccountCode;
        //        model.SecondLevel = "";
        //        model.ThirdLevel = "";
        //        model.FourthLevel = "";
        //        model.FifthLevel = "";
        //    }
        //    else
        //    {
        //        int BLevel = Convert.ToInt32(level[0].AccountLevel) + 1;
        //        //model.BranchLevel = level[0].BranchLevel;
        //        if (level[0].AccountLevel == "1")
        //        {
        //            model.AccountLevel = BLevel.ToString();
        //            model.FirstLevel = level[0].FirstLevel;
        //            model.SecondLevel = model.AccountCode;
        //            model.ThirdLevel = "";
        //            model.FourthLevel = "";
        //            model.FifthLevel = "";
        //        }
        //        if (level[0].AccountLevel == "2")
        //        {
        //            model.AccountLevel = BLevel.ToString();
        //            model.FirstLevel = level[0].FirstLevel;
        //            model.SecondLevel = level[0].SecondLevel;
        //            model.ThirdLevel = model.AccountCode;
        //            model.FourthLevel = "";
        //            model.FifthLevel = "";
        //        }
        //        if (level[0].AccountLevel == "3")
        //        {
        //            model.AccountLevel = BLevel.ToString();
        //            model.FirstLevel = level[0].FirstLevel;
        //            model.SecondLevel = level[0].SecondLevel;
        //            model.ThirdLevel = level[0].ThirdLevel;
        //            model.FourthLevel = model.AccountCode;
        //            model.FifthLevel = "";
        //        }
        //        if (level[0].AccountLevel == "4")
        //        {
        //            model.AccountLevel = BLevel.ToString();
        //            model.FirstLevel = level[0].FirstLevel;
        //            model.SecondLevel = level[0].SecondLevel;
        //            model.ThirdLevel = level[0].ThirdLevel;
        //            model.FourthLevel = level[0].FourthLevel;
        //            model.FifthLevel = model.AccountCode;
        //        }

        //    }

        //    var rec = _accountInformationRepository.Filter(x => x.AccountCode == model.AccountCode && x.BankCode==PortalContext.CurrentUser.BankCode).ToList();
        //    if (rec.Count <= 0)
        //    {
        //        var AInfo = new AccountInformation
        //        {
        //            BankCode=PortalContext.CurrentUser.BankCode,
        //            AccountCode = model.AccountCode.Trim(),
        //            AccountHead = model.AccountHead,
        //            Category = model.Category,
        //            AccountStatusCode = model.AccountStatusCode,
        //            AccountLevel = model.AccountLevel,
        //            FirstLevel = model.FirstLevel.Trim(),
        //            SecondLevel = model.SecondLevel.Trim(),
        //            ThirdLevel = model.ThirdLevel.Trim(),
        //            FourthLevel = model.FourthLevel.Trim(),
        //            FifthLevel = model.FifthLevel.Trim()
        //        };

        //        ResponseModel.ResponsStatus = _accountInformationRepository.Save(AInfo);
        //        ResponseModel.Message = "Chart of Accounts Saved successfully";
        //    }
        //    else
        //    {
        //        var recInfo = rec.First();
        //            //recInfo.BankCode = BankCode;
        //            //recInfo.AccountCode = model.AccountCode;
        //            recInfo.AccountHead = model.AccountHead;
        //            recInfo.Category = model.Category;
        //            recInfo.AccountStatusCode = model.AccountStatusCode;
        //            //recInfo.AccountLevel = model.AccountLevel;
        //            //recInfo.FirstLevel = model.FirstLevel;
        //            //recInfo.SecondLevel = model.SecondLevel;
        //            //recInfo.ThirdLevel = model.ThirdLevel;
        //            //recInfo.FourthLevel = model.FourthLevel;
        //            //recInfo.FifthLevel = model.FifthLevel;
        //        ResponseModel.ResponsStatus = _accountInformationRepository.Edit(recInfo);
        //        ResponseModel.Message = "Chart of Accounts Updated successfully";

        //    }
        //    return ResponseModel;

        //}

    }
}
