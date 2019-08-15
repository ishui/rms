<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.TaskTempletOut" CodeFile="TaskTempletOut.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>导出成为模板</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no" onload="winload()">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">导出成为模板</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD class="form-item" width="30%">工 作 项：</TD>
								<TD width="70%"><asp:Label runat="server" ID="lblTaskName"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">导出模板:</TD>
								<TD>
									<input type="radio" runat="server" id="rdoType0" name="rdoType" value="0" onclick="ChangeType();"
										checked>新建模板 <input type="radio" runat="server" id="rdoType1" name="rdoType" value="1" onclick="ChangeType();">覆盖已有模板&nbsp;&nbsp;&nbsp;&nbsp;
								</TD>
							<TR id="trType0">
								<TD class="form-item" width="30%">模板名称：</TD>
								<TD width="70%">
									<asp:TextBox id="txtTempletName" runat="server" CssClass="input"></asp:TextBox>
									<font color="red">*</font></TD>
							</TR>
							<TR id="trType1">
								<TD class="form-item">选择模板:</TD>
								<TD>
									<select id="sltTemplet" runat="server" NAME="sltTemplet">
									</select>
									<font color="red">*</font>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">生成时间：</TD>
								<TD>
									<cc3:calendar id="xc_date" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
										Value="" Display="True"></cc3:calendar></TD>
							</TR>
							<TR>
								<td class="form-item"></td>
								<td></td>
							</TR>
						</table>
						<table cellspacing="10" width="100%">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server"
										onclick="if (!ConfirmOk()) return false;" onserverclick="btnSave_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<INPUT id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <INPUT id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server">
			<script language="javascript">
<!--

function winload()
{
	ChangeType();
}

function ChangeType()
{
	if (Form1.rdoType0.checked)
	{
		document.all.trType0.style.display = "";
		document.all.trType1.style.display = "none";
	}
	else
	{
		document.all.trType0.style.display = "none";
		document.all.trType1.style.display = "";
	}
}

function ConfirmOk()
{
	if (Form1.rdoType1.checked)
	{
		if (!confirm("确实要覆盖模板吗？")) return false;
	}

	document.all.divHintSave.style.display='';	
	return true;
}

//-->
			</script>
		</form>
	</body>
</HTML>
