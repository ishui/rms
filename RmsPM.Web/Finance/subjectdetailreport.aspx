<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SubjectDetailReport" CodeFile="SubjectDetailReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��Ŀ��ϸ����</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE WIDTH="100%" BORDER="0" CELLSPACING="0" CELLPADDING="0" bgcolor="#ffffff">
				<TR>
					<TD align="center">
						<TABLE WIDTH="98%" BORDER="0" CELLSPACING="0" CELLPADDING="0">
							<TR>
								<TD width="20%"></TD>
								<TD nowrap>
									<asp:Label id="lblYear" runat="server"></asp:Label>���
									<asp:Label id="lblMonth" runat="server"></asp:Label>�·ݿ�Ŀ��ϸ����</TD>
								<TD width="10%"></TD>
								<TD width="20%"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD>��Ŀ��</TD>
								<TD>
									<asp:Label id="lblProjectName" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE WIDTH="100%" style="BORDER-COLLAPSE: collapse" borderColor="blue" cellSpacing="0"
							cellPadding="0" border="1" id="tbReport" runat="server">
							<tr height="24" align="center">
								<td rowspan="2">��Ŀ</td>
								<td rowspan="2">����</td>
								<td colspan="2">�ڳ���</td>
								<td colspan="2">��ĩ��</td>
							</tr>
							<tr align="center">
								<td>��</td>
								<td>��</td>
								<td>��</td>
								<td>��</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
