<%@ Page Language="c#" Inherits="RmsPM.Web.WorkPlan.Footer" CodeFile="Footer.aspx.cs" %>

<%@ Register Src="OfficeHelpLink.ascx" TagName="OfficeHelpLink" TagPrefix="uc1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Footer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">

    <script language="javascript" src="Rms.js"></script>

    <link href="Images/head.css" rel="stylesheet" type="text/css">
</head>
<body bgcolor="#014E82" leftmargin="0" topmargin="0">
    <form id="Form1" method="post" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="1" colspan="3" bgcolor="#505355">
                </td>
            </tr>
            <tr>
                <td height="1" colspan="3" bgcolor="#ffffff">
                </td>
            </tr>
            <tr>
                <td height="25" valign="middle">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="foot">
                                当前用户：<span id="spanUserName" runat="server"></span></td>
                            <td width="2">
                                <img src="images/btm_line.gif" width="2" height="15"></td>
                            <td style="cursor: hand" class="foot" title="修改密码" onClick="doChangePWD(); return false;"
                                align="left">
                                <img src="images/chgpwd.gif" height="16"></td>
                            <td style="cursor: hand" class="foot" title="修改辅助密码" onClick="doChangeOWN(); return false;">
                                <img src="images/btn_key.gif" width="16" height="16"></td>
                            <td width="2">
                                <img src="images/btm_line.gif" width="2" height="15"></td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td runat="server" id="tdInLineUser" name="tdInLineUser">
                            </td>
                            <td width="20">
                            </td>
                            <td runat="server" id="TdMsgImg">
                            </td>
                        </tr>
                    </table>
              </td>
                <td valign="middle" style="height:25px; text-align:right;">
                    <uc1:OfficeHelpLink ID="OfficeHelpLink1" Visible="false" runat="server" />
              </td>
                <td align="right" valign="top" class="foot">
                     版权所有 鼎耀</td>
                <td style="display: none">
                    <iframe name="frameBlank" src="blank.aspx" frameborder="no" width="1" height="1"
                        scrolling="no" marginwidth="0" marginheight="0"></iframe>
                </td>
            </tr>
        </table>
    </form>

    <script language="javascript">
<!--
	function doChangePWD()
	{
		OpenCustomWindow('Systems/ChgPwd.aspx?ModifyPwd=pwd','修改密码', 280, 150);
	}
	function doChangeOWN()
	{
		OpenCustomWindow('Systems/ChgPwd.aspx','修改密码', 280, 150);
	}
	function goInLinePage()
	{
		OpenCustomWindow('SendMsg/InLineUser.aspx','在线用户', 280, 400);
	}
	function goMsgManage()
	{
		OpenLargeWindow('SendMsg/SendMsgList.aspx','消息维护');
	}
	
	
//-->
    </script>

</body>
</html>
