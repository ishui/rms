<%@ Page language="c#" Inherits="RmsPM.Web.Systems.SystemGroupModify" CodeFile="SystemGroupModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ϵͳ���</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ϵͳ���</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="100" class="form-item">�������ƣ�</TD>
								<TD><input type="text" class="input" size="30" id="txtGroupName" name="txtGroupName"
										runat="server"><font color="red">*</font>
								</TD>
							</TR>
							<TR>
								<TD width="100" class="form-item">�ࡡ���ţ�</TD>
								<TD><input type="text" class="input" size="10" id="txtSortID" name="txtSortID"
										runat="server">
								</TD>
							</TR>
							<TR>
								<TD width="100" class="form-item">Ӣ�����ƣ�</TD>
								<TD><input type="text" class="input" size="30" id="txtGroupNameEn" name="txtGroupNameEn"
										runat="server">
								</TD>
							</TR>
							<tr>
								<td class="form-item">�������</td>
								<td><asp:Label Runat="server" ID="lblParentName"></asp:Label></td>
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
		<input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtGroupCode" type="hidden" name="txtGroupCode" runat="server">
		<input id="txtParentCode" type="hidden" name="txtParentCode" runat="server">
		<input id="txtClassCode" type="hidden" name="txtClassCode" runat="server">
		</form>
	</body>
</HTML>
