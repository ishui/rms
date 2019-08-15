<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>
<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PaymentDetailModify" CodeFile="PaymentDetailModify.aspx.cs" %>
<%@ Register TagPrefix="uc3" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc3" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="uc4" TagName="InputGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>请款单</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<script language="javascript" src="../Rms.js" ></script>
		<script language="javascript" src="../images/convert.js"></script>
		<link href="../Images/index.css" type="text/css" rel="stylesheet" />
		<link href="../Images/infra.css" type="text/css" rel="stylesheet" />
		<script language="javascript">

//选择供应商
function SelectSupplier()
{
	OpenLargeWindow("../SelectBox/SelectSupplier.aspx?HelpPage=PayMent", "选择供应商");
}

//选择供应商返回
function DoSelectSupplierReturn(code, name)
{
	Form1.txtSupplyCode.value = code;
	Form1.txtSupplyName.value = name;
	document.all.spanSupplyName.innerText = name;
}

//选择供应商返回值赋值供应商，受款人，开户银行，银行帐号
function SelectSupplierAddInitValue(code,name,bank,account,reciver)
{
    Form1.txtSupplyCode.value = code;
	Form1.txtSupplyName.value = name;
	Form1.txtPayer.value = reciver;
	Form1.txtBankName.value = bank;
	Form1.txtBankAccount.value = account;
	document.all.spanSupplyName.innerText = name;
}

//选择分摊楼栋
function SelectBuilding(i)
{
	Form1.txtSelectDetailItemIndex.value = i;
	var AlloType = document.all("dgList__ctl" + i + "_txtAlloType").value;
	var code = document.all("dgList__ctl" + i + "_txtBuildingCodeAll").value;
	OpenCustomWindow("../SelectBox/SelectAlloBuilding.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&AlloType=" + AlloType + "&SelectCode=" + escape(code) + "&ReturnFunc=SelectBuildingReturn","选择楼栋", 400, 540);
//	OpenCustomWindow("../PBS/SelectBuilding.aspx?CanSelectArea=0&ProjectCode=" + Form1.txtProjectCode.value + "&SelectCode=" + escape(code) + "&ReturnFunc=SelectBuildingReturn","选择楼栋", 400, 540);
}

//选择分摊楼栋返回
function SelectBuildingReturn(AlloType, code, name)
{
	var i = Form1.txtSelectDetailItemIndex.value;
	
	if (AlloType.toUpperCase() == "P")
	{
		name = "项目";
	}
	
	document.all("dgList__ctl" + i + "_txtAlloType").value = AlloType;
	document.all("dgList__ctl" + i + "_txtBuildingCodeAll").value = code;
	document.all("dgList__ctl" + i + "_divBuildingNameAll").innerText = name;
	document.all("dgList__ctl" + i + "_txtBuildingNameAll").value = name;
}


function InfraMoneyValueChange(oEdit, oldValue, oEvent)
{
	//InfraCalcSum();
}

//计算合计
function InfraCalcSum()
{
	var c = parseInt(document.all.dgList.rows.length) - 2;
	var tempMoney = 0;
	var sum = 0;
	
	for(i=0;i<c;i++)
	{
		tempMoney = ConvertFloat(document.all("dgList:_ctl" + (i + 2) + ":txtItemMoney").value);
		sum = sum + tempMoney;
	}

	//格式化
	sum = formatNumber(sum, "#,###.00");
//	sum = FormatNumber(sum, 2);

	document.all.txtMoney.value = sum;	
	document.all("dgList__ctl" + (c + 2) + "_lblSumItemMoney").innerText = sum;
//	alert(sum);
}

//合同信息	
function ViewContractInfo()
{
	OpenFullWindow("../Contract/ContractInfo.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&ContractCode=" + Form1.txtContractCode.value,'合同信息');
}

//费用项信息
function ViewCostCode(code)
{
	OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=' + Form1.txtProjectCode.value + '&CostCode=' + code ,'动态费用项信息');
}

