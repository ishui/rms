<%@ Page language="c#" Inherits="RmsPM.Web.Cost.BudgetHistoryList" CodeFile="BudgetHistoryList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>历次预算列表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">费用管理 
									-
									<asp:Label id="lblBudget" runat="server"></asp:Label>
								</td>
								<td width="79" onclick="window.navigate('../Desktop.aspx'); return false;" style="CURSOR: hand"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<asp:datagrid id="dgList" runat="server" CssClass="list" AutoGenerateColumns="False" CellPadding="2"
							Width="100%">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="名称">
									<ItemTemplate>
										<a onclick="doViewBudget(this); return false;" href="##" 
										code='<%#  DataBinder.Eval(Container.DataItem, "BudgetCode") %>'
										isDynamic='<%#  DataBinder.Eval(Container.DataItem, "isDynamic") %>'
										>
											<%#  DataBinder.Eval(Container.DataItem, "BudgetName") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="总预算">
									<ItemTemplate>
										<div align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "TotalMoney") ) %></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="期前累计发生费用">
									<ItemTemplate>
										<div align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "BeforeHappenCost") ) %></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="本期预算">
									<ItemTemplate>
										<div align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost") ) %></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="后续预算">
									<ItemTemplate>
										<div align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost") ) %></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="制定人">
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "MakePerson").ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="MakeDate" HeaderText="制定时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="审核人">
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "CheckPerson").ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CheckDate" HeaderText="审核时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
							</Columns>
						</asp:datagrid>
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
		</form>
		<script language="javascript">
<!--
	function doViewBudget( obj)
	{
		var code = obj.code;
		var isDynamic = obj.isDynamic;
		if ( isDynamic == "0" )
			window.navigate( '../Cost/Budget.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode=' + code  ,'预算费用' );
		else
			window.navigate( '../Cost/DynamicCost.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode=' + code  ,'动态费用' );
		
		
	}
//-->
		</script>
	</body>
</HTML>
