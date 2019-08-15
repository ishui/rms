<%@ Page language="c#" Inherits="RmsPM.Web.Systems.DepartmentView" CodeFile="DepartmentView.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>查看部门</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" bgcolor="#ffffff">
				<tr>
					<td valign="top">
						<TABLE id="Table29" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
							<TR>
								<TD align="center" class="TableToolBar">查看部门</TD>
							</TR>
						</TABLE>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD vAlign="top">
									<TABLE id="Table2" borderColor="#f7f3f7" cellSpacing="0" cellPadding="3" rules="rows" width="100%"
										border="1">
										<TR>
											<TD class="tdText">部门名称：</TD>
											<TD class="tdBlank"><asp:Label Runat="server" ID="lblUnitName"></asp:Label></TD>
										</TR>
										<TR>
											<TD class="tdText">部门负责人：</TD>
											<TD class="tdBlank"><asp:Label Runat="server" ID="lblPrincipal"></asp:Label></TD>
										</TR>
										<TR>
											<TD class="tdText">备注：</TD>
											<TD class="tdBlank"><asp:Label Runat="server" ID="lblRemark"></asp:Label></TD>
										</TR>
									</TABLE>
									<INPUT id="txtAction" type="hidden" name="txtAction" runat="server"> <INPUT id="txtUnitCode" type="hidden" name="txtUnitCode" runat="server">
									<INPUT id="txtCloseScript" type="hidden" name="txtCloseScript" runat="server">
								</TD>
							</TR>
							<TR id="trButton" runat="server">
								<TD height="40" align="center">
									<INPUT type="button" value="返回" onclick="window.close();" class="Button"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td height="30" align="center" bgcolor="#99bce4"><a href="#" onclick="self.close();return false;" class="close">关闭本窗口</a></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
