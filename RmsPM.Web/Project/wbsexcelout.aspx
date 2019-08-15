<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSExcelOut" CodeFile="WBSExcelOut.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>导出到Excel</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">导出到Excel</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form" id="tableMain">
							<TR style="display:none">
								<TD class="form-item" width="100">Excel文件名称：</TD>
								<TD>
									<asp:TextBox id="txtExcelName" runat="server" CssClass="input" Width="176px"></asp:TextBox>
								</TD>
							</TR>
							<TR>
								<td class="form-item">从指定的自定义编号开始导出</td>
								<td><asp:TextBox id="txtCode" runat="server" CssClass="input" Width="176px"></asp:TextBox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator2" runat="server" ControlToValidate="txtCode" ErrorMessage="*"></asp:RequiredFieldValidator></td>
							</TR>
						</table>
						<table cellspacing="10" width="100%" id="tableButton">
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
