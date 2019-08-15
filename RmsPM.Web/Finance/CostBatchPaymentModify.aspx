<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.CostBatchPaymentModify" CodeFile="CostBatchPaymentModify.aspx.cs" %>
<%@ Register TagPrefix="uc4" TagName="InputGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>成本批量请款单</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<script language="javascript" src="../Rms.js" ></script>
		<script language="javascript" src="../images/convert.js"></script>
		<link href="../Images/index.css" type="text/css" rel="stylesheet" />
		<link href="../Images/infra.css" type="text/css" rel="stylesheet" />
		<LINK href="../CostBudget/CostBudget.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../CostBudget/CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript" src="CostBatchPaymentModify.js" charset="gb2312"></SCRIPT>
		<script language="javascript">

var headCount = 1;

function winload()
{
	CBTree_InitTree("Tree", "../images/plus.gif", "../images/minus.gif", headCount);
	
	CostBatchPaymentModify_InitTree("Tree", "../images/plus.gif", "../images/minus.gif", headCount);

	CBTree_ExpandTreeByNodeDefaultExpand();
}


		</script>
	</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none">
			    <input type="button" runat="server" id="btnCostBudgetSetChange" name="btnCostBudgetSetChange" value="btnCostBudgetSetChange" onserverclick="btnCostBudgetSetChange_ServerClick" />
			</div>
			<table height="100%" cellspacing="0" cellpadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">成本批量请款单</td>
				</tr>
				<tr>
					<td valign="top" align="center">
						<table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="form-item" width="80">请款单号：</td>
								<td>
								    <input class="input" id="txtPaymentID" style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px"
										readonly type="text" name="txtPaymentID" runat="server" />
								</td>
								<td class="form-item">预算表：</td>
								<td>
									<select class="select" id="sltCostBudgetSet" onchange="document.all.btnCostBudgetSetChange.click();" name="sltCostBudgetSet"
										runat="server">
										<option value="" selected>--请选择--</option>
									</select><font color="red">*</font>
								</td>
								<td class="form-item">请款总额：</td>
								<td>
								    <input id="txtMoney" style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px; TEXT-ALIGN: right"
										readonly type="text" size="12" name="txtMoney" runat="server" />&nbsp;元
								</td>
							</tr>
							<tr>
								<td class="form-item">请款类型：</td>
								<td colspan="3"><uc4:inputgroup id="ucGroup" runat="server" ClassCode="0601"></uc4:inputgroup><font color="red">*</font>
								</td>
								<td class="form-item">最后付款日：</td>
								<td>
								    <cc3:calendar id="dtPayDate" runat="server" CalendarResource="../Images/CalendarResource/" Display="True"
										Value="" ReadOnly="False"></cc3:calendar><font color="red">*</font>
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
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<div id="tbl-container" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
										<table class="tbl-list" id="Tree" onkeydown="if(event.keyCode==13) event.keyCode=9" cellSpacing="0"
											cellPadding="0" width="100%">
											<thead>
												<tr class="list-title">
													<th noWrap align="center">费用项</th>
													<th noWrap align="center">请款金额(元)</th>
												</tr>
											</thead>
											<tbody>
												<asp:repeater id="dgList" Runat="server">
													<ItemTemplate>
														<tr style="display:none" class="tr-list" id='TreeNode_<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' Deep='<%# DataBinder.Eval(Container, "DataItem.Deep") %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' ChildCount='<%# DataBinder.Eval(Container, "DataItem.ChildCount") %>' RowIndex='<%# Container.ItemIndex %>' DefaultExpand='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>' onclick="CBTree_SetRowSelected(this);">
															<td nowrap title='<%# DataBinder.Eval(Container, "DataItem.SortID") %>'><span id='TreeNodeSpan_<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'></span><span style='width:15px'><img id='TreeNodeImg_<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' style="display:none;cursor:hand" onclick="CBTree_ImgExpandClick(this);"></span>
																<%# DataBinder.Eval(Container, "DataItem.CostName") %>
																<input type="hidden" runat="server" id="txtCostCode" name="txtCostCode" value='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'>
																<input type="hidden" runat="server" id="txtIsExpand" name="txtIsExpand" value='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>'>
															</td>
															<td nowrap align="right">
                                                                <span id="spanItemMoney" runat="server" style="display:none" RowIndex='<%# Container.ItemIndex %>'>
																	<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ItemMoney"))%>
																</span><input type="text" runat="server" class="input-nember" size="16" name="txtItemMoney" id="txtItemMoney" value='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ItemMoney")) %>' RowIndex='<%# Container.ItemIndex %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' onblur="MoneyBlur(this, true);" onfocus="MoneyFocus(this);">
															</td>
														</tr>
													</ItemTemplate>
												</asp:repeater>
											</tbody>
										</table>
                        </div>
					</td>
				</tr>
				<tr id="trAutoCreatePayout" runat="server" visible="false">
					<td>
						<table cellPadding="0" width="100%">
							<tr>
								<TD class="note">注：成本批量请款审核后修改时将自动同步付款</TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="10" width="100%">
							<tr>
								<td align="center">
								    <input class="submit" id="btnSave" onclick="document.all.divHintSave.style.display = '';"
										type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick" /> 
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
			<input id="txtSelectDetailItemIndex" type="hidden" name="txtSelectDetailItemIndex" runat="server" />
			<input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server" />
			<input id="txtPBSType" type="hidden" name="txtPBSType" runat="server" />
			<input id="txtPBSCode" type="hidden" name="txtPBSCode" runat="server" />
			<input id="txtAutoCreatePayout" type="hidden" name="txtAutoCreatePayout" runat="server" />
		</form>
	</body>
</html>
