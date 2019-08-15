<%@ Page language="c#" Inherits="RmsPM.Web.Finance.ImportUFUnitDlg" CodeFile="ImportUFUnitDlg.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>导入核算部门</title>
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
					<td><asp:label Runat="server" id="Label1" Font-Bold="True">导入核算部门</asp:label></td>
				</tr>
				<tr align="center">
					<td style="color:blue">导入内容将覆盖该项目的所有核算科目</td>
				</tr>
				<tr style="DISPLAY:none" id="hint" align="center">
					<td style="COLOR:red">正在导入，请稍候...</td>
				</tr>
				<tr align="center">
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td>文件：</td>
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
								<td style="COLOR: blue">文件格式说明：<br>
									1.文件类型必须是csv（逗号分隔）<br>
									2.文件的第1行为标题行，以后为数据行。<br>
									3.数据行依次为：部门编号、部门名称
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr height="50">
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr align="center">
								<td><input type="button" class="button" id="btnOK" name="btnOK" value="导入" onclick="document.all.hint.style.display='block';"
										runat="server" onserverclick="btnOK_ServerClick"></td>
								<td><input type="button" class="button" name="btnCancel" value="取消" onclick="window.close();"></td>
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
