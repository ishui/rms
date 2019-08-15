<%@ Page language="c#" Inherits="RmsPM.Web.Document.DocumentTypeModify" CodeFile="DocumentTypeModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>文档类型修改</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25" id="tdTitle"
						runat="server">文档类型修改</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="120" class="form-item">文档类型名称：</TD>
								<TD><input type="text" id="txtTypeName" name="txtTypeName" class="input" runat="server" style="WIDTH:100%"></TD>
							</TR>
							<tr>
								<TD class="form-item">上级文档类型：</TD>
								<TD><asp:label id="lblParentName" runat="server"></asp:label></TD>
							</tr>
							<TR>
								<TD class="form-item">备注：</TD>
								<TD><textarea runat="server" style="WIDTH:100%" s id="txtDescription" name="txtDescription" rows="5"
										class="textarea"></textarea>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<INPUT id="txtAct" type="hidden" name="txtAct" runat="server"> <INPUT id="txtDocumentTypeCode" type="hidden" name="txtDocumentTypeCode" runat="server">
			<input type="hidden" id="txtParentCode" name="txtParentCode" runat="server">
		</form>
	</body>
</HTML>
