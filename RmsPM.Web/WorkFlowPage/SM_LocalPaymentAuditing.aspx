<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SM_LocalPaymentAuditing.aspx.cs" Inherits="WorkFlowPage_SM_LocalPaymentAuditing" %>
<%@ Reference Control="~/workflowcontrol/workflowformopinion.ascx" %>
<%@ Reference Control="~/workflowoperation/sm_paymentauditing.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/workflowcasestate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CheckControl" Src="../WorkFlowOperation/CheckControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OperationControl" Src="../WorkFlowOperation/SM_PaymentAuditing.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>中期付款审批表</title>
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
					<td class="table" vAlign="top">
						<TABLE class="blackbordertable" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TBODY>
								<tr>
									<td class="blackbordertd" align="center" colspan="5"><br />
										<font size="3"><STRONG>中期付款审批表</STRONG></font><br />
										<br />
									</td>
								</tr>
								<TR>
									<TD colspan="3">
										<uc1:OperationControl id="ucOperationControl" runat="server"></uc1:OperationControl>
									</TD>
								</TR>
								

								<TR>
									<TD colspan="3">
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
