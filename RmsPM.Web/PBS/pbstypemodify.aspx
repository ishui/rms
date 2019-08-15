<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSModify" CodeFile="PBSTypeModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>产品组合</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">产品组合</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="100" class="form-item">名称：</TD>
								<TD><input type="text" class="input" size="30" id="txtPBSTypeName" name="txtPBSTypeName"
										runat="server"><font color="red">*</font>
								</TD>
							</TR>
							<tr>
								<td class="form-item">所属产品组合：</td>
								<td><asp:Label Runat="server" ID="lblParentName"></asp:Label></td>
							</tr>
							<tr>
								<TD class="form-item" width="20%">描述：</TD>
								<TD><textarea id="txtDescription" name="txtDescription" runat="server" rows="7" style="WIDTH:100%"></textarea></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
									<input style="display:none" id="btnDelete" name="btnDelete" type="button" class="submit" value="删 除" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
										runat="server" onserverclick="btnDelete_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		<input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtPBSTypeCode" type="hidden" name="txtPBSTypeCode" runat="server">
		<input id="txtParentCode" type="hidden" name="txtParentCode" runat="server">
		<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
