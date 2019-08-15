<%@ Reference Control="~/usercontrols/inputsubject.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutModify" CodeFile="PayoutModify.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSubject" Src="../UserControls/InputSubject.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>付款单</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<script language="javascript" src="../Rms.js"></script>
		<script language="javascript" src="../images/convert.js"></script>
		<script language="javascript" src="../images/checkbox.js"></script>
		<script language="javascript" src="../images/infra.js"></script>
		<link href="../Images/index.css" type="text/css" rel="stylesheet" />
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet" />
		<style>
			.list-un { BACKGROUND-COLOR: #d4d0c8 }
		</style>
		<script language="javascript">
		
var m_IsAutoCalc = false;

		/*
//选择供应商
function SelectSupplier()
{
	OpenCustomWindow("SelectSupplierList.aspx", "选择供应商", 500, 580);
}

//选择供应商返回
function SelectSupplierReturn(code, name)
{
	Form1.txtPayer.value = name;
	Form1.txtSupplyCode.value = code;
}

//选择费用项
function SelectCost(i)
{
	Form1.txtSelectDetailItemIndex.value = i;
	OpenCustomWindow("../SelectBox/SelectCost.aspx?Type=Single&ProjectCode=" + Form1.txtProjectCode.value,"选择费用项", 500, 400);
}

//选择费用项返回
function GetReturnSingleCostCode(code, name )
{
	var i = Form1.txtSelectDetailItemIndex.value;
	document.all("dgList__ctl" + i + "_txtCostCode").value = code;
	document.all("dgList__ctl" + i + "_divCostName").innerText = name;
	document.all("dgList__ctl" + i + "_txtCostName").value = name;
}

//选择楼栋
function SelectBuilding(i)
{
	Form1.txtSelectDetailItemIndex.value = i;
	var code = "";
	code = document.all("dgList__ctl" + i + "_txtBuildingCodeAll").value;
	OpenCustomWindow("../PBS/SelectBuilding.aspx?CanSelectArea=0&ProjectCode=" + Form1.txtProjectCode.value + "&SelectCode=" + escape(code) + "&ReturnFunc=SelectBuildingReturn","选择楼栋", 400, 540);
}

//选择楼栋返回
function SelectBuildingReturn(code, name)
{
	var i = Form1.txtSelectDetailItemIndex.value;
	document.all("dgList__ctl" + i + "_txtBuildingCodeAll").value = code;
	document.all("dgList__ctl" + i + "_divBuildingNameAll").innerText = name;
	document.all("dgList__ctl" + i + "_txtBuildingNameAll").value = name;
}
*/
/*
var money;

function MoneyFocus(obj)
{
	money = obj.value;
}

function MoneyBlur(obj)
{
	if (obj.value != money)
	  CalcSum();
}

//计算合计
function CalcSum()
{
	var c = parseInt(document.all.dgList.rows.length) - 2;
	var tempMoney = 0;
	var sum = 0;
	
	for(i=0;i<c;i++)
	{
		tempMoney = ConvertFloat(document.all("dgList__ctl" + (i + 2) + "_txtPayoutMoney").value);
			
		sum = sum + tempMoney;

	}

	document.all.txtSumPayoutMoney.value = sum;
	document.all("dgList__ctl" + (c + 2) + "_lblSumPayoutMoney").innerText = sum;
//	alert(sum);
}
*/

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
//	
//    var edit_CashID = "dgList__ctl" + (i + 1) + "_ucItemCash_ExchangeRateControl_C";
//	var edit_CashID_H = "dgList__ctl" + (i + 1) + "_ucItemCash_ExchangeRateControl_CV";
//	var edit_ExchangeRateID = "dgList__ctl" + (i + 1) + "_ucItemCash_ExchangeRateControl_E";
//	var edit_ExchangeRateID_H = "dgList__ctl" + (i + 1) + "_ucItemCash_ExchangeRateControl_EV";
//	var edit_RMBID = "dgList__ctl" + (i + 1) + "_ucItemCash_ExchangeRateControl_V";

//	var edit_MoneyType = "dgList__ctl" + (i + 1) + "_ucItemCash_ExchangeRateControl_M";
	
	for(i=0;i<c;i++)
	{
		var edit_RMBID_H = GetObjectInDataGrid("dgList",i + 2, "ucItemCash_ExchangeRateControl_R");
//		tempMoney = ConvertFloat(document.all("dgList:_ctl" + (i + 2) + ":txtPayoutMoney").value);
		tempMoney = ConvertFloat(edit_RMBID_H.value);
		sum = sum + tempMoney;
	}

	//格式化
	sum = formatNumber(sum, "#,###.00");

	document.all.txtSumPayoutMoney.value = sum;
	GetObjectInDataGrid("dgList",c + 2,"lblSumPayoutMoney").innerText = sum;
//	alert(sum);
}

