<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc3" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.ContractCostlist" CodeFile="ContractCostlist.ascx.cs" %>
<script language="javascript">
<!--

function InfraMoneyValueChange<% =this.ClientID.ToString() %>(oEdit, oldValue, oEvent)
{
	InfraCostSum("<% =this.ClientID.ToString() %>");
}
//-->
</script>
<div onkeydown="if(event.keyCode==13) event.keyCode=9">
	<asp:label id="hid_ContractCode" Visible="False" Runat="server"></asp:label><asp:label id="hid_ContractCostCode" Visible="False" Runat="server"></asp:label><asp:checkbox id="hid_CostEnable" Visible="False" Runat="server" Checked="True"></asp:checkbox>
	<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD class="form-item" width="100">序号：</TD>
			<TD><asp:label id="lblIndex" Runat="server" EnableViewState="False"></asp:label></TD>
			<TD colSpan="4"><asp:imagebutton id="imbDelete" Runat="server" ImageUrl="../images/del.gif" width="16" height="16"
					BorderWidth="0"></asp:imagebutton></TD>
		</TR>
		<tr>
			<td class="form-item">费用项：</td>
			<td width="300">
				<uc1:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server"></uc1:InputCostBudgetDtl><font color="red">*</font>
			</td>
			<td class="form-item" width="100">金额(元)：</td>
			<td>
				<asp:Label ID="hid_OldMoney" Runat="server" Visible="False"></asp:Label>
				<igtxt:webnumericedit id="txtTotalMoney" runat="server" MinDecimalPlaces="Two" ImageDirectory="../images/infragistics/images/"
					JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
					Width="100" CssClass="infra-input-nember"></igtxt:webnumericedit>
			</td>
		</tr>
		<tr>
			<td class="form-item">说明：</td>
			<td colSpan="3"><asp:textbox id="txtDescription" Runat="server" Width="100%" TextMode="MultiLine" Height="40"></asp:textbox></td>
		</tr>
	</table>
	<br>
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="intopic" width="200">付款计划</td>
			<td>
				<table>
					<tr>
						<td width="150">&nbsp;</td>
						<td><input class="button-small" id="btnNewCostItem" type="button" value="新增付款计划" name="btnNewCostItem"
								runat="server" onserverclick="btnNewCostItem_ServerClick"></td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
		<TR>
			<TD><asp:datagrid id="dgCostList" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server"
					Width="100%" CssClass="list" PageSize="15" AutoGenerateColumns="False" AllowSorting="True"
					ShowFooter="True">
					<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
					<HeaderStyle CssClass="list-title"></HeaderStyle>
					<FooterStyle CssClass="list-title"></FooterStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="ContractCostPlanCode"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="序号" ItemStyle-Width="80">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="计划付款时间&lt;font color=blue&gt;*&lt;/font&gt;" ItemStyle-Width="150" FooterText="合计">
							<ItemTemplate>
								<cc3:calendar id="dtPlanningPayDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "PlanningPayDate")  %>'>
								</cc3:calendar>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="付款条件" ItemStyle-Width="400">
							<ItemTemplate>
								<span id="spanPayConditionHtml"><input id="txtPayConditionText" runat="server" class="input" value='<%# DataBinder.Eval(Container, "DataItem.PayConditionText") %>' size="75">
								</span>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="金额（元）&lt;font color=blue&gt;*&lt;/font&gt;" ItemStyle-Width="120">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<ItemTemplate>
								<igtxt:webnumericedit Width="100" id="txtMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.Money") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
								</igtxt:webnumericedit>
							</ItemTemplate>
							<FooterTemplate>
								<asp:Label id="lblSumMoney" runat="server"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="删除"
							CommandName="Delete"></asp:ButtonColumn>
					</Columns>
					<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
						CssClass="ListHeadTr"></PagerStyle>
				</asp:datagrid></TD>
		</TR>
	</TABLE>
</div>
<br>
<br>
