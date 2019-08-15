<%@ Page language="c#" CodeFile="WBSPlanCheck.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSPlanCheck" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工作计划审阅</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
			function doCancel(){
				window.close();
			}
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr valign="middle">
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						审阅工作计划</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD class="form-item" align="right" width="30%">计划标题：</TD>
								<TD>
									<asp:Label id="lblTitle" runat="server"></asp:Label></TD>
								<TD class="form-item" align="right">计划提交日期：</TD>
								<TD>
									<asp:Label id="lblPlanDate" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item" align="right">计划内容：</TD>
								<TD height=100 valign=top colspan=3>
									<asp:Label id="lblContent" runat="server"></asp:Label>
								</TD>
							</TR>
							<tr>
								<td width="20%" align="right" class="form-item">审阅意见：
								</td>
								<td colspan=3>
									<textarea cols="50" rows="5" runat="server" class="textarea" id="arCheckResult" NAME="arCheckResult"></textarea>
								</td>
							</tr>
						</table>
						<table cellspacing="0" width="100%" class="form">
							<tr>
								<td align="center">审阅人：<asp:Label ID="lblCheckPerson" Runat="server"></asp:Label></td>
								<td align="center">审阅日期：<asp:Label ID="lblCheckDate" Runat="server"></asp:Label></td>
							</tr>
						</table>
						<table cellspacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="SaveToolsButton" type="button" value="确 定" runat="server" NAME="Button1">
									<input class="submit" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
										value="取 消"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
