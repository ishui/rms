<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingAuditing" Src="BiddingAuditing.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingTop" Src="BiddingTop.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingAuditingManage" CodeFile="BiddingAuditingManage.aspx.cs" %>
<%@ Register TagPrefix="uc2" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BiddingAuditingManage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
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
									<uc1:WorkFlowToolbar id="WorkFlowToolbar1" runat="server"></uc1:WorkFlowToolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<TABLE class="blackbordertable" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TBODY>
								<tr>
									<td class="blackbordertd" align="center" colspan="5"><br/>
										<font size="3"><STRONG>�б굥λ����</STRONG></font><br/>
										<br/>
									</td>
								</tr>
								<tr>
									<td colspan="5">
										<uc1:BiddingTop id="BiddingTop1" runat="server"></uc1:BiddingTop></td>
								</tr>
								<TR>
									<TD class="blackbordertdcontent"><br/>
										��<br/>
										��<br/>
										��<br/>
										��<br/>
										��<br/>
										λ<br/>
										<br/>
									</TD>
									<TD width="95%" colspan="4" class="blackbordertd">
										<uc1:BiddingAuditing id="BiddingAuditing1" runat="server"></uc1:BiddingAuditing><FONT face="����"></FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent"><br/>
										��<br/>
										��<br/>
										��<br/>
										��<br/>
										��
									</TD>
									<TD width="95%" colspan="4" class="blackbordertd"><FONT face="����">
											<uc2:WorkFlowOpinion id="WorkFlowOpinion1" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent"><br/>
										��<br/>
										��<br/>
										��<br/>
										<br/>
									</TD>
									<TD width="95%" colspan="4" class="blackbordertd"><FONT face="����">
											<uc2:WorkFlowOpinion id="WorkFlowOpinion2" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent"><br/>
										��<br/>
										Լ<br/>
										��<br/>
										<br/>
									</TD>
									<TD width="95%" colspan="4" class="blackbordertd"><FONT face="����">
											<uc2:WorkFlowOpinion id="WorkFlowOpinion3" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent" style="VERTICAL-ALIGN: middle"><br/>
										��<br/>
										Ŀ<br/>
										��<br/>
										��<br/>
										<br/>
									</TD>
									<TD class="blackbordertd" width="95%" colSpan="4">
										<asp:Panel Runat="server" ID="WorkFlow4" Visible="False">
											<uc2:WorkFlowOpinion id="WorkFlowOpinion4" runat="server"></uc2:WorkFlowOpinion>
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
				</TD></TR>
				<TR>
					<TD class="blackbordertdcontent"><br/>
						��<br/>
						��<br/>
						��<br/>
						��<br/>
						<br/>
					</TD>
					<TD colspan="2" class="blackbordertd"><FONT face="����">
							<uc2:WorkFlowOpinion id="WorkFlowOpinion5" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
					<TD colspan="2" class="blackbordertd"><FONT face="����">
							<uc2:WorkFlowOpinion id="WorkFlowOpinion6" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
				</TR>
				<TR>
					<TD class="blackbordertdcontent"><br/>
						��<br/>
						��<br/>
						��<br/>
						<br/>
					</TD>
					<TD class="blackbordertd"><FONT face="����">
							<uc2:WorkFlowOpinion id="WorkFlowOpinion7" runat="server"></uc2:WorkFlowOpinion>&nbsp;</FONT></TD>
					<TD class="blackbordertd"><FONT face="����">&nbsp;
							<uc2:WorkFlowOpinion id="WorkFlowOpinion8" runat="server"></uc2:WorkFlowOpinion></FONT></TD>
					<TD class="blackbordertd"><FONT face="����">&nbsp;
							<uc2:WorkFlowOpinion id="WorkFlowOpinion9" runat="server"></uc2:WorkFlowOpinion></FONT></TD>
					<TD class="blackbordertd"><FONT face="����">&nbsp;
							<uc2:WorkFlowOpinion id="WorkFlowOpinion10" runat="server"></uc2:WorkFlowOpinion></FONT></TD>
				</TR>
				<TR>
					<TD colspan="5">
						<uc1:WorkFlowCaseState id="WorkFlowCaseState1" runat="server"></uc1:WorkFlowCaseState><FONT face="����">&nbsp;</FONT></TD>
				</TR>
			</table>
			</TD></TR>
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
			</TBODY></TABLE>
		</form>
		<script>
function BiddingEmitView(code)
{
	OpenLargeWindow('BiddingReturnModifyPage.aspx?BiddingEmitCode='+code+'&State=view','���');
}
		</script>
	</body>
</HTML>
