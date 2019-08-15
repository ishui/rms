<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>
<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PaymentProductionModify" CodeFile="PaymentProductionModify.aspx.cs" %>
<%@ Register TagPrefix="uc3" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc3" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="uc4" TagName="InputGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>请款单</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script language="javascript">

//选择供应商
function SelectSupplier()
{
	OpenLargeWindow("../SelectBox/SelectSupplier.aspx", "选择供应商");
}

//选择供应商返回
function DoSelectSupplierReturn(code, name)
{
	Form1.txtSupplyCode.value = code;
	Form1.txtSupplyName.value = name;
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
	InfraCalcSum();
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



		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none">
				<input id="btnHidSummaryChange" type="button" name="btnHidSummaryChange" runat="server" onserverclick="btnHidSummaryChange_ServerClick">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">请款单</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD class="form-item" width="100px">请款单号：</TD>
								<TD colspan="3"><input class="input" id="txtPaymentID" style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px"
										readOnly type="text" name="txtPaymentID" runat="server">
								</TD>
								<TD class="form-item">请款总额：</TD>
								<TD><input id="txtMoney" style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px; TEXT-ALIGN: right"
										readOnly type="text" size="12" name="txtMoney" runat="server">&nbsp;元
								</TD>
							</tr>
							<!--							<TR>
								<TD class="form-item">付款任务：</TD>
								<TD colSpan="3"><SELECT class="select" id="sltTask" name="sltTask" runat="server">
										<OPTION selected>----请选择----</OPTION>
									</SELECT></TD>
							</TR>-->
							<TR>
								<TD class="form-item">受款单位：</TD>
								<TD colspan="3"><span id="spanSupplyName" runat="server"></span><A id="hrefSelectSupply" title="选择供应商" onclick="SelectSupplier()" href="#" runat="server"><IMG src="../images/ToolsItemSearch.gif" border="0"></A><font color="blue">*</font>
								</TD>
								<TD class="form-item">受 款 人：</TD>
								<td><input class="input" id="txtPayer" type="text" name="txtPayer" runat="server"><font color="blue">*</font></td>
							</TR>
							<tr>
								<TD class="form-item">请款部门：</TD>
								<TD colspan="3"><uc2:inputunit id="ucUnit" runat="server"></uc2:inputunit></TD>
								<TD class="form-item">最后付款日：</TD>
								<TD><cc3:calendar id="dtPayDate" runat="server" CalendarResource="../Images/CalendarResource/" Display="True"
										Value="" ReadOnly="False"></cc3:calendar><font color="red">*</font></TD>
							</tr>
							<TR id="trContract" runat="server">
								<TD class="form-item">合同名称：</TD>
								<TD colspan="3"><A onclick="ViewContractInfo();" href="#"><asp:label id="lblContractName" Runat="server"></asp:label></A></TD>
								<TD class="form-item">合同编号：</TD>
								<TD><asp:label id="lblContractID" runat="server"></asp:label></TD>
							</TR>
							<tr id="trContract6" runat="server">
								<td class="form-item">合同类型：</td>
								<td colSpan="5"><asp:label id="lblContractTypeName" Runat="server"></asp:label></td>
							</tr>
							<tr>
							    <td class="form-item">已完成工程量：</td>
							    <td><asp:Label ID="lblTotalProductionFactValue" runat="server"></asp:Label></td>
							    <td class="form-item" width="16%">已请款金额：</td>
							    <td><asp:Label ID="lblTotalPaymentValue" runat="server"></asp:Label></td>
							    <td class="form-item">可请款金额：</td>
							    <td><asp:Label ID="lblRemainProductionFactValue" runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td class="form-item">请款类型：</td>
								<td colspan="3"><uc4:inputgroup id="ucGroup" runat="server" ClassCode="0601"></uc4:inputgroup><font color="red">*</font>
								</td>
								<td class="form-item">付款期数：</td>
								<td>第&nbsp;<INPUT class="input" id="txtIssue" style="WIDTH: 50px; TEXT-ALIGN: right" type="text" name="txtOperIssue"
										runat="server"> &nbsp;期
								</td>
							</tr>
							<tr>
								<td class="form-item">开户银行：</td>
								<td colspan="3">
								    <input class="input" id="txtBankName" style="width: 150px" type="text" name="txtBankName" runat="server" />&nbsp;
								</td>
								<td class="form-item">银行帐号：</td>
								<td>
								    <input class="input" id="txtBankAccount" style="width: 150px" type="text" name="txtBankAccount" runat="server" />&nbsp;
								</td>
							</tr>							
							<tr>
								<TD class="form-item">备注：</TD>
								<td colSpan="5"><textarea id="txtRemark" style="WIDTH: 100%" name="txtRemark" rows="2" runat="server"></textarea></td>
							</tr>
							<TR style="DISPLAY: none">
								<TD class="form-item">付款用途：</TD>
								<TD colspan="3"><input class="input" id="txtPurpose" type="text" size="30" name="txtPurpose" runat="server"></TD>
								<TD class="form-item">单据张数：</TD>
								<TD><input class="input" id="txtRecieptCount" type="text" name="txtRecieptCount" runat="server"></TD>
							</TR>
							<tr>
								<td class="form-item">附件文档：</td>
								<td colspan="5"><uc1:AttachmentAdd id="myAttachMentAdd" runat="server"></uc1:AttachmentAdd></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic" width="200">请款明细</td>
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
									<TD vAlign="top"><asp:datagrid id="dgList" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server" DataKeyField="PaymentItemCode"
											CellPadding="0" AllowSorting="True" GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True"
											Width="100%" CssClass="list"><HEADERSTYLE CssClass="list-title"></HEADERSTYLE>
											<FOOTERSTYLE CssClass="list-title"></FOOTERSTYLE>
											<COLUMNS>
												<ASP:TEMPLATECOLUMN HeaderText="<input type='checkbox' name='chkAll' onclick='SelectAll();' title='全选或全不选'>"
													Visible="False">
													<HEADERSTYLE HorizontalAlign="Center"></HEADERSTYLE>
													<ITEMSTYLE HorizontalAlign="Center" Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<INPUT onclick="ChkClick(this, true);" type=checkbox value='<%#DataBinder.Eval(Container, "DataItem.PaymentItemCode")%>' name=chkSelect>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="序号">
													<HEADERSTYLE HorizontalAlign="Center"></HEADERSTYLE>
													<ITEMSTYLE HorizontalAlign="Center" Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<%# Container.ItemIndex + 1 %>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="款项名称">
													<HEADERSTYLE HorizontalAlign="Left"></HEADERSTYLE>
													<ITEMSTYLE Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<DIV id="div_Cost" runat="server">
															<SELECT id=sltSummary style="DISPLAY: none" onchange=SelectSummaryChange(myindex); 
																name=sltSummary runat="server" myindex="<%# Container.ItemIndex %>" DataValueField="AllocateCode" 
																DataTextField="ItemName" DataSource="<%# GetSelectSummaryDataSource() %>">
															</SELECT>
															<INPUT class=input id=txtSummary type=text value='<%# DataBinder.Eval(Container, "DataItem.Summary") %>' name=txtSummary runat="server">
														</DIV>
														<INPUT id=txtPaymentItemCode type=hidden value='<%# DataBinder.Eval(Container, "DataItem.PaymentItemCode") %>' name=txtPaymentItemCode runat="server">
														<INPUT id=txtAllocateCode type=hidden value='<%# DataBinder.Eval(Container, "DataItem.AllocateCode") %>' name=txtAllocateCode runat="server">
														<INPUT id="txtContractCostCashCode" type=hidden value='<%# DataBinder.Eval(Container, "DataItem.ContractCostCashCode") %>' name=txtContractCostCashCode runat="server">
													</ITEMTEMPLATE>
													<FOOTERSTYLE HorizontalAlign="Center"></FOOTERSTYLE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="币种金额">
													<HEADERSTYLE HorizontalAlign="Left"></HEADERSTYLE>
													<ITEMSTYLE Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<UC1:EXCHANGERATE id="ucExchangeRate" runat="server" ValueChange='<%# "ExchangeRateChange(" + Container.ItemIndex + ")" %>'>
														</UC1:EXCHANGERATE>
													</ITEMTEMPLATE>
													<FOOTERTEMPLATE>
														<ASP:LABEL id="lblSumItemMoney" runat="server"></ASP:LABEL>
													</FOOTERTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="请款金额(元)" Visible="False">
													<HEADERSTYLE HorizontalAlign="Right"></HEADERSTYLE>
													<ITEMSTYLE HorizontalAlign="Right" Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<IGTXT:WEBNUMERICEDIT id=txtItemMoney runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItemMoney") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
															<CLIENTSIDEEVENTS ValueChange="InfraMoneyValueChange"></CLIENTSIDEEVENTS>
														</IGTXT:WEBNUMERICEDIT>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="费用项" FooterText="合计">
													<HEADERSTYLE HorizontalAlign="Left"></HEADERSTYLE>
													<ITEMSTYLE Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<UC3:INPUTCOSTBUDGETDTL id=ucCostBudgetDtl runat="server" CostCode='<%#DataBinder.Eval(Container, "DataItem.CostCode")%>' CostBudgetSetCode='<%#DataBinder.Eval(Container, "DataItem.CostBudgetSetCode")%>'>
														</UC3:INPUTCOSTBUDGETDTL>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="分摊" Visible="False">
													<HEADERSTYLE HorizontalAlign="Left"></HEADERSTYLE>
													<ITEMSTYLE Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<SPAN id="divBuildingNameAll" runat="server">
															<%# DataBinder.Eval(Container, "DataItem.BuildingNameAll") %>
														</SPAN><A onclick="SelectBuilding(<%#Container.ItemIndex + 2 %>);return false;" href="#">
															<IMG src="../images/ToolsItemSearch.gif" border="0"></A> <INPUT id=txtAlloType type=hidden value='<%#DataBinder.Eval(Container, "DataItem.AlloType")%>' name=txtAlloType runat="server">
														<INPUT id=txtBuildingCodeAll type=hidden value='<%#DataBinder.Eval(Container, "DataItem.BuildingCodeAll")%>' name=txtBuildingCodeAll runat="server">
														<INPUT id=txtBuildingNameAll type=hidden value='<%#DataBinder.Eval(Container, "DataItem.BuildingNameAll")%>' name=txtBuildingNameAll runat="server">
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="删除">
													<ITEMSTYLE HorizontalAlign="Center"></ITEMSTYLE>
													<HEADERSTYLE HorizontalAlign="Center"></HEADERSTYLE>
													<ITEMTEMPLATE>
														<ASP:LINKBUTTON id="btnDelete" runat="server" Text="<img src=../images/del.gif width=16 height=16 border=0>"
															CausesValidation="false" CommandName="Delete"></ASP:LINKBUTTON>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
											</COLUMNS>
											<PAGERSTYLE CssClass="ListHeadTr" HorizontalAlign="Center" PrevPageText="<<<上页" NextPageText="下页>>>"></PAGERSTYLE>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<table id="trContract2" style="DISPLAY: none" cellSpacing="0" cellPadding="0" border="0"
								runat="server">
								<tr>
									<td class="intopic" width="200">合同费用明细</td>
								</tr>
							</table>
							<TABLE id="trContract3" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
								align="center" border="0" runat="server">
								<TR>
									<TD vAlign="top"><asp:datagrid id="dgContractAllocation" runat="server" DataKeyField="AllocateCode" CellPadding="0"
											AllowSorting="True" GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%"
											CssClass="List"><HEADERSTYLE CssClass="list-title"></HEADERSTYLE>
											<FOOTERSTYLE CssClass="list-title"></FOOTERSTYLE>
											<COLUMNS>
												<ASP:TEMPLATECOLUMN HeaderText="序号">
													<HEADERSTYLE HorizontalAlign="Center"></HEADERSTYLE>
													<ITEMSTYLE HorizontalAlign="Center" Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<%# Container.ItemIndex + 1 %>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="款项名称" FooterText="合计">
													<HEADERSTYLE HorizontalAlign="Left"></HEADERSTYLE>
													<ITEMSTYLE Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<%# DataBinder.Eval(Container, "DataItem.ItemName") %>
													</ITEMTEMPLATE>
													<FOOTERSTYLE HorizontalAlign="Center"></FOOTERSTYLE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="费用项">
													<HEADERSTYLE HorizontalAlign="Left"></HEADERSTYLE>
													<ITEMSTYLE Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<%# RmsPM.BLL.CBSRule.GetCostName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostCode")))%>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="合同金额(元)">
													<HEADERSTYLE HorizontalAlign="Right"></HEADERSTYLE>
													<ITEMSTYLE HorizontalAlign="Right" Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<%# DataBinder.Eval(Container, "DataItem.Money", "{0:N}") %>
													</ITEMTEMPLATE>
													<FOOTERSTYLE HorizontalAlign="Right"></FOOTERSTYLE>
												</ASP:TEMPLATECOLUMN>
												<ASP:TEMPLATECOLUMN HeaderText="已请款(元)">
													<HEADERSTYLE HorizontalAlign="Right"></HEADERSTYLE>
													<ITEMSTYLE HorizontalAlign="Right" Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<%# DataBinder.Eval(Container, "DataItem.TotalPaymentMoney", "{0:N}") %>
													</ITEMTEMPLATE>
													<FOOTERSTYLE HorizontalAlign="Right"></FOOTERSTYLE>
												</ASP:TEMPLATECOLUMN>
											</COLUMNS>
											<PAGERSTYLE CssClass="ListHeadTr" HorizontalAlign="Center" PrevPageText="<<<上页" NextPageText="下页>>>"></PAGERSTYLE>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<table id="trContract4" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
								border="0" runat="server">
								<tr>
									<td class="intopic" width="200">合同付款计划</td>
									<td></td>
								</tr>
							</table>
							<TABLE id="trContract5" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
								align="center" border="0" runat="server">
								<TR>
									<TD vAlign="top">
									    
									    <asp:datagrid id="dgContractPaymentPlan" runat="server" CellPadding="0" AllowSorting="True" GridLines="Horizontal"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="list"><ALTERNATINGITEMSTYLE CssClass="AlterGridTr"></ALTERNATINGITEMSTYLE>
											<HEADERSTYLE CssClass="list-title"></HEADERSTYLE>
											<FOOTERSTYLE CssClass="list-title"></FOOTERSTYLE>
											<COLUMNS>
												<ASP:TEMPLATECOLUMN HeaderText="序号">
													<HEADERSTYLE HorizontalAlign="Center"></HEADERSTYLE>
													<ITEMSTYLE HorizontalAlign="Center" Wrap="False"></ITEMSTYLE>
													<ITEMTEMPLATE>
														<%# Container.ItemIndex + 1 %>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<ASP:BOUNDCOLUMN HeaderText="付款计划步骤" FooterText="合计" DataField="PlanStep"></ASP:BOUNDCOLUMN>
												<ASP:BOUNDCOLUMN HeaderText="付款时间" DataField="PlanningPayDate" DataFormatString="{0:yyyy-MM-dd}"></ASP:BOUNDCOLUMN>
												<ASP:BOUNDCOLUMN HeaderText="付款条件" DataField="PlanningPayCondition"></ASP:BOUNDCOLUMN>
												<ASP:BOUNDCOLUMN HeaderText="金 额" DataField="Money" DataFormatString="{0:N}">
													<HEADERSTYLE HorizontalAlign="Right"></HEADERSTYLE>
													<ITEMSTYLE HorizontalAlign="Right"></ITEMSTYLE>
													<FOOTERSTYLE HorizontalAlign="Right"></FOOTERSTYLE>
												</ASP:BOUNDCOLUMN>
											</COLUMNS>
											<PAGERSTYLE CssClass="list-title" HorizontalAlign="Right" PrevPageText="<<<上页" NextPageText="下页>>>"></PAGERSTYLE>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <INPUT id="txtIsNew" type="hidden" name="txtIsNew" runat="server">
			<INPUT id="txtContractCode" type="hidden" name="txtContractCode" runat="server"><INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server">
			<INPUT id="txtIsContract" type="hidden" name="txtCode" runat="server"> <INPUT id="txtPaymentCode" type="hidden" name="txtPaymentCode" runat="server">
			<INPUT id="txtDetailSno" type="hidden" name="txtDetailSno" runat="server"> <INPUT id="txtSupplyCode" type="hidden" name="txtSupplyCode" runat="server"><INPUT id="txtSupplyName" type="hidden" name="txtSupplyName" runat="server">
			<INPUT id="txtSelectDetailItemIndex" type="hidden" name="txtSelectDetailItemIndex" runat="server"><INPUT id="txtContractType" type="hidden" name="txtContractType" runat="server">
			<select id="sltSummaryEg" name="sltSummaryEg" runat="server">
				<option value="" selected></option>
			</select>
		</form>
	</body>
</HTML>
