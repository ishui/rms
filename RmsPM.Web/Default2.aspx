<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="Infragistics.WebUI.WebDataInput.vT1" Namespace="Infragistics.WebUI.WebDataInputT1" TagPrefix="igtxt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ª„¬ –≈œ¢</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <input id="Text1" type="text" />
 <igtxt:webnumericedit Width="97px" id="txtExchangeRate" runat="server" CssClass="infra-input-nember"  MinDecimalPlaces="4"
   ImageDirectory="./images/infragistics/images/" JavaScriptFileName="./images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="./images/infragistics/20051/scripts/ig_shared.js">
                    </igtxt:webnumericedit>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /></div>
    </form>
</body>
</html>
