<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SubjectModify" CodeFile="SubjectModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>��Ŀ�޸�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body bottomMargin="10" topMargin="10" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��Ŀ�޸�</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="85" class="form-item">��Ŀ��ţ�</TD>
								<TD><input type="text" class="input" id="txtSubjectCode" name="txtSubjectCode" size="30" runat="server"><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">��Ŀ���ƣ�</TD>
								<TD><input type="text" class="input" id="txtSubjectName" name="txtSubjectName" size="30" runat="server"><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">�跽��Ŀ��</TD>
								<TD>
									<asp:checkbox id="CheckBoxIsDebit" runat="server" Text="��" Checked="True"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="form-item">������Ŀ��</TD>
								<TD>
									<asp:checkbox id="CheckBoxIsCrebit" runat="server" Text="��" Checked="True"></asp:checkbox></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server" onserverclick="btnSave_ServerClick">
									<input id="btnDelete" name="btnDelete" type="button" class="submit" value="ɾ ��" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
										runat="server" onserverclick="btnDelete_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<INPUT id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
			<INPUT id="txtAct" type="hidden" name="txtAct" runat="server"><INPUT id="txtOldSubjectCode" type="hidden" name="txtOldSubjectCode" runat="server">
		</form>
	</body>
</HTML>
