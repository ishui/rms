<%@ Page language="c#" Inherits="RmsPM.Web.Sal.MapSalSupl" CodeFile="MapSalSupl.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>对应销售供应商</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">对应销售供应商</td>
				</tr>
				<tr style="DISPLAY:none" id="hint" align="center">
					<td style="COLOR:red">正在对应，请稍候...</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<tr>
								<td class="note">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;供应商 对应 合同</td>
							</tr>
							<tr>
								<td>
									<asp:RadioButtonList id="rdoType" runat="server" Width="300px" CellPadding="5" CellSpacing="5">
										<asp:ListItem Value="1" Selected="True">010101-0001xxy 对应 010101-0001xxxy</asp:ListItem>
										<asp:ListItem Value="2">Sx2001-1 对应 010101-0001Sxy</asp:ListItem>
										<asp:ListItem Value="3">200726-0001Sxy 对应 000726-0001Sxy</asp:ListItem>
										<asp:ListItem Value="4">2004xx1 对应 04nnnn-001xxy</asp:ListItem>
										<asp:ListItem Value="5">2004xx1 对应 04nnnn-0001xxy</asp:ListItem>
										<asp:ListItem Value="6">2004xx1 对应 04nnnn-00001xxy</asp:ListItem>
									</asp:RadioButtonList></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnOK" name="btnOK" type="button" class="submit" value="确 定" onclick="document.all.hint.style.display='block';"
										runat="server" onserverclick="btnOK_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"><input type="hidden" id="txtRefreshScript" name="txtRefreshScript" runat="server">
			<script language="javascript">
		document.all.hint.style.display="none";
			</script>
		</form>
	</body>
</HTML>
