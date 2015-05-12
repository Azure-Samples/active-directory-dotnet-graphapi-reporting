<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SampleAPIProject.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<script src="JavaScript.js"></script>
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>&nbsp;WebApp for Graph Reporting API</h1>
        <p>
            <asp:DropDownList ID="DropDownReports" runat="server" OnSelectedIndexChanged="DropDownReports_SelectedIndexChanged" AutoPostBack="True"/>
        </p>
        <h3>OData URL: (Change to test any report) </h3>
        <asp:TextBox ID="txtUrl" runat="server" Width="1252px"></asp:TextBox>

        <br/><br/>
        <asp:Button ID="btnStart" runat="server" Text="Get Results" OnClick="btnStart_Click" OnClientClick="return doHourGlass();"/>

        <br/>
        <asp:Label ID="LblError" ForeColor="red" runat="server" Text=""></asp:Label>
    </div>
        <div>
         <h3><asp:Label ID="lblResult" runat="server" Text="Result: "></asp:Label></h3>
         <asp:TextBox ID="txtResults" runat="server" TextMode="MultiLine" Height="1000px" Width="1105px" style="margin-top: 3px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
