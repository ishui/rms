<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRateControl" Src="ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="cc1" Namespace="RmsPM.BLL.ControlsLB" Assembly="RmsPM.BLL" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.ExchangeMoney_Control" CodeFile="ExchangeMoney_Control.ascx.cs" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
	<TR>
		<TD id="TD1" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
			runat="server"><igtxt:webnumericedit id="Webnumericedit1" onblur="BiddingCheckMoney(this);" runat="server" MinDecimalPlaces="Two"
				CssClass="infra-input-nember" ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
				JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" Width="152px"></igtxt:webnumericedit>
			<DIV id="lb_MoneyMessage" style="DISPLAY: inline; HEIGHT: 15px" runat="server"></DIV>
			<asp:label id="Lb_RMB" runat="server"></asp:label></TD>
		<TD style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
			align="right"><INPUT class="button" id="Bt_AddMoney" type="button" value="添加明细" name="btnSave" runat="server" onserverclick="Bt_AddMoney_ServerClick">&nbsp;<INPUT class="button" id="btnSave" type="button" value=" 保存 " name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;</FONT><INPUT class="button" id="Bt_Add" type="button" value=" 新增 " name="btnSave" runat="server" onserverclick="Bt_Add_ServerClick"><FONT face="宋体">&nbsp;</FONT><INPUT class="button" id="Bt_Close" type="button" value=" 关闭 " name="btnSave" runat="server" onserverclick="Bt_Close_ServerClick"></TD>
	</TR>
	<TR width="0">
		<TD style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 2px; BORDER-BOTTOM-STYLE: none"
			colSpan="2">
			<DIV id="ReturnDetail" style="Z-INDEX: 1; POSITION: absolute; BACKGROUND-COLOR: #e5f1fa"
				runat="server"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" DataKeyField="BiddingReturnCostCode">
					<FooterStyle CssClass="list-title"></FooterStyle>
					<HeaderStyle CssClass="list-title"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn Visible="False" HeaderText="费用项">
							<ItemTemplate>
								<asp:DropDownList id="DropDownList1" runat="server"></asp:DropDownList>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="False" HeaderText="金额">
							<ItemTemplate>
								<igtxt:webnumericedit id=Txt_Money onblur=BiddingCheckMoney(this); runat="server" Width="70px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" CssClass="infra-input-nember" MinDecimalPlaces="Two" ValueText='<%# DataBinder.Eval(Container, "DataItem.Cash") %>'>
								</igtxt:webnumericedit>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="False" HeaderText="币种信息">
							<ItemTemplate>
								<cc2:MoneyList id=MoneyList1 runat="server" Width="80px" Height="18px" DefaultSelectText='<%# DataBinder.Eval(Container, "DataItem.MoneyType") %>'>
								</cc2:MoneyList>
								<cc1:ExchangeTypes id="ExchangeTypes1" runat="server" Width="60px" Height="18px"></cc1:ExchangeTypes>
								<igtxt:webnumericedit id=input_ExchangeRate onblur=BiddingCheckMoney(this); runat="server" Width="50px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" CssClass="infra-input-nember" MinDecimalPlaces="Two" ValueText='<%# DataBinder.Eval(Container, "DataItem.ExchangeRate") %>'>
								</igtxt:webnumericedit>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="金额/币种/汇率/折合人民币">
							<ItemTemplate>
								<uc1:ExchangeRateControl id="ExchangeRateControl1" runat="server"></uc1:ExchangeRateControl>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="备注">
							<ItemTemplate>
								<TEXTAREA id="TB_Remark" onblur="ObjectReduce(this)" style="WIDTH: 160px; HEIGHT: 100%" onfocus="ObjectSpread(this)"
									name="txtRemark" rows="1" cols="20" runat="server"><%# DataBinder.Eval(Container, "DataItem.Remark")  %>;</TEXTAREA>
								<asp:Label id="Lb_Remark" runat="server" Width="160px" Visible="False" Height="18px">
									<%# DataBinder.Eval(Container, "DataItem.Remark") %>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:ButtonColumn Text="删除" ButtonType="PushButton" CommandName="Delete">
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</asp:ButtonColumn>
					</Columns>
				</asp:datagrid></DIV>
		</TD>
	</TR>
</TABLE>
