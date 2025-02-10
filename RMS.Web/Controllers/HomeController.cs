using Antlr.Runtime;
using DocumentFormat.OpenXml.Spreadsheet;
using HasCode;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WebForms;
using RMS.BLL.DashBoardTreeView.ProcurementByDne;
using RMS.BLL.DashBoardTreeView.RefitDocking;
using RMS.BLL.DashBoardTreeView.ShipInactive;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.DAL;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace RMS.Web.Controllers
{

    [Authorize]
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IUserURLRightManager _userURLRightManager;
        private IUserLogInfoManager _userLogInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private ICommonCodeManager _commonCodeManager;
        private IShipInactiveManager _shipInactiveManager;
        private IUserManager _userManager;
        private IRefitDockingScheduleManager _refitDockingScheduleManager;
        private IProcurementScheduleManager _procurementScheduleManager;
        private IShipcommOrgnaizationManager _shipcommOrgnaizationManager;
        private readonly IUpdateOplStateManager _updateOplStateManager;
        private readonly IvwNotificationManager _vwNotificationManager;
        private readonly IBranchInfoManager _branchInfoManager;
        private readonly IMachineryInfoManager _machineryInfoManager;
        private readonly IAsAsInfoManager _asAsInfoManager;
        private readonly IShipInfoManager _shipInfoManager;
        private readonly IMessageInfoManager _messageInfoManager;
        private readonly IDamageMachineriesInfoManager _damageMachineriesInfoManager;
        private readonly IDocumentationManager _documentationManager;
        RM_AGBEntities db = new RM_AGBEntities();
        public HomeController(IUserURLRightManager userURLRightManager, IUserLogInfoManager userLogInfoManager, IUserManager userManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager, IShipInactiveManager shipInactiveManager, IRefitDockingScheduleManager refitDockingScheduleManager, IProcurementScheduleManager procurementScheduleManager, IShipcommOrgnaizationManager shipcommOrgnaizationManager, IUpdateOplStateManager updateOplStateManager, IvwNotificationManager vwNotificationManager, IBranchInfoManager branchInfoManager, IMachineryInfoManager machineryInfoManager, IAsAsInfoManager asAsInfoManager, IShipInfoManager shipInfoManager, IMessageInfoManager messageInfoManager, IDamageMachineriesInfoManager damageMachineriesInfoManager, IDocumentationManager documentationManager)
        {
            _userURLRightManager = userURLRightManager;
            _userLogInfoManager = userLogInfoManager;
            _userManager = userManager;
            _controlShipInfoManager = controlShipInfoManager;
            _commonCodeManager = commonCodeManager;
            _shipInactiveManager = shipInactiveManager;
            _refitDockingScheduleManager = refitDockingScheduleManager;
            _procurementScheduleManager = procurementScheduleManager;
            _shipcommOrgnaizationManager = shipcommOrgnaizationManager;
            _updateOplStateManager = updateOplStateManager;
            _vwNotificationManager = vwNotificationManager;
            _branchInfoManager = branchInfoManager;
            _machineryInfoManager = machineryInfoManager;
            _asAsInfoManager = asAsInfoManager;
            _shipInfoManager = shipInfoManager;
            _messageInfoManager = messageInfoManager;
            _damageMachineriesInfoManager = damageMachineriesInfoManager;
            _documentationManager = documentationManager;

        }
        public async Task<ActionResult> Index(UpdateOplStateViewModel model)
        {
            model.Bulletin = await _messageInfoManager.GetCurrentMessageByRoleId(PortalContext.CurrentUser.RoleId);

            if (PortalContext.CurrentUser.UserRole == "DNE User" || PortalContext.CurrentUser.UserRole == "Admin Authority" || PortalContext.CurrentUser.UserRole == "PSO")
            {
                long controlShipInfoId = PortalContext.CurrentUser.ShipId;
                long branchInfoIdentity = PortalContext.CurrentUser.BranchInfoIdentity;
                model.DashboardFor = "Admin";
                model.OrgBranch = _branchInfoManager.GetOrgFromBankInfo();





                model.UpdateChartsOfAccounts = _updateOplStateManager.GetAll();
                var data = db.ProcurementByDNEs;
                IProcurementTarget chTarget = new ProcurementChartofAccountAdapter(data.ToList());
                var client = new ProcurementTreeViewBuilder(chTarget);
                model.ProcurementChartsOfAccounts = client.GetChartofAccount();

                var val = db.ShipInactiveDescriptions;
                IShipInactiveTarget valTarget = new ShipInactiveChartofAccountAdapter(val.ToList());
                var ss = new ShipInactiveTreeViewBuilder(valTarget);
                model.ShipInactiveChartsOfAccounts = ss.GetChartofAccount();

                var refitDocking = db.ApprovedRefitDockingSchedules;
                IRefitDockingTarget refitTarget = new RefitDockingChartofAccountAdapter(refitDocking.ToList());
                var re = new RefitDockingTreeViewBuilder(refitTarget);
                model.ApprovedRefitDockingSchedules = re.GetChartofAccount();
                //model.machineryInfos = _machineryInfoManager.GetMachinaryInfoGroupBy();

                model.EquipmentTypes = _machineryInfoManager.GetEquipmentType();


                model.asAndAsInfos = _asAsInfoManager.GetAll().ToList();
                var AsAsfirstsByCompareInGroups = from asAs in model.asAndAsInfos
                                                  group asAs by asAs.ControlShipInfoId into grp
                                                  select grp.OrderByDescending(a => a.AsAndAsIdentity).First();

                model.asAndAsInfos = AsAsfirstsByCompareInGroups.ToList();

                model.shipInfos = _shipInfoManager.GetAll().ToList();
                var ConShip = _controlShipInfoManager.FMsgShipBankInfo();

                model.shipInactives = _shipInactiveManager.GetNonOpsShip().ToList();


                //Message Info
                //List<MessageInfoView> messageInfoes = new List<MessageInfoView>();
                //foreach (var item in Msg)
                //{
                //    MessageInfoView messageInfoView = new MessageInfoView
                //    {
                //        MessageInfoIdentity = item.MessageInfoIdentity,
                //        Message = item.Message,
                //        MessageFromDate = item.MessageFromDate,
                //        MessageToDate = item.MessageToDate,
                //        MessageForAll = item.MessageForAll,
                //        Remark = item.Remark,
                //        UserId = item.UserId,
                //        LastUpdate = item.LastUpdate
                //    };
                //    messageInfoes.Add(messageInfoView);
                //}

                //Monthly Return
                //var monthlyReturns = _damageMachineriesInfoManager.GetMonthlyReturnByReturnStatus().OrderBy(x => x.ShipId).ToList();





                int totalShipCount;
                int totalBoatCount;
                int totalPontoonCount;
                int totalEstablishmentCount;
                if (controlShipInfoId != 0 || PortalContext.CurrentUser.UserName == "dne")
                {
                    totalShipCount = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1056).Count();
                    totalEstablishmentCount = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1057).Count();
                    totalPontoonCount = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1058).Count();
                    totalBoatCount = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1059).Count();
                    model.branchInfos = _branchInfoManager.GetOrganizationList();
                    model.controlShipInfos = _controlShipInfoManager.GetAll().Where(x => x.Status == 942 && x.ShipType == 1056).ToList();
                    model.refitDockingSchedules = _refitDockingScheduleManager.GetAll().ToList();
                    model.Documentations = _documentationManager.GetAll();
                    model.MonthlyReturns = _damageMachineriesInfoManager.GetMonthlyReturnByReturnStatus().OrderBy(x => x.ShipId).ToList();
                    model.quaterlyReturns = _damageMachineriesInfoManager.GetAllQuaterlyReturn().OrderBy(x => x.ShipId).ToList();
                }
                else
                {
                    List<ControlShipInfo> controlShipInfoModel = new List<ControlShipInfo>();
                    controlShipInfoModel = _controlShipInfoManager.GetControlShipInfoByOrganizationId(branchInfoIdentity).ToList();
                    totalShipCount = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1056 && x.Organization == branchInfoIdentity).Count();
                    totalEstablishmentCount = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1057 && x.Organization == branchInfoIdentity).Count();
                    totalPontoonCount = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1058 && x.Organization == branchInfoIdentity).Count();
                    totalBoatCount = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1059 && x.Organization == branchInfoIdentity).Count();
                    model.branchInfos = _branchInfoManager.GetOrganizationList().Where(x => x.BranchInfoIdentity == branchInfoIdentity).ToList();
                    model.controlShipInfos = _controlShipInfoManager.GetAll().Where(x => x.Status == 942 && x.ShipType == 1056 && x.Organization == branchInfoIdentity).ToList();
                    model.refitDockingSchedules = _refitDockingScheduleManager.GetAll().Where(x => x.ControlShipInfo.Organization == branchInfoIdentity).ToList();
                    model.Documentations = _documentationManager.GetAll().Where(x => x.ControlShipInfo.Organization == branchInfoIdentity).ToList();
                    model.MonthlyReturns = _damageMachineriesInfoManager.GetMonthlyReturnByReturnStatus().Where(x => x.ControlShipInfo.Organization == branchInfoIdentity).OrderBy(x => x.ShipId).ToList();
                    model.quaterlyReturns = _damageMachineriesInfoManager.GetAllQuaterlyReturn().Where(x => x.ControlShipInfo.Organization == branchInfoIdentity).OrderBy(x => x.ShipId).ToList();

                }

                model.TotalShip = totalShipCount;
                model.TotalEstablishment = totalEstablishmentCount;
                model.TotalPontoon = totalPontoonCount;
                model.TotalBoat = totalBoatCount;


                var DcByCompareInGroups = from d in model.Documentations
                                          group d by d.ShipId into grp
                                          select grp.OrderByDescending(a => a.DocumentationIdentity).First();
                model.Documentations = DcByCompareInGroups.ToList();
                foreach (var item in model.Documentations)
                {
                    item.ImageUrl = "/uploads/" + item.ImageUrl.Substring(10);

                }
                //model.vwNotifications = _vwNotificationManager.GetAll().Where(x => x.NxtLifeTime > 0).ToList();
            }

            else if (PortalContext.CurrentUser.UserRole == "Directorate" || PortalContext.CurrentUser.UserRole == "Dockyard")
            {
                model.DashboardFor = "DirectorateAndDockyardUser";
                model.UserFullName = PortalContext.CurrentUser.UserFullName;
                model.shipInactives = _shipInactiveManager.GetNonOpsShip().ToList();
                model.TotalShip = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1056).Count();
                model.TotalNonOplShip = _controlShipInfoManager.GetAll().Where(x => x.ShipType == 1056 && x.Status == 942).Count();

            }
            else
            {
                model.DashboardFor = "User";
                model.UserFullName = PortalContext.CurrentUser.UserFullName;
                model.BranchName = PortalContext.CurrentUser.BranchName;
                model.DistrictName = PortalContext.CurrentUser.DistrictName;
                model.ShipId = PortalContext.CurrentUser.ShipId;
                model.EquipmentTypes = _machineryInfoManager.GetEquipmentType();
                var monthlyReturns = _damageMachineriesInfoManager.GetMonthlyReturnByShipId(model.ShipId).OrderBy(x => x.ShipId).ToList();
                model.monthlyReturnCount = monthlyReturns.Count();
                model.quaterlyReturnCount = _damageMachineriesInfoManager.GetQuaterlyReturnByReturnByShipId(model.ShipId).ToList().Count();
                var machineInfo = _machineryInfoManager.GetMachinariesInfo(model.ShipId);
                model.machineryInfos = _machineryInfoManager.GetMachinariesInfoByShipId(model.ShipId);
                model.asAndAsInfos = _asAsInfoManager.GetAllByShipId(model.ShipId);
                model.Documentations = _documentationManager.GetAll();

                foreach (var item in model.Documentations)
                {
                    if (item.ImageUrl != null)
                    {
                        item.ImageUrl = "/uploads/" + item.ImageUrl.Substring(10);
                    }

                }
            }

            return View(model);

        }
        public ActionResult NotificationLifeTime(UpdateOplStateViewModel model)
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            //var Notfi = _vwNotificationManager.GetAll().Where(x => x.NxtLifeTime > 0).ToList();
            //var query = (from post in Notfi join meta in ConShip on post.ShipName equals meta.ControlName select post).ToList();
            //model.vwNotifications = query;

            return View(model);

        }
        public ActionResult NotificationRefit(UpdateOplStateViewModel model)
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            var Notfi = _vwNotificationManager.GetRefitInfo();
            var query = (from post in Notfi join meta in ConShip on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            model.vwRefitDokingNotifications = query;
            return View(model);

        }
        public ActionResult NotificationDoking(UpdateOplStateViewModel model)
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            var Notfi = _vwNotificationManager.GetDockingInfo();
            var query = (from post in Notfi join meta in ConShip on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            model.vwRefitDokingNotifications = query;
            return View(model);

        }
        public ActionResult SearchByKeyLifeTime(UpdateOplStateViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.ToLower();
                //model.vwNotifications = _vwNotificationManager.GetAll().Where(x => x.NxtLifeTime > 0 && (x.ShipName.ToLower().Contains(searchKey) || x.Machinery.ToLower().Contains(searchKey) || x.Model.ToLower().Contains(searchKey))).ToList();
                //model.MachineryInfos = _machineryInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName == null ? "".ToLower().Contains(searchKey) : x.ControlShipInfo.ControlName.ToLower().Contains(searchKey) || x.Machinery == null ? "".ToLower().Contains(searchKey) : x.Machinery.ToLower().Contains(searchKey) || x.Model == null ? "".ToLower().Contains(searchKey) : x.Model.ToLower().Contains(searchKey)).OrderByDescending(x => x.LastUpdate).ToList();
            }
            else
            {
                //model.vwNotifications = _vwNotificationManager.GetAll().Where(x => x.NxtLifeTime > 0).ToList(); ;
            }
            return View("NotificationLifeTime", model);
        }
        public ActionResult SearchByKeyRefit(UpdateOplStateViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.ToLower();
                model.vwRefitDokingNotifications = _vwNotificationManager.GetRefitInfo().Where(x => x.ShipName.ToLower().Contains(searchKey)).ToList();
                //model.MachineryInfos = _machineryInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName == null ? "".ToLower().Contains(searchKey) : x.ControlShipInfo.ControlName.ToLower().Contains(searchKey) || x.Machinery == null ? "".ToLower().Contains(searchKey) : x.Machinery.ToLower().Contains(searchKey) || x.Model == null ? "".ToLower().Contains(searchKey) : x.Model.ToLower().Contains(searchKey)).OrderByDescending(x => x.LastUpdate).ToList();
            }
            else
            {
                model.vwRefitDokingNotifications = _vwNotificationManager.GetRefitInfo().ToList(); ;
            }
            return View("NotificationRefit", model);
        }
        public ActionResult SearchByKeyDoking(UpdateOplStateViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.ToLower();
                model.vwRefitDokingNotifications = _vwNotificationManager.GetDockingInfo().Where(x => x.ShipName.ToLower().Contains(searchKey)).ToList();
                //model.MachineryInfos = _machineryInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName == null ? "".ToLower().Contains(searchKey) : x.ControlShipInfo.ControlName.ToLower().Contains(searchKey) || x.Machinery == null ? "".ToLower().Contains(searchKey) : x.Machinery.ToLower().Contains(searchKey) || x.Model == null ? "".ToLower().Contains(searchKey) : x.Model.ToLower().Contains(searchKey)).OrderByDescending(x => x.LastUpdate).ToList();
            }
            else
            {
                model.vwRefitDokingNotifications = _vwNotificationManager.GetDockingInfo().ToList(); ;
            }
            return View("NotificationDoking", model);
        }
        public ActionResult TreeViewIndex(UpdateOplStateViewModel model)
        {
            model.UpdateChartsOfAccounts = _updateOplStateManager.GetAll();
            return View(model);
        }

        public ActionResult ProcurementIndex(UpdateOplStateViewModel model)
        {
            var data = db.ProcurementByDNEs;
            IProcurementTarget chTarget = new ProcurementChartofAccountAdapter(data.ToList());
            var client = new ProcurementTreeViewBuilder(chTarget);
            model.ProcurementChartsOfAccounts = client.GetChartofAccount();
            return View(model);
        }

        public ActionResult ShipInactive(UpdateOplStateViewModel model)
        {
            var data = db.ShipInactiveDescriptions;
            IShipInactiveTarget chTarget = new ShipInactiveChartofAccountAdapter(data.ToList());
            var client = new ShipInactiveTreeViewBuilder(chTarget);
            model.ShipInactiveChartsOfAccounts = client.GetChartofAccount();
            return View(model);
        }
        public ActionResult ShipEdit(HomeInformation model)
        {

            return RedirectToAction("IndexDisplay", "ControlShipInfo");
        }

        public ActionResult ShipInactiveEdit(HomeInformation model)
        {

            return RedirectToAction("Index", "ShipInactive");
        }

        public ActionResult Download3()
        {


            var localReport = new LocalReport();
            var reportData = new List<vwProcurementSchedule>();
            var list = _procurementScheduleManager.GetAll();
            foreach (var item in list)
            {
                var oldData = new vwProcurementSchedule
                {
                    //OrgName = PortalContext.CurrentUser.BankName,
                    //OfficeName = PortalContext.CurrentUser.BankName,
                    //Parameter = "Ship's Information",
                    //ReportName = "Machinery Parameters - ", //+ item.ShipName,
                    ProcurementId = item.ProcurementId,
                    ProcurementType = item.ProcurementType,
                    Description = item.Description ?? "",
                    Qty = item.Qty,
                    proType = item.CommonCode.TypeValue,
                    //YearFrom = Convert.ToDateTime(dateFrom).Year,
                    //YearTo = Convert.ToDateTime(dateTo).Year,
                    EstTotalPrice = item.EstTotalPrice,
                    Currency = item.Currency,
                    AIPReceived = item.AIPReceived,
                    SpecSentToDTS = item.SpecSentToDTS,
                    SpecSentToNSSD = item.SpecSentToNSSD,
                    SpecSentToDGDP = item.SpecSentToDGDP,
                    TenderOpened = item.TenderOpened,
                    QuatationRec = item.QuatationRec,
                    AccepSentToDTS = item.AccepSentToDTS,
                    AccepSentToNSSD = item.AccepSentToNSSD,
                    AccepSentToDGDP = item.AccepSentToDGDP,
                    AccepSentToAFD = item.AccepSentToAFD,
                    ApprovedByAFD = item.ApprovedByAFD,
                    ConttractSigned = item.ConttractSigned,
                    Taka = item.Taka,
                    DTSReTender = item.DTSReTender,
                    DGDPReTender = item.DGDPReTender,
                    NSSDReTender = item.NSSDReTender,
                    TenderOpenReTender = item.TenderOpenReTender,
                    QuotRecReTender = item.QuotRecReTender,
                    Remark = item.Remark,
                    UserId = PortalContext.CurrentUser.UserName,
                    LastUpdate = DateTime.Now,

                };
                reportData.Add(oldData);
            }
            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportData;



            localReport.ReportPath = Server.MapPath("~/Reports/BNS/procurement.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);

        }
        //public ActionResult ALLOrg(HomeInformation model)
        //{
        //    model.ShipcommOrgnaizations = _shipcommOrgnaizationManager.GetAll().Where(x => x.Organization !=null).ToList();
        //    return View(model);
        //}

        //public ActionResult AllCommandShow(HomeInformation model)
        //{
        //    model.AllShipShow = _controlShipInfoManager.GetAll();
        //    return View(model);
        //}
        public ActionResult AllShipShipShow(HomeInformation model)
        {
            model.AllShipShow = _controlShipInfoManager.GetAll().Where(x => x.ParentCode == 0).ToList();
            return View(model);
        }

        //public ActionResult AllOplShipShow(HomeInformation model)
        //{
        //    model.AllShipShow = _controlShipInfoManager.GetAll().Where(x => x.ParentCode == 0 && x.Status == 941).ToList();

        //    model.AllShipShow =
        //       (from con in con join c in _commonCodeManager on con.Status equals c.CommonCodeID  where con.ControlLevel="1");
        //    return View(model);
        //}
        //public ActionResult AllNonOplShipShow(HomeInformation model)
        //{
        //    model.AllShipShow = _controlShipInfoManager.GetAll().Where(x => x.ParentCode == 0 && x.Status == 942).ToList();
        //    return View(model);
        //}

        //public ActionResult EIndex()
        //{
        //    string q = "Select * from vwHOBalance where BankCode='" + PortalContext.CurrentUser.BankCode + "'";
        //    IQueryable<vwHOBalance> data = db.vwHOBalances.Where(x => x.BankCode == PortalContext.CurrentUser.BankCode).OrderBy(x => x.AgentBranchCode);
        //    model.VwHoBalances = data.ToList();
        //    model.DailyTransactions = _iTransManager.GetVTransactionList();
        //    model.VwHoBalances = _vwHoBalanceManager.GetCoverBalances(PortalContext.CurrentUser.BankCode);
        //    return View();
        //}
        //public ActionResult Download()
        //{
        //    var localReport = new LocalReport();
        //    var model = new ShipInactiveViewModel { ShipInactives = _shipInactiveManager.GetAll() };

        //    var reportDataList = model.ShipInactives.Select(item => new vwShipInactive()
        //    {
        //        OrgName = "Bangladesh Navy",
        //        OfficeName = PortalContext.CurrentUser.BankName,
        //        Parameter = "Ship's Information",
        //        ReportName = "Machinery Parameters - ",
        //        SInactiveIdentity = item.SInactiveIdentity,
        //        ShipName = item.ControlShipInfo.ControlName,
        //        todate = DateTime.Now.Date,
        //        CrashDetails = item.CrashDetails,
        //        InactiveDate = item.InactiveDate,
        //        TakenStap = item.TakenStap,
        //        Reference = item.Reference,
        //        Command = item.Command,
        //        UserID = PortalContext.CurrentUser.UserName,
        //        LastUpdate = DateTime.Now

        //    }).ToList();


        //    var reportDataSource = new ReportDataSource { Name = "DataSet1" };
        //    localReport.DataSources.Add(reportDataSource);
        //    reportDataSource.Value = reportDataList;

        //    localReport.ReportPath = Server.MapPath("~/Reports/BNS/Incative.rdlc");
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;
        //    Warning[] warnings;
        //    string[] streams;

        //    byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
        //        out fileNameExtension, out streams, out warnings);
        //    Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);
        //    return File(renderedBytes, fileNameExtension);

        //}

        public ActionResult Download1()
        {
            var localReport = new LocalReport();
            var model = new RefitDockingScheduleViewModel { RefitDockingSchedules = _refitDockingScheduleManager.GetAll() };

            var reportDataList = model.RefitDockingSchedules.Select(item => new vwRefitDockingSchedule()
            {
                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Information",
                ReportName = "Machinery Parameters - ",
                RefitDockingIdentity = item.RefitDockingIdentity,
                //Conr= item.ControlShipInfo.ControlName,
                //Con = item.ShipNameIdentity,
                LastRefitFrom = item.LastRefitFrom,
                LastRefitTo = item.LastRefitTo,
                LastDockingFrom = item.LastDockingFrom,
                LastDockingTo = item.LastDockingTo,
                PNextRefitFrom = item.PNextRefitFrom,
                PNextRefitTo = item.PNextRefitTo,
                PNextDockingFrom = item.PNextDockingFrom,
                PNextDockingTo = item.PNextDockingTo,
                BranchCode = item.Docked,
                UserId = PortalContext.CurrentUser.UserName,
                LastUpdate = DateTime.Now

            }).ToList();


            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/NextRefitDockingSchudule.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);

        }


        //public ActionResult TwoFactorLogin(TwoFactorLoginViewModel model, string returnUrl)
        //{
        //    Random r = new Random();
        //    int num = r.Next();
        //    String otp = num.ToString("D6");
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.From = new MailAddress("kamrul@infinitytechltd.com");
        //    mailMessage.To.Add("kamrul@infinitytechltd.com");
        //    mailMessage.Subject = "Subject";
        //    mailMessage.Body = "This OTP IS "+otp;
        //    string usernm = "kamrul@infinitytechltd.com";
        //    string pass = "B@ng@123#";

        //    SmtpClient smtpClient = new SmtpClient();
        //    smtpClient.Host = "mail.infinitytechltd.com";
        //    smtpClient.Port = 587;
        //    smtpClient.UseDefaultCredentials = false;
        //    smtpClient.Credentials = new NetworkCredential(usernm, pass);
        //    smtpClient.EnableSsl = false;

        //    try
        //    {
        //        smtpClient.Send(mailMessage);
        //        Console.WriteLine("Email Sent Successfully.");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error: " + ex.Message);
        //    }


        //    return   Redirect(returnUrl ?? Url.Action("TwoFactorLogin", "Home"));
        //}  




        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            // Check CAPTCHA
            if (Session["Captcha"] == null || model.CaptchaInput != Session["Captcha"].ToString())
            {
                GenerateCaptcha(); // Regenerate CAPTCHA
                TempData["Msg"] = "Invalid CAPTCHA. Please try again.";
                return RedirectToAction("Login", "Account");
            }
            Random r = new Random();
            int num = r.Next(100000, 1000000);
            String otp = num.ToString();

            var scb = new IBSHasCode();
            string passd = "";
            passd = scb.CreateDoubleHas(model.UserName, "Infinity@123");


            var getUser = db.Users.Single(x => x.UserName == model.UserName);


            // LMS Mail system start 
            // Mail Part
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("10.10.10.21");
            //mail.From = new MailAddress("bncl@navy.mil.bd");
            //mail.To.Add(mailId);
            //mail.Subject = "Library Management System";
            //mail.IsBodyHtml = true;
            //string body = string.Empty;



            //using (StreamReader reader = new StreamReader(Server.MapPath("~/App_Data/Template/EmailTemplateForFine.txt")))
            //{
            //    body = reader.ReadToEnd();

            //}
            //body = body.Replace("{MemberName}", memName); //replacing the required things  

            //body = body.Replace("{BookId}", bookid);

            //body = body.Replace("{BookName}", bookName);
            //body = body.Replace("{issueDate}", issueDate);
            ////  mail.Body = "Your Member Id =" + model.MemberInfo.MemberId + " \n Your User Id = " + model.MemberOfficialInfo.CodeNumber + " \n Your PassWord = 123456";
            //mail.Body = body;
            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("bncl@navy.mil.bd", "Bn@357#L");
            //SmtpServer.EnableSsl = true;
            // LMS mail system end
            int isMailSend = 0;
            MailMessage mailMessage = new MailMessage();
            //SmtpClient smtpClient = new SmtpClient("10.10.10.21");
            mailMessage.From = new MailAddress("infinitytechnologybdltd@gmail.com");
            mailMessage.To.Add(getUser.EmailAddress);
            mailMessage.Subject = "MIMS OTP";
            mailMessage.Body = "Hi " + getUser.EmailAddress + "<br><br>";
            mailMessage.Body += "We received your request for a single-use code to use with your MIMS account.<br><br>";
            mailMessage.Body += "Your single-use code is: <b>" + otp + "</b>";

            mailMessage.IsBodyHtml = true; // Set this property to true to interpret the Body as HTML

            string usernm = "infinitytechnologybdltd@gmail.com";
            string pass = "dgkc perk doma suzo";

            //var smtpClient = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(usernm, pass)
            //};
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(usernm, pass);
            //smtpClient.Credentials = new NetworkCredential("mims_otp@navy.mil.bd", "Mim$#0tp00");
            smtpClient.EnableSsl = true;







            string msg = "";
            IBSHasCode sc = new IBSHasCode();
            string Pass = "";
            if (model.Password.Length > 4)
            {
                Pass = sc.CreateDoubleHas(model.UserName, model.Password);
                if (ModelState.IsValid)
                {

                    var sd = db.Users.Where(x => x.UserName == model.UserName).ToList();
                    if (sd.Count > 0)
                    {
                        if (sd[0].ApprovedUser == true)
                        {
                            if (Membership.ValidateUser(model.UserName, model.Password))
                            {
                                //if (sd[0].UserName == model.UserName && sd[0].Password == Pass)
                                //{

                                //int nod = Convert.ToInt16(DateTime.Now - sd[0].LasUpdateDate);
                                DateTime ud = Convert.ToDateTime(sd[0].LasUpdateDate);
                                DateTime nd = DateTime.Now;
                                TimeSpan ts = nd.Subtract(ud);
                                int nod = ts.Days;
                                //if (sd[0])

                                if (sd[0].PasswordValidityInDays < nod)
                                {
                                    var user = db.Users.Single(x => x.UserName == model.UserName);
                                    user.ApprovedUser = false;
                                    //db.SaveChanges();
                                    int IsSaved = db.SaveChanges();
                                    if (IsSaved > 0)
                                    {
                                        msg = "User Password Validity is Expird. Please Contact with Admin.";
                                        //return RedirectToAction("Login", "Account");
                                    }

                                }
                                else
                                {
                                    string clientIP = Request.GetOwinContext().Request.RemoteIpAddress;
                                    //string clientName = RequestContext.Principal.Identity.Name;
                                    string clientMachineName = "";
                                    //(Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                                    //PhysicalAddress[] physicalAddress = StoreNetworkInterfaceAddresses();
                                    //string ipAddress = GetIP(clientMachineName);
                                    int isSaved = _userLogInfoManager.SaveToEvent(clientMachineName, clientIP, "");
                                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                                    Session["username"] = model.UserName;
                                    Session["password"] = model.Password;
                                    Session["otp"] = otp;
                                    //return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                                    try
                                    {
                                        smtpClient.Send(mailMessage);
                                        Console.WriteLine("Email Sent Successfully.");
                                        isMailSend = 1;

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error: " + ex.Message);
                                    }
                                    if (isMailSend == 1)
                                    {
                                        return Redirect(returnUrl ?? Url.Action("TwoFactorLogin", "Account"));
                                    }
                                    else
                                    {
                                        TempData["Msg"] = "There is an issue With Sending OTP! Please Contact Authority!";
                                        return RedirectToAction("Login", "Account");
                                    }


                                    //  return RedirectToAction("Login", "Account");

                                }

                                //}
                                //else
                                //{
                                //    TempData["Msg"] = "User or Password Not Valid";

                                //}

                            }
                            else
                            {
                                int isUpdated = _userManager.UpdateFailedAttempt(model.UserName);
                                string errorMessage = _userManager.GetErrorMessage(model.UserName);
                                TempData["Msg"] = errorMessage;
                                return RedirectToAction("Login", "Account");
                            }
                            TempData["Msg"] = msg;
                        }
                        else
                        {
                            TempData["Msg"] = "User Not Activate. Please Contact with Admin.";
                            return RedirectToAction("Login", "Account");
                        }
                    }
                    else
                    {
                        TempData["Msg"] = "User Id Not Valid.";
                        return RedirectToAction("Login", "Account");
                    }
                    TempData["Msg"] = msg;

                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            else
            {
                TempData["Msg"] = "Please Input Valid Password.";
                return RedirectToAction("Login", "Account");
            }
            model.Password = "";
            model.UserName = "";


            return RedirectToAction("Login", "Account");
            //return View(model);
        }

        private void GenerateCaptcha()
        {
            Random rand = new Random();
            int num1 = rand.Next(1, 10);
            int num2 = rand.Next(1, 10);

            // Store both the question and the answer in session
            Session["CaptchaQuestion"] = $"{num1} + {num2} = ?";
            Session["Captcha"] = (num1 + num2).ToString();
        }
       
        
        public ActionResult tfa(string code)
        {

            try
            {
                if (String.IsNullOrEmpty(Session["username"] + "") || String.IsNullOrEmpty(Session["password"] + ""))
                    return RedirectToAction("Login");

                if (Session["otp"].ToString() == code.ToString())
                {
                    Session["otp"] = null;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Msg"] = "OTP Invalid. Please Enter Correct OTP.";
                    return RedirectToAction("TwoFactorLogin", "Account");
                }
            }
            catch (Exception er)
            {
                ModelState.AddModelError("", er.Message);
            }
            return RedirectToAction("Login", "Account");
        }


        //public ActionResult TwoFactorAuthenticate()
        //{
        //    var token = Request["CodeDigit"];
        //    TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
        //    string UserUniqueKey = Session["UserUniqueKey"].ToString();
        //    bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, token);
        //    if (isValid)
        //    {
        //        HttpCookie TwoFCookie = new HttpCookie("TwoFCookie");
        //        string UserCode = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(UserUniqueKey)));

        //        TwoFCookie.Values.Add("UserCode", UserCode);
        //        TwoFCookie.Expires = DateTime.Now.AddMinutes(45);
        //        Response.Cookies.Add(TwoFCookie);
        //        Session["IsValidTwoFactorAuthentication"] = true;
        //        return RedirectToAction("UserProfile", "Login");
        //    }
        //    return RedirectToAction("Login", "Login");
        //}

        //private string GetIP(string machineName)
        //{

        //    IPHostEntry ipEntry = Dns.GetHostEntry(machineName);
        //    IPAddress[] addr = ipEntry.AddressList;
        //    return addr[addr.Length - 1].ToString();
        //}
        //public static PhysicalAddress[] StoreNetworkInterfaceAddresses()
        //{
        //    IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
        //    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        //    if (nics == null || nics.Length < 1)
        //    {
        //        Console.WriteLine("  No network interfaces found.");
        //        return null;
        //    }
        //    PhysicalAddress[] addresses = new PhysicalAddress[nics.Length];
        //    int i = 0;
        //    foreach (NetworkInterface adapter in nics)
        //    {
        //        IPInterfaceProperties properties = adapter.GetIPProperties();
        //        PhysicalAddress address = adapter.GetPhysicalAddress();
        //        byte[] bytes = address.GetAddressBytes();
        //        PhysicalAddress newAddress = new PhysicalAddress(bytes);
        //        addresses[i++] = newAddress;
        //    }
        //    return addresses;
        //}
        public ActionResult Menu()
        {
            PortalUser portalUser = PortalContext.CurrentUser;
            if (portalUser.IsValidate)
            {
                var userRightList = _userURLRightManager.GetUserRightTreeView(PortalContext.CurrentUser.UserRole).OrderBy(x => x.MenuId);
                //var value = userRightList.OrderBy(x => x.MenuId);
                return PartialView("_menu", userRightList);
            }
            return Redirect(Url.Action("Login", "Home"));


        }
        public ActionResult Notification()
        {
            if (PortalContext.CurrentUser.IsValidate == false)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var notification = new NotificationCustomModel();
                notification = _vwNotificationManager.GetNotification();
                if (notification != null)
                {
                    ViewBag.count = notification.RunningHour + notification.NextDockingFrom + notification.NextRefitFrom + notification.MejorDemand + notification.SignalInfo;
                }
                else
                {
                    notification = new NotificationCustomModel
                    {
                        Count = 0,
                        RunningHour = 0,
                        NextDockingFrom = 0,
                        NextRefitFrom = 0,
                        MejorDemand = 0,
                        SignalInfo = 0
                    };
                }
                return PartialView("_Notification", notification);
            }
        }
        [HttpGet]
        public ActionResult DashboardIndex(UpdateOplStateViewModel model)
        {
            var notification = new NotificationCustomModel();
            notification = _vwNotificationManager.GetNotification();
            ViewBag.count = notification.RunningHour + notification.NextDockingFrom + notification.NextRefitFrom + notification.MejorDemand + notification.SignalInfo;
            model.NumberOfNotification = ViewBag.count;

            return View(model);
        }

    }
}