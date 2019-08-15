<%@ Reference Page="~/remind/remindmodify.aspx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSStatus" CodeFile="WBSStatus.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>任务状态维护</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/Index.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function doCheck()
			{
				var option = '<%=Request["Status"]%>';
				if(option=="Cancel")
				{
					if(window.confirm('这也会取消当前任务的所有子任务，你确定吗？'))
						return true;
					else
						return false;
				}
				if(option=="Finish")
				{
					if(window.confirm('你确实要结束当前任务吗？'))
						return true;
					else
						return false;
				}
				return true;
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr valign="middle">
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						<asp:Label id="lblTitle" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="1" cellpadding="0" cellspacing="0" class="form">
							<TR id="trTime" runat="server">
								<td class="form-item" width="20%">
									<asp:Label id="lblDate" runat="server"></asp:Label>
								</td>
								<TD>
									<cc3:calendar id="crDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
										Display="True"></cc3:calendar>
								</TD>
							</TR>
							<TR id="trReason" runat="server">
								<td class="form-item"><asp:Label id="lblReason" runat="server"></asp:Label></td>
								<TD>
									<asp:TextBox id="txtReason" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
								</TD>
							</TR>
							<TR id="trTask" runat="server">
								<td class="form-item"><asp:Label id="lblTask" runat="server"></asp:Label></td>
								<TD align="center">
									<div id="divTask" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100px" runat="server">
										<asp:datagrid id="dgTaskList" runat="server" CssClass="list" PageSize="1" AutoGenerateColumns="False"
											DataKeyField="WBSCode" Width="100%">
											<FooterStyle CssClass="FooterStyle"></FooterStyle>
											<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
											<ItemStyle Height="5px"></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="StatusName" HeaderText="工作项"></asp:BoundColumn>
												<asp:BoundColumn DataField="Master" HeaderText="责任人">
													<HeaderStyle Width="20%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CompletePercent" HeaderText="进度" DataFormatString="{0}%">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="选择">
													<ItemTemplate>
														<asp:CheckBox id="chkTask" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
									<asp:Label id="lblNoTask" runat="server"></asp:Label>
								</TD>
							</TR>
						</table>
						<br>
						<input class="submit" id="btConfirm" onclick="if(!doCheck()) return false;" type="button"
							value="确 定" name="btConfirm" runat="server" onserverclick="btConfirm_ServerClick">
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
