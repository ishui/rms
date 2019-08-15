<%@ Page language="c#" Inherits="RmsPM.Web.Sal.ImportSalOldDlg" CodeFile="ImportSalOldDlg.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>导入销售老数据</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">导入销售老数据</td>
				</tr>
				<tr style="DISPLAY:none" id="hint" align="center">
					<td style="COLOR:red">正在导入，请稍候...</td>
				</tr>
				<tr align="center">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<tr>
								<td width="80" class="form-item">对应项目：</td>
								<td><span id="divProjectName" runat="server"></span> <INPUT class="button-small" id="btnSelectProject" onclick="doOpenSelectProject();" type="button"
										value="选择项目" name="btnSelectProject">
								</td>
							</tr>
							<tr>
								<td class="form-item">文件：</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"></td>
							</tr>
							<tr>
								<td class="form-item">过滤基地：</td>
								<td><input class="input" type="text" id="txtJd" name="txtJd" runat="server" size="36"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr align="center">
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td style="COLOR: blue">文件格式说明：<br>
									1.文件类型必须是csv（逗号分隔）<br>
									2.文件的第1行为标题行，以后为数据行。<br>
									3.数据行依次为：序号、客户姓名、合同编号、拨房单号、基地、门牌号、室号、面积、合同单价、经营收入
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
									<input type="button" class="submit" id="btnClear" name="btnClear" value="清 空" onclick="if(!confirm('确实要清空该项目的所有销售收入吗？')) return false;"
										runat="server" onserverclick="btnClear_ServerClick"> <input id="btnOK" name="btnOK" type="button" class="submit" value="确 定" onclick="document.all.hint.style.display='block';"
										runat="server" onserverclick="btnOK_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtProjectName" name="txtProjectName" runat="server">
			<input type="hidden" id="txtRefreshScript" name="txtRefreshScript" runat="server">
			<script language="javascript">

//选择项目
function doOpenSelectProject()
{
	OpenMiddleWindow('../SelectBox/SelectProject.aspx?Type=single' ,'选择项目');
}

//选择项目返回
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