function SelectSummaryChange(i)
{
	Form1.txtSelectDetailItemIndex.value = i;
	Form1.btnHidSummaryChange.click();
}

function ExchangeRateChange(i)
{
	i = parseInt(i)
	
	if (( document.all("dgList:_ctl" + (i + 2) + ":ucExchangeRate:ExchangeRateControl_H").value != 
			document.all("dgList:_ctl" + (i + 2) + ":ucExchangeRate:ExchangeRateControl_M").value ) && 
		( document.all("dgList:_ctl" + (i + 2) + ":txtContractCostCashCode").value != "" && 
			document.all("dgList:_ctl" + (i + 2) + ":txtContractCostCashCode").value != "-1"))
	{
		Form1.txtSelectDetailItemIndex.value = i;
		Form1.btnHidSummaryChange.click();
	}
}

function  CheckMoney()
{
    var PaymentMoney = ConvertFloat(document.all("txtMoney").value);
    var UpperMoney =  ConvertFloat(document.all("hidUpperMoney").value);
    var AHMoney =  ConvertFloat(document.all("hidAHMoney").value);
    
//    alert(PaymentMoney + "+" + AHMoney + "?" + UpperMoney );
    var ReturnValue = false;
    var Hint = "总请款金额已经大于合同金额请款警戒线！是否继续请款？";
    

    if ( (PaymentMoney + AHMoney) > UpperMoney & UpperMoney>0)
    {
//        alert(PaymentMoney + "+" + AHMoney + ">" + UpperMoney );
        ReturnValue = window.confirm(Hint);
    }
    else
    {
//        alert(PaymentMoney + "+" + AHMoney + "<" + UpperMoney );

        ReturnValue = true;
    }
     
    if ( ReturnValue )
    {
        document.all.divHintSave.style.display = '';
    }
    
    return ReturnValue;
    
}


function SelectApplyPerson()
{
	OpenMiddleWindow('../SelectBox/SelectPerson.aspx?Flag=0&Type=Single','选择用户');
}

