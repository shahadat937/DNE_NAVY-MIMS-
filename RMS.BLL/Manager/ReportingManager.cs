using System.Collections.Generic;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using RMS.DAL;

namespace RMS.BLL.Manager
{
    public class ReportingManager:BaseManager,IReportingManager
    {
        private IReportingRepository _reportingRepository;
        RM_AGBEntities db = new RM_AGBEntities();
        public ReportingManager()
        {
            _reportingRepository= new ReportingRepository(Context);
        }

        public List<Reporting> GetReportingTeeView()
        {
            var firstLevel=new List<Reporting>();
            var secondLevel = new List<Reporting>();
            var thirdLevel = new List<Reporting>();

            var reportingTrees = new List<Reporting>();
            //var reportingTrees = _reportingRepository.Filter(x => x.IsEventLog && x.MultipleReport && x.QryType == PortalContext.CurrentUser.QryType);
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                reportingTrees = _reportingRepository.Filter(x => x.IsEventLog  && x.QryType=="S").ToList();
            }
            else
            {
                reportingTrees = _reportingRepository.Filter(x => x.IsEventLog).ToList();
            }
            foreach (Reporting reportingTree in reportingTrees)
            {
                if (reportingTree.ReportLevel=="1")
                {
                    firstLevel.Add(reportingTree);
                }
                if (reportingTree.ReportLevel=="2")
                {
                    secondLevel.Add(reportingTree);
                  
                }
                if (reportingTree.ReportLevel == "3")
                {
                    thirdLevel.Add(reportingTree);
                }
            }
            foreach (Reporting reporting in firstLevel)
            {
                reporting.ReportingTrees = GetFirstChaild(reporting.SlNo, secondLevel, thirdLevel).ToList();
            }

            return firstLevel;
        }
        public List<BranchInfo> ExchangeCompany()
        {
            var ec=db.BranchInfoes.Where(x => x.BranchLevel == "4" && x.FirstLevel == PortalContext.CurrentUser.BankCode ).ToList();
            return ec;
        }
        public List<BranchInfo> BankInfo()
        {
            var ec = db.BranchInfoes.Where(x => x.BranchLevel == "1" && x.BranchCode!="").ToList();
            return ec;
        }
        private IEnumerable<Reporting> GetFirstChaild(string slNo, List<Reporting> secondLevel,List<Reporting> thirdLevel)
        {
            slNo = slNo.Trim();
            secondLevel = secondLevel.Where(x => x.FirstLevel == slNo).ToList();
            foreach (var reporting in secondLevel)
            {
                reporting.ReportingTrees = GetSecondChaild(reporting.SlNo, secondLevel).ToList();
                yield return reporting;
            }

        }

        private IEnumerable<Reporting> GetSecondChaild(string slNo, List<Reporting> thirdLevel)
        {
            thirdLevel = thirdLevel.Where(x => x.SecondLevel == slNo).ToList();
            return thirdLevel;
        }
        public Reporting GetReportParameterBySlNo(string slNo)
        {
            return _reportingRepository.FindOne(x => x.SlNo == slNo);
        }
    }
}
