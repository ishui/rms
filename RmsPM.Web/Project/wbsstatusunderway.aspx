<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSStatusUnderWay" CodeFile="WBSStatusUnderWay.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title>WBSStatusTask</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script>
				<!--
					function Sort(SortField)
					{
						document.Form1.txtSortField.value = SortField;
						document.forms[0].submit();
					}
					
					function SelectTask(Condition)
					{
						window.location.href = "WBSStatusUnderWay.aspx?" + Condition +"&TaskStatus=<%=Request["TaskStatus"]%>&ProjectCode=<%=Request["ProjectCode"]%>&TaskExceed=" + document.all.TaskExceed.value;
					}
					
					function OpenTask(WBSCode)
					{
						OpenFullWindow('WBSInfo.aspx?WBSCode=' + WBSCode+"&ProjectCode=<%=Request["ProjectCode"]%>",900,600);
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
								<td class="topic" width="100%" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									工作管理 - 当前的工作
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
								<TD>开始时间从
									<cc3:calendar id="dtbStartFromDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>到
									<cc3:calendar id="dtbStartToDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>
									&nbsp;&nbsp;工作名称/编码：<input class="input" id="txtTaskName" style="WIDTH: 120px" type="text" name="txtTaskName"
										runat="server">
									&nbsp;&nbsp;负责人：<input class="input" id="txtMaster" style="WIDTH: 120px" type="text" name="txtMaster" runat="server">
								</td>
							</TR>
							<tr>
								<td>结束时间从
									<cc3:calendar id="dtbEndFromDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>到
									<cc3:calendar id="dtbEndToDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>
									&nbsp;&nbsp;重要程度：<select id="lstImportantLevel" style="WIDTH: 120px" name="lstImportantLevel" runat="server">
										<OPTION value="" selected>－－请选择－－</OPTION>
										<OPTION value="0">一般</OPTION>
										<option value="1">重要</option>
									</select>
									&nbsp;&nbsp;<input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<TD vAlign="top" class="table">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
						<asp:datagrid id="dgTaskList" runat="server" DataKeyField="WBSCode" AutoGenerateColumns="False"
							Width="100%" AllowSorting="True" CssClass="list">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<ItemStyle CssClass=""></ItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="工作项">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<a href="#" onclick="OpenTask(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>'><%# DataBinder.Eval(Container, "DataItem.PicName") %></a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Master" HeaderText="负责人"></asp:BoundColumn>
								<asp:BoundColumn DataField="CompletePercent" HeaderText="完成进度" DataFormatString="{0}%"></asp:BoundColumn>
								<asp:BoundColumn DataField="ImportantName" SortExpression="ImportantLevel" HeaderText="重要程度"></asp:BoundColumn>
								<asp:BoundColumn DataField="PlannedStartDate" SortExpression="PlannedStartDate" HeaderText="计划开始时间"
									DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="PlannedFinishDate" SortExpression="PlannedFinishDate" HeaderText="计划结束时间"
									DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="ActualStartDate" SortExpression="ActualStartDate" HeaderText="实际开始时间"
									DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
						<table width="100%">
							<tr id="trNoTask" runat="server">
								<td width="100%" align="center">没有符合条件的工作信息</td>
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
			<input id="txtSortField" type="hidden" name="txtSortField" runat="server">
		</form>
	</body>
</HTML>
