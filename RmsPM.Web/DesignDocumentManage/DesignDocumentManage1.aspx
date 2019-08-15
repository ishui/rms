<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignDocumentManage1.aspx.cs"
    Inherits="RmsPM.Web.LeaveManage.DesignDocumentManage_DesignDocumentManage1" %>

<%@ Register Src="DesignDocumentForFlow.ascx" TagName="DesignDocument" TagPrefix="uc2" %>
<%@ Register Src="../WorkFlowControl/workflowformopinion.ascx" TagName="workflowformopinion"
    TagPrefix="uc3" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowOpinion.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" height="25">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif">
                                方案设计评审</td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <uc1:WorkFlowToolbar ID="WorkFlowToolbar1" runat="server" OnToolbarCommand="WorkFlowToolbar1_ToolbarCommand">
                                </uc1:WorkFlowToolbar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table" valign="top" id="td_Print">
                    <table id="Table1" class="blackbordertable" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="center" colspan="2" class="blackbordertd">
                                <br>
                                <strong><font size="5">方案设计评审</font></strong>
                                <br>
                                <br>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="blackbordertd">
                                <font face="宋体">
                                    <uc2:DesignDocument ID="DesignDocument1" runat="server" />
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="blackbordertd">
                                <uc3:workflowformopinion ID="Workflowformopinion1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="blackbordertd">
                                <uc3:workflowformopinion ID="Workflowformopinion2" runat="server" />
                            </td>
                            <td class="blackbordertd">
                                <uc3:workflowformopinion ID="Workflowformopinion3" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="blackbordertd">
                                <uc3:workflowformopinion ID="Workflowformopinion4" runat="server" />
                            </td>
                            <td class="blackbordertd">
                                <uc3:workflowformopinion ID="Workflowformopinion5" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="blackbordertd">
                                <uc3:workflowformopinion ID="Workflowformopinion6" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="blackbordertd">
                                <uc3:workflowformopinion ID="Workflowformopinion7" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="blackbordertd">
                                <uc3:workflowformopinion ID="Workflowformopinion8" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="blackbordertd">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td><uc1:WorkFlowCaseState ID="WorkFlowCaseState1" runat="server"></uc1:WorkFlowCaseState></td></tr>
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
</body>
</html>
