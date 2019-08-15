<%@ Page language="c#" CodeFile="WBSExecuteCheck.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSExecuteCheck" %>
<HTML>
	<HEAD>
		<title>工作报告审阅</title>
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
						审阅工作报告</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD class="form-item" align="right" width="30%">报告题头：</TD>
								<TD>
									<asp:Label id="lblTitle" runat="server"></asp:Label></TD>
							<TR>
								<TD class="form-item" align="right">目前完成进度：</TD>
								<TD>
									<asp:Label id="lblPercent" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item" align="right">报告日期：</TD>
								<TD>
									<asp:Label id="lblExecuteDate" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item" align="right">执行情况：</TD>
								<TD>
									<asp:Label id="lblDetail" runat="server"></asp:Label>
								</TD>
							</TR>
							<tr>
								<td width="20%" align="right" class="form-item">审阅意见：
								</td>
								<td>
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
						<br>
						<table width="100%" border="0" cellpadding="0" cellspacing="0">
							<tr align="left" valign="top">
								<td class="intopic" width="200">附件列表</td>
							</tr>
							<tr valign="top">
								<td>
									<asp:datagrid id="dgAttach" runat="server" Width="100%" AutoGenerateColumns="False" AllowCustomPaging="False"
										GridLines="Horizontal" CellPadding="2" DataKeyField="TaskAttachMentCode" CssClass="list">
										<ItemStyle></ItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:HyperLinkColumn DataNavigateUrlField="TaskAttachMentCode" DataNavigateUrlFormatString="javascript:ViewAttach('{0}');"
												DataTextField="Title" HeaderText="标题"></asp:HyperLinkColumn>
											<asp:BoundColumn DataField="CreatePerson" HeaderText="创建人">
												<HeaderStyle Width="80px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="CreateDate" HeaderText="创建日期" DataFormatString="{0:yyyy-MM-dd}">
												<HeaderStyle Width="80px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="ModifyPerson" HeaderText="修改人">
												<HeaderStyle Width="80px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="ModifyDate" HeaderText="修改日期" DataFormatString="{0:yyyy-MM-dd}">
												<HeaderStyle Width="80px"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
							<tr width="100%" align="center" id="trAttachMent" runat="server">
								<td>
								<table width=100% class=form align=center><tr width=100% align=center><td>工作报告没有附件</td></tr></table>
								</td>								
							</tr>
						</table>
					</td>
				</tr>
				
			</table>
		</form>
	</body>
</HTML>
