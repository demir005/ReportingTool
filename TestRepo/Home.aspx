<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TestRepo.Home" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Dashboard.v18.2.Web.WebForms, Version=18.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Izvjestaj SaoOsig</title>
    <style type="text/css">
        .auto-style1 {
            margin-left: 160px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p class="auto-style1">
                Izaberi Izvjestaj :
                <br class="auto-style1" />
                <asp:DropDownList ID="ddlReportName" runat="server" Width="168px" DataTextField="Value" DataValueField="Key" OnSelectedIndexChanged="ddlReportName_SelectedIndexChanged" Height="16px">
                </asp:DropDownList>
                <br class="auto-style1" />
                Org Unit
                <br class="auto-style1" />
                <asp:DropDownList ID="ddlOrgUnit" runat="server" Height="17px" OnSelectedIndexChanged="ddlOrgUnit_SelectedIndexChanged" Width="157px" AppendDataBoundItems="True">                
                </asp:DropDownList>
                &nbsp;
                <br class="auto-style1" />
                Status:
                <br class="auto-style1" />
                <asp:DropDownList ID="ddlStatus" runat="server"  Height="16px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged1" Width="147px" AppendDataBoundItems="True">                               
                </asp:DropDownList>
                &nbsp;
            </p>
            <p class="auto-style1">
                <br class="auto-style1" />
                <%--<a href="javascript:ShowReport()">Show</a>--%>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Filter" Width="224px" />

            </p>
        </div>

        <%--<asp:GridView ID="gridview1" runat="server"></asp:GridView>--%>
        <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server" DisableHttpHandlerValidation="False" Height="1065px" ColorScheme="dark">
        </dx:ASPxWebDocumentViewer>


        <script type="text/javascript">
            function GetSelectedReportName() {
                return document.getElementById('<%=ddlReportName.ClientID%>').options[document.getElementById('<%=ddlReportName.ClientID%>').selectedIndex].value;
               // var ddlStatus = document.getElementById('<%=ddlStatus.ClientID%>').options[document.getElementById('<%=ddlStatus.ClientID%>').selectedIndex].value;
                //var ddlOrgUnit = document.getElementById('<%=ddlOrgUnit.ClientID%>').options[document.getElementById('<%=ddlOrgUnit.ClientID%>').selectedIndex].value;
            }

            function ShowReport() {
                var reportName = GetSelectedReportName();
                ASPxWebDocumentViewer1.OpenReport(reportName);
            }
        </script>

    </form>
</body>
</html>
