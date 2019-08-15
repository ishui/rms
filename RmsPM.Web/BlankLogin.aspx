<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlankLogin.aspx.cs" Inherits="RmsPM.Web.BlankLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>

<body  onload="Refresh();">
    <form id="form1" runat="server">
    <div>
    <asp:label runat="server" text="Label" id="lblMessage"></asp:label>
        <asp:Image ID="Image1" runat="server" /><!--a href=# onclick="OpenMainWindow();">开启系统</a>-->
        <br />
        <br />
        <p>
            <strong><span style="color: #3300ff"></span></strong>
        </p>
        <p>
            &nbsp;</p>
    </div>
    </form>
    
</body>
<script language="javascript"  src="Rms.js"></script>
<script language="javascript"  >
function Refresh()
{       
    setTimeout("window.location.reload();",1200000);    
}
function OpenMainWindow()
{
   OpenNormalWindow('blanklogin.aspx?act=open','房产项目管理系统');
   return false;
}
</script>
</html>