//总额变化时
function txtMoney_ValueChange(oEdit, oldValue, oEvent)
{
//	if (!confirm("总额已变，是否自动分摊？")) return;

	var objTable = document.all.dgList;
	if (!objTable) return;
	
	var count = objTable.rows.length - 2;
	
	var arrchk = document.all.chkSelect;
	if (!arrchk) return;
	
	//分摊总额
//	var TotalMoney = ConvertFloat(Form1.txtMoney.value);
	var TotalMoney =  ConvertFloat(document.all("ucExchangeRate_ExchangeRateControl_R").value);
	igedit_getById("txtMoney").setValue(TotalMoney);
	var TotalUsedMoney = 0;
	var TotalRemainMoney = TotalMoney;
	
	for(var i=0;i<count;i++)
	{
		var chk;
		
		if (count == 1)
		{
			chk = arrchk;
		}
		else
		{
			chk = arrchk[i];
		}

		if (chk.checked)
		{
			//剩余分摊总额
			TotalRemainMoney = TotalMoney - TotalUsedMoney;

			//明细项剩余付款额
			var DtlMoney = ConvertFloat(GetObjectInDataGrid("dgList",i + 2,"txtRemainItemMoney").value);
			
			//不能超出剩余分摊总额
			if (DtlMoney > TotalRemainMoney)
			{
				DtlMoney = TotalRemainMoney;
			}

			var edit_id = GetObjectNameInDataGrid("dgList",i + 2,"txtPayoutMoney");
			igedit_getById(edit_id).setValue(DtlMoney);
			
			//已分摊总额
			TotalUsedMoney = TotalUsedMoney + DtlMoney;
		
		}
	}

	InfraCalcSum();
}

