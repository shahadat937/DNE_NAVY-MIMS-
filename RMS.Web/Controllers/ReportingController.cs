using System;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Web.Models.ViewModel;
using RMS.DAL;
using RMS.Utility;
using System.Linq;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class ReportingController : Controller
    {
        RM_AGBEntities db = new RM_AGBEntities();
        private readonly IReportingManager _reportingManager;
        private ICommonCodeManager _commonCodeManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private IDamageMachineriesInfoManager _damageMachineriesInfoManager;
        public ReportingController(IReportingManager reportingManager, ICommonCodeManager commonCodeManager, IControlShipInfoManager controlShipInfoManager, IDamageMachineriesInfoManager damageMachineriesInfoManager)
        {
            _reportingManager = reportingManager;
            _commonCodeManager = commonCodeManager;
            _controlShipInfoManager = controlShipInfoManager;
            _damageMachineriesInfoManager = damageMachineriesInfoManager;
        }
        public ActionResult Index(ReportingViewModel model)
        {
            model.Ranks = _commonCodeManager.GetCommonCodeByType("RANK");
            model.NameOfCourses = _commonCodeManager.GetCommonCodeByType("NameofCourse");
            model.ReportingTreeViews = _reportingManager.GetReportingTeeView();
            return View(model);
        }

        public ActionResult ReportView(ReportingViewModel model)
        {
            //string BankCode = PortalContext.CurrentUser.BankCode.Trim();

            model.ExchangeCompany = _reportingManager.ExchangeCompany();
            //model.ShipInfoes = _controlShipInfoManager.Totalship();
            model.ShipInfoes = _controlShipInfoManager.ShipBranchInfo();
            model.MonthsList = _damageMachineriesInfoManager.returnReportNoByParameter("Monthly").ToList();
            model.QuarterList = _damageMachineriesInfoManager.returnReportNoByParameter("Quaterly").ToList();
            model.ReturnYearList = _damageMachineriesInfoManager.returnReportYears().ToList();
            model.ShipNameInfoes = _controlShipInfoManager.FMsgShipBankInfo();
            Reporting reportingObj = _reportingManager.GetReportParameterBySlNo(model.SlNo) ?? new Reporting();
            model.ReportName = reportingObj.ReportName;
            model.Para1 = reportingObj.Para1;
            model.Para2 = reportingObj.Para2;
            model.Para3 = reportingObj.Para3;
            model.Para4 = reportingObj.Para4;
            if (!model.Para1.Equals("0"))
            {
                model.Parameters.Add(model.Para1, "");
            }
            if (!model.Para2.Equals("0"))
            {
                model.Parameters.Add(model.Para2, "");
            }
            if (!model.Para3.Equals("0"))
            {
                model.Parameters.Add(model.Para3, "");
            }
            if (!model.Para4.Equals("0"))
            {
                model.Parameters.Add(model.Para4, "");
            }
            model.SlNo = model.SlNo;
            return View(model);
        }

        public ActionResult AspReport(ReportingViewModel model)
        {
            if (Session["paramertes"] == null)
            {
                Session["paramertes"] = model.Parameters;
                Session["slNo"] = model.SlNo;
            }
            var st = "~/Reports/TreeReportViwer.aspx";
            return Redirect("~/Reports/TreeReportViwer.aspx");
        }
        //public ActionResult RMSReport(RemittanceViewModel model)
        //{
        //    DateTime fromDate = Convert.ToDateTime(model.FromDate);
        //    DateTime toDate = Convert.ToDateTime(model.ToDate);
        //    //Response.Redirect(@"~/ASPReport/aspReportView.aspx?id=" + Id);
        //    return Redirect("~/Reports/FrmReportViewer.aspx?dateFrom="+fromDate+"?dateTo="+toDate);
        //}

    }
}