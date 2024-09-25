using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode;

namespace RMS.Web.Reports
{
    public partial class FrmReportViewer :Page
    {
        //private IRemittanceInfoManager _remittanceInfoManager=new RemittanceInfoManager();
        private Dictionary<string, string> paramertes=new Dictionary<string, string>();
        string securityCode;
        string toDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            rptViwer.ShowPrintButton = true;
            securityCode = Request.QueryString["securityCode"];
            if (!IsPostBack)
            {
                 paramertes = Session["paramertes"] as Dictionary<string, string> ??new Dictionary<string, string>();
                 RenderReprot();
                if (paramertes.Any())
                { 
                    Session["paramertes"] = null;
                }
            }
        }
        private void RenderReprot()
        {


            foreach (var paramerte in paramertes)
            {
                //fromDate = Convert.ToDateTime(paramerte.Value);
            }
            rptViwer.Reset();
            rptViwer.ProcessingMode = ProcessingMode.Local;
            rptViwer.LocalReport.ReportPath = Server.MapPath("~/Reports/ExpressVoucher.rdlc");
            //var adfasd=  _remittanceInfoManager.GetPaidRemittanceInfo(securityCode);
            //rptViwer.LocalReport.DataSources.Add(new ReportDataSource("RemittanceInfo", adfasd));
        }
    }
}