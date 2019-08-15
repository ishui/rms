<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<%@ Register Src="../BiddingControl/BiddingEmit.ascx" TagName="BiddingEmit" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
        <link href="../images/index.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
<asp:DropDownList ID="UnitDropDownList" runat="server" OnSelectedIndexChanged="UnitDropDownList_SelectedIndexChanged"
 OnTextChanged="UnitDropDownList_SelectedIndexChanged" AutoPostBack="true">    
    <asp:ListItem>222</asp:ListItem>
    <asp:ListItem>3333</asp:ListItem>
    <asp:ListItem>34444</asp:ListItem>
</asp:DropDownList>
          
    </div>
    </form>
</body>
</html>

