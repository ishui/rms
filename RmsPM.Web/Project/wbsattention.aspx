<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSAttention" CodeFile="WBSAttention.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script>
			<!--
				function OpenTask(Url)
				{
					OpenFullWindow(Url+"&ProjectCode=<%=Request["ProjectCode"]%>","");
				}

			-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif" width="100%"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									工作管理 - 关注工作
								</td>
								<td><IMG height="25" src="../images/topic_corr.gif"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table cellSpacing="0" cellPadding="0" class="search-area" width="100%">
							<tr>
								<TD>关注的类型：<input type="text" name="txtTitle" id="txtType" runat="server" class="input">
									&nbsp;&nbsp;关注的主题：<input type="text" name="txtTitle" id="txtTitle" runat="server" class="input">
								</td>
							</tr>
							<tr>
								<TD>开始时间：<cc3:calendar id="dtStartDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
													Value=""></cc3:calendar>
									&nbsp;&nbsp;结束时间：<cc3:calendar id="dtEndDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
													Value=""></cc3:calendar>
									&nbsp;&nbsp;<input class="submit" id="btnSearch" runat="server" type="button" value="搜 索" name="btnSearch" onserverclick="btnSearch_ServerClick">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<TD vAlign="top" class="table">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
						<asp:datagrid id="dgAttention" runat="server" DataKeyField="TaskAttentionCode" GridLines="Horizontal"
							AutoGenerateColumns="False" Width="100%" CellPadding="2" Height="32px" CssClass="list">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<ItemStyle CssClass=""></ItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="AddModule" HeaderText="关注的类型" DataFormatString="{0}"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="关注的主题">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<a href="#" onclick="OpenTask(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.Url") %>'><%# DataBinder.Eval(Container, "DataItem.AddTitle") %></a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="AddTime" HeaderText="加入日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:ButtonColumn Text="删除" HeaderText="操作" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid>
						<table id="tbNoAttention" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr align="center" width="100%">
								<td>没有关注工作</td>
							</tr>
						</table>
						</div>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
			<input type="hidden" id="hCode" runat="server">
		</form>
	</body>
</HTML>
