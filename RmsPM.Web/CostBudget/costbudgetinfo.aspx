<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectMonth" Src="../CostBudget/CostBudgetSelectMonth.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetInfo" CodeFile="CostBudgetInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>预算表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CostBudget/CostBudget.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
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
		<form id="Form1" method="post" runat="server" onclick1="HideContentMenu();">
			<div style="DISPLAY: none"><input id="btnShowTargetMoneyHis" onclick="BeforePost();document.all.divHintLoad.style.display = '';"
					type="button" value="btnShowTargetMoneyHis" name="btnShowTargetMoneyHis" runat="server" onserverclick="btnShowTargetMoneyHis_ServerClick">
				<input id="btnHideTargetMoneyHis" onclick="BeforePost();document.all.divHintLoad.style.display = '';"
					type="button" value="btnHideTargetMoneyHis" name="btnHideTargetMoneyHis" runat="server" onserverclick="btnHideTargetMoneyHis_ServerClick">
				<input id="btnShowBudgetMoneyHis" onclick="BeforePost();document.all.divHintLoad.style.display = '';"
					type="button" value="btnShowBudgetMoneyHis" name="btnShowBudgetMoneyHis" runat="server" onserverclick="btnShowBudgetMoneyHis_ServerClick">
				<input id="btnHideBudgetMoneyHis" onclick="BeforePost();document.all.divHintLoad.style.display = '';"
					type="button" value="btnHideBudgetMoneyHis" name="btnHideBudgetMoneyHis" runat="server" onserverclick="btnHideBudgetMoneyHis_ServerClick">
				<input id="btnRefresh" onclick="document.all.divHintLoad.style.display = '';" type="button"
					value="btnRefresh" name="btnRefresh" runat="server" onserverclick="btnRefresh_ServerClick"> <input id="btnRefreshTarget" onclick="document.all.divHintLoad.style.display = '';" type="button"
					value="btnRefreshTarget" name="btnRefreshTarget" runat="server" onserverclick="btnRefreshTarget_ServerClick"> <input id="btnRefreshBalance" onclick="document.all.divHintLoad.style.display = '';" type="button"
					value="btnRefreshBalance" name="btnRefreshBalance" runat="server" onserverclick="btnRefreshBalance_ServerClick"> <input id="btnRefreshPurchase" onclick="document.all.divHintLoad.style.display = '';" type="button"
					value="btnRefreshPurchase" name="btnRefreshPurchase" runat="server" onserverclick="btnRefreshPurchase_ServerClick"> <input id="btnRefreshCostBudgetContract" onclick="document.all.divHintLoad.style.display = '';"
					type="button" value="btnRefreshCostBudgetContract" name="btnRefreshCostBudgetContract" runat="server" onserverclick="btnRefreshCostBudgetContract_ServerClick">
				<input id="btnChangeMoneyUnit" onclick="BeforePost();document.all.divHintLoad.style.display = '';"
					type="button" value="btnChangeMoneyUnit" name="btnChangeMoneyUnit" runat="server" onserverclick="btnChangeMoneyUnit_ServerClick">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">造价控制表</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnPrint" onclick="Print()" type="button" value="打 印" name="btnPrint"
							runat="server"> <input class="button" id="btnExcel" onclick="Excel();" type="button" value="导 出" name="btnExcel"
							runat="server"> <input style="DISPLAY:none" class="button" id="btnLoadBackup" onclick="LoadBackup();" type="button"
							value="查看存档" name="btnLoadBackup" runat="server"> <input class="button" id="btnModify" style="DISPLAY: none" onclick="ModifyDynamic();" type="button"
							value="修 改" name="btnModify" runat="server"> <input class="button" id="btnModifyEx" style="DISPLAY: none" onclick="ModifyEx();" type="button"
							value="调 整" name="btnModifyEx" runat="server"> <input class="button" id="btnDelete" style="DISPLAY: none" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false; document.all.divHintSave.style.display = '';"
							type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnCheck" style="DISPLAY: none" onclick="javascript:if (!DoCheck()) return false;"
							type="button" value="审 核" name="btnCheck" runat="server" onserverclick="btnCheck_ServerClick"> <input class="button" id="btnViewHistory" style="DISPLAY: none" onclick="javascript:ViewHistory();"
							type="button" value="历 史" name="btnViewHistory" runat="server"> <input class="button" id="btnModifySet" style="DISPLAY: none" onclick="javascript:ModifySet();"
							type="button" value="预算表设置" name="btnModifySet" runat="server"> <input class="button" id="btnGoBack" style="DISPLAY: none" onclick="GoBack();" type="button"
							value="返 回" name="btnGoBack"> <input class="button" id="btnClose" onclick="window.close();" type="button" value="关 闭"
							name="btnClose"><IMG src="../images/btn_li.gif" align="absMiddle">切换至：<select runat="server" id="sltBackup" name="sltBackup" class="select" onchange="sltBackupChange(this);">
						</select>
					</td>
				</tr>
				<tr>
					<td class="table" id="tdMaster">
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item">名称：</TD>
								<TD><asp:label id="lblCostBudgetSetName" runat="server"></asp:label>（<span runat="server" id="lblDynamicDateDesc"></span>）</TD>
								<TD class="form-item">最后修改人：</TD>
								<TD><asp:label id="lblModifyPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">最后修改日期：</TD>
								<TD><asp:label id="lblModifyDate" runat="server"></asp:label></TD>
							</TR>
							<tr>
								<TD class="form-item">建筑面积(GFA)：</TD>
								<TD><asp:label id="lblBuildingArea" runat="server"></asp:label></TD>
								<TD class="form-item">单元数：</TD>
								<TD><asp:label id="lblHouseCount" runat="server"></asp:label></TD>
								<TD class="form-item">每单元建筑面积：</TD>
								<TD><asp:label id="lblHouseArea" runat="server"></asp:label></TD>
							</tr>
							<!--
								<TD class="form-item">版本号：</TD>
								<TD><asp:label id="lblVerID" Runat="server"></asp:label></TD>
								<TD class="form-item">类别：</TD>
								<TD colSpan="3"><asp:label id="lblGroupName" runat="server"></asp:label></TD>
							<TR>
								<TD class="form-item">部门：</TD>
								<TD colspan="3"><asp:label id="lblUnitName" runat="server"></asp:label></TD>
								<TD class="form-item">单位工程：</TD>
								<TD colspan="3"><asp:label id="lblPBSName" runat="server"></asp:label></TD>
								<TD class="form-item">费用项：</TD>
								<TD colSpan="3"><asp:label id="lblCostSortID" runat="server"></asp:label><asp:label id="lblCostName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">创 建 人：</TD>
								<TD><asp:label id="lblCreatePersonName" runat="server"></asp:label></TD>
								<TD class="form-item">创建日期：</TD>
								<TD><asp:label id="lblCreateDate" runat="server"></asp:label></TD>
								<TD class="form-item">审 核 人：</TD>
								<TD><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">审核日期：</TD>
								<TD><asp:label id="lblCheckDate" runat="server"></asp:label></TD>
							</TR>
								--></TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" id="tdList" vAlign="top">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td id="tdList1">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">项目费用明细（元
												<select class="select" id="sltMoneyUnit" style="DISPLAY: none" onchange="btnChangeMoneyUnit.click();"
													name="sltMoneyUnit" runat="server">
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
													<th noWrap align="center" rowSpan="2">
														合同编号</th>
													<th noWrap align="center" rowSpan="2">
														描述</th>
													<th noWrap align="center" rowSpan="2">
														承包商</th>
													<th noWrap align="center" rowSpan="2">
														说明</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
													<th id="tdTargetHead" noWrap align="center" rowSpan="2" style='display:<%=ShowColBudget?"":"none"%>' onmouseup1="ShowTargetHeadMenu(this);">
														已批预算<br>
														<asp:label id="lblTargetCheckDate" Runat="server"></asp:label>&nbsp;<A id="hrefTargetVerID" onclick="ShowTargetHeadMenu(this);return false;" href="#" runat="server"></A><span id="spanTargetVerID" runat="server"></span><span id="spanListTitleTargetMoney" style="DISPLAY: none" runat="server"><br>
															(<span id="spanListTitleTargetMoneyDesc" runat="server"></span>中)</span><br>
														(A)</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["TargetHisHead1"])%>
													<th noWrap align="center" rowSpan="2">
														<br>
														合同金额<br>
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
														(G1)=E/GFA</th>
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
												</tr>
												<tr class="list-title">
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["TargetHisHead2"])%>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title_target_money2"])%>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title_money2"])%>
												</tr>
											</thead>
											<tbody>
												<asp:repeater id="dgList" Runat="server" EnableViewState="False">
													<ItemTemplate>
