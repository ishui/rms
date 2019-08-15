<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostTargetModify" CodeFile="CostTargetModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputPBS" Src="../UserControls/InputPBS.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectMonth" Src="../CostBudget/CostBudgetSelectMonth.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>预算表目标费用修改</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<LINK href="../CostBudget/CostBudget.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript" src="CostTargetModify.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/convert.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript">
<!--

//费用项展开、折叠
function ImgExpand(sender, flag)
{
	var key = sender.key;

	var objTree = document.all(m_treeId);
	
	var currRow = document.all(m_rowId + key);
	
	SetRowInput(currRow, flag, '<%=ViewState["StartY"]%>', '<%=ViewState["EndY"]%>');
}

function winload()
{
	CBTree_InitTree("Tree", "../images/plus.gif", "../images/minus.gif", headCount);

	ExpandTreeByNodeDefaultExpand();
}

//-->
		</SCRIPT>
	</HEAD>
	<body scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">预算表目标费用<asp:label id="lblTitle" Runat="server">修改</asp:label></td>
				</tr>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item">预算表名称：</TD>
								<TD><asp:label id="lblCostBudgetSetName" runat="server"></asp:label></TD>
								<TD class="form-item">版本号：</TD>
								<TD><asp:label id="lblVerID" Runat="server"></asp:label></TD>
								<TD class="form-item">版本名称：</TD>
								<TD><input type="text" class="input" id="txtCostBudgetName" name="CostBudgetName" size="16" runat="server" /></TD>
							</TR>
							<TR>
								<TD class="form-item">部门：</TD>
								<TD><asp:label id="lblUnitName" runat="server"></asp:label></TD>
								<TD class="form-item">单位工程：</TD>
								<TD><asp:label id="lblPBSName" runat="server"></asp:label></TD>
								<TD class="form-item">类别：</TD>
								<TD><asp:label id="lblGroupName" runat="server"></asp:label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">目标费用明细（元）</td>
											<td><uc1:CostBudgetSelectMonth id="ucCostBudgetSelectMonth" runat="server" MaxYearsBetween=20></uc1:CostBudgetSelectMonth></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td vAlign="top">
									<div id="tbl-container" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
										<table class="tbl-list" id="Tree" onkeydown="if(event.keyCode==13) event.keyCode=9" cellSpacing="0"
											cellPadding="0" width="100%">
											<thead>
												<tr class="list-title">
													<th noWrap align="center" rowSpan="2">费用项</th>
													<th noWrap align="center" rowSpan="2" width="80">原目标费用</th>
													<th noWrap align="center" rowSpan="2" width="90">调整后目标费用</th>
													<th noWrap align="center" rowSpan="2">说明</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
												</tr>
												<tr class="list-title">
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
												</tr>
											</thead>
											<tbody>
												<asp:repeater id="dgList" Runat="server">
													<ItemTemplate>
														<tr style="display:none" id='TreeNode_<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' Deep='<%# DataBinder.Eval(Container, "DataItem.Deep") %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' ChildCount='<%# DataBinder.Eval(Container, "DataItem.ChildCount") %>' RowIndex='<%# Container.ItemIndex %>' DefaultExpand='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>' onclick="CBTree_SetRowSelected(this);">
															<td nowrap title='<%# DataBinder.Eval(Container, "DataItem.SortID") %>'><span id='TreeNodeSpan_<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'></span><span style='width:15px'><img id='TreeNodeImg_<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' style="display:none;cursor:hand" onclick="ImgExpand(this, -1);CBTree_ImgExpandClick(this);"></span>
																<%# DataBinder.Eval(Container, "DataItem.CostName") %>
																<input type="hidden" runat="server" id="txtCostCode" name="txtCostCode" value='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'>
																<input type="hidden" runat="server" id="txtCostBudgetDtlCode" name="txtCostBudgetDtlCode" value='<%# DataBinder.Eval(Container, "DataItem.CostBudgetDtlCode") %>'>
																<input type="hidden" runat="server" id="txtIsExpand" name="txtIsExpand" value='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>'>
															</td>
															<td nowrap align="right">
																<%# RmsPM.BLL.CostBudgetPageRule.GetMoneyShowString(DataBinder.Eval(Container, "DataItem.BudgetValidMoney")) %>
															</td>
															<td nowrap align="right">
																<span id="spanBudgetMoney" runat="server" style="display:none" RowIndex='<%# Container.ItemIndex %>'>
																	<%# RmsPM.BLL.CostBudgetPageRule.GetMoneyShowString(DataBinder.Eval(Container, "DataItem.BudgetMoney")) %>
																</span><input type="text" runat="server" class="input-nember" size="12" id="txtBudgetMoney" value='<%# RmsPM.BLL.CostBudgetPageRule.GetMoneyShowString(DataBinder.Eval(Container, "DataItem.BudgetMoney")) %>' RowIndex='<%# Container.ItemIndex %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' onblur="MoneyBlur(this, true);" onfocus="MoneyFocus(this);">
															</td>
															<td nowrap><input type="text" runat="server" class="input" size="40" id="txtDescription" name="txtDescription" value='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></td>
															<asp:PlaceHolder Runat="server" ID="phPlan"></asp:PlaceHolder>
														</tr>
													</ItemTemplate>
												</asp:repeater></tbody></table>
                                       </div>
								</td>
							</tr>
						</table>
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
			<!--
															<td nowrap align="right">
																<span id="spanPrice" runat="server" style="display:none" RowIndex='Container.ItemIndex'>
																	< RmsPM.BLL.CostBudgetPageRule.GetMoneyShowString(DataBinder.Eval(Container, "DataItem.Price")) %>
																</span><input type="text" runat="server" class="input-nember" size="10" id="txtPrice" value='< RmsPM.BLL.CostBudgetPageRule.GetMoneyShowString(DataBinder.Eval(Container, "DataItem.Price")) %>' RowIndex='<% Container.ItemIndex %>' ParentCode='<% DataBinder.Eval(Container, "DataItem.ParentCode") %>' onblur="PriceBlur(this, true);" onfocus="MoneyFocus(this);" NAME="txtPrice">
															</td>
															<td nowrap align="right">
																<span id="spanQty" runat="server" style="display:none" RowIndex='< Container.ItemIndex %>'>
																	< RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container, "DataItem.Qty")) %>
																</span><input type="text" runat="server" class="input-nember" size="10" id="txtQty" value='< RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container, "DataItem.Qty")) %>' RowIndex='<% Container.ItemIndex %>' ParentCode='<% DataBinder.Eval(Container, "DataItem.ParentCode") %>' onblur="QtyBlur(this, true);" onfocus="MoneyFocus(this);" NAME="txtQty">
															</td>
															<td nowrap>
																<%# DataBinder.Eval(Container, "DataItem.MeasurementUnit") %>
															</td>
															-->
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtCostBudgetCode" type="hidden" name="txtCostBudgetCode" runat="server"><input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
			<input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server"><input id="txtGroupCode" type="hidden" name="txtGroupCode" runat="server"><input id="txtValidCostBudgetCode" type="hidden" name="txtValidCostBudgetCode" runat="server">
			<INPUT id="txtFirstCostBudgetCode" type="hidden" name="txtFirstCostBudgetCode" runat="server">
			<input id="txtStatus" type="hidden" name="txtStatus" runat="server"> <input id="txtShowCostCode" type="hidden" name="txtShowCostCode" runat="server">
		</form>
	</body>
</HTML>
