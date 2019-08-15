<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XCN_biddingmessagemanage.aspx.cs" Inherits="BiddingManage_XCN_biddingmessagemanage" %>


<%@ Register TagPrefix="uc1" TagName="Control_BiddingEmitMoney" Src="Control_BiddingEmitMoney.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingMessageModify" Src="BiddingMessageModify.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>BiddingMessageManage</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25" colspan="5">
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <uc1:workflowtoolbar id="WorkFlowToolbar1" runat="server" Visible=false></uc1:workflowtoolbar><INPUT class="button" id="btnSave" type="button" value=" 保 存 " name="btnSave" runat="server" onserverclick="btnSave_ServerClick" ><INPUT class="button" id="btnUpdate" type="button" value=" 修 改 " name="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick" ><INPUT class="button" id="btnClose" onclick="javascript:window.close();" type="button"
	value="关闭窗口" runat="server">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table" valign="top" colspan="5">
                    <table class="blackbordertable" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="blackbordertd" align="center" colspan="5">
                                <br>
                                <font size="3"><strong>中标通知书审批表</strong></font><br>
                                <br>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <font face="宋体">
                                    <uc1:BiddingMessageModify ID="BiddingMessageModify1" runat="server"></uc1:BiddingMessageModify>
                                    <uc1:Control_BiddingEmitMoney ID="Control_BiddingEmitMoney1" runat="server"></uc1:Control_BiddingEmitMoney>
                                </font>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
