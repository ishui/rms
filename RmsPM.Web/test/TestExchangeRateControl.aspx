<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestExchangeRateControl.aspx.cs" Inherits="test_TestExchangeRateControl" %>
<%@ Register Src="../UserControls/inputExchangeRate.ascx" TagName="inputExchangeRate" TagPrefix="uc1" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    		<link href="../images/index.css" type="text/css" rel="stylesheet" />
		<link href="../images/infra.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:inputExchangeRate ID="ucExchangeRate" runat="server" />
    </div>
    </form>
</body>
</html>
