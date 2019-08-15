<%@ Reference Control="~/usercontrols/inputpbs.ascx" %>
<%@ Reference Control="~/usercontrols/inputuser.ascx" %>
<%@ Reference Control="~/usercontrols/inputunit.ascx" %>
<%@ Reference Control="~/usercontrols/inputsubject.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherModify" CodeFile="VoucherModify.aspx.cs" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Register TagPrefix="uc1" TagName="InputSubject" Src="../UserControls/InputSubject.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputPBS" Src="../UserControls/InputPBS.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>凭证</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/infra.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/infragistics/20051/scripts/ig_dropCalendar.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/infragistics/20051/scripts/ig_editDrop1.js"></SCRIPT>
		<style>.ItemGridTr1 { COLOR: #000000; BACKGROUND-COLOR: #ffffdd }
	.ItemGridTr2 { COLOR: #000000; BACKGROUND-COLOR: #f5f5f5 }
		</style>
		<SCRIPT language="javascript">
<!--
	var SelectedCount = 0;
	
	function ReloadDataGrid()
	{
		Form1.btnReload.click();
	}

	function ChkClick(obj, isrefresh)
	{
		var index = parseInt(obj.index);
		
		if (obj.checked)
		{
			SelectedCount = SelectedCount + 1;
		}
		else
		{
			SelectedCount = SelectedCount - 1;
		}
		
		if (isrefresh)
		{
			document.all.spanSelectCount.innerText = "选择了 " + SelectedCount + " 条";
		}
		
		ChkSelectRow(index, obj, dgList, "list-2", dgList.rows(index).myclass);
	}
	
	//全选或全不选
	function SelectAll(){
		SelectedCount = 0;
		var checked;
		var index;

		chk = document.all.chkAll;
		checked = chk.checked;
		
		var obj=document.all("chkSelect");

		if (obj == null)
			return false;
			
		
		if(obj[0]){
			for(var i=0;i<obj.length;i++){
				obj[i].checked=checked;
				
				if (obj[i].checked)
				{
					SelectedCount = SelectedCount + 1;
				}
		
				index = parseInt(obj[i].index);
				ChkSelectRow(index, obj[i], dgList, "list-2", dgList.rows(index).myclass);
			}
		}else{
			if(obj){
				obj.checked=checked;

				if (obj.checked)
				{
					SelectedCount = SelectedCount + 1;
				}
		
				index = parseInt(obj.index);
				ChkSelectRow(index, obj, dgList, "list-2", dgList.rows(index).myclass);
			}
		}

		document.all.spanSelectCount.innerText = "选择了 " + SelectedCount + " 条";
	}

	function GetSelected()
	{
		SelectedCount = 0;
		var s = "";
		var obj=document.all("chkSelect");
		
		if (obj == null)
			return false;
			
		if(obj[0]){
			for(var i=0;i<obj.length;i++){
				if (obj[i].checked)
				{
					if (s != "")
					{
						s = s + ",";
					}
					s = s + obj[i].value;
					
					SelectedCount = SelectedCount + 1;
//					checked = true;
//					break;
				}
			}
		}else{
			if(obj){
				if (obj.checked)
				{
					s = obj.value;
					SelectedCount = SelectedCount + 1;
//					checked = true;
				}
			}
		}

		document.all.spanSelectCount.innerText = "选择了 " + SelectedCount + " 条";
		return s;
	}

//批量修改
function BatchEdit()
{
	var s = GetSelected();

	if (s == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}
	
	Form1.txtSelect.value = s;

	return true;
}

function OpenBatchEdit()
{
	OpenMiddleWindow('../Finance/VoucherDetailBatchEdit.aspx?SubjectSetCode=' + Form1.txtSubjectSetCode.value,'批量修改凭证');

}
	
//批量删除
function BatchDelete()
{
	var s = GetSelected();

	if (s == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}
	
	Form1.txtSelect.value = s;

	document.all.btnHiddenBatchDelete.click();
	return true;
}
	
//选择供应商
function SelectSupl(i)
{
	Form1.txtSelectSubjectCodeSno.value = i;
	OpenCustomWindow("SelectSuplList.aspx", "选择供应商", 500, 580);
}

//选择供应商返回
function SelectSuplReturn(code, name)
{
	var i = Form1.txtSelectSubjectCodeSno.value;
	
	GetObjectInDataGrid("dgList", i, "txtSupplyCode").value = code;
	GetObjectInDataGrid("dgList", i, "txtSupplyName").value = name;
	GetObjectInDataGrid("dgList", i, "lblSupplyName").innerText = name;
}

//选择客户
function SelectSupplier(i)
{
	Form1.txtSelectSubjectCodeSno.value = i;
	OpenLargeWindow("../SelectBox/SelectSupplier.aspx", "选择客户");
}

//选择客户返回
function DoSelectSupplierReturn(code, name)
{
	var i = Form1.txtSelectSubjectCodeSno.value;
	
	GetObjectInDataGrid("dgList", i, "txtCustCode").value = code;
	GetObjectInDataGrid("dgList", i, "txtCustName").value = name;
	GetObjectInDataGrid("dgList", i, "lblCustName").innerText = name;
}

//选择核算项目
function SelectUFProject(i)
{
	Form1.txtSelectSubjectCodeSno.value = i;
//	OpenMiddleWindow("../SelectBox/SelectProject.aspx?AllowNull=1", "选择项目");
	OpenCustomWindow("SelectUFProjectList.aspx", "选择核算项目", 500, 580);
}

//选择项目返回
function DoSelectProject(code, name)
{
	var i = Form1.txtSelectSubjectCodeSno.value;
	
	GetObjectInDataGrid("dgList", i, "txtUFProjectCode").value = code;
	GetObjectInDataGrid("dgList", i, "txtUFProjectName").value = name;
	GetObjectInDataGrid("dgList", i, "lblUFProjectName").innerText = name;
}

//选择核算项目返回
function SelectUFProjectReturn(code, name)
{
	var i = Form1.txtSelectSubjectCodeSno.value;
	
	GetObjectInDataGrid("dgList", i, "txtUFProjectCode").value = code;
	GetObjectInDataGrid("dgList", i, "txtUFProjectName").value = name;
	GetObjectInDataGrid("dgList", i, "lblUFProjectName").innerText = name;
}

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
	var tempMoneyJ = 0;
	var tempMoneyD = 0;
	var sumJ = 0;
	var sumD = 0;
	
	for(i=0;i<c;i++)
	{
		tempMoneyJ = ConvertFloat(GetObjectInDataGrid("dgList", (i + 2), "txtDebitMoney").value);
		sumJ = sumJ + tempMoneyJ;

		tempMoneyD = ConvertFloat(GetObjectInDataGrid("dgList", (i + 2), "txtCrebitMoney").value);
		sumD = sumD + tempMoneyD;
	}

	//格式化
	sumJ = FormatNumber(sumJ, 2);
	sumD = FormatNumber(sumD, 2);

	GetObjectInDataGrid("dgList", (c + 2), "lblTotalDebitMoney").innerText = sumJ;
	GetObjectInDataGrid("dgList", (c + 2), "lblTotalCrebitMoney").innerText = sumD;
	
	Form1.txtTotalDebitMoney.value = sumJ;
	Form1.txtTotalCrebitMoney.value = sumD;
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
	var tempMoneyJ = 0;
	var tempMoneyD = 0;
	var sumJ = 0;
	var sumD = 0;
	
	for(i=0;i<c;i++)
	{
		tempMoneyJ = ConvertFloat(GetObjectInDataGrid("dgList", (i + 2), "txtDebitMoney").value);
		sumJ = sumJ + tempMoneyJ;

		tempMoneyD = ConvertFloat(GetObjectInDataGrid("dgList", (i + 2), "txtCrebitMoney").value);
		sumD = sumD + tempMoneyD;
	}

	//格式化
	sumJ = formatNumber(sumJ, "#,###.00");
	sumD = formatNumber(sumD, "#,###.00");

	GetObjectInDataGrid("dgList", (c + 2), "lblTotalDebitMoney").innerText = sumJ;
	GetObjectInDataGrid("dgList", (c + 2), "lblTotalCrebitMoney").innerText = sumD;

	Form1.txtTotalDebitMoney.value = sumJ;
	Form1.txtTotalCrebitMoney.value = sumD;
}

//-->
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						凭证</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="120">凭证编号：</TD>
								<TD><asp:textbox id="txtVoucherID" runat="server" ReadOnly="True" BorderStyle="None" Width="150px"
										CssClass="input"></asp:textbox></TD>
								<TD class="form-item" width="120">凭证类型：</TD>
								<TD><SELECT id="sltVoucherType" style="WIDTH: 150px" name="sltVoucherType" runat="server"></SELECT><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">制单日期：</TD>
								<TD><cc3:calendar id="dtbMakeDate" runat="server" ReadOnly="False" Value="" Display="True" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
									<!--
									<igtxt:WebDateTimeEdit Width="100" CssClass="infra-input-date" id="dtMakeDate" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
										JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
										DisplayModeFormat="yyyy-MM-dd" PromptChar=" ">
										<ButtonsAppearance CustomButtonDisplay="OnRight"></ButtonsAppearance>
										<SpinButtons Display="OnRight"></SpinButtons>
									</igtxt:WebDateTimeEdit>-->
									<font color="red">*</font></TD>
								<TD class="form-item">单据张数：</TD>
								<TD><input class="input-nember" size="4" id="txtReceiptCount" type="text" name="txtReceiptCount"
										runat="server"></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic" width="200">凭证分录</td>
								<td>新增：<input class="input-nember" id="txtAddDtlCount" type="text" maxLength="2" size="2" value="1"
										name="txtAddDtlCount" runat="server">条<input class="button-small" id="btnAddDtl" type="button" value="新增分录" name="btnAddDtl"
										runat="server" onserverclick="btnAddDtl_ServerClick"></td>
								<td>&nbsp;<input class="button-small" onclick="if (!BatchEdit()) return false;" type="button" value="批量修改"
										name="btnBatchEdit" id="btnBatchEdit" runat="server" onserverclick="btnBatchEdit_ServerClick"></td>
								<td>&nbsp;<input class="button-small" onclick="BatchDelete()" type="button" value="批量删除" name="btnBatchDelete"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<asp:datagrid id="dgList" runat="server" Width="100%" CssClass="List" CellPadding="0" AllowSorting="True"
								GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" onkeydown="if(event.keyCode==13) event.keyCode=9">
								<ItemStyle CssClass=""></ItemStyle>
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='SelectAll();' title='全选或全不选'&gt;">
										<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<input type="checkbox" name="chkSelect" onclick="ChkClick(this, true);" index='<%# Container.ItemIndex + 1%>' value='<%#DataBinder.Eval(Container, "DataItem.VoucherDetailCode")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="序号" Visible="True">
										<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="摘要">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<ItemTemplate>
											<asp:TextBox CssClass="input" ID="txtSummary" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Summary") %>'>
											</asp:TextBox><input type="hidden" id="txtVoucherDetailCode" name="txtVoucherDetailCode" value='<%# DataBinder.Eval(Container, "DataItem.VoucherDetailCode") %>' runat="server">
											<input type="hidden" runat="server" id="txtUFProjectCode" name="txtUFProjectCode" value='<%#DataBinder.Eval(Container, "DataItem.UFProjectCode")%>'>
											<input type="hidden" runat="server" id="txtUFProjectName" name="txtUFProjectName" value='<%#DataBinder.Eval(Container, "DataItem.UFProjectName")%>'>
										</ItemTemplate>
										<FooterTemplate>
											合计 
											<!--														<asp:Button Text="计算合计" CssClass="button-small" ID="btnCalcSum" Runat="server"></asp:Button>-->
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="科目">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<uc1:InputSubject id="ucInputSubject" runat="server" Value='<%#DataBinder.Eval(Container, "DataItem.SubjectCode")%>'>
											</uc1:InputSubject>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="借">
										<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
										<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
										<ItemTemplate>
											<igtxt:webnumericedit Width="80" id="txtDebitMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.DebitMoney") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
												<ClientSideEvents ValueChange="InfraMoneyValueChange"></ClientSideEvents>
											</igtxt:webnumericedit>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label runat="server" ID="lblTotalDebitMoney"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="贷">
										<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
										<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
										<ItemTemplate>
											<igtxt:webnumericedit Width="80" id="txtCrebitMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.CrebitMoney") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
												<ClientSideEvents ValueChange="InfraMoneyValueChange"></ClientSideEvents>
											</igtxt:webnumericedit>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label runat="server" ID="lblTotalCrebitMoney"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="合同编号">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<ItemTemplate>
											<asp:TextBox CssClass="input" ID="txtContractID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ContractID") %>'></asp:TextBox>
											<input type="hidden" runat="server" id="txtContractCode" name="txtContractCode" value='<%#DataBinder.Eval(Container, "DataItem.ContractCode")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="供应商">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" id="lblSupplyName">
												<%#DataBinder.Eval(Container, "DataItem.SupplyName")%>
											</asp:Label>
											<a href="#" onclick="SelectSupl(<%#Container.ItemIndex + 2 %>);return false;"><img src="../images/ToolsItemSearch.gif" border="0"></a>
											<input type="hidden" runat="server" id="txtSupplyCode" name="txtSupplyCode" value='<%#DataBinder.Eval(Container, "DataItem.SupplyCode")%>'>
											<input type="hidden" runat="server" id="txtSupplyName" name="txtSupplyName" value='<%#DataBinder.Eval(Container, "DataItem.SupplyName")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="客户">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" id="lblCustName">
												<%#DataBinder.Eval(Container, "DataItem.CustName")%>
											</asp:Label>
											<a href="#" onclick="SelectSupplier(<%#Container.ItemIndex + 2 %>);return false;"><img src="../images/ToolsItemSearch.gif" border="0"></a>
											<input type="hidden" runat="server" id="txtCustCode" name="txtCustCode" value='<%#DataBinder.Eval(Container, "DataItem.CustCode")%>'>
											<input type="hidden" runat="server" id="txtCustName" name="txtCustName" value='<%#DataBinder.Eval(Container, "DataItem.CustName")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="部门">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<uc2:InputUnit id="ucUFUnit" runat="server" Value='<%#DataBinder.Eval(Container, "DataItem.UFUnitCode")%>'>
											</uc2:InputUnit>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="人员">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<uc1:InputUser id="ucPaymentCheckPerson" runat="server" Value='<%#DataBinder.Eval(Container, "DataItem.PaymentCheckPerson")%>'></uc1:InputUser>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="单位工程">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<uc1:InputPBS id="ucPBS" runat="server" PBSType='<%#DataBinder.Eval(Container, "DataItem.PBSType")%>' Value='<%#DataBinder.Eval(Container, "DataItem.PBSCode")%>'></uc1:InputPBS>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="项目">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" id="lblUFProjectName">
												<%#DataBinder.Eval(Container, "DataItem.UFProjectName")%>
											</asp:Label>
											<a href="#" onclick="SelectUFProject(<%#Container.ItemIndex + 2 %>);return false;"><img src="../images/ToolsItemSearch.gif" border="0"></a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="票号">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox CssClass="input" runat="server" ID="txtBillNo" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.BillNo") %>'>
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td><span id="spanSelectCount"></span></td>
				</tr>
				<tr>
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="9" width="100%" border="0">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="暂 存" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<input class="submit" id="btnOK" type="button" value="确 定" name="btnOK" runat="server" onserverclick="btnOK_ServerClick">&nbsp;
									<INPUT class="submit" onclick="window.close();" type="button" value="取 消"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
			<INPUT id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><INPUT id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
			<INPUT id="txtVoucherCode" type="hidden" name="txtVoucherCode" runat="server"> <INPUT id="btnReload" style="DISPLAY: none; WIDTH: 20px; HEIGHT: 20px" type="button" value="重新加载"
				name="btnReload" runat="server" onserverclick="btnReload_ServerClick"> <INPUT id="txtRelaCode" type="hidden" name="txtRelaCode" runat="server"><INPUT id="txtAct" type="hidden" name="txtAct" runat="server">
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
			<INPUT id="txtIsNew" type="hidden" name="txtIsNew" runat="server"> <input id="txtSelect" type="hidden" name="txtSelect" runat="server">
			<input id="txtParam" type="hidden" name="txtParam" runat="server"> <input id="btnHiddenBatchDelete" style="DISPLAY: none" type="button" name="btnHiddenBatchDelete"
				runat="server" onserverclick="btnHiddenBatchDelete_ServerClick"> <input id="txtDetailSno" type="hidden" name="txtDetailSno" runat="server">
			<input type="hidden" name="txtSelectSubjectCodeSno">
			<input id="txtTotalDebitMoney" type="hidden" name="txtTotalDebitMoney" runat="server"><input id="txtTotalCrebitMoney" type="hidden" name="txtTotalCrebitMoney" runat="server">
		</form>
		<SCRIPT>
//	ig_initDropCalendar("dtMakeDate") ;
//	SetWeekName("SharedCalendar_514");
		
		</SCRIPT>
	</body>
</HTML>
