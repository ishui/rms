<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSStatusUnBegin" CodeFile="WBSStatusUnBegin.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
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
						window.location.href = "WBSStatusUnBegin.aspx?" + Condition +"&TaskStatus=<%=Request["TaskStatus"]%>&ProjectCode=<%=Request["ProjectCode"]%>&TaskExceed=" + document.all.TaskExceed.value;
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
									�������� - δ��ʼ�Ĺ���
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
								<TD>��ʼʱ���
									<cc3:calendar id="dtbStartFromDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>��
									<cc3:calendar id="dtbStartToDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>
									&nbsp;&nbsp;��������/���룺<input class="input" id="txtTaskName" style="WIDTH: 120px" type="text" name="txtTaskName"
										runat="server"> &nbsp;&nbsp;�����ˣ�<input class="input" id="txtMaster" style="WIDTH: 120px" type="text" name="txtMaster" runat="server">
								</TD>
							</tr>
							<tr>
								<td>����ʱ���
									<cc3:calendar id="dtbEndFromDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>��
									<cc3:calendar id="dtbEndToDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>
									&nbsp;&nbsp;��Ҫ�̶ȣ�<select id="lstImportantLevel" style="WIDTH: 120px" name="lstImportantLevel" runat="server">
										<OPTION value="" selected>������ѡ�񣭣�</OPTION>
										<OPTION value="0">һ��</OPTION>
										<option value="1">��Ҫ</option>
									</select>
									&nbsp;&nbsp;<input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<TD vAlign="top" class="table">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<asp:datagrid id="dgTaskList" runat="server" DataKeyField="WBSCode" AutoGenerateColumns="False"
								Width="100%" AllowSorting="True" CssClass="list" AllowPaging="True" PageSize="18">
								<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
								<ItemStyle CssClass=""></ItemStyle>
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="������">
										<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<a href="#" onclick="OpenTask(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>'><%# DataBinder.Eval(Container, "DataItem.PicName") %></a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Master" HeaderText="������"></asp:BoundColumn>
									<asp:BoundColumn DataField="CompletePercent" HeaderText="��ɽ���" DataFormatString="{0}%"></asp:BoundColumn>
									<asp:BoundColumn DataField="ImportantName" SortExpression="ImportantLevel" HeaderText="��Ҫ�̶�"></asp:BoundColumn>
									<asp:BoundColumn DataField="PlannedStartDate" SortExpression="PlannedStartDate" HeaderText="�ƻ���ʼʱ��"
										DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
									<asp:BoundColumn DataField="PlannedFinishDate" SortExpression="PlannedFinishDate" HeaderText="�ƻ�����ʱ��"
										DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								</Columns>
								<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
							<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgTaskList" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination>
							<table width="100%">
								<tr id="trNoTask" runat="server">
									<td width="100%" align="center"></td>
								</tr>
							</table>
						</div>
					</TD>
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