<tr id='TreeNode_<%# DataBinder.Eval(Container, "DataItem.CostBudgetDtlCode") %>' Deep='<%# DataBinder.Eval(Container, "DataItem.Deep") %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' <%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.ChildCount"))==0?"":("ChildCount="+DataBinder.Eval(Container, "DataItem.ChildCount")) %> <%# RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.IsExpand"))=="1"?"DefaultExpand=1":"" %>
	<%# RmsPM.BLL.ConvertRule.ToDecimal(DataBinder.Eval(Container, "DataItem.BalanceContractMoney"))==0?"":("BalanceContractMoney='" + RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.BalanceContractMoney")) + "'") %> onclick="RowClick(this);" onmouseup="RowMouseClick(this);">
<%# DataBinder.Eval(Container, "DataItem.PageHtml") %>
</tr>
													</ItemTemplate>
												</asp:repeater></tbody></table>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; TEXT-ALIGN: center">
				<TABLE id="tableHintLoad" height="80" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" scrolling="no"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; TEXT-ALIGN: center">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<INPUT id="txtExpandNode" type="hidden" name="txtExpandNode" runat="server"><INPUT id="txtCostBudgetCode" type="hidden" name="txtCostBudgetCode" runat="server">
			<INPUT id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
			<INPUT id="txtPBSType" type="hidden" name="txtPBSType" runat="server"><INPUT id="txtPBSCode" type="hidden" name="txtPBSCode" runat="server">
			<INPUT id="txtFirstCostBudgetCode" type="hidden" name="txtFirstCostBudgetCode" runat="server">
			<INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server"> <INPUT id="txtListScrollTop" type="hidden" name="txtListScrollTop" runat="server"><INPUT id="txtListScrollLeft" type="hidden" name="txtListScrollLeft" runat="server">
			<input id="txtSessionEntityID" type="hidden" name="txtSessionEntityID" runat="server">
			<INPUT id="txtLastSelectedRowID" type="hidden" name="txtLastSelectedRowID" runat="server">
			<INPUT id="txtLastSelectedRowClass" type="hidden" name="txtLastSelectedRowClass" runat="server"><INPUT id="Hidden3" type="hidden" name="txtListScrollLeft" runat="server">
			<INPUT id="txtShowTargetMoneyHisVerID" type="hidden" name="txtShowTargetMoneyHisVerID"
				runat="server"> <INPUT id="txtHasTargetHis" type="hidden" name="txtHasTargetHis" runat="server"><INPUT id="txtShowTargetHis" type="hidden" name="txtShowTargetHis" runat="server">
			<input id="txtCostBudgetBackupCode" type="hidden" name="txtCostBudgetBackupCode" runat="server"><input id="txtCostBudgetBackupSetCode" type="hidden" name="txtCostBudgetBackupSetCode"
				runat="server">
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
