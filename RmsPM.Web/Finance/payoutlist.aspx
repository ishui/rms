<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="uc1" TagName="InputSubject" Src="../UserControls/InputSubject.ascx" %>

<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutList" CodeFile="PayoutList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>付款管理</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">财务管理 
									- <span id="spanTitle" runat="server">付款管理</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnBuildVoucher" onclick="BuildVoucher(true);" type="button"
							value="生成凭证" name="btnBuildVoucher" runat="server"> <input class="button" id="btnSelectVoucher" onclick="BuildVoucher(false);" type="button"
							value="加入凭证" name="btnSelectVoucher" runat="server"> <input class="button" id="btnAdvSearch" style="DISPLAY: none" onclick="ShowAdvSearch()"
							type="button" value="高级查询" name="btnAdvSearch"> <input class="button" id="btnBatchModify" onclick="batchModify();" type="button" value="修改"
							name="btnBatchModify" runat="server">
					</td>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<table width="100%" height="100%">
							<tr>
								<td>
									<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0" onkeydown="SearchKeyDown();">
										<tr>
											<td>
												<table>
													<TR>
														<td>
															请款类型： <input id="chkIsContract" type="checkbox" value="1" name="chkIsContract" runat="server">合同请款 
															&nbsp;<input id="chkIsNotContract" type="checkbox" value="1" name="chkIsNotContract" runat="server">非合同请款 
															&nbsp;&nbsp;付款类型：<uc1:inputsystemgroup id="inputSystemGroup" runat="server" SelectAllLeaf="True"></uc1:inputsystemgroup>
															&nbsp;&nbsp;状态： &nbsp;<input id="chkStatus0" type="checkbox" value="1" name="chkStatus0" runat="server"><label for="chkStatus0">待审</label>&nbsp; 
															&nbsp;<input id="chkStatus1" type="checkbox" value="1" name="chkStatus1" runat="server"><label for="chkStatus1">已审</label>&nbsp; 
															&nbsp;&nbsp;<input class="submit" id="btnSearch" type="button" value="搜索" name="btnSearch" runat="server"
																onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick"> &nbsp;<IMG id="imgAdvSearch" title="高级查询" style="CURSOR: hand" onclick="ShowAdvSearch();" src="../images/search_more.gif">
														</td>
													</TR>
												</table>
												<table id="divAdvSearch" style="DISPLAY: none">
													<TR>
														<TD>付款单号：<INPUT class="input" id="txtPayoutID" style="WIDTH: 130px" type="text" size="12" name="txtPayoutID"
																runat="server">
															&nbsp;&nbsp;请款单号：<INPUT class="input" id="txtPaymentID" style="WIDTH: 130px" type="text" size="12" name="txtPaymentID"
																runat="server">
															&nbsp;&nbsp;凭证号：<INPUT class="input" id="txtVoucherID" style="WIDTH: 130px" type="text" size="12" name="txtVoucherID"
																runat="server">
														</TD>
													</TR>
													<tr>
														<TD>合同编号：<INPUT class="input" id="txtContractID" style="WIDTH: 130px" type="text" size="12" name="txtContractID"
																runat="server">
															&nbsp;&nbsp;合同名称：<INPUT class="input" id="txtContractName" style="WIDTH: 130px" type="text" size="12" name="txtContractName"
																runat="server">
															&nbsp;&nbsp;<input id="chkBatchPayment" type="checkbox" value="1" name="chkBatchPayment" runat="server" /><label for="chkBatchPayment">成本批量请款</label>
															&nbsp;&nbsp;<input id="chkNotBatchPayment" type="checkbox" value="1" name="chkNotBatchPayment" runat="server" checked /><label for="chkNotBatchPayment">非成本批量请款</label>
														</TD>
													</tr>
													<TR>
														<TD>受款单位：<INPUT class="input" id="txtSupplyName" type="text" size="30" name="txtSupplyName" runat="server"><A title="选择供应商" href="javascript:SelectSupplier()">
																<IMG src="../images/ToolsItemSearch.gif" border="0"></A>
															&nbsp;&nbsp;受款金额：<INPUT class="input"  style=" TEXT-ALIGN: right" id="TxtGreateCash"  type="text" size="12" name="TxtGreateCash"
																runat="server">&nbsp;-&nbsp;<INPUT class="input"  style="TEXT-ALIGN: right" id="TxtSmallCash" type="text" size="12" name="TxtSmallCash"
																runat="server">
															&nbsp;&nbsp;受 款 人：<INPUT class="input" id="txtPayer" style="WIDTH: 130px" type="text" size="12" name="txtPayer"
																runat="server">
														</TD>
													</TR>
													<tr>
														<TD>付款日期：<cc3:calendar id="dtbPayoutDate0" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														    &nbsp;到&nbsp;：<cc3:calendar id="dtbPayoutDate1" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														</TD>
													</tr>
                                                    <tr>
                                                        <td>贷方科目：<uc1:InputSubject id="ucInputSubjectStart" runat="server"></uc1:InputSubject>
                                                            &nbsp;到&nbsp;：<uc1:InputSubject id="ucInputSubjectEnd" runat="server"></uc1:InputSubject>
                                                        </td>
                                                    </tr>
                                                    <TR>
														<TD>处 理 人：<uc1:inputuser id="ucInputPerson" runat="server"></uc1:inputuser>
														    &nbsp;&nbsp;处理日期：<cc3:calendar id="dtbInputDate0" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														    &nbsp;到&nbsp;：<cc3:calendar id="dtbInputDate1" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														</TD>
													</TR>
													<TR>
														<TD>审 核 人：<uc1:inputuser id="ucCheckPerson" runat="server"></uc1:inputuser>
														    &nbsp;&nbsp;审核日期：<cc3:calendar id="dtbCheckDate0" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														    &nbsp;到&nbsp;：<cc3:calendar id="dtbCheckDate1" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														</TD>
													</TR>
												</table>
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" CssClass="list" CellPadding="0" AllowPaging="True" PageSize="15"
											AutoGenerateColumns="False" AllowSorting="True" Width="100%" ShowFooter="True">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="True" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.PayoutCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="付款单号"
													SortExpression="PayoutID">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "PayoutCode") %>'>
															<%#  DataBinder.Eval(Container.DataItem, "PayoutID") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="StatusName" HeaderText="状态" SortExpression="Status">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="相关请款单号" FooterText="合计"
													SortExpression="PaymentCodes">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "PaymentCodes") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SupplyName" HeaderText="受款单位" SortExpression="SupplyName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Payer" HeaderText="受款人" SortExpression="Payer">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PayoutDate" HeaderText="付款日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="PayoutDate">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="付款金额(元)" SortExpression="Money">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Money", "{0:N}") %>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="贷方科目" SortExpression="SubjectCode">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.SubjectRule.GetSubjectFullName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.SubjectCode")), RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.SubjectSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="CheckPersonName" HeaderText="审核人" SortExpression="CheckPersonName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CheckDate" HeaderText="审核日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="CheckDate">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="凭证号">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="javascript:GotoVoucher(this.val);" val='<%#DataBinder.Eval(Container, "DataItem.VoucherCode")%>'>
															<%# RmsPM.BLL.PaymentRule.GetVoucherName(DataBinder.Eval(Container, "DataItem.VoucherCode").ToString())%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<cc1:gridpagination id="GridPagination1" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="GridPagination1_PageIndexChange"></cc1:gridpagination>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</TABLE>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtStatus" type="hidden" name="txtStatus" runat="server">
			<input id="txtSelect" type="hidden" name="txtSelect" runat="server"> <input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
			<input id="txtSumMoney" type="hidden" value="none" name="txtSumMoney" runat="server">
			<script language="javascript">
