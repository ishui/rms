<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignDocumentModify.aspx.cs"
    Inherits="DesignDocumentManage_DesignDocumentModify" %>

<%@ Register Src="DesignDocument.ascx" TagName="DesignDocument" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25" runat="server" id="tdtitle"></td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <input name="btnModify" id="btnModify" type="button" value=" 修改 " class="button"
                                    runat="server" onserverclick="btnModify_ServerClick">
                                <input name="btnSave" id="btnSave" type="button" value=" 保存 " class="button" runat="server"
                                    onserverclick="btnSave_ServerClick">
                                <input name="btnDel" id="btnDel" type="button" value=" 删除 " class="button" runat="server"
                                    onserverclick="btnDel_ServerClick">
                                <input name="btnAuding" id="btnAuding" type="button" value="提交申请" class="button"
                                    runat="server">
                                <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                    onclick="javascript:window.close();"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table" valign="top" height="100%" style="height:100%;">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:DesignDocument ID="DesignDocument1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <font face="宋体"></font>
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
                                <img height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="6" bgcolor="#e4eff6">
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript">
    function OpenAuding()
	{
	    if('<%=Request["Type"] + "" %>' == "s")
	    {
		    OpenFullWindow('DesignDocumentManage.aspx?DesignDocumentCode=<%= this.DesignDocument1.ApplicationCode%>','施工设计评审');
		}
		else
		{
		    OpenFullWindow('DesignDocumentManage1.aspx?DesignDocumentCode=<%= this.DesignDocument1.ApplicationCode%>','方案设计评审');
		}
	}
    </script>
</body>
</html>
