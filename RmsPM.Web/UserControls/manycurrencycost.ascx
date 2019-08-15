<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="inputCost" Src="inputCost.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.ManyCurrencyCost" CodeFile="ManyCurrencyCost.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="RmsPM.BLL.ControlsLB" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="cc2" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRateControl" Src="ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<script language="javascript" src="../images/convert.js"></script>
<script type="text/javascript" language="javascript" src="../UserControls/manycurrencycost.js"></script>

<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
	<TR>
		<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 22px; BORDER-BOTTOM-STYLE: none;WIDTH: 80px"
			align="left">�ܼƽ��:</td>
			<td style="text-align:left;border-top-style: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 22px; BORDER-BOTTOM-STYLE: none;">
			<DIV id="divTotalMoney" runat="server"></DIV>
		</td>
		<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
		<input class="button" id="btnQuery" type="button" value=" �鿴 " name="btnQuery" runat="server" onserverclick="btnQuery_ServerClick"><input class="button" id="btnSave" type="button" value=" ���� " name="btnSave" runat="server" onserverclick="btnSave_ServerClick"><input class="button" id="btnAdd" type="button" value=" ���� " name="btnAdd" runat="server" onserverclick="btnAdd_ServerClick"><input class="button" id="btnClose" type="button" value=" �ر� " name="btnClose" runat="server" onserverclick="btnClose_ServerClick">
		</td>
	</TR>
	<TR width="0">
		<TD id="tdDetial" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 0px; BORDER-BOTTOM-STYLE: none"
			colSpan="5" runat="server">
			<DIV id="CostDetail" style="Z-INDEX: 1; POSITION: absolute; BACKGROUND-COLOR: #e5f1fa;"
				runat="server"><asp:datagrid id="DataGrid1" runat="server" DataKeyField="CashDetialCode" AutoGenerateColumns="False">
					<FooterStyle CssClass="list-title"></FooterStyle>
					<HeaderStyle CssClass="list-title"></HeaderStyle>
					<Columns>
					<asp:BoundColumn Visible="False" DataField="MoneyType" HeaderText="MoneyType"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="Cash" HeaderText="Cash"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="RMB" HeaderText="RMB"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="ExchangeRate" HeaderText="ExchangeRate"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="���/����/����/�ۺ������">
							<ItemTemplate>
								<uc1:ExchangeRateControl id="ExchangeRateControl1" runat="server"></uc1:ExchangeRateControl>
							</ItemTemplate>
						</asp:TemplateColumn>				
						<asp:ButtonColumn Text="ɾ��" CommandName="Delete" HeaderText="ɾ��">
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</asp:ButtonColumn>
						</Columns>
				</asp:datagrid></DIV>
		</TD>
	</TR>
</TABLE>
