<%@ Page language="c#" Inherits="RmsPM.Web.Cost.SelectBudgetYear" CodeFile="SelectBudgetYear.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择预算年度</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">制定预算</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TBODY>
								<TR>
									<TD class="form-item">预算名称：</TD>
									<TD colspan="3"><input type="text" runat="server" id="txtbudgetName" class="input" NAME="Text2" style="WIDTH: 424px; HEIGHT: 18px"
											size="65"></TD>
								</TR>
								<TR>
									<TD class="form-item" width="15%">预算起止：</TD>
									<TD width="35%" noWrap><INPUT type="text" class="input-readonly" id="txtStartDate" runat="server" size="15">--
										<INPUT class="input-readonly" id="txtEndDate" type="text" name="Text1" runat="server" size="15">
									</TD>
									<TD class="form-item" width="15%">预算周期(月)：</TD>
									<TD width="35%"><INPUT class="input-readonly" id="txtPeriodMonth" type="text" name="Text1" runat="server"></TD>
								</TR>
								<TR>
									<TD class="form-item" noWrap>预算期数：</TD>
									<TD noWrap>第<INPUT class="input-readonly" id="txtPeriodIndex" type="text" name="Text1" runat="server"
											size="2">期&nbsp;&nbsp;/&nbsp;&nbsp; 总<INPUT class="input-readonly" id="txtTotalPeriods" type="text" name="Text1" runat="server"
											size="2">期</TD>
									<TD class="form-item" width="20%"><FONT face="宋体"></FONT></TD>
									<TD width="30%"></TD>
								</TR>
								<TR>
									<TD class="form-item">备注：</TD>
									<TD colspan="3"><textarea id="txtRemark" style="WIDTH:80%" runat="server" class="textarea" rows="3"></textarea></TD>
								</TR>
								<TR>
									<TD class="form-item" noWrap>制 定 人：</TD>
									<TD>
										<asp:Label id="lblMakePersonName" runat="server"></asp:Label></TD>
									<TD class="form-item" noWrap>制定时间：</TD>
									<TD>
										<asp:Label id="lblMakeDate" runat="server"></asp:Label></TD>
								</TR>
							</TBODY>
						</table>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp; 
									&nbsp; <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
