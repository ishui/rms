<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Systems.ProjectConfig" CodeFile="ProjectConfig.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>合同预算控制</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white"
				id="Table1">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">合同预算控制</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<br>
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form" id="Table2">
							<TR>
								<TD>
									<asp:RadioButtonList id="rblBudgetControl" runat="server" Width="100%" CellPadding="5" CellSpacing="5">
										<asp:ListItem Value="1">没有控制</asp:ListItem>
										<asp:ListItem Value="2" Selected="True">总额控制</asp:ListItem>
										<asp:ListItem Value="3">精确控制</asp:ListItem>
									</asp:RadioButtonList></TD>
							</TR>
						</table>
						<table cellspacing="10" width="100%" id="Table3">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