//总额分摊到一条明细
function ApportDtlSingle(index, objCheck)
{
	var i = parseInt(index);
	var c = parseInt(document.all.dgList.rows.length) - 2;
	var TotalMoney, TotalUsedMoney, TotalRemainMoney, DtlMoney, DtlCash, DtlExchangeRate;
	
	var edit_id = GetObjectNameInDataGrid("dgList",i + 1,"txtPayoutMoney");

	var edit_CashID = GetObjectInDataGrid("dgList",i + 1, "ucItemCash_ExchangeRateControl_C");
	var edit_CashID_H =  GetObjectInDataGrid("dgList",i + 1,"ucItemCash_ExchangeRateControl_CV");
	var edit_ExchangeRateID =  GetObjectInDataGrid("dgList",i + 1,"ucItemCash_ExchangeRateControl_E");
	var edit_ExchangeRateID_H =  GetObjectInDataGrid("dgList",i + 1,"ucItemCash_ExchangeRateControl_EV");
	var edit_RMBID =  GetObjectInDataGrid("dgList",i + 1, "ucItemCash_ExchangeRateControl_V");
	var edit_RMBID_H =  GetObjectInDataGrid("dgList",i + 1, "ucItemCash_ExchangeRateControl_R");
	var edit_MoneyType =  GetObjectInDataGrid("dgList",i + 1, "ucItemCash_ExchangeRateControl_M");
	
	
	if (objCheck.checked)
	{
		//勾上时，自动分摊金额
		GetObjectInDataGrid("dgList",i + 1, "txtSelect").value = 1;

		InfraSetNumericEditVisible(edit_id, true);
		
		//分摊总额
//		TotalMoney = ConvertFloat(Form1.txtMoney.value);

		TotalMoney =  ConvertFloat(document.all("ucExchangeRate_ExchangeRateControl_R").value);
		
		if (TotalMoney == "")
		{
			return;
		}
			
		if (isNaN(TotalMoney))
		{
			alert("付款总额不是有效的数值");
			return;
		}

		//已分摊总额
		TotalUsedMoney = ConvertFloat(GetObjectInDataGrid("dgList", c + 2, "lblSumPayoutMoney").innerText);
		
		//剩余分摊总额
		TotalRemainMoney = TotalMoney - TotalUsedMoney;
		
		//明细项剩余付款额
		DtlMoney = ConvertFloat(GetObjectInDataGrid("dgList",i + 1,"txtRemainItemMoney").value);
		
		//不能超出剩余分摊总额
		if (DtlMoney > TotalRemainMoney)
		{
			DtlMoney = TotalRemainMoney;
		}

		igedit_getById(edit_id).setValue(DtlMoney);
		
        DtlExchangeRate =  ConvertFloat(edit_ExchangeRateID.value);
        
        if ( DtlExchangeRate != 0 )
        {
            DtlCash = DtlMoney / DtlExchangeRate;
            DtlCash = formatNumber(DtlCash, "####.00");
        }

        DtlMoney = formatNumber(DtlMoney, "#,###.00");
        
        edit_RMBID.innerText = DtlMoney;
        edit_RMBID_H.value = DtlMoney;
        
        edit_CashID.value = DtlCash;
        edit_CashID_H.value = DtlCash;

        
        edit_CashID.disabled = false;
        edit_ExchangeRateID.disabled = false;
        edit_RMBID.disabled = false;
        edit_MoneyType.disabled = false;
  
	}
	else
	{
		//未勾上时，金额清空
		GetObjectInDataGrid("dgList",i + 1, "txtSelect").value = 0;

        edit_CashID.disabled = true;
        edit_ExchangeRateID.disabled = true;
        edit_RMBID.disabled = true;
        edit_MoneyType.disabled = true;
		
        edit_MoneyType.value = document.all("hidDDLRMBValue").value;
        edit_ExchangeRateID.value = "1.00";
        edit_ExchangeRateID_H.value = "1.00";

        edit_RMBID.innerText = "0.00";
        edit_RMBID_H.value = "0.00";
        
        edit_CashID.value = 0.00;
        edit_CashID_H.value = 0.00;
        
		InfraSetNumericEditVisible(edit_id, false);
		igedit_getById(edit_id).setValue("");
	}
	
	InfraCalcSum();
}

//明细打勾
function MyChkSelectRow(index, objCheck, objTable, selectedClass, unselectedClass)
{
	var i = parseInt(index);
	
	if (!objTable) return;
	if (!objTable.rows(i)) return;

	if (objCheck.checked)
	{
		objTable.rows(i).className = selectedClass;
	}
	else
	{
		objTable.rows(i).className = unselectedClass;
	}

	ApportDtlSingle(index, objCheck);
}

//初始化明细打勾
function IniDtlCheck()
{
	var objCheck = document.all.chkSelect;
	var ischecked;

	if (!objCheck) return;
	
		if(objCheck[0])
		{
			l = objCheck.length;
		
			for(var i=0;i<l;i++)
			{
				ischecked = (objCheck[i].getAttribute("chk") == "1");
				
				if (objCheck[i].checked != ischecked)
				{
					objCheck[i].click();
				}
			}
		}
		else
		{
			if(objCheck)
			{
				ischecked = (objCheck.getAttribute("chk") == "1");

				if (objCheck.checked != ischecked)
				{
					objCheck.click();
				}
			}
		}
}

