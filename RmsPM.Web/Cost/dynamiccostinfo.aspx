<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicCostInfo" CodeFile="DynamicCostInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>动态费用信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body onload="IniBody(); return false;">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">动态费用信息</td>
				</tr>
				<tr height="25">
					<td class="tools-area" vAlign="top">
						<INPUT class="button" id="btnAdjust" onclick="doAdjust(); return false;" type="button"
							value="调 整" name="btnModifyList" runat="server">&nbsp; <input class="button" id="btnClose" onclick="window.self.close();" type="button" value="关 闭"
							name="btnClose" runat="server"> <input class="button" id="btnModifyList" onclick="doViewModifyList(); return false;" type="button"
							value="历次变更" name="btnModifyList" runat="server">&nbsp;<INPUT class="button" id="btnModifyDetail" onclick="doModifyDetail(); return false;" type="button"
							value="细化预算" name="btnModifyDetail" runat="server">
					</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top">
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD width="700">
									<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<td align="left" width="100">单位（万元）</td>
											<td id="tdCostName" runat="server"></td>
											<td></td>
										</TR>
									</table>
									<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<TD class="form-item">估算金额：</TD>
											<TD><asp:label id="lblCostEstimate" runat="server" CssClass="Lable"></asp:label></TD>
											<TD class="form-item">预算金额：</TD>
											<TD><asp:label id="lblBudgetCost" runat="server" CssClass="Lable"></asp:label></TD>
											<TD class="form-item">动态费用：</TD>
											<TD><asp:label id="lblDynamciCost" runat="server" CssClass="Lable"></asp:label></TD>
										</tr>
										<TR>
											<TD class="form-item">期前累计发生数：</TD>
											<TD><asp:label id="lblBeforeHappenCost" runat="server" CssClass="Lable"></asp:label></TD>
											<TD class="form-item">本期动态：</TD>
											<TD><asp:label id="lblCurrentDynamicCost" runat="server" CssClass="Lable"></asp:label></TD>
											<TD class="form-item">后续总预算：</TD>
											<TD><asp:label id="lblAfterPlanCost" runat="server" CssClass="Lable"></asp:label></TD>
										</TR>
										<tr>
											<TD class="form-item">累计发生：</TD>
											<TD><asp:label id="lblAPMoney" runat="server" CssClass="Lable"></asp:label></TD>
											<TD class="form-item">待发生费用：</TD>
											<TD><asp:label id="lblDPMoney" runat="server" CssClass="Lable"></asp:label></TD>
											<TD class="form-item">期前赢余：</TD>
											<TD><asp:label id="lblSurplusCost" runat="server" CssClass="Lable"></asp:label></TD>
										</tr>
										<tr>
											<TD class="form-item">合同占用：</TD>
											<TD><asp:label id="lblContractUse" runat="server" CssClass="Lable"></asp:label></TD>
											<TD class="form-item">合同申请：</TD>
											<TD><asp:label id="lblContractApply" runat="server" CssClass="Lable"></asp:label></TD>
											<TD class="form-item">非合同发生：</TD>
											<TD><asp:label id="lblUnContractHappened" runat="server" CssClass="Lable"></asp:label></TD>
										</tr>
									</table>
									<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD>明细：</TD>
											<td nowrap>&nbsp;<font class="trBudget"><img border="0" src="../Images/Yes.GIF" height="15" width="15">预算</font></td>
											<td nowrap>&nbsp;<font class="trAH"><img border="0" src="../Images/Yes.GIF" height="15" width="15">已发生</font></td>
											<td nowrap>
												<asp:checkboxlist id="chklistView" runat="server" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
													<asp:ListItem Value="ContractUse" Selected="True">
														<font class="trUse">合同占用</font></asp:ListItem>
													<asp:ListItem Value="Apply">
														<font class="trApply">合同申请</font></asp:ListItem>
												</asp:checkboxlist>
											</td>
											<td nowrap><img border="0" src="../Images/Yes.GIF" height="15" width="15">余款 <font class="tdAlert">
													（不足）</font></td>
											<td>&nbsp;</td>
											<TD nowrap><input id="chkChild" type="checkbox" value="1" runat="server">显示子项</TD>
											<td nowrap><input class="submit" id="btnView" type="button" value="查 看" runat="server" onserverclick="btnView_ServerClick">
											</td>
										</TR>
									</table>
								</TD>
								<TD vAlign="top" width="420">
									<table cellSpacing="10" width="100%">
										<tr>
											<td>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td class="intopic" width="200">相关合同</td>
														<td><A onclick="doMoreContract();" href="##"><IMG height="17" src="../images/desktop/bn_more.gif" width="60" border="0"></A></td>
													</tr>
												</table>
												<table class="list" cellSpacing="0" cellPadding="0" width="98%" align="center" border="0">
													<tr class="list-title">
														<td noWrap>合同名称</td>
														<td noWrap align="right">该项费用金额（元）</td>
														<td noWrap align="right">该项费用已发生（元）</td>
													</tr>
													<asp:repeater id="repeatContract" runat="server">
														<ItemTemplate>
															<tr class="list-i">
																<td nowrap>
																	<a href="#"  onclick='doViewContract(this.code)' code='<%# DataBinder.Eval(Container.DataItem, "ContractCode") %>' >
																		<%# DataBinder.Eval(Container.DataItem, "ContractName") %>
																	</a>
																</td>
																<td>
																	<div align="right"><%# RmsPM.BLL.StringRule.BuildShowNumberString( DataBinder.Eval(Container.DataItem, "ContractCostMoney") ) %></div>
																</td>
																<td>
																	<div align="right"><%# RmsPM.BLL.StringRule.BuildShowNumberString ( DataBinder.Eval(Container.DataItem, "ContractPayed") ) %></div>
																</td>
															</tr>
														</ItemTemplate>
													</asp:repeater></table>
											</td>
										</tr>
									</table>
								</TD>
								<td>&nbsp;</td>
							</TR>
							<TR>
								<TD colSpan="3">
									<table class="table" id="tableMain" style="DISPLAY: none" borderColor="#31659c" cellSpacing="0"
										cellPadding="3" width="98%" align="center" border="1">
										<tr align="center">
											<td noWrap rowSpan="2">费用名称&nbsp;&nbsp;&nbsp;<button class="button-small" onclick="doDispayBudget();" type="button" value="O">&lt;&gt;</button>
											</td>
											<td id="tdTitleTotalMoney" noWrap rowSpan="2">估算费用</td>
											<td id="tdTitleBudgetCost" noWrap rowSpan="2">预算费用</td>
											<td id="tdTitleDynamicCost" noWrap rowSpan="2">动态费用</td>
											<td id="tdTitleBeforeHappenCost" noWrap rowSpan="2">预算前累计发生</td>
											<td noWrap colSpan="13">本期预算</td>
											<td noWrap colSpan="5">后续预算</td>
										</tr>
										<tr>
											<td noWrap>本期预算</td>
											<td id="tdMonthTitle1" noWrap runat="server">1</td>
											<td id="tdMonthTitle2" noWrap runat="server">2</td>
											<td id="tdMonthTitle3" noWrap runat="server">3</td>
											<td id="tdMonthTitle4" noWrap runat="server">4</td>
											<td id="tdMonthTitle5" noWrap runat="server">5</td>
											<td id="tdMonthTitle6" noWrap runat="server">6</td>
											<td id="tdMonthTitle7" noWrap runat="server">7</td>
											<td id="tdMonthTitle8" noWrap runat="server">8</td>
											<td id="tdMonthTitle9" noWrap runat="server">9</td>
											<td id="tdMonthTitle10" noWrap runat="server">10</td>
											<td id="tdMonthTitle11" noWrap runat="server">11</td>
											<td id="tdMonthTitle12" noWrap runat="server">12</td>
											<td id="tdYearTitle0" noWrap runat="server">后续总预算</td>
											<td id="tdYearTitle1" noWrap runat="server">后续1期</td>
											<td id="tdYearTitle2" noWrap runat="server">后续2期</td>
											<td id="tdYearTitle3" noWrap runat="server">后续3期</td>
											<td id="tdYearTitle4" noWrap runat="server">后续4期</td>
											<td id="tdYearTitle5" noWrap runat="server">后续5期</td>
											<td id="tdYearTitle6" noWrap runat="server">后续6期</td>
											<td id="tdYearTitle7" noWrap runat="server">后续7期</td>
											<td id="tdYearTitle8" noWrap runat="server">后续8期</td>
											<td id="tdYearTitle9" noWrap runat="server">后续9期</td>
											<td id="tdYearTitle10" noWrap runat="server">后续10期</td>
										</tr>
										<asp:repeater id="repeat1" runat="server">
											<ItemTemplate>
												<tr id='<%# "tr" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' pCode ='<%# DataBinder.Eval(Container.DataItem, "ParentCode").ToString() %>' ChildCount='<%# DataBinder.Eval(Container.DataItem, "ChildCount").ToString() %>'
											fCode = '<%# "@"  + DataBinder.Eval(Container.DataItem, "FullCode").ToString() %>'
											code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' 
											isEnd=""
											>
													<td nowrap class='<%# "list-" +  DataBinder.Eval(Container.DataItem, "Deep").ToString() %>'>
														<font color="GrayText">
															<%# DataBinder.Eval(Container.DataItem, "SortID") %>
														</font><a id='<%# "aDown" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' href="##" onclick="doExpand(this.code,'0')" code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'  >
															<img src="../images/Plus.gif" border="0"></a> <a id='<%# "aUp" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  href="##" onclick="doExpand(this.code,'1')" code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
															<img src="../images/Minus.gif" border="0"></a> <a href="##" onclick='doViewDynamicCost(this.code); return false;' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
															<%# DataBinder.Eval(Container.DataItem, "CostName") %>
														</a>
													</td>
													<td nowrap align="right" id="tdTotalMoney">
														<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "TotalMoney") ) %>
													</td>
													<td nowrap align="right" id="tdBudgetCost"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "BudgetCost") ) %></td>
													<td nowrap align="right" id="tdDynamicCost"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "DynamicCost") ) %></td>
													<td nowrap align="right" id="tdBeforeHappenCost"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "BeforeHappenCost") ) %></td>
													<td nowrap align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost") ) %></td>
													<td nowrap align="right" id="tdMonth1">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget1")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH1")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse1")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply1")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget1")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth2">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget2")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH2")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse2")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply2")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget2")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth3">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget3")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH3")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse3")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply3")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget3")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth4">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget4")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH4")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse4")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply4")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget4")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth5">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget5")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH5")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse5")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply5")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget5")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth6">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget6")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH6")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse6")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply6")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget6")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth7">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget7")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH7")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse7")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply7")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget7")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth8">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget8")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH8")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse8")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply8")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget9")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth9">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget9")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH9")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse9")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply9")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget9")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth10">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget10")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH10")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse10")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply10")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget10")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth11">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget11")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH11")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse11")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply11")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget11")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdMonth12">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trMonthBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthBudget12")  %></td>
															</tr>
															<tr height="20" id='trMonthAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH12")  %></td>
															</tr>
															<tr height="20" id='trMonthUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse12")  %></td>
															</tr>
															<tr height="20" id='trMonthApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply12")  %></td>
															</tr>
															<tr height="20" id='trMonthSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthSurplusBudget12")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear0"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost") )  %></td>
													<td nowrap align="right" id="tdYear1">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget1")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH1")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse1")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply1")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget1")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear2">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget2")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH2")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse2")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply2")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget2")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear3">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget3")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH3")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse3")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply3")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget3")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear4">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget4")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH4")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse4")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply4")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget4")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear5">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget5")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH5")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse5")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply5")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget5")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear6">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget6")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH6")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse6")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply6")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget6")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear7">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget7")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH7")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse7")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply7")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget7")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear8">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget8")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH8")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse8")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply8")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget8")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear9">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget9")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH9")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse9")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply9")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget9")  %></td>
															</tr>
														</table>
													</td>
													<td nowrap align="right" id="tdYear10">
														<table border="0" cellpadding="0" cellspacing="0">
															<tr height="20" id='trYearBudget' class="trBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearBudget10")  %></td>
															</tr>
															<tr height="20" id='trYearAH' class="trAH">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH10")  %></td>
															</tr>
															<tr height="20" id='trYearUse' class="trUse">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse10")  %></td>
															</tr>
															<tr height="20" id='trYearApply' class="trApply">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply10")  %></td>
															</tr>
															<tr height="20" id='trYearSurplusBudget' class="trSurplusBudget">
																<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearSurplusBudget10")  %></td>
															</tr>
														</table>
													</td>
												</tr>
											</ItemTemplate>
										</asp:repeater></table>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<br>
			<br>
			<input id="txtYear" type="hidden" name="txtYear" runat="server"> <input id="txtMonth" type="hidden" name="txtMonth" runat="server">
			<input id="txtAfterPeriod" type="hidden" name="txtAfterPeriod" runat="server"> <input id="txtPeriodMonth" type="hidden" name="txtPeriodMonth" runat="server">
			<input id="txtAllCode" type="hidden" name="txtAllCode" runat="server"> <input id="txtView" type="hidden" runat="server">
			<input id="txtBudgetCode" type="hidden" runat="server">
		</form>
		<script language="javascript">
