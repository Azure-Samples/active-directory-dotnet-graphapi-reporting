<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SampleAPIProject.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<script src="JavaScript.js"></script>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>&nbsp;WebApp for Graph Reporting API</h1>
        <p>
            <asp:DropDownList ID="DropDownReports" runat="server" OnSelectedIndexChanged="DropDownReports_SelectedIndexChanged" CausesValidation="True" AutoPostBack="True">
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/$metadata?api-version=1.5">Get the OData Service Metadata Document (CSDL)</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/audit?api-version=1.5">Get Audit report for last 30 days</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/audit/?api-version=1.5&amp;$filter=eventTime%20gt%202014-03-01%20and%20eventTime%20lt%202015-04-23">Get Audit report filtered by date range</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/accountProvisioning?api-version=1.5">Get Account Provisioning report</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/signInsFromUnknownSources?api-version=1.5">Get Sign Ins From Unknown Sources  report</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/signInsFromIPAddressesWithSuspiciousActivity?api-version=1.5">Get Sign Ins From IP Addresses With Suspicious Activity report</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/signInsFromMultipleGeographies?api-version=1.5">Get Sign Ins From Multiple Geographies report</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/signInsFromPossiblyInfectedDevices?api-version=1.5">Get Sign Ins From Possibly Infected Devices report</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/irregularSignInActivity?api-version=1.5">Get Irregular Sign In Activity report</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/usersWithAnomalousSignInActivity?api-version=1.5">Get Users With Anomalous Sign In Activity report</asp:ListItem>
                <asp:ListItem Value="https://graph.windows.net/bltest.onmicrosoft.com/reports/signInsAfterMultipleFailures?api-version=1.5">Get Sign Ins After Multiple Failures report</asp:ListItem>
            </asp:DropDownList>
        </p>
        <h3>OData URL:</h3>
        <asp:TextBox ID="txtUrl" runat="server" Width="1252px"></asp:TextBox>
        <br/><br/>
        <asp:Button ID="btnStart" runat="server" Text="Get Results" OnClick="btnStart_Click" OnClientClick="return doHourGlass();"/>

        <br/>
        <asp:Label ID="LblError" ForeColor="red" runat="server" Text=""></asp:Label>
    </div>
        <div style="margin-top: 0px">
        <h3>Access Token Received: </h3>
        <asp:Label ID="lblToken" runat="server" Text=""></asp:Label>
     
         <h3>Result:</h3>
         <asp:TextBox ID="txtResults" runat="server" TextMode="MultiLine" Height="1000px" Width="1105px" style="margin-top: 3px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