function DoSelectUser(userCode,userName,flag)
{
	// 选经办人
	if ( flag == 0 )
	{
		Form1.txtApplyPerson.value = userCode;
		Form1.txtApplyPersonName.value = userName;
	}
}


		</script>
	</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none">
				<input id="btnHidSummaryChange" type="button" name="btnHidSummaryChange" runat="server" onserverclick="btnHidSummaryChange_ServerClick" />
			</div>
			<table height="100%" cellspacing="0" cellpadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">请款单</td>
				</tr>
				<tr>
					<td valign="top" align="center">
						<table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr runat=server id="TrContractPayment">
								<td class="form-item" width="20%">请款单号：</td>
								<td>
								    <input class="input" id="txtPaymentID" style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px"
										readonly type="text" name="txtPaymentID" runat="server" />
								</td>

								<td class="form-item">请款总额：</td>
								<td>
								    <input id="txtMoney" style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px; TEXT-ALIGN: right"
										readonly type="text" size="12" name="txtMoney" runat="server" />&nbsp;元
										<input id="txttemMoney" type="hidden">
								</td>
							</tr>
							<tr id="TrPayment" runat="server">
			                    <td class="form-item" width="10%">请款名称：</td>
				                <td colspan="3">
				                 <input class="input" id="TxtPaymentName" style="width:45%;"  type="text" name="txtBankName" runat="server" /><font color="red">*</font>
				                </td>
							</tr>
							<tr>
								<td class="form-item">受款单位：</td>
								<td>
								    <span id="spanSupplyName" runat="server"></span>
								    <a id="hrefSelectSupply" title="选择供应商" onclick="SelectSupplier()" href="#" runat="server"><IMG src="../images/ToolsItemSearch.gif" border="0"></A><font color="blue">*</font>
								</td>
								<td class="form-item">受 款 人：</td>
								<td>
								    <input class="input" id="txtPayer" type="text" name="txtPayer" runat="server" /><font color="blue">*</font>
								</td>
							</tr>
							<tr>
								<td class="form-item">请款部门：</td>
								<td><uc2:inputunit id="ucUnit" runat="server"></uc2:inputunit></td>
								<td class="form-item">最后付款日：</td>
								<td>
								    <cc3:calendar id="dtPayDate" runat="server" CalendarResource="../Images/CalendarResource/" Display="True"
										Value="" ReadOnly="False"></cc3:calendar><%if("shidaipm"!=this.up_sPMNameLower){ %><font color="red">*</font><%} %>
							    </td>
							</tr>
							<tr id="trContract" runat="server">
								<td class="form-item">合同名称：</td>
								<td>
								    <a onclick="ViewContractInfo();" href="#"><asp:label id="lblContractName" Runat="server"></asp:label></a>
								</td>
								<td class="form-item">合同编号：</td>
								<td><asp:label id="lblContractID" runat="server"></asp:label></td>
							</tr>
							<tr id="trContract6" runat="server">
								<td class="form-item">合同类型：</td>
								<td><asp:label id="lblContractTypeName" Runat="server"></asp:label></td>
								<td class="form-item">付款期数：</td>
								<td>第&nbsp;<input class="input" id="txtIssue" style="WIDTH: 50px; TEXT-ALIGN: right" type="text" name="txtOperIssue"
										runat="server"  /> &nbsp;期<font color="red">*</font>
								</td>
							</tr>
							<tr>
								<td class="form-item">请款类型：</td>
								<td><uc4:inputgroup id="ucGroup" runat="server" ClassCode="0601"></uc4:inputgroup><font color="red">*</font>
								</td>
								<td class="form-item">相关业务：</td>
								<td><input id="txtPaymentTitle" runat="server" class="input" type="text" /></td>
							</tr>
							<tr>
								<td class="form-item">开户银行：</td>
								<td>
								    <input class="input" id="txtBankName" style="width: 150px" type="text" name="txtBankName" runat="server" />&nbsp;
								</td>
								<td class="form-item">银行帐号：</td>
								<td>
								    <input class="input" id="txtBankAccount" style="width: 150px" type="text" name="txtBankAccount" runat="server" />&nbsp;
								</td>
							</tr>
							<tr>
								<td class="form-item">经办人：</td>
								<td colspan="3">
								    <input class="input-readonly" id="txtApplyPersonName" readonly type="text" name="txtApplyPersonName" runat="server" />
								    <input id="txtApplyPerson" readonly="true" type="hidden" name="txtApplyPerson" runat="server" />
									<a onclick="SelectApplyPerson();return false;" href="#"><img src="../images/ToolsItemSearch.gif" border="0" /></A>
								</td>
							</tr>
							<tr>
								<td class="form-item">备注：</td>
								<td colspan="3">
								    <textarea id="txtRemark" style="WIDTH: 100%" name="txtRemark" rows="4" runat="server"></textarea>
								</td>
							</tr>
							<tr style="display: none">
								<td class="form-item">付款用途：</td>
								<td>
								    <input class="input" id="txtPurpose" type="text" size="30" name="txtPurpose" runat="server" />
								</td>
								<td class="form-item">单据张数：</td>
								<td>
								    <input class="input" id="txtRecieptCount" type="text" name="txtRecieptCount" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="form-item">附件文档：</td>
								<td colspan="3">
								    <uc1:AttachMentAdd id="myAttachMentAdd" runat="server" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top">
						<table cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td class="intopic" width="200">请款明细</td>
								<td>
								    <input class="button-small" id="btnAddDtl" type="button" value="新 增" name="btnAddDtl" runat="server" onserverclick="btnAddDtl_ServerClick" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<div style="overflow: auto; width: 100%; height: 100%; position:absolute">
							<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
								<tr>
									<td valign="top">
									    <asp:DataGrid id="dgList" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server" DataKeyField="PaymentItemCode"
											CellPadding="0" AllowSorting="True" GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True"
											Width="100%" CssClass="list">
											    <HeaderStyle CssClass="list-title" />
											    <FooterStyle CssClass="list-title" />
											    <Columns>
												<asp:TemplateColumn HeaderText="<input type='checkbox' name='chkAll' onclick='SelectAll();' title='全选或全不选'>" Visible="False">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle HorizontalAlign="Center" Wrap="False" />
													<ItemTemplate>
														<input onclick="ChkClick(this, true);" type="checkbox" value='<%#DataBinder.Eval(Container, "DataItem.PaymentItemCode")%>' name="chkSelect" />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle Wrap="False" HorizontalAlign="Center" />
													<ItemStyle HorizontalAlign="Center" Wrap="False" />
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="实际金额" Visible="False">
													<HeaderStyle HorizontalAlign="Right" />
													<ItemStyle HorizontalAlign="Right" Wrap="False" />
													<ItemTemplate>
														<igtxt:WebNumericEdit id="txtItemMoney" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItemMoney") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
															<ClientSideEvents ValueChange="InfraMoneyValueChange" />
														</igtxt:WebNumericEdit>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="款项名称"  FooterText="合计（RMB）：">
													<HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="Right" />
													<ItemTemplate>
														<div id="div_Cost" runat="server">
															<select id="sltSummary" style="DISPLAY: none" onchange="SelectSummaryChange(myindex);" 
																name="sltSummary" runat="server" myindex="<%# Container.ItemIndex %>" DataValueField="AllocateCode" 
																DataTextField="ItemName" DataSource="<%# GetSelectSummaryDataSource() %>">
															</select>
															<input class="input" id="txtSummary" type="text" value='<%# DataBinder.Eval(Container, "DataItem.Summary") %>' name="txtSummary" runat="server" />
														</div>
														<input id="txtPaymentItemCode" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.PaymentItemCode") %>' name="txtPaymentItemCode" runat="server" />
														<input id="txtAllocateCode" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.AllocateCode") %>' name="txtAllocateCode" runat="server" />
														<input id="txtContractCostCashCode" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.ContractCostCashCode") %>' name="txtContractCostCashCode" runat="server" />
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center" />
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="请款金额">
													<HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />													
													<ItemTemplate>
														<uc1:ExchangeRate id="ucExchangeRate" runat="server" ValueChange='<%# "ExchangeRateChange(" + Container.ItemIndex + ")" %>' />
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumItemMoney0" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="预付款">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />													
													<ItemTemplate>
														<igtxt:WebNumericEdit Width="60" id="txtItemCash1" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItemCash1") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
															<ClientSideEvents ValueChange="InfraMoneyValueChange" />
														</igtxt:WebNumericEdit>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumItemMoney1" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="验收款">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />													
													<ItemTemplate>
														<igtxt:WebNumericEdit Width="60" id="txtItemCash2" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItemCash2") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
															<ClientSideEvents ValueChange="InfraMoneyValueChange" />
														</igtxt:WebNumericEdit>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumItemMoney2" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="保修款">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />													
													<ItemTemplate>
														<igtxt:WebNumericEdit Width="60" id="txtItemCash3" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItemCash3") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
															<ClientSideEvents ValueChange="InfraMoneyValueChange" />
														</igtxt:WebNumericEdit>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumItemMoney3" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="结算扣款">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />													
													<ItemTemplate>
														<igtxt:WebNumericEdit Width="60" id="txtItemCash4" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItemCash4") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
															<ClientSideEvents ValueChange="InfraMoneyValueChange" />
														</igtxt:WebNumericEdit>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumItemMoney4" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="扣款保留">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />													
													<ItemTemplate>
														<igtxt:WebNumericEdit Width="60" id="txtItemCash5" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItemCash5") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
															<ClientSideEvents ValueChange="InfraMoneyValueChange" />
														</igtxt:WebNumericEdit>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumItemMoney5" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="其他扣款">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />													
													<ItemTemplate>
														<igtxt:WebNumericEdit Width="60" id="txtItemCash9" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItemCash9") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
															<ClientSideEvents ValueChange="InfraMoneyValueChange" />
														</igtxt:WebNumericEdit>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumItemMoney6" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="实际金额">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" Width="80"/>
													<FooterStyle HorizontalAlign="right" />
													<ItemTemplate>
													    <asp:Label ID="lblItemCash" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumItemMoney" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项">
													<HeaderStyle Wrap="False" HorizontalAlign="Left" />
													<ItemStyle Wrap="False"/>
													<ItemTemplate>
														<uc3:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server" CostCode='<%#DataBinder.Eval(Container, "DataItem.CostCode")%>' CostBudgetSetCode='<%#DataBinder.Eval(Container, "DataItem.CostBudgetSetCode")%>' />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="分摊" Visible="False">
													<HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<ItemTemplate>
														<span id="divBuildingNameAll" runat="server">
															<%# DataBinder.Eval(Container, "DataItem.BuildingNameAll") %>
														</span>
														<a onclick="SelectBuilding(<%#Container.ItemIndex + 2 %>);return false;" href="#">
															<img src="../images/ToolsItemSearch.gif" border="0" />
													    </a>
													    <input id="txtAlloType" type="hidden" value='<%#DataBinder.Eval(Container, "DataItem.AlloType")%>' name="txtAlloType" runat="server" />
														<input id="txtBuildingCodeAll" type="hidden" value='<%#DataBinder.Eval(Container, "DataItem.BuildingCodeAll")%>' name="txtBuildingCodeAll" runat="server" />
														<input id="txtBuildingNameAll" type="hidden" value='<%#DataBinder.Eval(Container, "DataItem.BuildingNameAll")%>' name="txtBuildingNameAll" runat="server" />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="删除">
													<ItemStyle Wrap="False" HorizontalAlign="Center"/>
													<HeaderStyle Wrap="False" HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:LinkButton id="btnDelete" runat="server" Text="<img src=../images/del.gif width=16 height=16 border=0>"
															CausesValidation="false" CommandName="Delete"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle CssClass="ListHeadTr" HorizontalAlign="Center" PrevPageText="<<<上页" NextPageText="下页>>>" />
										</asp:DataGrid>
									</td>
								</tr>
							</table>
							<table id="trContract2" style="display: none" cellspacing="0" cellpadding="0" border="0"
								runat="server">
								<tr>
									<td class="intopic" width="200">合同费用明细</td>
								</tr>
							</table>
							<table id="trContract3" style="display: none" cellspacing="0" cellpadding="0" width="100%"
								align="center" border="0" runat="server">
								<tr>
									<td valign="top">
									    <asp:DataGrid id="dgContractAllocation" runat="server" DataKeyField="AllocateCode" CellPadding="0"
											AllowSorting="True" GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%"
											CssClass="List">
											<HeaderStyle CssClass="list-title" />
											<FooterStyle CssClass="list-title" />
											<Columns>
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle HorizontalAlign="Center" Wrap="False" />
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="款项名称" FooterText="合计">
													<HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.ItemName") %>
													</ItemTemplate>
													<FOOTERSTYLE HorizontalAlign="Center" />
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项">
													<HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<ItemTemplate>
														<%# RmsPM.BLL.CBSRule.GetCostName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostCode")))%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="合同金额(元)">
													<HeaderStyle HorizontalAlign="Right" />
													<ItemStyle HorizontalAlign="Right" Wrap="False" />
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Money", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right" />
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="已请款(元)">
													<HeaderStyle HorizontalAlign="Right" />
													<ItemStyle HorizontalAlign="Right" Wrap="False" />
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.TotalPaymentMoney", "{0:N}") %>
													</ItemTemplate>
													<FOOTERSTYLE HorizontalAlign="Right" />
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle CssClass="ListHeadTr" HorizontalAlign="Center" PrevPageText="<<<上页" NextPageText="下页>>>" />
										</asp:DataGrid>
									</td>
								</tr>
							</table>
							<table id="trContract4" style="display: none" cellspacing="0" cellpadding="0" width="100%"
								border="0" runat="server">
								<tr>
									<td class="intopic" width="200">合同付款计划</td>
									<td></td>
								</tr>
							</table>
							<table id="trContract5" style="DISPLAY: none" cellspacing="0" cellpadding="0" width="100%"
								align="center" border="0" runat="server">
								<tr>
									<td valign="top">
									    
									    <asp:DataGrid id="dgContractPaymentPlan" runat="server" CellPadding="0" AllowSorting="True" GridLines="Horizontal"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="list">
											<AlternatingItemStyle CssClass="AlterGridTr" />
											<HeaderStyle CssClass="list-title" />
											<FooterStyle CssClass="list-title" />
											<Columns>
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle HorizontalAlign="Center" Wrap="False" />
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn HeaderText="付款计划步骤" FooterText="合计" DataField="PlanStep"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="付款时间" DataField="PlanningPayDate" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="付款条件" DataField="PlanningPayCondition"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="金 额" DataField="Money" DataFormatString="{0:N}">
													<HeaderStyle HorizontalAlign="Right" />
													<ItemStyle HorizontalAlign="Right" />
													<FooterStyle HorizontalAlign="Right" />
												</asp:BoundColumn>
											</Columns>
											<PagerStyle CssClass="list-title" HorizontalAlign="Right" PrevPageText="<<<上页" NextPageText="下页>>>" />
										</asp:DataGrid>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="10" width="100%">
							<tr>
								<td align="center">
								    <input id="btnSave" runat="server" class="submit" name="btnSave" onclick="javascript:if(!CheckMoney()) return false;"
                                         onserverclick="btnSave_ServerClick" type="button"
                                        value="确 定" /> 
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="display: none; left: 1px; width: 100%; position: absolute; top: 200px; background-color: transparent">
				<table id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td vAlign="top" align="center">
						    <iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</td>
					</tr>
				</table>
			</div>
			<div id="divHintSave" style="display: none; left: 1px; width: 100%; position: absolute; top: 200px">
				<table id="tableHintSave" height="100" cellspacing="0" cellpadding="0" width="100%" border="0">
					<tr>
						<td vAlign="top" align="center">
						    <iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</td>
					</tr>
				</table>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server" /> 
			<input id="txtIsNew" type="hidden" name="txtIsNew" runat="server" />
			<input id="txtContractCode" type="hidden" name="txtContractCode" runat="server" />
			<input id="txtStatus" type="hidden" name="txtStatus" runat="server" />
			<input id="txtIsContract" type="hidden" name="txtCode" runat="server" /> 
			<input id="txtPaymentCode" type="hidden" name="txtPaymentCode" runat="server" />
			<input id="txtDetailSno" type="hidden" name="txtDetailSno" runat="server" /> 
			<input id="txtSupplyCode" type="hidden" name="txtSupplyCode" runat="server" />
			<input id="txtSupplyName" type="hidden" name="txtSupplyName" runat="server" />
			<input id="hidUpperMoney" type="hidden" name="hidContractMoney" runat="server" />
			<input id="hidAHMoney" type="hidden" name="hidUpperMoney" runat="server" />
			<input id="txtSelectDetailItemIndex" type="hidden" name="txtSelectDetailItemIndex" runat="server" />
			<input id="txtContractType" type="hidden" name="txtContractType" runat="server" />
			<select id="sltSummaryEg" name="sltSummaryEg" runat="server">
				<option value="" selected></option>
			</select>
		</form>
	</body>
</html>
