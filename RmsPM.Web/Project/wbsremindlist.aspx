<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSRemindList" CodeFile="WBSRemindList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WBSRemindList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
		function OpenRemind(Url)
		{
			OpenFullWindow(Url+"&ViewRemind=true&ProjectCode=<%=Request["ProjectCode"]%>");
		}
		function SubmitCondition()
		{
			window.Form1.submit();			
		}
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" width="100%" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									工作管理 -<asp:label id="lblTitle" runat="server">Label</asp:label>
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
								<TD>提醒内容：<input class="input" id="txtRemindMessage" style="WIDTH: 120px" type="text" name="txtRemindMessage"
													runat="server">
									&nbsp;&nbsp;提醒类别：<asp:dropdownlist id="ddlRemindType" runat="server" Width="120">
													<asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
												</asp:dropdownlist>
									&nbsp;&nbsp;<input class="submit" id="btnSearch" onclick="SubmitCondition();return false;" type="button"
													value="搜 索" name="btnSearch">
								</TD>
							</TR>
							<TR>
								<TD>开始时间：<cc3:calendar id="dtbStartDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
									&nbsp;&nbsp;结束时间：<cc3:calendar id="dtbEndDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
								</td>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<TD vAlign="top" class="table">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
						<asp:datagrid id="dgRemindList" runat="server" AllowSorting="True" Width="100%"
							AutoGenerateColumns="False" DataKeyField="RemindObjectCode"  CssClass="list">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<ItemStyle CssClass=""></ItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="RemindType" HeaderText="提醒类别"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="提醒内容">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<a href="#" onclick="OpenRemind(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.Url") %>'><%# DataBinder.Eval(Container, "DataItem.Message") %></a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CreateDate" SortExpression="CreateDate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="EndDate" SortExpression="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:ButtonColumn HeaderText="操作" Text="删除" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
						<table width="100%">
							<tr id="trNoRemind" runat="server">
								<td align="center" width="100%">没有符合条件的提醒信息</td>
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
			<input id="txtSortField" type="hidden" name="txtSortField" runat="server"> <input id="TaskStatus" type="hidden" name="TaskStatus" runat="server">
			<input id="TaskExceed" type="hidden" name="TaskExceed" runat="server">
		</form>
	</body>
</HTML>
