<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc3" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.ContractCostModify" CodeFile="ContractCostModify.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ExchangRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<script language="javascript">
<!--
function <%# this.ClientID %>SumRMB()
{

	var sClientID = "<%# this.ClientID %>";
	var sClientName = sClientID;
	
	while(sClientName.indexOf("_")>0) 
	{
		sClientName = sClientName.replace("_",":");
	}
	while(sClientName.indexOf("::")>0) 
	{
		sClientName = sClientName.replace("::",":_");
	}

	var c = parseInt(document.all( sClientID + "_dgCostCash").rows.length) - 2;
	var tempMoney = 0;
	var sum = 0;


	for(i=0;i<c;i++)
	{
		var a=ConvertFloat(<%# this.ClientID %>GetValue(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate:amount"));
		var b=ConvertFloat(<%# this.ClientID %>GetValue(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate:unitprise"));
		
//		if(a==0)
//		{
//			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate:amount").value=0;
//		}
//		if(b==0)
//		{
//			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate:unitprise").value=0;
//		}
		
//				if(ConvertFloat(a)!=0||ConvertFloat(b)!=0)
//			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate:ExchangeRateControl_C").readOnly =true;
//		else
//			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate:ExchangeRateControl_C").readOnly =false;
		
		var s=a*b;
	
		if (s!=0)	
		{
//			alert(s);
			
//			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate"+ ":ExchangeRateControl_EV").value=  s  ;
//			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate"+ ":ExchangeRateControl_E").value=  s  ;
			document.all("rptCostList__ctl0_ucCostModify_dgCostCash__ctl" + (i + 2) + "_ucExchangeRate"+ "_ExchangeRateControl_V").innerText=  s  ;
			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate"+ ":ExchangeRateControl_C").value=  s  ;
			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate"+ ":ExchangeRateControl_R").value=  s  ;
			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate"+ ":ExchangeRateControl_CV").value=  s  ;
//			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate"+ ":ExchangeRateControl_H").value=  s  ;
			//rptCostList__ctl0_ucCostModify_dgCostCash__ctl2_ucExchangeRate_ExchangeRateControl_V
			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate"+ ":ExchangeRateControl_R").value=  s  ;
			document.all(sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate"+ ":ExchangeRateControl_O").value=  s  ;
		
		}
		
		tempMoney = ConvertFloat( <%# this.ClientID %>GetRMBValue( sClientName + ":dgCostCash:_ctl" + (i + 2) + ":ucExchangeRate" ));
		sum = sum + tempMoney;
	}

	//alert('sum:'+sum);
	//alert(get2(gettext(document.getElementById( "DropDownList1"))));
	//alert(sum*get2(gettext(document.getElementById( "DropDownList1"))));
	//document.getElementById("StampDuty_t").value=sum*get2(gettext(document.getElementById( "DropDownList1")));		
	sum1=sum;
	//格式化
	sum = formatNumber(sum, "#,###.00");

	document.all( sClientID + "_dgCostCash__ctl" + (c + 2) + "_lblSumCashMoney").innerText = sum;
//	alert(sum1);
	return sum1;
//document.all( sClintID + "_dgCostList__ctl" + (c + 2) + "_lblSumMoney").innerText = sum;
}
function <%# this.ClientID %>calc(){
	<%# this.ClientID %>SumRMB();
	CalcStampDuty();
}
function <%# this.ClientID %>GetRMBValue( sClientName )
{	
	return document.all(sClientName + ":ExchangeRateControl_R").value;
}
function <%# this.ClientID %>GetValue( sClientName )
{	
	return document.all(sClientName).value;
}



-->
</script>
<asp:label id="hid_ContractCode" Runat="server" Visible="False"></asp:label><asp:label id="hid_ContractCostCode" Runat="server" Visible="False"></asp:label><asp:checkbox id="hid_CostReadOnly" Runat="server" Visible="False"></asp:checkbox>
<div onkeydown="if(event.keyCode==13) event.keyCode=9">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td valign="top" width="50%">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="intopic" width="200">合同款项明细()：<asp:label id="lblIndex" Runat="server"></asp:label></td>
						<td><asp:imagebutton id="imbDelete" Runat="server" BorderWidth="0" height="16" width="16" ImageUrl="../images/del.gif"></asp:imagebutton></td>
					</tr>
				</table>
				<table cellSpacing="0" cellPadding="0" width="99%" border="0" class="list">
					<tr>
						<td class="list-title" width="100">&nbsp;</td>
						<td class="list-title">明细</td>
					</tr>
					<tr>
						<td class="list-i" align="right">费用项：</td>
						<td class="list-i"><uc1:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server"></uc1:InputCostBudgetDtl><font color="red">*</font></td>
					</tr>
					<tr>
						<td class="list-i" align="right">说 明：</td>
						<td class="list-i"><asp:TextBox id="txtDescription" Runat="server" Width="100%" CssClass="input" TextMode="MultiLine"
								Rows="2" Height="50"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="list-title">&nbsp;</td>
						<td class="list-title">&nbsp;</td>
					</tr>
				</table>
				<table cellSpacing="0" cellPadding="0" width="99%" border="0">
					<tr>
						<td class="intopic" width="200">合同金额&nbsp;<font color="red">*</font></td>
						<td>
							<table>
								<tr>
									<td width="150">&nbsp;</td>
									<td>
										<input visible="false" class="button-small" id="btnNewCostCash" type="button" value="新增币种" name="btnNewCostCash"
											runat="server" onserverclick="btnNewCostCash_ServerClick">
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<asp:datagrid id="dgCostCash" Runat="server" ShowFooter="True" Width="99%" AutoGenerateColumns="False"
					CssClass="list" AllowPaging="False">
					<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
					<ItemStyle CssClass="list-i"></ItemStyle>
					<HeaderStyle CssClass="list-title"></HeaderStyle>
					<FooterStyle CssClass="list-title"></FooterStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="ContractCostCode"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="ContractCostCashCode"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="编号" ItemStyle-Width="30">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="金额/汇率">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
							<ItemTemplate>
								<uc1:ExchangeRate id="ucExchangeRate" runat="server" ValueChange='<%# this.ClientID + "calc()"%>'>
								</uc1:ExchangeRate>
							</ItemTemplate>
							<FooterTemplate>
								<asp:Label id="lblSumCashMoney" runat="server"></asp:Label>&nbsp;
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="删除"
							CommandName="Delete" ItemStyle-Width="30"></asp:ButtonColumn>
					</Columns>
				</asp:datagrid>
			</td>
			<td valign="top" width="50%" align="right">
				<table cellSpacing="0" cellPadding="0" width="99%" border="0">
					<tr>
						<td class="intopic" width="200">付款计划</td>
						<td>
							<table>
								<tr>
									<td width="150">&nbsp;</td>
									<td><input class="button-small" id="btnNewCostItem" type="button" value="新增付款计划" name="btnNewCostItem"
											runat="server" onserverclick="btnNewCostItem_ServerClick"></td>
									<td><input class="button-small" id="btnBuildPlan" type="button" value="按公式生成付款计划" name="btnBuildPlan"
											runat="server" onserverclick="btnBuildPlan_ServerClick"></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<asp:datagrid id="dgCostPlan" onkeydown="if(event.keyCode==13) event.keyCode=9" Runat="server"
					ShowFooter="True" Width="99%" AutoGenerateColumns="False" CssClass="list" AllowPaging="False">
					<AlternatingItemStyle Height="30"></AlternatingItemStyle>
					<ItemStyle Height="100px"></ItemStyle>
					<HeaderStyle CssClass="list-title"></HeaderStyle>
					<FooterStyle CssClass="list-title"></FooterStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="ContractCostCode"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="ContractCostPlanCode"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="序号" ItemStyle-Width="30">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="计划付款时间" ItemStyle-Width="120">
							<ItemTemplate>
								<cc3:calendar id="dtPlanningPayDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "PlanningPayDate")  %>'>
								</cc3:calendar>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="金额（元）" ItemStyle-Width="100">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<ItemTemplate>
								<igtxt:webnumericedit Width="100" id="txtMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.Money") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
									<ClientSideEvents ValueChange='InfraMoneyValueChange<% =this.ClientID.ToString() %>'>
									</ClientSideEvents>
								</igtxt:webnumericedit>
							</ItemTemplate>
							<FooterTemplate>
								<asp:Label id="lblSumPlanMoney" runat="server"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="付款条件">
							<ItemTemplate>
								<span id="spanPayConditionHtml"><input id="txtPayConditionText" runat="server" class="input" value='<%# DataBinder.Eval(Container, "DataItem.PayConditionText") %>' size="75" NAME="txtPayConditionText" style="width:100%">
								</span>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="删除"
							CommandName="Delete" ItemStyle-Width="30"></asp:ButtonColumn>
					</Columns>
				</asp:datagrid>
			</td>
		</tr>
	</table>
	<br>
	<asp:Label ID="lblJS" Runat="server"></asp:Label>
</div>
