<%@ Page language="c#" Inherits="RmsPM.Web.Systems.RoleModify" CodeFile="RoleModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>角色修改</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">角色信息</td>
				</tr>
				<tr>
					<td vAlign="top">
						<TABLE class="form" cellSpacing="0" cellPadding="0" border="0" width="100%">
							<TR>
								<TD class="form-item" width="100">名称：</TD>
								<TD><asp:textbox id="txtRoleName" runat="server" CssClass="input"></asp:textbox><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">说明：</TD>
								<TD><asp:textbox id="txtDescription" runat="server" CssClass="input" Width="464px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="form-item" width="100">排序：</TD>
								<TD><asp:textbox id="sortID" runat="server" CssClass="input"></asp:textbox></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr id=trSave>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
		</form>
	</body>

</HTML>
