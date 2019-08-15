<%@ Page Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingEmitManage" CodeFile="BiddingEmitManage.aspx.cs" %>

<%@ Register Src="BiddingEmitModify.ascx" TagPrefix="uc1" TagName="BiddingEmitModify" %>
<%@ Register Src="BiddingDtlModify.ascx" TagName="BiddingDtlModify" TagPrefix="uc2" %>
<%@ Register Src="BiddingReturnModify.ascx" TagPrefix="uc1" TagName="BiddingReturnModify" %>
<!doctype html public "-//w3c//dtd html 4.0 transitional//en" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>第<%= BiddingEmitTitle %>次发标</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../images/index.css" type="text/css" rel="stylesheet" />
    <link href="../oa/ControlSource/PaginationControlStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
        type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowChange()
        {
		    OpenMiddleWindow('BiddingEmitChange.aspx?EmitCode=<%=  Request.QueryString["BiddingEmitCode"]+"" %>','发标');
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                <asp:Label ID="Lb_Title" runat="server">发标</asp:Label></td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"/></td>
                            <td class="tools-area">
                                <input class="button" id="btnSave" type="button" value=" 保存 " name="btnSave" runat="server"
                                    onserverclick="btnSave_ServerClick"/>
                                <input class="button" id="btnDel" type="button" value=" 删除 " name="btnDel" runat="server"
                                    onserverclick="btnDel_ServerClick"/>&nbsp;
                                <input class="button" id="btnChange" type="button" value=" 修改 " runat="server"
                                    onclick="ShowChange();return false;" visible="false"/>&nbsp;
                                <input class="button" id="btnOpen" type="button" value=" 开标 " runat="server"
                                    onserverclick="btnOpen_ServerClick" visible="false"/>
                                <input class="button" id="btnReSNandPWD" type="button" value=" 重发邮件" runat="server"
                                    onserverclick="btnReSNandPWD_ServerClick" visible="false"/>
                                <input class="button" id="btnClose" onclick="javascript:window.close();" type="button"
                                    value=" 关闭 " name="btnClose" runat="server" onserverclick="btnClose_ServerClick"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table" valign="top">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                <uc1:BiddingEmitModify ID="BiddingEmitModify1" runat="server"></uc1:BiddingEmitModify>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc2:BiddingDtlModify ID="BiddingDtlModify1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:BiddingReturnModify ID="BiddingReturnModify1" runat="server"></uc1:BiddingReturnModify>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12"/></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
