using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using RMS.Model;

namespace RMS.Web.Reports
{
    public partial class GLViwer : System.Web.UI.Page
    {
        private List<uvGLTransaction> uvGLTransactions;
        private string reportPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                uvGLTransactions = Session["model"] as List<uvGLTransaction>;
                reportPath = Session["path"].ToString();
                RenderReport();
            }
        }

        private void RenderReport()
        {
            rptViwer.Reset();
            rptViwer.ProcessingMode = ProcessingMode.Local;
            rptViwer.LocalReport.ReportPath = Server.MapPath(reportPath);
            rptViwer.LocalReport.DataSources.Add(new ReportDataSource("vGLTransaction", uvGLTransactions));
        }
    }
}