<%@ Register TagPrefix="uc1" TagName="InputPBS" Src="../UserControls/InputPBS.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.ReportCostByPBS" CodeFile="ReportCostByPBS.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>成本分项汇总表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<LINK href="../CostBudget/CostBudget.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../CostBudget/CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript" src="../CostBudget/CostBudgetInfo.js" charset="gb2312"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
<script language="javascript">
<!--

var headCount = 4;

function mywinload()
{
	document.all("theadMain").style.display = (Form1.txtReportClick.value == "1")?"":"none";
	document.all("trProjectName").style.display = (Form1.txtReportClick.value == "1")?"":"none";
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

//-->
</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" style="BORDER-RIGHT:0px"
		onload="mywinload();winload();">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnPrint" onclick="Print();" type="button" value="打 印" name="btnPrint"
							runat="server">
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
														<td>统计日期：<cc3:calendar id="dtDateBegin" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
																Display="True" Value=""></cc3:calendar>
															&nbsp;到：<cc3:calendar id="dtDateEnd" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
																Display="True" Value=""></cc3:calendar>
															&nbsp;&nbsp;<input class="submit" id="btnSearch" type="button" value="开始" name="btnSearch" runat="server"
																onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick">
														</td>
													</TR>
												</table>
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr id="trProjectName">
								<td vAlign="top">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="note" nowrap><asp:label id="lblProjectName" Runat="server"></asp:label>成本分项汇总表（元）</TD>
											<td class="note" width="100%"><span runat="server" id="lblDateDesc"></span></td>
										</TR>
									</table>
								</td>
							</tr>
							<tr height="100%" runat="server">
								<td>
									<div id="tbl-container" onkeydown="return ListKeyDown(this);">
										<table class="tbl-list" id="Tree">
											<thead id="theadMain" style="display:none">
												<tr class="list-title">
													<th noWrap align="center" rowSpan="2">
														单位工程分项</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["TitleHtml1"])%>
													<th noWrap align="center" rowSpan="2">
														合计</th>
												</tr>
												<tr class="list-title" style="DISPLAY:none">
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["TitleHtml2"])%>
												</tr>
												<tr class="list-title">
													<th noWrap align="center" style="border-top:0">
														建筑面积(平米)</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["TitleHtmlArea"])%>
													<th style="border-top:0">&nbsp;<%=RmsPM.BLL.ConvertRule.ToString(ViewState["TotalPBSArea"])%></th>
												</tr>
												<tr class="list-title">
													<th noWrap align="center" style="border-top:0">
														分摊比例(%)</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["TitleHtmlAreaPercent"])%>
													<th style="border-top:0">100%</th>
												</tr>
											</thead>
											<tbody>
												<asp:repeater id="dgList" Runat="server" EnableViewState="False">
													<ItemTemplate>
														<tr style="display:none" id='TreeNode_<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' Deep='<%# DataBinder.Eval(Container, "DataItem.Deep") %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' ChildCount='<%# DataBinder.Eval(Container, "DataItem.ChildCount") %>' RowIndex='<%# Container.ItemIndex %>' DefaultExpand='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>'
															onclick="RowClick(this);" onmouseup="">
															<td nowrap title='<%# DataBinder.Eval(Container, "DataItem.SortID") %>'><span id='TreeNodeSpan_<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>'></span><span style='width:15px'><img id='TreeNodeImg_<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' style="display:none;cursor:hand" onclick="CBTree_ImgExpandClick(this);"></span>
																<%# DataBinder.Eval(Container, "DataItem.CostName") %>
																<input type="hidden" runat="server" id="txtCostCode" name="txtCostCode" value='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'>
																<input type="hidden" runat="server" id="txtDtlCode" name="txtDtlCode" value='<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>'>
																<input type="hidden" runat="server" id="txtIsExpand" name="txtIsExpand" value='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>'>
															</td>
															<%# DataBinder.Eval(Container, "DataItem.MoneyHtml") %>
															<td nowrap align="right">
																<%# RmsPM.BLL.CostBudgetPageRule.GetContractPayHref(RmsPM.BLL.CostBudgetPageRule.GetMoneyShowString(DataBinder.Eval(Container, "DataItem.Money"), RmsPM.BLL.CostBudgetPageRule.m_MoneyUnit.yuan), DataBinder.Eval(Container, "DataItem.CostCode"), "", "") %>
															</td>
														</tr>
													</ItemTemplate>
												</asp:repeater></tbody></table>
									</div>
								</td>
							</tr>
						</table>
					</td>
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
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtSelect" type="hidden" name="txtSelect" runat="server"><input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
			<input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
			<INPUT id="txtListScrollTop" type="hidden" name="txtListScrollTop" runat="server"><INPUT id="txtListScrollLeft" type="hidden" name="txtListScrollLeft" runat="server">
			<INPUT id="txtLastSelectedRowID" type="hidden" name="txtLastSelectedRowID" runat="server">
			<INPUT id="txtLastSelectedRowClass" type="hidden" name="txtLastSelectedRowClass" runat="server"><INPUT id="Hidden3" type="hidden" name="txtListScrollLeft" runat="server">
			<input id="txtReportClick" type="hidden" name="txtReportClick" runat="server">
		</form>
	</body>
</HTML>
