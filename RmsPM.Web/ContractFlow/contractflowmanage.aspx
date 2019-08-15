<%@ Register TagPrefix="uc1" TagName="ContractInfoControl" Src="ContractInfoControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ContractAuditingControl" Src="ContractAuditingControl.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ContractFlow.ContractFlowManage" CodeFile="ContractFlowManage.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ContractModifyButtonControl" Src="ContractModifyButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowOpinion.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ContractFlowManage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/JoyBox.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">∫œÕ¨…Í«Îµ•</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><uc1:workflowtoolbar id="WorkFlowToolbar1" runat="server" ontoolbarcommand="WorkFlowToolbar1_ToolbarCommand"></uc1:workflowtoolbar>
						<uc1:ContractModifyButtonControl id="ContractModifyButtonControl1" runat="server"></uc1:ContractModifyButtonControl></td>
				</tr>
			</table>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD vAlign="top" width="100%">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td><uc1:contractinfocontrol id="ContractInfoControl1" runat="server"></uc1:contractinfocontrol></td>
							</tr>
							<tr>
								<td>
									<uc1:ContractAuditingControl id="ContractAuditingControl1" runat="server"></uc1:ContractAuditingControl></td>
							</tr>
						</table>
					</TD>
					<TD vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="300" border="0">
							<tr>
								<td><uc1:workflowopinion id="WorkFlowOpinion1" runat="server"></uc1:workflowopinion></td>
							</tr>
							<tr>
								<td><uc1:workflowopinion id="WorkFlowOpinion2" runat="server"></uc1:workflowopinion></td>
							</tr>
							<tr>
								<td><uc1:workflowopinion id="WorkFlowOpinion3" runat="server"></uc1:workflowopinion></td>
							</tr>
							<tr>
								<td><uc1:workflowopinion id="WorkFlowOpinion4" runat="server"></uc1:workflowopinion></td>
							</tr>
							<tr>
								<td><uc1:workflowopinion id="WorkFlowOpinion5" runat="server"></uc1:workflowopinion></td>
							</tr>
							<tr>
								<td><uc1:workflowopinion id="WorkFlowOpinion6" runat="server"></uc1:workflowopinion></td>
							</tr>
							<tr>
								<td><uc1:workflowopinion id="WorkFlowOpinion7" runat="server"></uc1:workflowopinion></td>
							</tr>
							<tr>
								<td><uc1:workflowopinion id="WorkFlowOpinion8" runat="server"></uc1:workflowopinion></td>
							</tr>
							<tr>
								<td><uc1:workflowopinion id="WorkFlowOpinion9" runat="server"></uc1:workflowopinion></td>
							</tr>
							<tr>
								<td>
									<uc1:WorkFlowOpinion id="WorkFlowOpinion10" runat="server"></uc1:WorkFlowOpinion></td>
							</tr>
							<tr>
								<td>
									<uc1:WorkFlowOpinion id="WorkFlowOpinion11" runat="server"></uc1:WorkFlowOpinion></td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr>
					<td colspan="2">
						<uc1:WorkFlowCaseState id="WorkFlowCaseState1" runat="server"></uc1:WorkFlowCaseState></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
