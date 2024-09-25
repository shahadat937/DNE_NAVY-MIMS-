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
    public class UpdateOplStateController : Controller
    {
         RM_AGBEntities db = new RM_AGBEntities();
        // GET: BranchInfo
        private readonly IUpdateOplStateManager  _updateOplStateManager;
       // private readonly ICommonCodeManager _commoncodeManager;
        public UpdateOplStateController(IUpdateOplStateManager updateOplStateManager)
        {
            _updateOplStateManager = updateOplStateManager;
        }
        public ActionResult Index(UpdateOplStateViewModel model)
        {
            
            ModelState.Clear();
            model.UpdateOPlStatesTreeViews = _updateOplStateManager.GetOplTeeView(80);

            model.AccountCategory = db.CommonCodes.Where(x => x.Type == "organization" ).ToList();
            model.AccountStatus = db.CommonCodes.Where(x => x.Type == "ShipStatus").ToList();
            
            //UpdateOPlState AI = _updateOplStateManager.GetOplByCode(model.id);
            //if (AI != null)
            //{
            
                
            //    model.id = AI.id;
            //    model.Name = AI.Name;
            //    model.LEVEL = AI.LEVEL;
            //    model.firstLevel = AI.firstLevel;
            //    model.SecendLevel = AI.SecendLevel;
            //    model.ThirdLevel = AI.ThirdLevel;

            //    if (AI.LEVEL == 1)
            //    {
            //        model.firstLevel = AI.firstLevel;
            //    }
            //    //if (AI.LEVEL == 2)
            //    //{
            //    //    model.firstLevel = AI.SecendLevel;
            //    //}
            //    if (AI.LEVEL == 3)
            //    {
            //        model.firstLevel = AI.ThirdLevel;
            //    }

            //    model.UpdateOPlState.Add(AI);

            //}
            return View(model);
        }     
        //public ActionResult Save(AccountInformationViewModel model)
        //{          
        //    ResponseModel ResponseModel = new ResponseModel();
        //    ResponseModel = _accountInfoManager.SaveAccountInfo(model);
        //    if (ResponseModel.ResponsStatus > 0)
        //    {
        //        TempData["message"] = ResponseModel.Message;
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", model);
        //}
        //public ActionResult Delete(AccountInformationViewModel model)
        //{
        //    if (model.AccountCode!=null)
        //    {
        //        var AcInfo = db.AccountInformations.Where(x => x.AccountCode == model.AccountCode && x.BankCode == PortalContext.CurrentUser.BankCode).FirstOrDefault();

        //        db.AccountInformations.Remove(AcInfo);
        //        db.SaveChanges();
        //    }
        //    TempData["message"] = "Chart Of Accounts Successfully Deleted.";
        //    return RedirectToAction("Index");
        //}
    }
}