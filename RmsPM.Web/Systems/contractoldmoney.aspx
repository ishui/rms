<%@ Page language="c#" Inherits="RmsPM.Web.Systems.ContractOldMoney" CodeFile="ContractOldMoney.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��ͬ�ܼ۱ȽϿ���</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white"
				id="Table1">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��ͬ�ܼ۱ȽϿ���</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<br>
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form" id="Table2">
							<TR>
								<TD>
									<asp:RadioButtonList id="rblProportion" runat="server" Width="100%" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal">
										<asp:ListItem Value="1">�ر�</asp:ListItem>
										<asp:ListItem Value="2">����</asp:ListItem>
									</asp:RadioButtonList></TD>
							</TR>
						</table>
						<table cellspacing="10" width="100%" id="Table3">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
