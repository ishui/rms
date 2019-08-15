<%@ Page Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingMessageManage"
    CodeFile="BiddingMessageManage.aspx.cs" %>

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
                                <uc1:WorkFlowToolbar ID="WorkFlowToolbar1" runat="server"></uc1:WorkFlowToolbar>
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
                        <tr>
                            <td class="blackbordertdcontent">
                                <br>
                                建<br>
                                筑<br>
                                设<br>
                                计<br>
                                部
                            </td>
                            <td width="95%" colspan="4" class="blackbordertd">
                                <font face="宋体">
                                    <uc1:WorkFlowOpinion ID="WorkFlowOpinion1" runat="server"></uc1:WorkFlowOpinion>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="blackbordertdcontent">
                                <br>
                                工<br>
                                程<br>
                                部<br>
                                <br>
                            </td>
                            <td width="95%" colspan="4" class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="WorkFlowOpinion2" runat="server"></uc1:WorkFlowOpinion>
                                <font face="宋体">&nbsp;</font></td>
                        </tr>
                        <tr>
                            <td class="blackbordertdcontent">
                                <br>
                                合<br>
                                约<br>
                                部<br>
                                <br>
                            </td>
                            <td width="95%" colspan="4" class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="WorkFlowOpinion3" runat="server"></uc1:WorkFlowOpinion>
                                <font face="宋体">&nbsp;</font></td>
                        </tr>
                        <tr>
                            <td class="blackbordertdcontent" style="vertical-align: middle">
                                <br>
                                项<br>
                                目<br>
                                总<br>
                                监<br>
                                <br>
                            </td>
                            <td class="blackbordertd" width="95%" colspan="4">
                                <asp:Panel runat="server" ID="WorkFlow4">
                                    <uc1:WorkFlowOpinion ID="WorkFlowOpinion4" runat="server" ></uc1:WorkFlowOpinion>
                                </asp:Panel>
                                <asp:Repeater ID="rptMeetSign" runat="server">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <uc1:WorkFlowOpinion ID="wfoItemOpinion" runat="server"></uc1:WorkFlowOpinion>
                                            </td>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <td width="50%" style="border-left: #000000 1px solid;">
                                            <uc1:WorkFlowOpinion ID="wfoAlternatingItemOpinion" runat="server"></uc1:WorkFlowOpinion>
                                        </td>
                                        </TR>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </TABLE>
                                    </FooterTemplate>
                                </asp:Repeater>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="blackbordertdcontent">
                                <br>
                                总<br>
                                部<br>
                                总<br>
                                监<br>
                                <br>
                            </td>
                            <td colspan="2" class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="WorkFlowOpinion5" runat="server"></uc1:WorkFlowOpinion>
                                <font face="宋体">&nbsp;</font></td>
                            <td colspan="2" class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="WorkFlowOpinion6" runat="server"></uc1:WorkFlowOpinion>
                                <font face="宋体">&nbsp;</font></td>
                        </tr>
                        <tr>
                            <td class="blackbordertdcontent">
                                <br>
                                董<br>
                                事<br>
                                会<br>
                                <br>
                            </td>
                            <td class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="WorkFlowOpinion7" runat="server"></uc1:WorkFlowOpinion>
                                <font face="宋体">&nbsp;</font></td>
                            <td class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="WorkFlowOpinion8" runat="server"></uc1:WorkFlowOpinion>
                                <font face="宋体">&nbsp;</font></td>
                            <td class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="WorkFlowOpinion9" runat="server"></uc1:WorkFlowOpinion>
                                <font face="宋体">&nbsp;</font></td>
                            <td class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="WorkFlowOpinion10" runat="server"></uc1:WorkFlowOpinion>
                                <font face="宋体">&nbsp;</font></td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <uc1:WorkFlowCaseState ID="WorkFlowCaseState1" runat="server"></uc1:WorkFlowCaseState>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
