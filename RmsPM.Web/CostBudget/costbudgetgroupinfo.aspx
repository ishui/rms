<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetGroupInfo" CodeFile="CostBudgetGroupInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectMonth" Src="../CostBudget/CostBudgetSelectMonth.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>预算表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<LINK href="../CostBudget/CostBudget.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript" src="CostBudgetInfo.js" charset="gb2312"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<script language="javascript" src="../Images/locked-column.js"></script>
		<SCRIPT language="javascript">
<!--

//-->
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" onload="winload();" rightMargin="0">
		<form id="Form1" method="post" runat="server" onclick="HideContentMenu();">
			<div style="DISPLAY:none">
				<input type="button" runat="server" id="btnShowTargetMoneyHis" name="btnShowTargetMoneyHis"
					value="btnShowTargetMoneyHis" onclick="BeforePost();document.all.divHintLoad.style.display = '';" onserverclick="btnShowTargetMoneyHis_ServerClick">
				<input type="button" runat="server" id="btnHideTargetMoneyHis" name="btnHideTargetMoneyHis"
					value="btnHideTargetMoneyHis" onclick="BeforePost();document.all.divHintLoad.style.display = '';" onserverclick="btnHideTargetMoneyHis_ServerClick">
				<input type="button" runat="server" id="btnShowBudgetMoneyHis" name="btnShowBudgetMoneyHis"
					value="btnShowBudgetMoneyHis" onclick="BeforePost();document.all.divHintLoad.style.display = '';" onserverclick="btnShowBudgetMoneyHis_ServerClick">
				<input type="button" runat="server" id="btnHideBudgetMoneyHis" name="btnHideBudgetMoneyHis"
					value="btnHideBudgetMoneyHis" onclick="BeforePost();document.all.divHintLoad.style.display = '';" onserverclick="btnHideBudgetMoneyHis_ServerClick">
				<input type="button" runat="server" id="btnRefresh" name="btnRefresh" value="btnRefresh"
					onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnRefresh_ServerClick"> <input type="button" runat="server" id="btnRefreshTarget" name="btnRefreshTarget" value="btnRefreshTarget"
					onclick="document.all.divHintLoad.style.display = '';"> <input type="button" runat="server" id="btnRefreshBalance" name="btnRefreshBalance" value="btnRefreshBalance"
					onclick="document.all.divHintLoad.style.display = '';"> <input type="button" runat="server" id="btnChangeMoneyUnit" name="btnChangeMoneyUnit" value="btnChangeMoneyUnit"
					onclick="BeforePost();document.all.divHintLoad.style.display = '';" onserverclick="btnChangeMoneyUnit_ServerClick">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">预算表项目费用</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnPrint" onclick="Print()" type="button" value="打 印" name="Print"
							runat="server"> <input style="DISPLAY:none" class="button" id="btnLoadBackup" onclick="LoadBackupGroup();"
							type="button" value="查看存档" name="btnLoadBackup" runat="server"> <input class="button" id="btnClose" onclick="window.close();" type="button" value="关 闭"
							name="btnClose"><IMG src="../images/btn_li.gif" align="absMiddle">切换至：<select runat="server" id="sltBackup" name="sltBackup" class="select" onchange="sltBackupGroupChange(this);">
						</select>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top" id="tdMaster">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="note" nowrap><asp:label id="lblGroupName" Runat="server"></asp:label>汇总</TD>
								<td class="note" width="100%">（<span runat="server" id="lblDynamicDateDesc"></span>）</td>
							</TR>
						</table>
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<TD class="form-item">建筑面积(GFA)：</TD>
								<TD><asp:label id="lblBuildingArea" runat="server"></asp:label></TD>
								<TD class="form-item">单元数：</TD>
								<TD><asp:label id="lblHouseCount" runat="server"></asp:label></TD>
								<TD class="form-item">每单元建筑面积：</TD>
								<TD><asp:label id="lblHouseArea" runat="server"></asp:label></TD>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top" id="tdList">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td id="tdList1">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">项目费用明细（元
												<select style="DISPLAY:none" runat="server" id="sltMoneyUnit" name="sltMoneyUnit" class="select"
													onchange="btnChangeMoneyUnit.click();">
													<option value="wan">万元</option>
													<option value="yuan" selected>元</option>
												</select>）</td>
											<td><uc1:costbudgetselectmonth id="ucCostBudgetSelectMonth" runat="server" OnClientPost="BeforePost()" MaxYearsBetween=20></uc1:costbudgetselectmonth></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td vAlign="top">
									<div id="tbl-container" onkeydown="return ListKeyDown(this);">
										<table class="tbl-list" id="Tree">
											<thead>
												<tr class="list-title">
													<th noWrap align="center" rowSpan="2">
														费用项</th>
													<th noWrap align="center" rowSpan="2" style='display:<%=ShowColBudget?"":"none"%>'>
														<br>
														已批预算<br>
														(A)<span runat="server" id="spanListTitleTargetMoney" style="DISPLAY:none"><br>
															(<span runat="server" id="spanListTitleTargetMoneyDesc"></span>中)</span></th>
													<th noWrap align="center" rowSpan="2">
														<br>
														已定合同<br>
														(B)</th>
													<th noWrap align="center" rowSpan="2">
														<br>
														已定变更<br>
														(C)</th>
													<th noWrap align="center" rowSpan="2">
														待定<br>
														合同/变更<br>
														(D)</th>
													<th noWrap align="center" rowSpan="2">
														<br>
														估计最终价<br>
														(E)=B+C+D</th>
													<th noWrap align="center" rowSpan="2" style='display:<%=ShowContractAccountMoney?"":"none"%>'>
														<br>
														已结算<br>
														(E2)</th>
													<th noWrap align="center" rowSpan="2" style='display:<%=ShowColBudget?"":"none"%>'>
														<br>
														差额<br>
														(F)=E-A</th>
													<th noWrap align="center" rowSpan="2" style='display:<%=ShowColBeforeChange?"":"none"%>'>
														预算<br>
														单方造价<br>
														(G2)=A/GFA</th>
													<th noWrap align="center" rowSpan="2" style='display:<%=ShowColBeforeChange?"":"none"%>'>
														变更前<br>
														单方造价<br>
														(G3)=B/GFA</th>
													<th noWrap align="center" rowSpan="2">
														<br>
														单方造价<br>
														(G)=E/GFA</th>
													<th noWrap align="center" rowSpan="2">
														<br>
														单元造价<br>
														E/单元数</th>
													<th noWrap align="center" rowSpan="2">
														<br>
														累计已批<br>
														(H)</th>
													<th noWrap align="center" rowSpan="2">
														<br>
														已批%<br>
														(I)=H/E</th>
													<th noWrap align="center" rowSpan="2">
														<br>
														未批款<br>
														(J)=E-H</th>
													<th noWrap align="center" rowSpan="2">
														<br>
														累计已付<br>
														(K)</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
												</tr>
												<tr class="list-title">
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title_target_money2"])%>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title_money2"])%>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
												</tr>
											</thead>
											<tbody>
												<asp:repeater id="dgList" Runat="server" EnableViewState="False">
													<ItemTemplate>
														<tr style="display:none" id='TreeNode_<%# DataBinder.Eval(Container, "DataItem.CostBudgetDtlCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.CostBudgetDtlCode") %>' Deep='<%# DataBinder.Eval(Container, "DataItem.Deep") %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' ChildCount='<%# DataBinder.Eval(Container, "DataItem.ChildCount") %>' RowIndex='<%# Container.ItemIndex %>' DefaultExpand='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>'
															onclick="RowClick(this);" onmouseup="">
															<td nowrap title='<%# DataBinder.Eval(Container, "DataItem.SortID") %>'><span id='TreeNodeSpan_<%# DataBinder.Eval(Container, "DataItem.CostBudgetDtlCode") %>'></span><span style='width:15px'><img id='TreeNodeImg_<%# DataBinder.Eval(Container, "DataItem.CostBudgetDtlCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.CostBudgetDtlCode") %>' style="display:none;cursor:hand" onclick="CBTree_ImgExpandClick(this);"></span>
																<%# DataBinder.Eval(Container, "DataItem.CostName") %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.BudgetMoney")) %><%# RmsPM.BLL.CostBudgetDynamic.GetChangeMoneyShowHtml(ViewState["HasTargetChange"], DataBinder.Eval(Container, "DataItem.BudgetMoney"), DataBinder.Eval(Container, "DataItem.BudgetChangeMoney"), GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.BudgetChangeMoney")), "") %>' style='display:<%=ShowColBudget?"":"none"%>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.BudgetMoney")) %>
																<%# RmsPM.BLL.CostBudgetDynamic.GetChangeMoneyShowHtml(ViewState["HasTargetChange"], DataBinder.Eval(Container, "DataItem.BudgetMoney"), DataBinder.Eval(Container, "DataItem.BudgetChangeMoney"), GetMoneyShowString(DataBinder.Eval(Container, "DataItem.BudgetChangeMoney")), "") %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractMoney")) %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractMoney")) %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractChangeMoney")) %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractChangeMoney")) %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractApplyMoney")) %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractApplyMoney")) %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractTotalMoney")) %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractTotalMoney")) %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractAccountMoney")) %>' style='display:<%=ShowContractAccountMoney?"":"none"%>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.BuildingPrice"), "ContractAccountMoney")%>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractBudgetBalance")) %>' style='<%# IsRemindContractBudgetBalance?RmsPM.BLL.CostBudgetPageRule.GetContractBudgetBalanceRemindStyle(DataBinder.Eval(Container, "DataItem.ContractBudgetBalance")):"" %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractBudgetBalance")) %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.BudgetPrice")) %>' style='display:<%=ShowColBudget?"":"none"%>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.BudgetPrice"), "price")%>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractOriginalPrice")) %>' style='display:<%=ShowColBeforeChange?"":"none"%>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractOriginalPrice"), "price")%>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.BuildingPrice")) %>' style='display:<%=ShowColBeforeChange?"":"none"%>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.BuildingPrice"), "price") %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.HousePrice")) %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.HousePrice"), "price") %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractPay")) %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractPay")) %>
															</td>
															<td nowrap align="right">
																<%# RmsPM.BLL.StringRule.BuildShowPercentString(DataBinder.Eval(Container, "DataItem.ContractPayPercent"), "####") %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractPayBalance")) %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractPayBalance")) %>
															</td>
															<td nowrap align="right" title='<%# GetMoneyShowHint(DataBinder.Eval(Container, "DataItem.ContractPayReal")) %>'>
																<%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractPayReal")) %>
															</td>
															<%# DataBinder.Eval(Container, "DataItem.PlanDataHtml") %>
														</tr>
														<asp:PlaceHolder Runat="server" ID="phContract"></asp:PlaceHolder>
													</ItemTemplate>
												</asp:repeater></tbody></table>
									</div>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<INPUT id="txtExpandNode" type="hidden" name="txtExpandNode" runat="server">
			<INPUT id="txtGroupCode" type="hidden" name="txtGroupCode" runat="server"> <INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server">
			<input id="txtSessionEntityID" type="hidden" name="txtSessionEntityID" runat="server">
			<INPUT id="txtListScrollTop" type="hidden" name="txtListScrollTop" runat="server"><INPUT id="txtListScrollLeft" type="hidden" name="txtListScrollLeft" runat="server">
			<INPUT id="txtLastSelectedRowID" type="hidden" name="txtLastSelectedRowID" runat="server">
			<INPUT id="txtLastSelectedRowClass" type="hidden" name="txtLastSelectedRowClass" runat="server"><INPUT id="Hidden3" type="hidden" name="txtListScrollLeft" runat="server">
			<input id="txtCostBudgetBackupCode" type="hidden" name="txtCostBudgetBackupCode" runat="server">
		</form>
		<script language="javascript">
<!--

if (window.opener)
{
//	Form1.btnClose.style.display = "";
//	Form1.btnGoBack.style.display = "none";
}
	
//-->
		</script>
	</body>
</HTML>
