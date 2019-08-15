<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSExecuteList" CodeFile="WBSExecuteList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WBSExecuteList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
		<!--
			//��ӹ�������
			function DoAddNewExecute(WBSCode)
			{
				OpenFullWindow('WBSExecute.aspx?ActionState=Insert&WBSCode='+WBSCode+"&ProjectCode=<%=Request["ProjectCode"]%>",'');
			}			
			//�򿪹�������������
			function OpenExecute(ExecuteCode)
			{
				OpenFullWindow('WBSExecuteInfo.aspx?TaskExecuteCode=' + ExecuteCode+"&ProjectCode=<%=Request["ProjectCode"]%>",'');
			}

		//-->
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
							<tr width="100%">
								<td class="topic" background="../images/topic_bg.gif" width="100%"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									�������� - ��������
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
								<td>
									�������⣺<input style="WIDTH: 120px" type="text" name="txtTitle" id="txtTitle" class="input" runat="server">
									&nbsp;&nbsp;�������ڣ�<cc3:calendar id="dtStartDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>
									&nbsp;&nbsp;����<cc3:calendar id="dtEndDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
										Value=""></cc3:calendar>
									<input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:datagrid id="dgList" runat="server" Width="100%" DataKeyField="TaskExecuteCode" AutoGenerateColumns="False"
								PageSize="20" CssClass="list" AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
								<ItemStyle CssClass=""></ItemStyle>
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="��������">
										<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<a style="cursor:hand" onclick="javascript:OpenExecute(this.code);" code='<%# DataBinder.Eval(Container, "DataItem.TaskExecuteCode") %>'>
												<%# RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container, "DataItem.Detail"), 20) %>
											</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ExecutePersonName" HeaderText="������" SortExpression="ExecutePersonName"></asp:BoundColumn>
									<asp:BoundColumn DataField="ExecuteDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ExecuteDate"></asp:BoundColumn>
									<asp:BoundColumn DataField="InputDate" HeaderText="¼������" DataFormatString="{0:yyyy-MM-dd}" SortExpression="InputDate"></asp:BoundColumn>
								</Columns>
								<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
							<table align="center" width="100%">
								<tr id="trNoExecute" runat="server" align="center" valign="top">
									<td colspan="2">�޹�������</td>
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
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
