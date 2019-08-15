<%@ Page language="c#" Inherits="TiannuoPM.Web.HelperScripts.ftb_uploadimage" CodeFile="ftb.selectimage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>上传图片</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	
	</HEAD>
	<body class="manibody">
		<form id="Form1" method="post" runat="server" encType="multipart/form-data">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top" align="center" width="1" bgColor="#6c7893">
					</td>
					<td vAlign="top" align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" bgColor="#6c7893" border="0">
							<tbody>
								<tr>
									<td><img height="1" src="" width="1"></td>
								</tr>
							</tbody>
						</table>
						<table cellSpacing="0" borderColorDark="#6c7893" cellPadding="0" width="100%" bgColor="#e9e9eb"
							borderColorLight="#ffffff" border="1">
							<TR>
								<TD noWrap borderColorLight="#e1e5f4" align="center" borderColorDark="#e1e5f4"></TD>
							</TR>
							<TR>
								<TD noWrap borderColorLight="#e1e5f4" align="center" borderColorDark="#e1e5f4"><FONT face="宋体"></FONT></TD>
							</TR>
							<tr>
								<td noWrap borderColorLight="#e1e5f4" align="center" borderColorDark="#e1e5f4">
									<table width="90%" height="100%" border="1" cellpadding="5" cellspacing="0" bordercolorlight="#6c7893"
										bordercolordark="#ffffff" class="taball">
										<tr>
											<td colspan="2" align="left" class="tabtop"><STRONG>输入或者修改一个图片</STRONG></td>
										</tr>
										<TR>
											<TD class="tabtop" align="center">文件路径</TD>
											<TD class="tabtop"><INPUT id="FileUpload" style="WIDTH: 184px; HEIGHT: 22px" type="file" size="11" name="File1"
													runat="server"></TD>
										</TR>
										<tr>
											<td class="tabtop" colSpan="2"><asp:label id="lblMessage" runat="server" ForeColor="Red"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
							<TR>
								<TD noWrap borderColorLight="#e1e5f4" align="center" borderColorDark="#e1e5f4">
									<table width="90%" height="100%" border="0" cellpadding="0" cellspacing="0">
										<TR>
											<TD width="20%">&nbsp;</TD>
											<TD width="30%"><INPUT type="button" value="确定" class="button" id="btnOK" name="btnOK" runat="server" onserverclick="btnOK_ServerClick"></TD>
											<TD width="20%">&nbsp;</TD>
											<TD width="30%"><INPUT type="button" value="取消" class="button" onclick="window.close();"></TD>
										</TR>
									</table>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
