<%@ Reference Control="~/workflowcontrol/workflowformopinion.ascx" %>
<%@ Reference Control="~/workflowoperation/sm_contractaccountauditing.ascx" %>
<%@ Page language="c#" Inherits="WorkFlowPage_SM_ContractAccountAuditing" CodeFile="SM_ContractAccountAuditing.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="OperationControl" Src="../WorkFlowOperation/SM_ContractAccountAuditing.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CheckControl" Src="../WorkFlowOperation/CheckControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>结算审批表</title>
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
							<tr>
								<td class="blackbordertd" align="center" colspan="4"><br>
									<font size="3"><STRONG>结算审批表</STRONG></font><br>
									<br>
								</td>
							</tr>
							<TR>
								<TD colspan="4">
									<uc1:OperationControl id="ucOperationControl" runat="server"></uc1:OperationControl>
								</TD>
							</TR>
							<TR>
								<TD class="blackbordertdcontent" width="5%"><BR>
									主<BR>
									管<BR>
									估<BR>
									算<BR>
									师<BR>
								</TD>
								<TD width="95%" colspan="3" class="blackbordertd">
									<uc1:WorkFlowOpinion id="wfoOpinion1" runat="server"></uc1:WorkFlowOpinion></TD>
							</TR>
							<TR>
								<TD class="blackbordertdcontent"><BR>
									项<BR>
									目<BR>
									合<BR>
									约<BR>
									部<BR>
									经<BR>
									理<BR>
								</TD>
								<TD width="95%" colspan="3" class="blackbordertd">
									<uc1:WorkFlowOpinion id="wfoOpinion2" runat="server"></uc1:WorkFlowOpinion></TD>
							</TR>
							<TR>
                                <td class="blackbordertdcontent"><br />
							        项<br />
							        目<br />
							        总<br />
							        监<br />
						        </td>
						        <td width="95%" colspan="3">
						            <table cellpadding="0" cellspacing="0" border="0" width="100%">
						                <tr id="trMajordomo" runat="server">
						                    <td>
						                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
						                            <tr>
						                                <td id="tdMajordomo2" runat="server" class="blackbordertd">
						                                    <uc1:WorkFlowOpinion id="wfoOpinion9" runat="server"></uc1:WorkFlowOpinion>
						                                </td>
						                                <td  class="blackbordertd">
						                                    <uc1:WorkFlowOpinion id="wfoOpinion10" runat="server"></uc1:WorkFlowOpinion>
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
							</TR>
							<TR>
								<TD class="blackbordertdcontent"><BR>
									总<BR>
									部<BR>
									总<BR>
									监<BR>
								</TD>
								<TD class="blackbordertd" width="31%">
									<uc1:WorkFlowOpinion id="wfoOpinion3" runat="server"></uc1:WorkFlowOpinion></TD>
								<TD class="blackbordertd" width="32%">
									<uc1:WorkFlowOpinion id="wfoOpinion4" runat="server"></uc1:WorkFlowOpinion></TD>
								<TD class="blackbordertd" width="32%">
									<uc1:WorkFlowOpinion id="wfoOpinion5" runat="server"></uc1:WorkFlowOpinion></TD>
							</tr>
							<TR>
								<TD class="blackbordertdcontent"><BR>
									董<BR>
									事<BR>
									会<BR>
									<BR>
								</TD>
								<td colspan="3">
								    <table width="100%" cellpadding="0" cellspacing="0" border="0">
								        <tr>
								            <TD class="blackbordertd">
									            <uc1:WorkFlowOpinion id="wfoOpinion6" runat="server"></uc1:WorkFlowOpinion></TD>
								            <TD class="blackbordertd">
									            <uc1:WorkFlowOpinion id="wfoOpinion7" runat="server"></uc1:WorkFlowOpinion></TD>
								        </tr>
								        <tr>
								            <TD class="blackbordertd">
									            <uc1:WorkFlowOpinion id="wfoOpinion8" runat="server"></uc1:WorkFlowOpinion></TD>
								            <TD class="blackbordertd">
									            <uc1:WorkFlowOpinion id="wfoOpinion11" runat="server"></uc1:WorkFlowOpinion></TD>
								        </tr>
								    </table>
								</td>
							</tr>
							<TR>
								<TD colspan="4">
									<uc1:WorkFlowCaseState id="wfcCaseState" runat="server"></uc1:WorkFlowCaseState></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
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
			</table>
		</form>
	</body>
</HTML>
