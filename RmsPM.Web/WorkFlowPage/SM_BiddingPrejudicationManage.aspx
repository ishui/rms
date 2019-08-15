<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SM_BiddingPrejudicationManage.aspx.cs" Inherits="WorkFlowPage_SM_BiddingPrejudicationManage" %>
<%@ Reference Control="~/workflowcontrol/workflowformopinion.ascx" %>
<%@ Reference Control="~/workflowoperation/sm_paymentauditing.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/workflowcasestate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CheckControl" Src="../WorkFlowOperation/CheckControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OperationControl" Src="../WorkFlowOperation/SM_BiddingPrejudicationManage.ascx" %>
<%@ Register TagPrefix="uc2" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierList" Src="../BiddingManage/UCBiddingSupplierList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierModify" Src="../BiddingManage/UCBiddingSupplierModify.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>投标单位审批表</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="25">
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td class="tools-area">
									<uc1:WorkFlowToolbar id="wftToolbar" runat="server"></uc1:WorkFlowToolbar>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top" runat="server" id="td_Print">
						<TABLE class="blackbordertable" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TBODY>
								<tr>
									<td class="blackbordertd" align="center" colspan="5"><br />
										<font size="3"><STRONG>投标单位审批表</STRONG></font><br />
										<br />
									</td>
								</tr>
								<TR>
									<TD colspan="5">
										<uc1:OperationControl id="ucOperationControl" runat="server"></uc1:OperationControl>
									</TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent"><br/>
										建<br/>
										筑<br/>
										设<br/>
										计<br/>
										部

									</TD>
									<TD width="95%" colspan="4" class="blackbordertd"><FONT face="宋体">
											<uc2:WorkFlowOpinion id="wfoOpinion1" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent"><br/>
										工<br/>
										程<br/>
										部<br/>
										<br/>
									</TD>
									<TD width="95%" colspan="4" class="blackbordertd"><FONT face="宋体">
											<uc2:WorkFlowOpinion id="wfoOpinion2" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent"><br/>
										合<br/>
										约<br/>
										部<br/>
										<br/>
									</TD>
									<TD width="95%" colspan="4" class="blackbordertd"><FONT face="宋体">
											<uc2:WorkFlowOpinion id="wfoOpinion3" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent" style="VERTICAL-ALIGN: middle"><br/>
										项<br/>
										目<br/>
										总<br/>
										监<br/>
										<br/>
									</TD>
									<TD class="blackbordertd" width="95%" colSpan="4">
										<asp:Panel Runat="server" ID="WorkFlow4" Visible="False">
											<uc2:WorkFlowOpinion id="wfoOpinion4" runat="server"></uc2:WorkFlowOpinion>
										</asp:Panel>
										    <asp:Repeater ID="rptMeetSign" Runat="server">
											    <HeaderTemplate>
												    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
											    </HeaderTemplate>
											    <ItemTemplate>
												    <tr>
													    <td><uc1:WorkFlowOpinion id="wfoItemOpinion" runat="server"></uc1:WorkFlowOpinion></td>
											    </ItemTemplate>
											    <AlternatingItemTemplate>
												    <td width="50%" style="BORDER-LEFT: #000000 1px solid; "><uc1:WorkFlowOpinion id="wfoAlternatingItemOpinion" runat="server"></uc1:WorkFlowOpinion></td>
							                    </TR>
							                    </AlternatingItemTemplate>
							                    <FooterTemplate>
					                    </TABLE>
					                    </FooterTemplate> </asp:Repeater>&nbsp;
				           </td>             
				        </tr>
				     
				        <TR>
					        <TD class="blackbordertdcontent"><br/>
						        总<br/>
						        部<br/>
						        总<br/>
						        监<br/>
						        <br/>
					        </TD>
					        <TD colspan="2" class="blackbordertd"><FONT face="宋体">
							        <uc2:WorkFlowOpinion id="wfoOpinion5" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
					        <TD colspan="2" class="blackbordertd"><FONT face="宋体">
							        <uc2:WorkFlowOpinion id="wfoOpinion6" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
				        </TR>
				        <TR>
					                <TD class="blackbordertdcontent"><br/>
						                董<br/>
						                事<br/>
						                会<br/>
						                <br/>
					                </TD>
					                <TD class="blackbordertd"><FONT face="宋体">
							                <uc2:WorkFlowOpinion id="wfoOpinion7" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
					                <TD class="blackbordertd"><FONT face="宋体">&nbsp;
							                <uc2:WorkFlowOpinion id="wfoOpinion8" runat="server"></uc2:WorkFlowOpinion></FONT></TD>
					                <TD class="blackbordertd"><FONT face="宋体">&nbsp;
							                <uc2:WorkFlowOpinion id="wfoOpinion9" runat="server"></uc2:WorkFlowOpinion></FONT></TD>
					                <TD class="blackbordertd"><FONT face="宋体">&nbsp;
							                <uc2:WorkFlowOpinion id="wfoOpinion10" runat="server"></uc2:WorkFlowOpinion></FONT></TD>
				         </TR>

								<TR>
									<TD colspan="5">
										<uc1:WorkFlowCaseState id="wfcCaseState" runat="server"></uc1:WorkFlowCaseState></TD>
								</TR>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="6" bgcolor="#e4eff6"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
