<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="RmsPM.Web.Default3" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href="~/Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="~/Images/infra.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <igtxt:webnumericedit id="txtPerCash1" runat="server" CssClass="infra-input-nember" Width="50" JavaScriptFileNameCommon="./images/infragistics/20051/scripts/ig_shared.js"
											            JavaScriptFileName="./images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="./images/infragistics/images/"
											            MinDecimalPlaces="One"></igtxt:webnumericedit>
    </div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click1" />
    </form>
</body>
</html>
