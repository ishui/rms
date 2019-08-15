<%@ Page language="c#" Inherits="RmsPM.Web.Cost.BudgetModify" CodeFile="BudgetModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>费用预算修改</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body onkeydown="if(event.keyCode==13) event.keyCode=9" onload="IniBody(); return false;">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="95%" align="center" border="0" height="98%"
				id="tableFull">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						<asp:Label id="lblBudgetName" runat="server">Label</asp:Label></td>
				</tr>
				<tr>
					<td height="25" class="note">单位（万元） <font class="trAH">已发生</font> <font class="trUse">合同占用</font>
						<font class="trApply">合同申请</font></td>
				</tr>
				<TR>
					<td class="topic" vAlign="top" align="center">
						<table class="list" cellSpacing="0" cellPadding="0" width="98%" align="center" border="0"
							id="tableMain" style="DISPLAY:none">
							<TR>
								<td noWrap rowSpan="2">费用名称</td>
								<td noWrap rowSpan="2">估算费用</td>
								<td noWrap rowSpan="2">预算费用</td>
								<td noWrap rowSpan="2">期前累计发生</td>
								<td noWrap colSpan="13" align="center">本期预算</td>
								<td noWrap colSpan="11" align="center">后续预算</td>
							</TR>
							<tr>
								<td noWrap>本期预算</td>
								<td id="tdMonthTitle1" noWrap width="50">1</td>
								<td id="tdMonthTitle2" noWrap width="50">2</td>
								<td id="tdMonthTitle3" noWrap width="50">3</td>
								<td id="tdMonthTitle4" noWrap width="50">4</td>
								<td id="tdMonthTitle5" noWrap width="50">5</td>
								<td id="tdMonthTitle6" noWrap width="50">6</td>
								<td id="tdMonthTitle7" noWrap width="50">7</td>
								<td id="tdMonthTitle8" noWrap width="50">8</td>
								<td id="tdMonthTitle9" noWrap width="50">9</td>
								<td id="tdMonthTitle10" noWrap width="50">10</td>
								<td id="tdMonthTitle11" noWrap width="50">11</td>
								<td id="tdMonthTitle12" noWrap width="50">12</td>
								<td id="tdYearTitle0" noWrap>后续总预算</td>
								<td noWrap id="tdYearTitle1" runat="server">后续1期</td>
								<td noWrap id="tdYearTitle2" runat="server">后续2期</td>
								<td noWrap id="tdYearTitle3" runat="server">后续3期</td>
								<td noWrap id="tdYearTitle4" runat="server">后续4期</td>
								<td noWrap id="tdYearTitle5" runat="server">后续5期</td>
								<td noWrap id="tdYearTitle6" runat="server">后续6期</td>
								<td noWrap id="tdYearTitle7" runat="server">后续7期</td>
								<td noWrap id="tdYearTitle8" runat="server">后续8期</td>
								<td noWrap id="tdYearTitle9" runat="server">后续9期</td>
								<td noWrap id="tdYearTitle10" runat="server">后续10期</td>
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
											</font>&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "CostName") %>
											<input type=hidden id='<%# "txtCostName" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  value='<%# DataBinder.Eval(Container.DataItem, "CostName") %>' NAME="txtCostName">
										</td>
										<td nowrap align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "TotalMoney") ) %></td>
										<td nowrap align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "BudgetCost") ) %></td>
										<td nowrap align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "BeforeHappenCost") ) %></td>
										<td nowrap align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost") ) %></td>
										<td nowrap align="right"  id='<%# "tdMonth1" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right">
														<%# DataBinder.Eval(Container.DataItem, "MonthAH1")  %>
													</td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse1")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply1")  %></td>
												</tr>
												<tr >
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost1" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost1")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost1" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost1")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth2" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH2")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse2")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply2")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost2" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost2")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost2" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost2")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth3" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH3")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse3")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply3")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost3" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost3")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost3" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost3")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth4" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH4")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse4")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply4")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost4" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost4")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost4" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost4")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth5" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH5")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse5")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply5")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost5" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost5")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost5" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost5")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth6" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH6")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse6")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply6")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost6" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost6")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost6" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost6")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth7" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH7")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse7")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply7")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost7" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost7")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost7" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost7")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth8" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH8")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse8")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply8")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost8" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost8")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost8" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost8")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth9" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH9")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse9")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply9")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost9" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost9")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost9" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost9")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth10" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH10")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse10")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply10")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost10" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost10")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost10" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost10")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth11" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH11")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse11")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply11")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost11" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost11")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost11" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost11")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdMonth12" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trMonthAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthAH12")  %></td>
												</tr>
												<tr height="20" id='trMonthUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthUse12")  %></td>
												</tr>
												<tr height="20" id='trMonthApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "MonthApply12")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtCurrentPlanCost12" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost12")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divCurrentPlanCost12" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.CurrentPlanCost12")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<div id='<%# "divAfterPlanCost" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost")) %></div>
										</td>
										<td nowrap align="right"  id='<%# "tdYear1" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH1")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse1")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply1")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost1" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost1")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost1" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost1")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear2" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH2")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse2")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply2")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost2" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost2")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost2" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost2")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear3" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH3")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse3")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply3")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost3" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost3")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost3" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost3")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear4" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH4")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse4")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply4")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost4" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost4")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost4" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost4")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear5" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH5")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse5")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply5")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost5" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost5")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost5" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost5")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear6" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH6")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse6")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply6")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost6" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost6")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost6" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost6")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear7" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH7")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse7")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply7")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost7" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost7")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost7" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost7")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear8" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH8")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse8")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply8")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost8" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost8")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost8" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost8")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear9" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH9")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse9")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply9")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost9" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost9")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost9" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost9")) %></div>
													</td>
												</tr>
											</table>
										</td>
										<td nowrap align="right"  id='<%# "tdYear10" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr height="20" id='trYearAH' class="trAH">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearAH10")  %></td>
												</tr>
												<tr height="20" id='trYearUse' class="trUse">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearUse10")  %></td>
												</tr>
												<tr height="20" id='trYearApply' class="trApply">
													<td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "YearApply10")  %></td>
												</tr>
												<tr>
													<td>
														<input  type="text" id='<%# "txtAfterPlanCost10" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost10")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
														<div id='<%# "divAfterPlanCost10" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(DataBinder.Eval(Container, "DataItem.AfterPlanCost10")) %></div>
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</ItemTemplate>
							</asp:repeater>
						</table>
						<table width="100%" cellspacing="10">
							<TR>
								<TD align="center"><INPUT class="submit" id="btnSave" onclick=" if ( !doGetResult() ) return false;  " type="button"
										value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;<INPUT class="submit" onclick="window.close();" type="button" value="取 消">
								</TD>
							</TR>
						</table>
					</td>
				</TR>
			</TABLE>
			<iframe id="iframeSave" style="DISPLAY: none" src="../Cost/SavingWating.htm" frameBorder="no"
				width="100%" scrolling="auto" height="70%"></iframe><input id="txtYear" type="hidden" name="txtYear" runat="server">
			<input id="txtMonth" type="hidden" name="txtMonth" runat="server"> <input id="txtAfterPeriod" type="hidden" name="txtAfterPeriod" runat="server">
			<input id="txtPeriodMonth" type="hidden" name="txtPeriodMonth" runat="server"> <input id="txtAllCode" type="hidden" name="txtAllCode" runat="server">
			<input type="hidden" runat="server" id="txtResult" NAME="txtResult">
		</form>
		<script language="javascript">
