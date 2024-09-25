using RMS.BLL.IManager;
using RMS.DAL;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    public class CommonCodeController : Controller
    {
        RM_AGBEntities db = new RM_AGBEntities();
        private readonly ICommonCodeManager _commonCode;
        private readonly IBranchInfoManager _branchInfoManager;
        public CommonCodeController(ICommonCodeManager CommonCodeManager, IBranchInfoManager branchInfoManager)
        {
            _commonCode = CommonCodeManager;
            _branchInfoManager = branchInfoManager;

        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult Index(CommonCodeViewModel model)
        {
            //Type = model.Type;
            string BankCode = _branchInfoManager.FineOneByBranchInfoIdentity(PortalContext.CurrentUser.BankCodeIdentity).BranchCode;
            //string BankCode = PortalContext.CurrentUser.BankCode;
            model.CommonCode = _commonCode.GetCommonCode(model, BankCode);            
            //model.GetCommonCodeByType = _commonCode.GetCommonType(BankCode);
            var CCByCompareInGroups = from cc in db.CommonCodes                                     
                                      group cc by cc.Type into grp
                                      select grp.OrderBy(a => a.Type).FirstOrDefault();
            model.CommonCodeByType = CCByCompareInGroups.ToList();
            ModelState.Clear();
            if (model.Code != null)
            {
                CommonCode CC = _commonCode.FindOne(Convert.ToInt32(model.Code));
                model.CommonCodeID = CC.CommonCodeID;
                model.Code = CC.Code;
                model.Type = CC.Type;
                model.TypeValue = CC.TypeValue;
                model.DisplayCode = CC.DisplayCode;
                model.Status = CC.Status;
                model.BTypeValue = CC.BTypeValue;
                model.CommonCode.Add(CC);               

            }
            model.CommonCodeTreeViews = _commonCode.GetCommonCodeTeeView(BankCode, model.Type);
            return View(model);
        }      
        public ActionResult Save(CommonCodeViewModel model)
        {
            //string ty = "?Type=";
            string BankCode = _branchInfoManager.FineOneByBranchInfoIdentity(PortalContext.CurrentUser.BankCodeIdentity).BranchCode;
            //string BankCode = PortalContext.CurrentUser.BankCode;
            model.BankCode = BankCode;
            ResponseModel ResponseModel = new ResponseModel();
            ResponseModel = _commonCode.SaveCommonCode(model);
            if (ResponseModel.ResponsStatus > 0)
            {
                TempData["message"] = ResponseModel.Message;
                return RedirectToAction("Index", new { Type=model.Type});
            }
            return View( "Index",model);
        }
        public ActionResult Delete(CommonCodeViewModel model)
        {
            if (model.Code != null)
            {
                string BankCode = _branchInfoManager.FineOneByBranchInfoIdentity(PortalContext.CurrentUser.BankCodeIdentity).BranchCode;
                var CCode = db.CommonCodes.Where(x => x.CommonCodeID == model.CommonCodeID && x.Type==model.Type && x.BankCode == BankCode).FirstOrDefault();

                db.CommonCodes.Remove(CCode);
                db.SaveChanges();
                TempData["message"] = "Common Code Successfully Deleted.";
                return RedirectToAction("Index", new { Type = model.Type });
            }            
            return View("Index",model);
        }

    }
}