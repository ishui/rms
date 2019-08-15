<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectMonth" Src="../CostBudget/CostBudgetSelectMonth.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CashFlow.RptFinIOList" CodeFile="RptFinIOList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RptFinIOList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript" src="../Images/locked-column.js"></script>
		<style>.list-t1 { FONT-WEIGHT: bold; BACKGROUND-COLOR: #f0fff0; TEXT-ALIGN: left }
	.list-highlight { BACKGROUND-COLOR: #ffff89 }
		</style>
		<script language="javascript">

var LastSelectedRow;
var LastSelectedRowClass;

//打印
function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=tdMaster$tbl-container&css=" + escape("../images/locked-column.css"), "打印");
}

//设置行选中
function SetRowSelected(sender)
{
	if (LastSelectedRow)
	{
		LastSelectedRow.className = LastSelectedRowClass;
		
		LastSelectedRowClass = "";
		LastSelectedRow = "";
	}
	
	LastSelectedRow = sender;
	LastSelectedRowClass = LastSelectedRow.className;
	LastSelectedRow.className = "list-highlight";
}

function winload()
{
//	LockColumn("tbList", 1);
}

		</script>
	</HEAD>
	<body onload="winload()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none">
				<input class="button" id="btnPrint" onclick="Print()" type="button" value="打 印" name="Print"
					runat="server">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<tr>
					<td id="tdMaster">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note" vAlign="bottom" width="200" height="25">项目现金流量表（<asp:label id="lblSourceName" Runat="server"></asp:label>）</td>
								<td><uc1:costbudgetselectmonth id="ucCostBudgetSelectMonth" runat="server" OnClientPost="" ShowMonthVisible="False"
										ShowMonth="True" MaxYearsBetween=20></uc1:costbudgetselectmonth></td>
								<td noWrap align="right">单位：元&nbsp;&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div id="tbl-container">
							<table class="tbl-list" id="tbList" width="100%">
								<thead>
									<tr class="list-title" align="center">
										<th noWrap rowSpan="2">年度<br>
											<asp:label id="lblMonthTypeName" Runat="server"></asp:label></th>
										<th noWrap rowSpan="2">合计</th>
										<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
									</tr>
									<tr class="list-title" align="center">
										<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td class="list-t1" style="TEXT-ALIGN: left; height: 23px;" colSpan='<%=RmsPM.BLL.ConvertRule.ToInt(ViewState["MonthCount"]) + 2%>'>项目现金流入</td>
									</tr>
									<asp:repeater id="dgListI" Runat="server">
										<ItemTemplate>
											<tr onclick="SetRowSelected(this);">
												<td nowrap class="list-c"><%# DataBinder.Eval(Container.DataItem, "PBSTypeNameHtml")%></td>
												<td align="right" nowrap class="sum"><%# RmsPM.BLL.CashFlowRule.FormatSalListValue(DataBinder.Eval(Container.DataItem, "TotalMoney"), "")%></td>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtml")%>
											</tr>
										</ItemTemplate>
									</asp:repeater><asp:repeater id="dgListITotal" Runat="server">
										<ItemTemplate>
											<tr onclick="SetRowSelected(this);">
												<td nowrap class="sum-item">流入</td>
												<td align="right" nowrap class="sum"><%# RmsPM.BLL.CashFlowRule.FormatSalListValue(DataBinder.Eval(Container.DataItem, "TotalMoney"), "")%></td>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtml")%>
											</tr>
										</ItemTemplate>
									</asp:repeater>
									<tr>
										<td class="list-t1" style="TEXT-ALIGN: left" colSpan='<%=RmsPM.BLL.ConvertRule.ToInt(ViewState["MonthCount"]) + 2%>'>项目现金流出</td>
									</tr>
									<asp:repeater id="dgListO" Runat="server">
										<ItemTemplate>
											<tr onclick="SetRowSelected(this);">
												<td nowrap class="list-c"><%# DataBinder.Eval(Container.DataItem, "CostNameHtml")%></td>
												<td align="right" nowrap class="sum"><%# RmsPM.BLL.CashFlowRule.FormatSalListValue(DataBinder.Eval(Container.DataItem, "TotalMoney"), "")%></td>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtml")%>
											</tr>
										</ItemTemplate>
									</asp:repeater><asp:repeater id="dgListOTotal" Runat="server">
										<ItemTemplate>
											<tr onclick="SetRowSelected(this);">
												<td nowrap class="sum-item">流出</td>
												<td align="right" nowrap class="sum"><%# RmsPM.BLL.CashFlowRule.FormatSalListValue(DataBinder.Eval(Container.DataItem, "TotalMoney"), "")%></td>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtml")%>
											</tr>
										</ItemTemplate>
									</asp:repeater><asp:repeater id="dgListTotal" Runat="server">
										<ItemTemplate>
											<tr onclick="SetRowSelected(this);">
												<td nowrap class="sum-item">净现金流</td>
												<td align="right" nowrap class="sum"><%# RmsPM.BLL.CashFlowRule.FormatSalListValue(DataBinder.Eval(Container.DataItem, "TotalMoney"), "")%></td>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtml")%>
											</tr>
										</ItemTemplate>
									</asp:repeater>
								</tbody>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 50px">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
