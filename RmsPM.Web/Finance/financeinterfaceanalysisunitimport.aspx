<%@ Page language="c#" Inherits="RmsPM.Web.Finance.FinanceInterfaceAnalysisUnitImport" CodeFile="FinanceInterfaceAnalysisUnitImport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���벿�Ų������</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">���벿�Ų������</td>
				</tr>
				<tr align="center">
					<td style="COLOR:blue">�������ݽ����������ƶ�Ӧ�������</td>
				</tr>
				<tr height="100%">
					<td valign="top" align="center">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="form">
							<tr>
								<td class="form-item" width="80">���ף�</td>
								<td><asp:Label Runat="server" id="lblSubjectSetName"></asp:Label></td>
							</tr>
							<tr>
								<td class="form-item">�ļ���</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"></td>
							</tr>
						</TABLE>
						<br>
						<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td style="COLOR: blue">�ļ���ʽ˵����<br>
									1.�ļ����ͱ�����csv�����ŷָ���<br>
									2.�ļ��ĵ�1��Ϊ�����У��Ժ�Ϊ�����С�<br>
									3.����������Ϊ���������ơ��������
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="9" width="100%" border="0">
							<tr align="center">
								<td><input type="button" class="submit" id="btnOK" name="btnOK" value="����" runat="server" onserverclick="btnOK_ServerClick">
									<input type="button" class="submit" name="btnCancel" value="ȡ��" onclick="window.close();"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<input type="hidden" runat="server" id="txtSubjectSetCode" name="txtSubjectSetCode">
			<script language="javascript">
			</script>
		</form>
	</body>
</HTML>
