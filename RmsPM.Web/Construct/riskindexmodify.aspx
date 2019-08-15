<%@ Page language="c#" Inherits="RmsPM.Web.Construct.RiskIndexModify" CodeFile="RiskIndexModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>风险指数</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">风险指数</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="80" class="form-item">风险指数：</TD>
								<TD><input type="text" class="input" size="30" id="txtIndexName" name="txtIndexName" runat="server"><font color="red">*</font>
								</TD>
							</TR>
							<tr>
								<td class="form-item">风险级别：</td>
								<TD><input type="text" class="input-nember" size="10" id="txtIndexLevel" name="txtIndexLevel"
										runat="server"><font color="red">*</font>
								</TD>
							</tr>
							<tr>
								<td class="form-item">默认指数：</td>
								<TD><input type="checkbox" runat="server" id="chkIsDefault" name="chkIsDefault">
								</TD>
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
									<input id="btnDelete" name="btnDelete" type="button" class="submit" value="删 除" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
										runat="server" onserverclick="btnDelete_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtIndexCode" type="hidden" name="txtIndexCode" runat="server">
		</form>
	</body>
</HTML>
