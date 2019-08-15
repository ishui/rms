<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SubjectDetailReportPre" CodeFile="SubjectDetailReportPre.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>付款查询</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="table" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									财务管理 -&nbsp;科目明细表
								</td>
								<td style="CURSOR: hand" onclick="window.navigate('../Desktop.aspx'); return false;"
									width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<TABLE WIDTH="80%" ALIGN="center" BORDER="0" CELLSPACING="0" CELLPADDING="0">
							<TR>
								<TD align="center" height="20"></TD>
							</TR>
							<TR>
								<TD align="center">
									<TABLE class="tableMain" id="Table51" cellSpacing="1" cellPadding="0" width="30" align="center"
										border="0">
										<TR>
											<TD class="tdText" NOWRAP>
												年度：</TD>
											<TD NOWRAP class="tdBlank"><SELECT id="sltYear" name="sltYear" runat="server">
													<OPTION selected></OPTION>
												</SELECT></TD>
											<TD class="tdText" NOWRAP>月份：</TD>
											<TD class="tdBlank" NOWRAP><SELECT id="sltMonth" name="sltMonth" runat="server">
													<OPTION value="1" selected>一月</OPTION>
													<OPTION value="2">二月</OPTION>
													<OPTION value="3">三月</OPTION>
													<OPTION value="4">四月</OPTION>
													<OPTION value="5">五月</OPTION>
													<OPTION value="6">六月</OPTION>
													<OPTION value="7">七月</OPTION>
													<OPTION value="8">八月</OPTION>
													<OPTION value="9">九月</OPTION>
													<OPTION value="10">十月</OPTION>
													<OPTION value="11">十一月</OPTION>
													<OPTION value="12">十二月</OPTION>
												</SELECT></TD>
										</TR>
										<TR>
											<TD class="tdText" NOWRAP></TD>
											<TD NOWRAP></TD>
											<TD class="tdText" NOWRAP></TD>
											<TD class="tdBlank" NOWRAP></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD align="center">
									<INPUT id="btnReport" type="button" value="统 计" onclick="doReport();return false;" class="submit">
								</TD>
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
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--
	function doReport()
	{
		OpenLargeWindow('SubjectDetailReport.aspx?ProjectCode=<%=Request["ProjectCode"]%>&iYear=' + Form1.sltYear.value + "&iMonth=" + Form1.sltMonth.value );
	}
//-->
		</script>
	</body>
</HTML>
