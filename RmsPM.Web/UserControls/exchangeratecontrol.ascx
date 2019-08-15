<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.ExchangeRateControl" CodeFile="ExchangeRateControl.ascx.cs" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<asp:panel id="EditMoney" runat="server">
	<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
				vAlign="bottom" noWrap width="30%">
				<asp:checkbox id="hid_SimpleMode" Runat="server" Visible="False" Checked="True"></asp:checkbox>
		<asp:Panel runat="server" ID="Amount_Unitprise"><asp:Label ID="lbl_unitprise" runat="server" Text="单价:"></asp:Label>
				<asp:TextBox style="BEHAVIOR: url('../Images/RmsControls/javaScripts/Money.htc')"
					runat="server" Width="60" CssClass="input-nember" ID="unitprise" ></asp:TextBox>
				*<asp:Label ID="lbl_amount" runat="server" Text="数量:"></asp:Label>&nbsp;
				<asp:TextBox CssClass="input-nember" ID="amount" runat="server" Width="31px"></asp:TextBox>
				
				=</asp:Panel><asp:label id="Lb_CashTitle" runat="server">金额:</asp:label>
				<asp:TextBox id="ExchangeRateControl_C" style="BEHAVIOR: url('../Images/RmsControls/javaScripts/Money.htc')"
					runat="server" Width="80" CssClass="input-nember" preset="currency"></asp:TextBox><INPUT id="ExchangeRateControl_CV" style="DISPLAY: none; WIDTH: 5px; HEIGHT: 22px" size="1"
					runat="server"><input type="hidden" id="ExchangeRateControl_PreV" runat="server" />
			</TD>
			<TD style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
				vAlign="bottom" width="30%">
				<asp:dropdownlist id="ExchangeRateControl_M" style="BEHAVIOR: url('../Images/RmsControls/javaScripts/Money.htc')"
					Runat="server" preset="MoneyType"></asp:dropdownlist>
				<asp:dropdownlist id="ddlExchangeRateType" Runat="server" Visible="False" onselectedindexchanged="ddlExchangeRateType_SelectedIndexChanged">
					<asp:ListItem Value="RemittanceAverage" Selected="True">中间价</asp:ListItem>
					<asp:ListItem Value="RemittanceBuy">现汇买入价</asp:ListItem>
					<asp:ListItem Value="CashBuy">现钞买入价</asp:ListItem>
					<asp:ListItem Value="RemittanceSell">现汇卖出价</asp:ListItem>
					<asp:ListItem Value="CashSell">现钞卖出价</asp:ListItem>
				</asp:dropdownlist></TD>
			<TD style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
				vAlign="bottom" noWrap width="20%">
				<asp:label id="Lb_ExchangeTitle" runat="server">汇率:</asp:label>
				<asp:TextBox id="ExchangeRateControl_E" style="BEHAVIOR: url('../Images/RmsControls/javaScripts/Money.htc')"
					runat="server" Width="40px" CssClass="input-nember" preset="currency"></asp:TextBox><INPUT id="ExchangeRateControl_EV" style="DISPLAY: none; WIDTH: 3px; HEIGHT: 22px" size="1"
					runat="server"><INPUT id="ExchangeRateControl_H" style="DISPLAY: none; WIDTH: 3px; HEIGHT: 22px" size="1"
					name="Text1" runat="server">
			</TD>
			<TD style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
				vAlign="bottom" noWrap align="right" width="20%">
				<asp:label id="Lb_RMBTitle" runat="server">本币:</asp:label>
				<asp:label id="ExchangeRateControl_V" runat="server"></asp:label><INPUT id="ExchangeRateControl_R" style="DISPLAY: none; WIDTH: 16px; HEIGHT: 22px" type="text"
					size="1" runat="server"><INPUT id="ExchangeRateControl_O" style="DISPLAY: none; WIDTH: 16px; HEIGHT: 22px" type="text"
					size="1" name="Text1" runat="server">
			</TD>
			<TD id="td_Yuan" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
				vAlign="bottom" runat="server" visible="False"><FONT face="宋体" color="red">
					<DIV id="ExchangeRateControl_Y" runat="server"></DIV>
				</FONT>
			</TD>
		</TR>
	</TABLE>
</asp:panel><asp:panel id="ViewMoney" runat="server" Visible="false">
	<TABLE width="100%" border="0">
		<TR style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
			<TD style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
				noWrap>
				<DIV id="MoneyValue" runat="server"><FONT face="宋体"></FONT></DIV>
			</TD>
		</TR>
	</TABLE>
</asp:panel>
<input type="hidden" id="ExchangeRateControl_TemSum" runat="server" />

