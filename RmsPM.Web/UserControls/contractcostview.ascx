<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.ContractCostView" CodeFile="ContractCostView.ascx.cs" %>
<div id="div_CostBudgetDtl" runat="server" Visible="False">
	<uc1:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server" Visable="False"></uc1:InputCostBudgetDtl>
	<asp:label id="lblIndex" EnableViewState="False" Runat="server" Visible="False"></asp:label>
</div>
<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="form-item" width="15%">��λ���̣�</td>
		<td width="18%"><asp:label id="lblPBSName" Runat="server"></asp:label></td>
		<td class="form-item" width="15%">�����</td>
		<td width="18%"><asp:label id="lblCostName" Runat="server"></asp:label></td>
		<td class="form-item" width="15%">���(Ԫ)��</td>
		<td width="19%"><asp:label id="lblTotalMoney" Runat="server"></asp:label></td>
	</tr>
	<tr>
		<td class="form-item">����(Ԫ)��</td>
		<td><asp:label id="lblAHMoney" Runat="server"></asp:label></td>
		<td class="form-item">����%��</td>
		<td colspan="3"><asp:label id="lblAHMoneyPer" Runat="server"></asp:label></td>
	</tr>
	<tr>
		<td class="form-item">�Ѹ�(Ԫ)��</td>
		<td><asp:label id="lblAPMoney" Runat="server"></asp:label></td>
		<td class="form-item">δ��(Ԫ)��</td>
		<td colspan="3"><asp:label id="lblUPMoney" Runat="server"></asp:label></td>
	</tr>
	<tr>
		<td class="form-item">˵����</td>
		<td colSpan="5"><asp:label id="lblDescription" Runat="server"></asp:label></td>
	</tr>
</table>
<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
	<TR>
		<TD><asp:datagrid id="dgCostList" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
				PageSize="15" CssClass="list" Width="100%">
				<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
				<HeaderStyle CssClass="list-title"></HeaderStyle>
				<FooterStyle CssClass="list-title"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="ContractCostPlanCode"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="���" ItemStyle-Width="100">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1 %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="PlanningPayDate" HeaderText="�ƻ�����ʱ��" DataFormatString="{0:yyyy-MM-dd}"
						HeaderStyle-Wrap="False" ItemStyle-Wrap="False" ItemStyle-Width="100"></asp:BoundColumn>
					<asp:BoundColumn DataField="PayConditionText" HeaderText="��������" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"
						FooterText="�ϼƣ�" FooterStyle-HorizontalAlign="Right"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="�� ��" ItemStyle-Width="80">
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
				<PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
					CssClass="ListHeadTr"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
</TABLE>
