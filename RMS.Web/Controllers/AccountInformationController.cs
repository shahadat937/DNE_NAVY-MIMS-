using RMS.BLL.IManager;
using RMS.DAL;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    public class AccountInformationController : Controller
    {
         RM_AGBEntities db = new RM_AGBEntities();
        // GET: BranchInfo
        private readonly IAccountInformationManager  _accountInfoManager;
        private readonly ICommonCodeManager _commoncodeManager;
        public AccountInformationController(IAccountInformationManager accountInfoManager)
        {
            _accountInfoManager = accountInfoManager;
        }
        public ActionResult Index(AccountInformationViewModel model)
        {
            string bankCode = PortalContext.CurrentUser.BankCode;
            ModelState.Clear();
            model.AccountInformationTreeViews = _accountInfoManager.GetAccountInfoTeeView(bankCode);

            model.AccountCategory = db.CommonCodes.Where(x => x.Type == "AccountCategory" && x.BankCode == bankCode).ToList();
            model.AccountStatus = db.CommonCodes.Where(x => x.Type == "AccountStatus" && x.BankCode==bankCode).ToList();
            if (model.AccountCode != null)
            {
                AccountInformation AI = _accountInfoManager.GetAccountInfoByCode(model.AccountCode);
                model.BankCode = AI.BankCode;
                model.AccountCode = AI.AccountCode.Trim();
                model.AccountHead = AI.AccountHead;
                model.Category = AI.Category;
                model.AccountStatusCode = AI.AccountStatusCode;
                model.AccountLevel = AI.AccountLevel;
                model.FirstLevel = AI.FirstLevel;
                model.SecondLevel = AI.SecondLevel;
                model.ThirdLevel = AI.ThirdLevel;
                model.FourthLevel = AI.FourthLevel;
                model.FifthLevel = AI.FifthLevel;
                if (AI.AccountLevel == "1")
                {
                    model.FirstLevel = AI.FirstLevel;
                }
                if (AI.AccountLevel == "2")
                {
                    model.FirstLevel = AI.FirstLevel;
                }
                if (AI.AccountLevel == "3")
                {
                    model.FirstLevel = AI.SecondLevel;
                }
                if (AI.AccountLevel == "4")
                {
                    model.FirstLevel = AI.ThirdLevel;
                }
                model.AccountInformation.Add(AI);

            }
            return View(model);
        }     
        public ActionResult Save(AccountInformationViewModel model)
        {          
            ResponseModel ResponseModel = new ResponseModel();
            ResponseModel = _accountInfoManager.SaveAccountInfo(model);
            if (ResponseModel.ResponsStatus > 0)
            {
                TempData["message"] = ResponseModel.Message;
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
        public ActionResult Delete(AccountInformationViewModel model)
        {
            if (model.AccountCode!=null)
            {
                var AcInfo = db.AccountInformations.Where(x => x.AccountCode == model.AccountCode && x.BankCode == PortalContext.CurrentUser.BankCode).FirstOrDefault();

                db.AccountInformations.Remove(AcInfo);
                db.SaveChanges();
            }
            TempData["message"] = "Chart Of Accounts Successfully Deleted.";
            return RedirectToAction("Index");
        }
    }
}