<%@ Page language="c#" Inherits="RmsPM.Web.Sal.SalSuplImport" CodeFile="SalSuplImport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�������۹�Ӧ��</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">�������۹�Ӧ��</td>
				</tr>
				<tr align="center">
					<td style="color:blue">�������ݽ����Ǹ���Ŀ���������۹�Ӧ��</td>
				</tr>
				<tr style="display:none" id="hint" align="center">
					<td style="color:red">���ڵ��룬���Ժ�...</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<tr>
								<td width="80" class="form-item">��Ӧ��Ŀ��</td>
								<td><select class="select" id="sltProject" name="sltProject" runat="server">
										<option value="" selected>-------------��ѡ��-------------</option>
									</select></td>
							</tr>
							<tr>
								<td class="form-item">�ļ���</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"></td>
							</tr>
						</TABLE>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td style="COLOR: blue">�ļ���ʽ˵����<br>
									1.�ļ����ͱ�����csv�����ŷָ���<br>
									2.�ļ��ĵ�1��Ϊ�����У��Ժ�Ϊ�����С�<br>
									3.�����еĵ�1�б���Ϊ��Ӧ�̴��룬��2�б���Ϊ��Ӧ������
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
									<input id="btnOK" name="btnOK" type="button" class="submit" value="ȷ ��" runat="server" onserverclick="btnOK_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		<script language="javascript">
		document.all.hint.style.display="none";
		</script>
		</form>
	</body>
</HTML>
