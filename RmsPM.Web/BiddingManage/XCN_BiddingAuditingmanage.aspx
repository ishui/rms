<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XCN_BiddingAuditingmanage.aspx.cs" Inherits="BiddingManage_XCN_BiddingAuditingmanage" %>

<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingAuditing" Src="BiddingAuditing.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingTop" Src="BiddingTop.ascx" %>

<%@ Register TagPrefix="uc2" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>XCN_BiddingAuditingManage</title>
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
									<uc1:workflowtoolbar id="WorkFlowToolbar1" runat="server" Visible=false></uc1:workflowtoolbar><INPUT class="button" id="btnSave" type="button" value=" 保 存 " name="btnSave" runat="server" onserverclick="btnSave_ServerClick" ><INPUT class="button" id="btnUpdate" type="button" value=" 修 改 " name="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick" ><INPUT class="button" id="btnClose" onclick="javascript:window.close();" type="button"
	value="关闭窗口" runat="server"></td>
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
										<font size="3"><STRONG>中标单位审批</STRONG></font><br/>
										<br/>
									</td>
								</tr>
								<tr>
									<td colspan="5">
										<uc1:BiddingTop id="BiddingTop1" runat="server"></uc1:BiddingTop></td>
								</tr>
								<TR>
									<TD class="blackbordertdcontent"><br/>
										最<br/>
										后<br/>
										议<br/>
										标<br/>
										单<br/>
										位<br/>
										<br/>
									</TD>
									<TD width="95%" colspan="4" class="blackbordertd">
										<uc1:BiddingAuditing id="BiddingAuditing1" runat="server"></uc1:BiddingAuditing><FONT face="宋体"></FONT></TD>
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
			</TABLE>
		</form>
		<script>
function BiddingEmitView(code)
{
	OpenLargeWindow('BiddingReturnModifyPage.aspx?BiddingEmitCode='+code+'&State=view','会标');
}
		</script>
	</body>
</HTML>
