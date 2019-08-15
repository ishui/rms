<%@ Page language="c#" Inherits="RmsPM.Web.Sal.SalSuplImport" CodeFile="SalSuplImport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>导入销售供应商</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">导入销售供应商</td>
				</tr>
				<tr align="center">
					<td style="color:blue">导入内容将覆盖该项目的所有销售供应商</td>
				</tr>
				<tr style="display:none" id="hint" align="center">
					<td style="color:red">正在导入，请稍候...</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<tr>
								<td width="80" class="form-item">对应项目：</td>
								<td><select class="select" id="sltProject" name="sltProject" runat="server">
										<option value="" selected>-------------请选择-------------</option>
									</select></td>
							</tr>
							<tr>
								<td class="form-item">文件：</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"></td>
							</tr>
						</TABLE>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td style="COLOR: blue">文件格式说明：<br>
									1.文件类型必须是csv（逗号分隔）<br>
									2.文件的第1行为标题行，以后为数据行。<br>
									3.数据行的第1列必须为供应商代码，第2列必须为供应商名称
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
									<input id="btnOK" name="btnOK" type="button" class="submit" value="确 定" runat="server" onserverclick="btnOK_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
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
