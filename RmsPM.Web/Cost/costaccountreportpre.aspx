<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Cost.CostAccountReportPre" CodeFile="CostAccountReportPre.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>成本费用对比表</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" class="table">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">费用管理 
									- 成本费用对比表
								</td>
								<td width="79" onclick="window.navigate('../Desktop.aspx'); return false;" style="CURSOR: hand"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table width="100%">
							<tr>
								<td></td>
							</tr>
						</table>
						<TABLE id="Table51" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0"
							class="form">
							<TR>
								<TD class="form-item" NOWRAP>
									统计截至时间：</TD>
								<TD NOWRAP>
									<cc3:calendar id="dtbEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
										Display="True"></cc3:calendar></TD>
								<TD class="form-item" NOWRAP></TD>
								<TD NOWRAP></TD>
							</TR>
							<TR>
								<TD class="form-item" NOWRAP>编制单位：</TD>
								<TD NOWRAP>
									<input type="text" id="txtUnit" runat="server" class="input"></TD>
								<TD class="form-item" NOWRAP>制表时间：</TD>
								<TD NOWRAP>
									<cc3:calendar id="dtbReportDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
							</TR>
						</TABLE>
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<TR>
								<TD align="center">
									<INPUT id="btnReport" type="button" value="统 计" class="submit" runat="server" onserverclick="btnReport_ServerClick">
								</TD>
							</TR>
						</table>
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
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--

//-->
		</script>
	</body>
</HTML>
