<%@ Page language="c#" Inherits="RmsPM.Web.Finance.FinanceInterfaceAnalysisUserModify" CodeFile="FinanceInterfaceAnalysisUserModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��Ա�������</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--

//-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��Ա�������</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="80" class="form-item">��Ա��</TD>
								<TD><asp:Label Runat="server" ID="lblUserName"></asp:Label></TD>
							</TR>
							<tr>
								<TD class="form-item">���ף�</TD>
								<TD><asp:Label Runat="server" ID="lblSubjectSetName"></asp:Label></TD>
							</tr>
							<tr>
								<TD class="form-item">������룺</TD>
								<TD><input type="text" runat="server" class="input" id="txtU8Code" name="txtU8Code"></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server" onserverclick="btnSave_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtUserCode" type="hidden" name="txtUserCode" runat="server">
			<input type="hidden" name="txtSubjectSetCode" id="txtSubjectSetCode" runat="server">
		</form>
	</body>
</HTML>
