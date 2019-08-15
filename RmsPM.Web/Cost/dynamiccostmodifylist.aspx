<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicCostModifyList" CodeFile="DynamicCostModifyList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��̬�����޸ļ�¼</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��̬�����޸ļ�¼</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table class="search-area" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<table>
										<tr>
											<td>������Ŀ��</td>
											<td><select id="sltCost" runat="server" NAME="sltCost">
													<option value="" selected>-----��ѡ��-----</option>
												</select></td>
											<td>����״̬��</td>
											<td><select id="sltFlag" runat="server" NAME="sltFlag">
													<option value="">-----��ѡ��-----</option>
													<option selected value="1">δ���</option>
													<option value="2">�����</option>
													<option value="3">����</option>
												</select></td>
											<td><INPUT class="submit" id="btnSearch" type="button" value="�� ��" name="Button1" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
										<tr>
											<td nowrap colspan="2">����ʱ�䣺<cc3:calendar id="dtMakeDate0" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
												&nbsp;����<cc3:calendar id="dtMakeDate1" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
													Display="True"></cc3:calendar></td>
											<td nowrap colspan="2">���ʱ�䣺<cc3:calendar id="dtCheckDate0" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True"></cc3:calendar>
												&nbsp;����<cc3:calendar id="dtCheckDate1" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True"></cc3:calendar></td>
											<td></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<asp:datagrid id="dgList" runat="server" ShowFooter="True" Width="100%" CellPadding="2" AutoGenerateColumns="False"
							CssClass="list">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="����ʱ��">
									<ItemTemplate>
										<a href="##" onclick="doViewApply(this)" budgetCode='<%# DataBinder.Eval(Container.DataItem, "BudgetCode") %>' >
											<%# DataBinder.Eval(Container.DataItem, "CheckDate","{0:yyyy-MM-dd}") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="����������">
									<ItemTemplate>
										<a href="##" onclick="doViewDynamicCostInfo(this)" budgetCode='<%# DataBinder.Eval(Container.DataItem, "BudgetCode") %>' costCode='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
											<%# DataBinder.Eval(Container.DataItem, "SortID") %>
											<%# DataBinder.Eval(Container.DataItem, "CostName") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="��̬���">
									<ItemTemplate>
										<div align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "BudgetCost")) %></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="���״̬">
									<ItemTemplate>
										<div><%# DataBinder.Eval(Container.DataItem, "FlagName") %></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="�����">
									<ItemTemplate>
										<div><%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "CheckPerson").ToString() ) %></div>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--
		function doViewApply(obj)
		{
			var budgetCode = obj.budgetCode;
			OpenFullWindow( '../Cost/DynamicApplyInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode='+budgetCode,"��̬����������Ϣ" );
		}
			
		function doViewDynamicCostInfo(obj)
		{
			var costCode = obj.costCode;
			var budgetCode = obj.budgetCode;
			OpenFullWindow( '../Cost/DynamicCostInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode='+budgetCode + '&CostCode=' + costCode  ,"��̬����������Ϣ" );
		}
		
//-->
		</script>
	</body>
</HTML>