<!--

	var ERR_NUMBER = "非法的数值！";

	var IMaxMonth = 12;
	var IMaxPeriod = 10;

	function ShowNumberDetail( costCode, numberType, startDate, endDate )
	{
		OpenMiddleWindow( '../Cost/ShowNumberDetail.aspx?CostCode=' + costCode + '&NumberType=' + numberType + '&StartDate=' + startDate + '&EndDate=' + endDate  ,null );
	}

	function IniBody()
	{
	
		var iYear = parseInt( Form1.txtYear.value);
		var iMonth = parseInt( Form1.txtMonth.value);
		var periodMonth = parseInt ( Form1.txtPeriodMonth.value);
		var afterPeriod = parseInt ( Form1.txtAfterPeriod.value);
		
		var allCodesString = Form1.txtAllCode.value;
		var codes = allCodesString.split(',');
		var iCount = codes.length;

		for ( var i=0;i<iCount;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
				var trObj = document.all( "tr"+codetemp );
				if ( parseInt(trObj.ChildCount) > 0 )
				{
					trObj.isEnd = "0";
					
					for ( var j=1;j<=IMaxMonth;j++)
						document.all( "txtCurrentPlanCost" + j + codetemp ).style.display = "none";

					for ( var j=1;j<=IMaxPeriod;j++)
						document.all( "txtAfterPlanCost" + j + codetemp ).style.display = "none";

				}
				else
				{
					trObj.isEnd = "1";
					

					for ( var j=1;j<=IMaxMonth;j++)
						document.all( "divCurrentPlanCost" + j + codetemp ).style.display = "none";

					for ( var j=1;j<=IMaxPeriod;j++)
						document.all( "divAfterPlanCost" + j + codetemp ).style.display = "none";
				}

				
				for ( var j=1;j<=IMaxMonth;j++)
				{
					if ( j> periodMonth)
						document.all( "tdMonth" + j + codetemp ).style.display = "none";
					
				}
				
				for ( var j=1;j<=IMaxPeriod;j++)
				{
					if ( j> afterPeriod)
						document.all( "tdYear" + j + codetemp ).style.display = "none";
				}
			}
		}
		
		
		
		for ( var i=1;i<=IMaxMonth;i++)
		{
			
			if ( i> periodMonth)
				document.all( "tdMonthTitle" + i ).style.display = "none";

			document.all("tdMonthTitle" + i).innerText = getNextMonth(iYear,iMonth,i) ;
			
		}
		
		for ( var i=1;i<=IMaxPeriod;i++)
		{
			if ( i> afterPeriod)
				document.all( "tdYearTitle" + i ).style.display = "none";
		}
		
		undoHidden();
		document.all("tableMain").style.display = "";

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


