<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherTypeModify" CodeFile="VoucherTypeModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ƾ֤����</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ƾ֤����</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="100">ƾ֤���ͱ�ţ�</TD>
								<TD><INPUT class="input" id="txtCode" type="text" runat="server" NAME="txtCode"><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">ƾ֤�������ƣ�</TD>
								<TD><INPUT class="input" id="txtName" type="text" runat="server" NAME="txtName"><font color="red">*</font></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="9" width="100" align="center" border="0">
							<tr>
								<td align="center">
									<input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnOK" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<INPUT class="submit" id="btnDelete" type="button" value="ɾ ��" onclick="if (!confirm('ȷʵҪɾ����')) return false; "
										runat="server" onserverclick="btnDelete_ServerClick">&nbsp; <INPUT class="submit" onclick="window.close();" type="button" value="ȡ ��"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
			<input type="hidden" runat="server" id="txtOldCode" name="txtOldCode">
			<SCRIPT language="javascript">
<!--
//-->
			</SCRIPT>
		</form>
	</body>
</HTML>
