<%@ Page language="c#" Inherits="RmsPM.Web.Finance.ImportUFProjectDlg" CodeFile="ImportUFProjectDlg.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���������Ŀ</title>
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
				<tr align="center" height="50">
					<td><asp:label Runat="server" id="Label1" Font-Bold="True">���������Ŀ</asp:label></td>
				</tr>
				<tr align="center">
					<td style="color:blue">�������ݽ��������к�����Ŀ</td>
				</tr>
				<tr style="DISPLAY:none" id="hint" align="center">
					<td style="COLOR:red">���ڵ��룬���Ժ�...</td>
				</tr>
				<tr align="center">
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td>�ļ���</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr align="center">
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td style="COLOR: blue">�ļ���ʽ˵����<br>
									1.�ļ����ͱ�����csv�����ŷָ���<br>
									2.�ļ��ĵ�1��Ϊ�����У��Ժ�Ϊ�����С�<br>
									3.����������Ϊ����Ŀ��š���Ŀ����
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="10" width="100%" border="0">
							<tr align="center">
								<td><input type="button" class="submit" id="btnOK" name="btnOK" value="����" onclick="document.all.hint.style.display='block';"
										runat="server" onserverclick="btnOK_ServerClick">
										<input type="button" class="submit" name="btnCancel" value="ȡ��" onclick="window.close();"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<script language="javascript">
		document.all.hint.style.display="none";
			</script>
		</form>
	</body>
</HTML>
