<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetBackup" CodeFile="CostBudgetBackup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��Ŀ���ô浵</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��Ŀ���ô浵</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="form-item" width="100">��Ŀ���ƣ�</td>
								<td><asp:Label Runat="server" ID="lblProjectName"></asp:Label></td>
							</tr>
							<tr>
								<TD class="form-item">������</TD>
								<TD><textarea class="textarea" id="txtBackupDescription" style="WIDTH: 100%" name="txtBackupDescription"
										rows="5" runat="server"></textarea>
								</TD>
							</tr>
							<tr style="display:none">
								<TD class="form-item" width="100">�浵���ƣ�</TD>
								<td><input type="text" class="input" runat="server" id="txtBackupName" name="txtBackupName"
										style="WIDTH:100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="ȡ ��"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
		</form>
		<script language="javascript">
		</script>
	</body>
</HTML>
