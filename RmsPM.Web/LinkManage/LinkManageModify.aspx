<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkManageModify.aspx.cs" Inherits="LinkManage_LinkManageModify" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>系统链接</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">系统链接</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
				
						    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							    <TR>
								    <TD width="80" class="form-item">软件名称：</TD>
								    <TD><input type="text" class="input" size="30" id="txtSoftwareName" name="txtSoftwareName"
										    runat="server"><font color="red">*</font>
								    </TD>
							    </TR>
							    <TR>
								    <TD width="80" class="form-item">链接地址：</TD>
								    <TD><input type="text" class="input" size="30" id="TxtLinkUrl" name="TxtLinkUrl"
										    runat="server"><font color="red">*</font>
								    </TD>
							    </TR>
							    <tr>
							        <TD width="80" class="form-item">创建日期：</TD>
								    <TD runat="server" id="tdCreateDate">&nbsp;
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
			<input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtSystemID" type="hidden" name="txtSystemID" runat="server">
		</form>
	</body>
</HTML>
