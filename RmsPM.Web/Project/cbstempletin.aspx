<%@ Page language="c#" Inherits="RmsPM.Web.Project.CBSTempletIn" CodeFile="CBSTempletIn.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>导出费用项</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no" bgcolor="#ffffff">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">导出费用项</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<tr>
								<td class="note" colspan="2" align="center">确实要导出吗？</td>
							</tr>
							<TR style="display:none">
								<TD class="form-item" width="80">模板名称：</TD>
								<TD>
									<asp:TextBox id="txtTempletName" runat="server" CssClass="input" Width="176px"></asp:TextBox>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE height="100%" cellSpacing="10" cellPadding="0" width="100%" border="0">
							<tr align="center">
								<td><input type="button" class="submit" id="btnOK" name="btnOK" value="确 定" onclick="doSave();"
										runat="server" onserverclick="btnOK_ServerClick"> <input type="button" class="submit" name="btnCancel" value="取 消" onclick="window.close();"></td>
							</tr>
						</TABLE>
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
			<INPUT id="txtInputCode" type="hidden" name="txtInputCode" runat="server">
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
