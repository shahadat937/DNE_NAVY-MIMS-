<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Previewer.aspx.cs" Inherits="RMS.Web.Reports.Previewer" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <rsweb:ReportViewer ID="rptViwer" runat="server" ZoomMode="PageWidth" Height="1200px" WaitMessageFont-Size="14pt" Width="100%"
         BackColor="white" BorderColor="white" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" AsyncRendering="False" Font-Names="Verdana" ShowBackButton="true" ShowPrintButton="true" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
