<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Supplier.SupplierOpinionModify" CodeFile="SupplierOpinionModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>编辑评估记录</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">供应商评价</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD class="form-item" width="20%">名称：</TD>
								<TD width="30%">
									<asp:textbox id="SupplierName" runat="server" ReadOnly="True" CssClass="input"></asp:textbox></TD>
								<TD class="form-item" width="20%">评 价 人：</TD>
								<TD noWrap width="30%">
									<asp:textbox id="OpinionPerson" runat="server" CssClass="input"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="form-item">评估时间：</TD>
								<TD>
									<cc3:calendar id="dtOpinionDate" runat="server" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Display="True"></cc3:calendar></TD>
								<TD class="form-item">事件：</TD>
								<TD noWrap>
									<asp:textbox id="Event" runat="server" Width="200px" CssClass="input"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="form-item">评价意见：</TD>
								<TD colSpan="3">
									<asp:textbox id="Opinion" runat="server" Width="98%" TextMode="MultiLine" Height="72px" CssClass="textbox"></asp:textbox></TD>
							</TR>
						</table>
						<table cellspacing="10" width="100%">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<input id="btnDelete" name="btnDelete" type="button" class="submit" value="删 除" runat="server" onserverclick="btnDelete_ServerClick">&nbsp;
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
