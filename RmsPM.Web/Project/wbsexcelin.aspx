<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSExcelIn" CodeFile="WBSExcelIn.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Excel导入</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white"
				id="tableFull">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">Excel导入</td>
				</tr>
				<tr>
					<td height="25" class="note">注意：导入工作项至指定节点下<!--将全部覆盖原先的工作结构及从属工作-->
					</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form" id="tableMain">
							<tr>
								<td class="form-item">文件：</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"></td>
							</tr>
							<tr>
								<td class="form-item">导入到指定的自定义节点下：</td>
								<td><asp:TextBox id="txtCode" runat="server" CssClass="input" Width="176px"></asp:TextBox></td>
							</tr>
						</table>
						<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td style="COLOR: blue">文件格式说明：<br>
									1.文件类型必须是csv（逗号分隔）<br>
									2.文件的第1行为标题行，以后为数据行。<br>
									3.数据行依次为：自定义编号,工作项名称,负责人,工作状态,重要程度,工作进度,计划开始时间,计划结束时间,实际开始时间,实际结束时间,大纲数字
								</td>
							</tr>
						</TABLE>
						<table width="100%" cellspacing="10" id="tableButton">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server"
										onclick="doSave();" onserverclick="btnSave_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 100px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
		</form>
		<script language="javascript">
<!--

	function doSave()
	{
		document.all.divHintSave.style.display='';
	}


//-->
		</script>
	</body>
</HTML>
