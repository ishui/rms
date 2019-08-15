<%@ Reference Control="~/workflowcontrol/workflowformopinion.ascx" %>
<%@ Reference Control="~/workflowoperation/sm_contractauditing.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CheckControl" Src="../WorkFlowOperation/CheckControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OperationControl" Src="../WorkFlowOperation/SM_ContractAuditing.ascx" %>
<%@ Page Language="c#" Inherits="WorkFlowPage_SM_ContractAuditing" CodeFile="SM_ContractAuditing.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>合同审批表</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <uc1:WorkFlowToolbar ID="wftToolbar" runat="server"></uc1:WorkFlowToolbar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table" valign="top">
                    <table class="blackbordertable" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="blackbordertd" align="center" colspan="5">
                                <br>
                                <font size="3"><strong>合同审批表</strong></font><br>
                                <br>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" class="blackbordertd">
                                <uc1:OperationControl ID="ucOperationControl" runat="server"></uc1:OperationControl>
                            </td>
                        </tr>
                        <tr>
                            <td class="blackbordertdcontent" width="5%">
                                <br>
                                建<br>
                                筑<br>
                                设<br>
                                计<br>
                                部
                            </td>
                            <td width="95%" colspan="4" class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="wfoOpinion1" runat="server"></uc1:WorkFlowOpinion>
                                &nbsp;</td>
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
                                <uc1:WorkFlowOpinion ID="wfoOpinion2" runat="server"></uc1:WorkFlowOpinion>
                                &nbsp;</td>
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
                                <uc1:WorkFlowOpinion ID="wfoOpinion3" runat="server"></uc1:WorkFlowOpinion>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="blackbordertdcontent">
                                <br>
                                法<br>
                                务<br>
                                部<br>
                                <br>
                            </td>
                            <td width="95%" colspan="4" class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="wfoOpinion4" runat="server"></uc1:WorkFlowOpinion>
                                &nbsp;</td>
                        </tr>
                        <tr>
							<td class="blackbordertdcontent"><br />
								项<br />
								目<br />
								总<br />
								监<br />
							</td>
							<td width="95%" colspan="4">
							    <table cellpadding="0" cellspacing="0" border="0" width="100%">
							        <tr id="trMajordomo" runat="server">
							            <td>
							                <table cellpadding="0" cellspacing="0" border="0" width="100%">
							                    <tr>
							                        <td id="tdMajordomo2" runat="server" class="blackbordertd">
							                            <uc1:WorkFlowOpinion id="wfoOpinion12" runat="server"></uc1:WorkFlowOpinion>
							                        </td>
							                        <td  class="blackbordertd">
							                            <uc1:WorkFlowOpinion id="wfoOpinion13" runat="server"></uc1:WorkFlowOpinion>
							                        </td>
							                    </tr>
							                </table>
							            </td>
							        </tr>
							        <tr id="trMeetSign" runat="server" visible="false">
							            <td class="blackbordertd">
                                            <asp:Repeater ID="rptMeetSign" Runat="server">
									            <HeaderTemplate>
										            <table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
									            </HeaderTemplate>
									            <ItemTemplate>
										            <tr>
											            <td>
												            <uc1:WorkFlowOpinion id="wfoItemOpinion" runat="server"></uc1:WorkFlowOpinion>
											            </td>
									            </ItemTemplate>
									            <AlternatingItemTemplate>
											            <td width="50%" style="BORDER-LEFT: #000000 1px solid; ">
												            <uc1:WorkFlowOpinion id="wfoAlternatingItemOpinion" runat="server"></uc1:WorkFlowOpinion>
											            </td>
										            </TR>
									            </AlternatingItemTemplate>
									            <FooterTemplate>
										            </TABLE>
									            </FooterTemplate>
								            </asp:Repeater>
							            </td>
							        </tr>
							    </table>
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
                            <td colspan="4">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="blackbordertd" width="31%">
                                            <uc1:WorkFlowOpinion ID="wfoOpinion5" runat="server"></uc1:WorkFlowOpinion>
                                            &nbsp;
                                        </td>
                                        <td class="blackbordertd" width="32%">
                                            <uc1:WorkFlowOpinion ID="wfoOpinion6" runat="server"></uc1:WorkFlowOpinion>
                                            &nbsp;
                                        </td>
                                        <td class="blackbordertd" width="32%">
                                            <uc1:WorkFlowOpinion ID="wfoOpinion7" runat="server"></uc1:WorkFlowOpinion>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
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
                                <uc1:WorkFlowOpinion ID="wfoOpinion8" runat="server"></uc1:WorkFlowOpinion>
                                &nbsp;</td>
                            <td class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="wfoOpinion9" runat="server"></uc1:WorkFlowOpinion>
                                &nbsp;</td>
                            <td class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="wfoOpinion10" runat="server"></uc1:WorkFlowOpinion>
                                &nbsp;</td>
                            <td class="blackbordertd">
                                <uc1:WorkFlowOpinion ID="wfoOpinion11" runat="server"></uc1:WorkFlowOpinion>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <uc1:WorkFlowCaseState ID="wfcCaseState" runat="server"></uc1:WorkFlowCaseState>
                                &nbsp;</td>
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
</body>
</html>
