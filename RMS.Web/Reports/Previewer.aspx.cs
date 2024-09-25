using System;
using System.Web.UI;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode;
using ReportDataSource = Microsoft.Reporting.WebForms.ReportDataSource;
namespace RMS.Web.Reports
{
    public partial class Previewer :Page
    {
        //private IRemittanceInfoManager _remittanceInfoManager = new RemittanceInfoManager();
        string securityCode;
        private string path;
        protected void Page_Load(object sender, EventArgs e)
        {
            securityCode = Request.QueryString["securityCode"];
            path = Request.QueryString["path"];
            RenderReprot();
        }
        private void RenderReprot()
        {
            rptViwer.Reset();
            rptViwer.ProcessingMode = ProcessingMode.Local;
            rptViwer.LocalReport.ReportPath = Server.MapPath(path);
            //var adfasd = _remittanceInfoManager.GetPaidRemittanceInfo(securityCode);
           // rptViwer.LocalReport.DataSources.Add(new ReportDataSource("RemittanceInfo", adfasd));
        }
    }
}