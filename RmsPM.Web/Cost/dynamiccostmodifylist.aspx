<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicCostModifyList" CodeFile="DynamicCostModifyList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>动态费用修改记录</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">动态费用修改记录</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table class="search-area" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<table>
										<tr>
											<td>费用项目：</td>
											<td><select id="sltCost" runat="server" NAME="sltCost">
													<option value="" selected>-----请选择-----</option>
												</select></td>
											<td>调整状态：</td>
											<td><select id="sltFlag" runat="server" NAME="sltFlag">
													<option value="">-----请选择-----</option>
													<option selected value="1">未审核</option>
													<option value="2">已审核</option>
													<option value="3">作废</option>
												</select></td>
											<td><INPUT class="submit" id="btnSearch" type="button" value="搜 索" name="Button1" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
										<tr>
											<td nowrap colspan="2">申请时间：<cc3:calendar id="dtMakeDate0" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
												&nbsp;到：<cc3:calendar id="dtMakeDate1" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
													Display="True"></cc3:calendar></td>
											<td nowrap colspan="2">审核时间：<cc3:calendar id="dtCheckDate0" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True"></cc3:calendar>
												&nbsp;到：<cc3:calendar id="dtCheckDate1" runat="server" CalendarResource="../Images/CalendarResource/"
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
								<asp:TemplateColumn HeaderText="调整时间">
									<ItemTemplate>
										<a href="##" onclick="doViewApply(this)" budgetCode='<%# DataBinder.Eval(Container.DataItem, "BudgetCode") %>' >
											<%# DataBinder.Eval(Container.DataItem, "CheckDate","{0:yyyy-MM-dd}") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="费用项名称">
									<ItemTemplate>
										<a href="##" onclick="doViewDynamicCostInfo(this)" budgetCode='<%# DataBinder.Eval(Container.DataItem, "BudgetCode") %>' costCode='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
											<%# DataBinder.Eval(Container.DataItem, "SortID") %>
											<%# DataBinder.Eval(Container.DataItem, "CostName") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="动态金额">
									<ItemTemplate>
										<div align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "BudgetCost")) %></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="审核状态">
									<ItemTemplate>
										<div><%# DataBinder.Eval(Container.DataItem, "FlagName") %></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="审核人">
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
			OpenFullWindow( '../Cost/DynamicApplyInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode='+budgetCode,"动态费用申请信息" );
		}
			
		function doViewDynamicCostInfo(obj)
		{
			var costCode = obj.costCode;
			var budgetCode = obj.budgetCode;
			OpenFullWindow( '../Cost/DynamicCostInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode='+budgetCode + '&CostCode=' + costCode  ,"动态费用申请信息" );
		}
		
//-->
		</script>
	</body>
</HTML>
