<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectYm" Src="../CostBudget/CostBudgetSelectYm.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostPlanList" CodeFile="CostPlanList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>付款计划</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/XmlCom.js"></SCRIPT>
		<SCRIPT language="javascript" src="../CostBudget/CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript" src="../CostBudget/CostBudgetInfo.js" charset="gb2312"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<LINK href="../CostBudget/CostBudget.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
		
//改变年月范围后
function CostBudgetSelectMonthClick()
{
    return true;
}

/*
//修改
function Modify()
{
	var CostBudgetSetCode = Form1.txtCostBudgetSetCode.Value;
	if (CostBudgetSetCode == "")
	{
	    alert("请选择预算表");
	    return false;
	}
	
	OpenFullWindow("CostPlanBatchModify.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + CostBudgetSetCode + "&StartYm=" + StartYm + "&EndYm=" + EndYm, "CostPlanBatchModify");
}
*/
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="note" nowrap><asp:label id="lblProjectName" Runat="server"></asp:label>项目精确付款计划表（元）</TD>
								<td nowrap width="100%" style="padding-left:80px"><uc1:costbudgetselectym id="ucCostBudgetSelectMonth" runat="server" OnClientPost="CostBudgetSelectMonthClick()" ShowMonthVisible="False"
										ShowMonth="True" MaxYearsBetween=20></uc1:costbudgetselectym></td>
							</TR>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<td valign="top">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
							<tr>
								<TD vAlign="top" align="left">
									<div id="tbl-container" onkeydown="return ListKeyDown(this);">
										<table class="tbl-list" id="Tree">
											<thead>
												<tr class="list-title">
													<th noWrap align="center" rowSpan="2">
														费用项</th>
													<th noWrap align="center" rowSpan="2">
														合同编号</th>
													<th noWrap align="center" rowSpan="2">
														合同名称</th>
													<th noWrap align="center" rowSpan="2">
														承包商</th>
													<th noWrap align="center" rowSpan="2">
														合同金额
													</th>
													<th noWrap align="center" rowSpan="2">
														累计已批
													</th>
													<th noWrap align="center" rowSpan="2">
														累计已付
													</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
												</tr>
												<tr class="list-title">
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
												</tr>
											</thead>
											<tbody>
												<asp:repeater id="dgList" Runat="server">
													<ItemTemplate>
														<tr style="display:none" id='TreeNode_<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' Deep='<%# DataBinder.Eval(Container, "DataItem.Deep") %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' ChildCount='<%# DataBinder.Eval(Container, "DataItem.ChildCount") %>' RowIndex='<%# Container.ItemIndex %>' DefaultExpand='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>'
															onclick="RowClick(this);" onmouseup="">
															<td nowrap title='<%# DataBinder.Eval(Container, "DataItem.SortID") %>'><span id='TreeNodeSpan_<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>'></span><span style='width:15px'><img id='TreeNodeImg_<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' style="display:none;cursor:hand" onclick="CBTree_ImgExpandClick(this);"></span>
																<%# DataBinder.Eval(Container, "DataItem.CostName") %>
																<input type="hidden" runat="server" id="txtCostCode" name="txtCostCode" value='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'>
																<input type="hidden" runat="server" id="txtDtlCode" name="txtDtlCode" value='<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>'>
																<input type="hidden" runat="server" id="txtIsExpand" name="txtIsExpand" value='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>'>
															</td>
															<td nowrap class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'>
																<%# DataBinder.Eval(Container, "DataItem.ContractID") %>
															</td>
															<td nowrap class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'>
																<%# RmsPM.BLL.CostPlan.GetContractHref(DataBinder.Eval(Container, "DataItem.ContractCode"), DataBinder.Eval(Container, "DataItem.ContractName"), DataBinder.Eval(Container, "DataItem.RecordType"))%>
															</td>
															<td nowrap class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'>
																<a href="#" onclick="ViewSupplierInfo(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>'>
																	<%# DataBinder.Eval(Container, "DataItem.SupplierName") %>
																</a>
															</td>
															<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractTotalMoney")) %>
															</td>
															<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractPay")) %>
															</td>
															<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractPayReal")) %>
															</td>
															<%# DataBinder.Eval(Container, "DataItem.PlanDataHtml") %>
														</tr>
													</ItemTemplate>
												</asp:repeater></tbody></table>
									</div>
								</TD>
							</tr>
						</table>
					</td>
				</TR>
			</TABLE>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 80px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 80px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server">
			<INPUT id="txtListScrollTop" type="hidden" name="txtListScrollTop" runat="server"><INPUT id="txtListScrollLeft" type="hidden" name="txtListScrollLeft" runat="server">
			<INPUT id="txtLastSelectedRowID" type="hidden" name="txtLastSelectedRowID" runat="server">
			<INPUT id="txtLastSelectedRowClass" type="hidden" name="txtLastSelectedRowClass" runat="server"><INPUT id="Hidden3" type="hidden" name="txtListScrollLeft" runat="server">
			<input id="txtStartYm" type="hidden" name="txtStartYm" runat="server"><input id="txtEndYm" type="hidden" name="txtEndYm" runat="server">
		</form>
	</body>
</HTML>