//费用项信息
function ViewCostCode(code)
{
	OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=' + Form1.txtProjectCode.value + '&CostCode=' + code ,'动态费用项信息');
}

        </script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="IniDtlCheck();"
		onkeydown="if(event.keyCode==13) event.keyCode=9">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">付款单</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD class="form-item">付款单号：</TD>
								<TD><input class="input" id="txtPayoutID" style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px"
										readOnly type="text" name="txtPayoutID" runat="server"></TD>
								<TD class="form-item">付款类型：</TD>
								<td><select class="select" id="sltPaymentType" name="sltPaymentType" runat="server">
										<option value="" selected>--请选择--</option>
									</select>
								</td>
								<TD colspan="2">
									<div style="display:none">
										<igtxt:webnumericedit Width="100px" id="txtMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
											ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
											<ClientSideEvents ValueChange="txtMoney_ValueChange"></ClientSideEvents>
										</igtxt:webnumericedit>
									</div>
								</TD>								
							</tr>
							<TR>
								<TD class="form-item">受款单位：</TD>
								<TD><asp:label id="lblSupplyName" Runat="server"></asp:label></TD>
								<TD class="form-item">受 款 人：</TD>
								<TD><input class="input" id="txtPayer" type="text" name="txtPayer" runat="server"></TD>
								<TD class="form-item">付款日期：</TD>
								<TD><cc3:calendar id="dtPayoutDate" runat="server" CalendarResource="../Images/CalendarResource/"
										Display="True" Value="" ReadOnly="False"></cc3:calendar><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">系统类型：</TD>
								<TD>
									<uc1:InputSystemGroup id="inputSystemGroup" runat="server"></uc1:InputSystemGroup><font color="red">*</font>
								</TD>
								<TD class="form-item">贷方科目：</TD>
								<td colspan="3"><uc1:InputSubject id="ucInputSubject" runat="server"></uc1:InputSubject><asp:Label runat="server" ID="lblSubjectCodeHint" ForeColor="red">*</asp:Label>
								</td>
							</TR>
							<tr>
								<TD class="form-item">支 票 号：</TD>
								<TD><input class="input" id="txtBillNo" type="text" name="txtBillNo" runat="server"></TD>
								<TD class="form-item">票 据 号：</TD>
								<TD><input class="input" id="txtInvoNo" type="text" name="txtInvoNo" runat="server"></TD>
								<TD class="form-item">单据张数：</TD>
								<TD><input class="input-nember" size="4" id="txtReceiptCount" type="text" name="txtReceiptCount"
										runat="server"></TD>
							</tr>
							<tr>
								<td class="form-item">付款总额：</td>
								<td colspan="5">
									<table width="300"cellpadding="0" cellspacing="0">
										<tr>
											<td><uc1:ExchangeRate id="ucExchangeRate" runat="server" ValueChange="txtMoney_ValueChange()"></uc1:ExchangeRate></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic" width="200">付款明细</td>
							</tr>
						</table>
					</td>
				<tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD vAlign="top"><asp:datagrid id="dgList" runat="server" DataKeyField="PayoutItemCode" CellPadding="0" AllowSorting="True"
											GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="list">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="True" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="MyChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, '', 'list-un');" value='<%#DataBinder.Eval(Container, "DataItem.PayoutItemCode")%>' chk='<%#DataBinder.Eval(Container, "DataItem.Checked")%>' checked>
														<input type="hidden" id="txtSelect" name="txtSelect" runat="server" value='<%#DataBinder.Eval(Container, "DataItem.Checked")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="款项" FooterText="合计">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Summary") %>
														<input type="hidden" id="txtPayoutItemCode" name="txtPayoutItemCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PayoutItemCode") %>'>
														<input type="hidden" id="txtPaymentItemCode" name="txtPaymentItemCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentItemCode") %>'>
														<input type="hidden" id="txtSummary" name="txtSummary" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.Summary") %>'>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="本次付款金额">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<uc1:ExchangeRate id="ucItemCash" runat="server" ValueChange='InfraCalcSum()'></uc1:ExchangeRate>
														<div  style="display:none;">
    													    <igtxt:webnumericedit Width="80" id="txtPayoutMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.PayoutMoney") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
															    <ClientSideEvents ValueChange="InfraMoneyValueChange"></ClientSideEvents>
														    </igtxt:webnumericedit>
														</div>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumPayoutMoney"></asp:Label>&nbsp;
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="请款金额<br>(元)">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.ItemMoney", "{0:N}") %>
														<input type="hidden" runat="server" ID="txtItemMoney" name="txtItemMoney" Value='<%# DataBinder.Eval(Container, "DataItem.ItemMoney") %>'>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="已付金额<br>(元)">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.TotalPayoutMoney", "{0:N}") %>
														<input type="hidden" runat="server" ID="txtTotalPayoutMoney" name="txtTotalPayoutMoney" Value='<%# DataBinder.Eval(Container, "DataItem.TotalPayoutMoney") %>'>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="未付金额<br>(元)">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.RemainItemMoney", "{0:N}") %>
														<input type="hidden" runat="server" ID="txtRemainItemMoney" name="txtRemainItemMoney" Value='<%# DataBinder.Eval(Container, "DataItem.RemainItemMoney") %>'>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>

												<asp:TemplateColumn HeaderText="科目" HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="False">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<uc1:InputSubject id="ucInputSubject" runat="server" Value='<%#DataBinder.Eval(Container, "DataItem.SubjectCode")%>'>
														</uc1:InputSubject>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<span id="divCostName" runat="server"><a href="#" onclick="ViewCostCode(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "CostCode") %>'>
																<%#DataBinder.Eval(Container, "DataItem.CostName")%>
															</a></span><input type="hidden" runat="server" id="txtCostCode" name="txtCostCode" value='<%#DataBinder.Eval(Container, "DataItem.CostCode")%>'>
														<input type="hidden" runat="server" id="txtCostName" name="txtCostName" value='<%#DataBinder.Eval(Container, "DataItem.CostName")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位工程">
													<HeaderStyle HorizontalAlign="Left" Wrap="false"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="分摊到楼栋" Visible="False">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<span id="divBuildingNameAll" runat="server">
															<%# DataBinder.Eval(Container, "DataItem.BuildingNameAll") %>
														</span><input type="hidden" runat="server" id="txtBuildingCodeAll" name="txtBuildingCodeAll" value='<%#DataBinder.Eval(Container, "DataItem.BuildingCodeAll")%>'>
														<input type="hidden" runat="server" id="txtBuildingNameAll" name="txtBuildingNameAll" value='<%#DataBinder.Eval(Container, "DataItem.BuildingNameAll")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="请款单号">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PaymentID") %>
														<input type="hidden" id="txtPaymentID" name="txtPaymentID" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentID") %>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="删除" Visible="False">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton runat="server" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
															CommandName="Delete" CausesValidation="false" ID="btnDelete"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server">
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
			<INPUT id="txtIsNew" type="hidden" name="txtIsNew" runat="server">
			<INPUT id="txtPaymentCode" type="hidden" name="txtPaymentCode" runat="server">
			<INPUT id="txtPayoutCode" type="hidden" name="txtPayoutCode" runat="server">
			<INPUT id="txtDetailSno" type="hidden" name="txtDetailSno" runat="server">
			<INPUT id="txtSupplyCode" type="hidden" name="txtSupplyCode" runat="server">
			<INPUT id="txtSelectDetailItemIndex" type="hidden" name="txtSelectDetailItemIndex" runat="server">
			<INPUT id="txtSumPayoutMoney" type="hidden" name="txtSumPayoutMoney" runat="server">
			<INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server">
			<INPUT id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
			<input id="hidDDLRMBValue" runat="server" type="hidden" name="hidDDLRMBValue" />
		</form>
	</body>
</HTML>
