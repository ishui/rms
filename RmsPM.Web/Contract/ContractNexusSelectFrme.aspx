<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractNexusSelectFrme.aspx.cs" Inherits="Contract_ContractNexusSelectFrme" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>相关单据</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <iframe src="ContractNexusSelect.aspx?ContractCode=<%= Request["ContractCode"]+"" %>&ProjectCode=<%= Request["ProjectCode"]+"" %>&NexusCodes=<%= Request["NexusCodes"]+"" %>" height="100%" marginheight="0" marginwidth="0" width="100%"></iframe>
    </form>
</body>
</html>
