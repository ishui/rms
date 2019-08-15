<%@ Page language="c#" Inherits="RmsPM.Web.ExchangeRate.ExchangeRateModify" CodeFile="ExchangeRateModify.aspx.cs" %>
<%@ Register TagPrefix="uc4" TagName="InputGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc3" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="uc3" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>汇率信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script language="javascript">


function InfraRemittanceAverage(oEdit, oldValue, oEvent)
{
	InfraCalcSum();
}

//计算合计
function InfraCalcSum()
{
	var c = parseInt(document.all.dgExchangeRateList.rows.length) - 2;
	var RemittanceBuy = 0;
	var RemittanceSell = 0;
	var RemittanceAverage = 0;
	
	for(i=0;i<c;i++)
	{
		RemittanceBuy = ConvertFloat(document.all("dgExchangeRateList:_ctl" + (i + 2) + ":txtRemittanceBuy").value);
		RemittanceSell = ConvertFloat(document.all("dgExchangeRateList:_ctl" + (i + 2) + ":txtRemittanceSell").value);
		
		if ( RemittanceBuy != 0 && RemittanceSell != 0 )
		{
			RemittanceAverage = (RemittanceBuy + RemittanceSell)/2;

			document.all("dgExchangeRateList:_ctl" + (i + 2) + ":txtRemittanceAverage").value = formatNumber(RemittanceAverage, "#,###.00");

		}
		
	}

}


		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">汇率管理</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnDelete" type="button" value="删除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">
					</td>
				</TR>
				<tr>
					<td align="right" valign="middle" height="40">单位：人民币/100外币&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic" width="200">汇率明细</td>
								<td><input class="button-small" id="btnAddDtl" type="button" value="新 增" name="btnAddDtl" runat="server" onserverclick="btnAddDtl_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD vAlign="top"><asp:datagrid id="dgExchangeRateList" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server"
											DataKeyField="ExchangeRateCode" CellPadding="0" AllowSorting="True" GridLines="Both" AutoGenerateColumns="False"
											PageSize="15" ShowFooter="True" Width="100%" CssClass="list">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:BoundColumn HeaderText="系统编号" DataField="ExchangeRateCode" Visible="False"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="币种">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<SELECT id="sltMoneyType" style="WIDTH: 136px" name="sltMoneyType" runat="server">
															<OPTION value="" selected>---------请选择---------</OPTION>
														</SELECT>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="现汇买入价">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>                                                        
														<asp:TextBox id="txtRemittanceBuy"  runat="server"  CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.RemittanceBuy") %>' >
															</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="现钞买入价">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>	
														<asp:TextBox id="txtCashBuy" runat="server" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.CashBuy") %>' >
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="现汇卖出价">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="txtRemittanceSell" runat="server"  CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.RemittanceSell") %>'>
													
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="现钞卖出价">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="txtCashSell" runat="server"  CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.CashSell") %>' >
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="中间价">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="txtRemittanceAverage" runat="server" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.RemittanceAverage") %>' >
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="日期">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<cc3:calendar id="dtCreateDate" runat="server" CalendarResource="../Images/CalendarResource/" Display="True" Value='<%# DataBinder.Eval(Container, "DataItem.CreateDate") %>'>
														</cc3:calendar>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="删除">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton runat="server" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
															CommandName="Delete" CausesValidation="false" ID="btnDeleteDtl"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
									</TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" onclick="document.all.divHintSave.style.display = '';"
										type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
		</form>
	</body>
</HTML>
