<%@ Page language="c#" Inherits="RmsPM.Web.Sal.SalSuplModify" CodeFile="SalSuplModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>销售供应商</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">销售供应商</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<tr>
								<td class="form-item">项目：</td>
								<td><select class="select" id="sltProject" name="sltProject" runat="server">
										<option value="" selected>-------------请选择-------------</option>
									</select></td>
							</tr>
							<tr>
								<td class="form-item">供应商代码：</td>
								<td><input type="text" class="input" id="txtSuplCode" name="txtSuplCode" runat="server"></td>
							</tr>
							<tr>
								<td class="form-item">供应商名称：</td>
								<td><input type="text" class="input" id="txtSuplName" name="txtSuplName" runat="server"></td>
							</tr>
						</table>
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
			</table>
			<input type="hidden" id="txtSystemID" name="txtSystemID" runat="server"><input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