<!--

	function ShowNumberDetail( costCode, numberType, startDate, endDate )
	{
		OpenMiddleWindow( '../Cost/ShowNumberDetail.aspx?CostCode=' + costCode + '&NumberType=' + numberType + '&StartDate=' + startDate + '&EndDate=' + endDate  ,null );
	}
 
	function doMoreContract()
	{
		OpenMiddleWindow( '../Contract/MoreContractList.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode=<%=Request["CostCode"]%>','更多相关合同' );
	}

	function doAdjust()
	{
		OpenFullWindow( '../Cost/DynamicApplyModify.aspx?Action=AddNew&ProjectCode=<%=Request["ProjectCode"]%>&InputCostCode=<%=Request["CostCode"]%>','动态调整' );
	}
	

    function doViewModifyList()
    {
		OpenLargeWindow( '../Cost/DynamicCostModifyList.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode=<%=Request["CostCode"]%>' ,'动态费用调整记录' );
    }
    
    
    function doViewCurse()
    {
		OpenMiddleWindow( '../Cost/CostContrast.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode=<%=Request["CostCode"]%>' ,'对比曲线' );
    }

	function doViewDynamicCost( code )
	{
		window.navigate( '../Cost/DynamicCostInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode=<%=Request["BudgetCode"]%>&CostCode=' + code  ,'动态费用信息' );
	}

	function doModifyDetail()
	{
		var budgetCode = Form1.txtBudgetCode.value;
		if ( budgetCode == '' )
		{
			alert ('请先制定预算');
			return;
		}
		window.navigate( '../Cost/BudgetModify.aspx?From=DynamicCost&Type=Detail&ProjectCode=<%=Request["ProjectCode"]%>&CostCode=<%=Request["CostCode"]%>&BudgetCode=' + budgetCode ,'细化预算' );
	}

	function doExpensesQuota()
	{
		var budgetCode = Form1.txtBudgetCode.value;
		if ( budgetCode == '' )
		{
			alert ('请先制定预算');
			return;
		}
		window.navigate( '../Cost/ExpensesQuota.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode=<%=Request["CostCode"]%>&BudgetCode=' + budgetCode ,'细化预算' );
	}
	

	function doViewContract( code )
	{
		OpenLargeWindow( '../Contract/ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode='+code ,'合同信息' );
	}

	function doViewPayment( code ,startDate, endDate )
	{
		OpenLargeWindow( '../Finance/PaymentInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&PaymentCode='+code + '&StartDate=' + startDate + '&EndDate=' + endDate  ,'请款单信息' );
	}

	var IMaxMonth = 12;
	var IMaxPeriod = 10;

	function IniBody()
	{
	
		var iYear = parseInt( Form1.txtYear.value);
		var iMonth = parseInt( Form1.txtMonth.value);
		var periodMonth = parseInt ( Form1.txtPeriodMonth.value);
		var afterPeriod = parseInt ( Form1.txtAfterPeriod.value);
	

		var obj = document.all("tdMonth1");
		var iCount = obj.length;
		
		for ( var i=1;i<=IMaxMonth;i++)
		{
			var monthObj = document.all( "tdMonth" + i );
			if ( obj[0] )
			{
				for ( var j=0;j<iCount;j++)
				{
					if ( i> periodMonth)
						monthObj[j].style.display = "none";
				}
			}
			else
			{
				if ( i> periodMonth)
					monthObj.style.display = "none";
			}
			
			if ( i> periodMonth)
				document.all( "tdMonthTitle" + i ).style.display = "none";
			
			document.all("tdMonthTitle" + i).innerText = getNextMonth(iYear,iMonth,i) ;
			
		}


		for ( var i=1;i<=IMaxPeriod;i++)
		{
			var yearObj = document.all( "tdYear" + i );
			if ( obj[0] )
			{
				for ( var j=0;j<iCount;j++)
				{
					if ( i> afterPeriod)
						yearObj[j].style.display = "none";
				}
			}
			else
			{
				if ( i> afterPeriod)
					yearObj.style.display = "none";
			}
			
			if ( i> afterPeriod)
				document.all( "tdYearTitle" + i ).style.display = "none";
		}
		

		var allCodesString = Form1.txtAllCode.value;
		var codes = allCodesString.split(',');
		var iCount0 = codes.length;	

		for ( var i=0;i<iCount0;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
				var trObj = document.all( "tr"+codetemp );
				

				if ( parseInt(trObj.ChildCount) > 0 )
				{
					trObj.isEnd = "0";
					document.all( "aDown"+codetemp  ).style.display="none"  ;
				}
				else
				{
					trObj.isEnd = "1";
					
					document.all( "aDown"+codetemp  ).style.display="none"  ;
					document.all( "aUp"+codetemp  ).style.display="none"  ;
				}
			}
		}
		
		doViewItem();
		doDispayBudget();
		document.all("tableMain").style.display = "";
	}

	function doViewItem()
	{
		var view = Form1.txtView.value;
		var sAH = "none";
		var sBudget = "none";
		var sUse = "none";
		var sApply = "none";
		if (  HasString(view,"AH") )
			sAH = "";
		if (  HasString(view,"Budget") )
			sBudget = "";
		if (  HasString(view,"Use") )
			sUse = "";
		if (  HasString(view,"Apply") )
			sApply = "";

		doDisplay( document.all("trMonthUse"), sUse);
		doDisplay( document.all("trMonthApply"), sApply);
		doDisplay( document.all("trYearUse"), sUse);
		doDisplay( document.all("trYearApply"), sApply);
	}


	function doDisplay ( obj , dsp )
	{
		if ( obj[0] )
		{
			var length = obj.length;
			for ( var i=0;i<length;i++)
				obj[i].style.display = dsp;
		}
		else
		{
			obj.style.display = dsp;
		}
	}

	function doExpand( code, flag)
	{
		var tr = document.all("tr"+code );
		var fullCode = tr.fCode;
		tr.isEnd = flag ;

		if ( flag == "1" )
		{
	
			if ( parseInt(tr.ChildCount) > 0 )
			{
				document.all( "aUp"+code  ).style.display="none"  ;
				document.all( "aDown"+code  ).style.display=""  ;
			}
						
		}
		else if ( flag == "0" )
		{

			if ( parseInt(tr.ChildCount) > 0 )
			{
				document.all( "aUp"+code  ).style.display=""  ;
				document.all( "aDown"+code  ).style.display="none"  ;
			}

		}
		
		var allCodesString = Form1.txtAllCode.value;
		var codes = allCodesString.split(',');
		var iCount = codes.length;
		for ( var i=0;i<iCount;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
				var trObj = document.all( "tr"+codetemp );
				if ( flag == "1" )
				{
					if ( HasString( trObj.fCode,fullCode ) && trObj.code != code  )
					{
						trObj.style.display = "none";
					}
				}
				else if ( flag=="0" )
				{
					if ( trObj.pCode == code )
					{
						trObj.style.display = "";
						doExpand(trObj.code,trObj.isEnd);
					}
				}
			}
		}

	}

	function getNextMonth( iYear, iMonth, iPlusMonth )
	{
		var iMonth = iPlusMonth + iMonth - 1 ;
		if ( iMonth > IMaxMonth )
		{
			iYear = iYear +1;
			iMonth = iMonthTemp -IMaxMonth;
		}
		return iYear + "-" + iMonth;
	}

	
	function doDispayBudget()
	{
		var dsp = document.all("tdTitleTotalMoney").style.display;
		if ( dsp == "" )
			dsp = "none";
		else
			dsp = "";

		document.all("tdTitleTotalMoney").style.display = dsp ;
		document.all("tdTitleBudgetCost").style.display = dsp ;
		document.all("tdTitleDynamicCost").style.display = dsp ;
		document.all("tdTitleBeforeHappenCost").style.display = dsp ;
		
		var obj0 = document.all("tdTotalMoney");
		var obj1 = document.all("tdBudgetCost");
		var obj2 = document.all("tdDynamicCost");
		var obj3 = document.all("tdBeforeHappenCost");
		if ( obj0[0])
		{
			var iLength = obj0.length;
			for ( var i=0;i<iLength;i++)
			{
				obj0[i].style.display = dsp;
				obj1[i].style.display = dsp;
				obj2[i].style.display = dsp;
				obj3[i].style.display = dsp;
			}
			
		}
		else
		{
			obj0.style.display = dsp;
			obj1.style.display = dsp;
			obj2.style.display = dsp;
			obj3.style.display = dsp;
		}

	}

//-->
		</script>
	</body>
</HTML>
