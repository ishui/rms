<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectYm" Src="../CostBudget/CostBudgetSelectYm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCSelectProjectMulti" Src="../UserControls/UCSelectProjectMulti.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CashFlow.RptFinIOGroup" CodeFile="RptFinIOGroup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RptFinIOGroup</title>
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
	MyLockColumn("tbList", 1);
}

function MyLockColumn(TableID, ColCount)
{
	var table = document.getElementById(TableID);
	var trs = table.getElementsByTagName("TR");
	
	for(var i=0;i<trs.length;i++)
	{
		var tr = trs.item(i);
		
		//特殊
		if ((i == 1) || (i == 2))
		    continue;
		
		for(var j=0;j<ColCount;j++)
		{
			if (tr.cells[j])
			{
				tr.cells[j].className = "locked";
			}
		}
	}
}

function DoReport()
{
    Form1.btnDoReport.click();
}

		</script>
	</HEAD>
	<body onload="winload()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<TR>
					<TD vAlign="top" align="left">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="100%">
									<select class="select" runat="server" id="sltSource" name="sltSource" onchange="DoReport();">
										<option value="Fact" selected>实际已批</option>
										<option value="FactPay">实际已付</option>
										<option value="Plan">计划基准</option>
									</select>
									<select class="select" runat="server" id="sltMonthType" name="sltMonthType" onchange="DoReport();">
										<option value="q" selected>季度</option>
										<option value="m">月度</option>
									</select>
									<uc1:costbudgetselectym id="ucCostBudgetSelectMonth" runat="server" OnClientPost="" ShowMonthVisible="False"
										ShowMonth="True" ShowButton="False"></uc1:costbudgetselectym>
									项目：<uc1:UCSelectProjectMulti id="ucSelectProjectMulti" runat="server"></uc1:UCSelectProjectMulti>
									<input class="submit" id="btnDoReport" type="button" value="统 计" runat="server"
										NAME="btnDoReport" onserverclick="btnDoReport_ServerClick">
									&nbsp;<input class="submit" id="btnPrint" onclick="Print()" type="button" value="打 印" name="btnPrint"
										runat="server">
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td id="tdMaster">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note" vAlign="bottom" width="200" height="25">集团现金流量表（<asp:label id="lblSourceName" Runat="server"></asp:label>）</td>
								<td></td>
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
										<th noWrap rowSpan="3">项目</th>
										<th noWrap colspan='<%=RmsPM.BLL.ConvertRule.ToInt(ViewState["MonthCount"]) + 1%>'>现金流入</th>
										<th noWrap colspan='<%=RmsPM.BLL.ConvertRule.ToInt(ViewState["MonthCount"]) + 1%>'>现金流出</th>
										<th noWrap colspan='<%=RmsPM.BLL.ConvertRule.ToInt(ViewState["MonthCount"]) + 1%>'>净流量</th>
									</tr>
									<tr class="list-title" align="center">
										<th noWrap rowSpan="2">合计</th>
										<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
										<th noWrap rowSpan="2">合计</th>
										<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
										<th noWrap rowSpan="2">合计</th>
										<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
									</tr>
									<tr class="list-title" align="center">
										<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
										<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
										<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
									</tr>
								</thead>
								<tbody>
									<asp:repeater id="dgList" Runat="server">
										<ItemTemplate>
											<tr onclick="SetRowSelected(this);">
												<td nowrap class="list-c"><%# DataBinder.Eval(Container.DataItem, "ProjectName")%></td>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtmlI")%>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtmlO")%>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtml")%>
											</tr>
										</ItemTemplate>
									</asp:repeater>
                                    <asp:repeater id="dgListTotal" Runat="server">
										<ItemTemplate>
											<tr onclick="SetRowSelected(this);">
												<td nowrap style="background-color: #FFE4B5;font-weight: bold;text-align: right;">总计</td>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtmlI")%>
												<%# DataBinder.Eval(Container.DataItem, "MoneyHtmlO")%>
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
		</form>
	</body>
</HTML>
