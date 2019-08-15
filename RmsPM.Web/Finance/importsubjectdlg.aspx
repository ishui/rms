<%@ Page language="c#" Inherits="RmsPM.Web.Sal.ImportSubjectDlg" CodeFile="ImportSubjectDlg.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>导入科目</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">导入科目</td>
				</tr>
				<tr align="center">
					<td style="color:blue">导入内容将覆盖该帐套的所有科目</td>
				</tr>
				<tr height="100%">
					<td valign="top" align="center">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="form">
							<tr>
								<td class="form-item">文件：</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"></td>
							</tr>
						</TABLE>
						<br>
						<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td style="COLOR: blue">文件格式说明：<br>
									1.文件类型必须是csv（逗号分隔）<br>
									2.文件的第1行为标题行，以后为数据行。<br>
									3.数据行依次为：类型、级次、科目编码、科目名称、助记码、外币币种、计量单位、辅助账类型、账页格式、余额方向
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE height="100%" cellSpacing="10" cellPadding="0" width="100%" border="0">
							<tr align="center">
								<td><input type="button" class="submit" id="btnOK" name="btnOK" value="确 定" onclick="document.all.divHintSave.style.display='';"
										runat="server" onserverclick="btnOK_ServerClick"> <input type="button" class="submit" name="btnCancel" value="取 消" onclick="window.close();"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 100px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" name="txtSubjectSetCode" id="txtSubjectSetCode" runat="server">
			<script language="javascript">
			</script>
		</form>
	</body>
</HTML>