/*	

	function doSSingle(obj)
	{
		var code = obj.code;
		var upString = document.all( "txtUnitPrice"+code ).value;
		var pqString = document.all( "txtProjectQuantity"+code ).value;

		if ( upString == "" )
		{
			alert ( "单价： 请填写数值"  );
			return;
		}
		
		if ( pqString == "" )
		{
			alert ( "数量： 请填写数值"  );
			return;
		}
		
		if (!checknumber( upString  ,"单价")){
			return ;
		}
		
		if (!checknumber( pqString ,"数量")){
			return ;
		}
	
		var up = parseFloat(upString);
		var pq = parseFloat(pqString);
		document.all( "txtTotalMoney"+code ).value = up*pq/10000;

	}
	
*/



	function checknumber(data,lbl){
		var tmp ;
		if (data == "")
		{
			return true;
		}
		var re = /^[\-\+]?([1-9]\d*|0|[1-9]\d{0,2}(,\d{3})*)(\.\d+)?([Ee][\-\+]?\d+)?$/;
		if (re.test(data)){
			gar = data + '.';
			tmp = gar.split('.');
			if (tmp[0].length > 15) {
					alert(lbl+":"+ERR_NUMBER);
			}
			return true;
		}
		alert(lbl+":"+ERR_NUMBER);
		return false;
	}


	function doGetResult()
	{
	
	
		var iYear = parseInt( Form1.txtYear.value);
		var iMonth = parseInt( Form1.txtMonth.value);
		var periodMonth = parseInt ( Form1.txtPeriodMonth.value);
		var afterPeriod = parseInt ( Form1.txtAfterPeriod.value);
		
		var allCodesString = Form1.txtAllCode.value;
		var codes = allCodesString.split(',');
		var iCount = codes.length;
		var re = "";
		
		
		// 检验输入的合法性
		for ( var i=0;i<iCount;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
			
				var trObj = document.all( "tr"+codetemp );
				if ( trObj.style.display == "" && trObj.isEnd == "1" )
				{
			
					var costName = document.all( "txtCostName" + codetemp ).value  ;
					
					//月份的
					for ( var j=1 ; j<=periodMonth ;j++)
					{
						if ( ! checknumber( document.all( "txtCurrentPlanCost" + j + codetemp ).value  , costName + "- 本期费用" ) )
						{
							return false;
						}
					}
					
					//后续期数
					for ( var j=1 ; j<=afterPeriod ;j++)
					{
						if ( ! checknumber( document.all( "txtAfterPlanCost" + j + codetemp ).value  , costName + "- 后续费用" ) )
						{
							return false;
						}
					}

				}
			}
		}
		
		
		for ( var i=0;i<iCount;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
				var trObj = document.all( "tr"+codetemp );
				if ( trObj.style.display == "" && trObj.isEnd == "1"  )
				{
					re+=trObj.code + ",";
					re+="T" +",";
					//月份的
					for ( var j=1 ; j<=IMaxMonth ;j++)
					{
						
						re+=document.all( "txtCurrentPlanCost" + j + codetemp ).value + ",";
					}

					for ( var j=1 ; j<=IMaxPeriod ;j++)
					{
						re+=document.all( "txtAfterPlanCost" + j + codetemp ).value + ",";
					}
						
					re+=";";
				}
				else
				{
					re+=trObj.code + ",F;";
				}
			}
		}
		Form1.txtResult.value = re;
		doSave();
		return true;
	}

	function doSave()
	{
		document.all("iframeSave").style.display = "";
		document.all("tableFull").style.display = "none";
	}

	function undoHidden()
	{
		document.all("iframeSave").style.display = "none";
		document.all("tableFull").style.display = "";
	}
	
//-->
		</script>
	</body>
</HTML>
