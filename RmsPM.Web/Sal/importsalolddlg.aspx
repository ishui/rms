<%@ Page language="c#" Inherits="RmsPM.Web.Sal.ImportSalOldDlg" CodeFile="ImportSalOldDlg.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��������������</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��������������</td>
				</tr>
				<tr style="DISPLAY:none" id="hint" align="center">
					<td style="COLOR:red">���ڵ��룬���Ժ�...</td>
				</tr>
				<tr align="center">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<tr>
								<td width="80" class="form-item">��Ӧ��Ŀ��</td>
								<td><span id="divProjectName" runat="server"></span> <INPUT class="button-small" id="btnSelectProject" onclick="doOpenSelectProject();" type="button"
										value="ѡ����Ŀ" name="btnSelectProject">
								</td>
							</tr>
							<tr>
								<td class="form-item">�ļ���</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"></td>
							</tr>
							<tr>
								<td class="form-item">���˻��أ�</td>
								<td><input class="input" type="text" id="txtJd" name="txtJd" runat="server" size="36"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr align="center">
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td style="COLOR: blue">�ļ���ʽ˵����<br>
									1.�ļ����ͱ�����csv�����ŷָ���<br>
									2.�ļ��ĵ�1��Ϊ�����У��Ժ�Ϊ�����С�<br>
									3.����������Ϊ����š��ͻ���������ͬ��š��������š����ء����ƺš��Һš��������ͬ���ۡ���Ӫ����
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input type="button" class="submit" id="btnClear" name="btnClear" value="�� ��" onclick="if(!confirm('ȷʵҪ��ո���Ŀ����������������')) return false;"
										runat="server" onserverclick="btnClear_ServerClick"> <input id="btnOK" name="btnOK" type="button" class="submit" value="ȷ ��" onclick="document.all.hint.style.display='block';"
										runat="server" onserverclick="btnOK_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtProjectName" name="txtProjectName" runat="server">
			<input type="hidden" id="txtRefreshScript" name="txtRefreshScript" runat="server">
			<script language="javascript">

//ѡ����Ŀ
function doOpenSelectProject()
{
	OpenMiddleWindow('../SelectBox/SelectProject.aspx?Type=single' ,'ѡ����Ŀ');
}

//ѡ����Ŀ����
function DoSelectProject(projectCode,projectName)
{
	document.all("divProjectName").innerHTML = projectName;
	document.all("txtProjectName").value = projectName;
	document.all("txtProjectCode").value = projectCode;
}
			</script>
		</form>
	</body>
</HTML>
