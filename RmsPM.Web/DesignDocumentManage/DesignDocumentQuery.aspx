<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignDocumentQuery.aspx.cs"
    Inherits="RmsPM.Web.DesignDocumentManage.DesignDocumentManage_DesignDocumentQuery" %>

<%@ Register Src="DesignDocumentList.ascx" TagName="DesignDocumentList" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../oa/ControlSource/PaginationControlStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" style="height:100%;">
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span
                                    id="spanTitle"> 方案设计查询</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <input name="btnAddNew" id="btnAddNew" type="button" value=" 新增 " class="button"
                                    onclick="javascript:OpenNew();return false;" runat="server">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table" valign="top"  height="100%">
                    <table class="search-area" cellspacing="0" cellpadding="0" width="100%" border="0" height="100%">
                        <tr>
                            <td>
                                <table height="100%">
                                    <tr>
                                        <td nowrap>
                                            名 称：</td>
                                        <td nowrap>
                                            <input id="txtTitle" type="text" runat="server" class="input" /></td>
                                        <td nowrap>
                                            部&nbsp;&nbsp;&nbsp; 门：</td>
                                        <td nowrap>
                                            <uc1:InputUnit ID="Unit" runat="server" ImagePath="../Images/"></uc1:InputUnit>
                                        </td>
                                        <td nowrap>
                                            日&nbsp;&nbsp;&nbsp; 期：</td>
                                        <td nowrap>
                                            <font face="宋体">
                                                <cc1:Calendar ID="QueryDate" runat="server" Value="" CalendarResource="../Images/CalendarResource/">
                                                </cc1:Calendar>
                                            </font>
                                        </td>
                                        <td nowrap>
                                            <font face="宋体"></font>
                                        </td>
                                        <td nowrap>
                                            <font face="宋体"></font>
                                        </td>
                                        <td>
                                            <font face="宋体"></font>
                                        </td>
                                        <td>
                                            <font face="宋体"></font>
                                        </td>
                                        <td>
                                            <input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
                                                onserverclick="btnSearch_ServerClick"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <uc1:DesignDocumentList ID="DesignDocumentList1" runat="server"></uc1:DesignDocumentList>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12"></td>
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

    <script>
	function OpenNew()
	{
		OpenFullWindow('DesignDocumentModify.aspx?State=edit&ProjectCode=<%= Request["ProjectCode"]+"" %>&Type=f&Title=方案设计新增','方案设计新增');
	}
	
    </script>

</body>
</html>
