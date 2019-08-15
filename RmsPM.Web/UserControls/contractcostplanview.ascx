<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.ContractCostPlanView" CodeFile="ContractCostPlanView.ascx.cs" %>
<br>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="intopic" width="200">付款计划</td>
		<td><input class="button-small" id="btnModifyPaymentPlan" onclick="DoModifyPaymentPlan();return false;"
				type="button" value="修改付款计划" name="btnModifyPaymentPlan" runat="server"></td>
	</tr>
</table>
<asp:datagrid id="dgCostPlanList" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
	CssClass="list" Width="100%">
	<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<FooterStyle CssClass="list-title"></FooterStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="ContractCostPlanCode"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="序号" ItemStyle-Width="100">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<%# Container.ItemIndex + 1 %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="费用项" Visible="False">
			<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<uc1:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server" Visable="False"></uc1:InputCostBudgetDtl>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="单位工程">
			<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:Label ID="lblPBSName" Runat="server"></asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="费用项">
			<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:Label ID="lblCostName" Runat="server"></asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="PlanningPayDate" HeaderText="计划付款时间" DataFormatString="{0:yyyy-MM-dd}"
			HeaderStyle-Wrap="False" ItemStyle-Wrap="False" ItemStyle-Width="100"></asp:BoundColumn>
		<asp:BoundColumn DataField="PayConditionText" HeaderText="付款条件" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"
			FooterText="合计：" FooterStyle-HorizontalAlign="Right"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="金 额" ItemStyle-Width="80">
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
			<FooterStyle HorizontalAlign="Right"></FooterStyle>
			<ItemTemplate>
				<asp:Label id="lblMoney" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Money","{0:N}") %>'>
				</asp:Label>
			</ItemTemplate>
			<FooterTemplate>
				<asp:Label id="lblSumMoney" runat="server"></asp:Label>
			</FooterTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