<!--
var CurrUrl = window.location.href;

//选择供应商
function SelectSupplier()
{
	OpenLargeWindow("../SelectBox/SelectSupplier.aspx", "选择供应商");
}

//选择供应商返回
function DoSelectSupplierReturn(code, name)
{
//	Form1.txtSupplyCode.value = code;
	Form1.txtSupplyName.value = name;
}

//查看付款
function View(PayoutCode)
{
	window.navigate('../Finance/PayoutInfo.aspx?PayoutCode='+ PayoutCode + "&FromUrl=" + escape(CurrUrl));
}

//生成凭证
function BuildVoucher(isNew)
{
	var s = ChkGetSelected(document.all.chkSelect);
		
	if (s == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}
	
	Form1.txtSelect.value = s;

	if (isNew)
	{
		OpenCustomWindow("../Finance/VoucherModify.aspx?Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value + "&RelaCode=" + s,"凭证修改", 780, 580);
	}
	else
	{
		OpenMiddleWindow("../Finance/SelectVoucher.aspx?Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value,"选择凭证");
	}

//		document.all.btnBatchHidden.click();
//		return true;
}

function batchModify()
{
	var s = ChkGetSelected(document.all.chkSelect);
		
	if (s == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}
	
	Form1.txtSelect.value = s;

	OpenCustomWindow("../Finance/PayoutBatchModify.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&SelectCode=" + s,"批量修改供应商", 780, 580);
	//OpenMiddleWindow("../Finance/SelectVoucher.aspx?Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value,"选择凭证");
	

}

	//加入凭证
function SelectVoucherReturn(VoucherCode)
{
	var RelaCode = Form1.txtSelect.value;
	OpenCustomWindow("../Finance/VoucherModify.aspx?VoucherCode=" + VoucherCode + "&Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value + "&RelaCode=" + RelaCode,"凭证修改", 780, 580);
}

//查看凭证
function GotoVoucher(VoucherCode)
{
	OpenCustomWindow("../Finance/VoucherInfo.aspx?VoucherCode=" + VoucherCode + "&Open=1", "凭证信息", 760, 540);
}

//高级查询
function ShowAdvSearch()
{
	var display = Form1.txtAdvSearch.value;
	
	if ( display == "none" )
	{
		display = "block";
	}
	else
	{
		display = "none";
	}
	
	Form1.txtAdvSearch.value = display;
	
	SetAdvSearch();;
}

function SetAdvSearch()
{
	document.all("divAdvSearch").style.display = Form1.txtAdvSearch.value;

	if ( Form1.txtAdvSearch.value == "none" )
	{
//		Form1.imgAdvSearch.src = "../images/ArrowDown.gif";
		Form1.imgAdvSearch.title = "高级查询";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		Form1.imgAdvSearch.title = "隐藏高级查询";
	}
}

//搜索条件按回车
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		Form1.btnSearch.click();
	}
}

SetAdvSearch();

//-->
			</script>
		</form>
	</body>
</HTML>
