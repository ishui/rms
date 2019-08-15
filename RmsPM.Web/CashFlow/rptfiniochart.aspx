<%@ Register TagPrefix="uc1" TagName="RptFinIOChartLine" Src="../CashFlow/RptFinIOChartLine.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RptFinIOChartColumn" Src="../CashFlow/RptFinIOChartColumn.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CashFlow.RptFinIOChart" CodeFile="RptFinIOChart.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RptFinIOChart</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Rms.js"></script>
		<script language="javascript">

//金额链接
function OpenMoney(RowId, ColumnName, MoneyType, BeginDate, EndDate)
{
    if (RowId == "Fact") //净现金流 - 已批
    {
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&PayDateBegin=" + BeginDate + "&PayDateEnd=" + EndDate, 'ShowPaymentItemList');
	}
	else if (RowId == "FactPay") //净现金流 - 已付
    {
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&PayoutDateBegin=" + BeginDate + "&PayoutDateEnd=" + EndDate, 'ShowPayoutItemList');
	}
	else if (RowId == "FactO") //实际支出
    {
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&PayoutDateBegin=" + BeginDate + "&PayoutDateEnd=" + EndDate, 'ShowPayoutItemList');
	}
}

		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<tr height="100%">
					<td>
						<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table cellSpacing="0" cellPadding="0" border="0" width="100%">
								<tr>
									<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note" vAlign="bottom" width="200" height="25"><asp:label runat="server" ID="lblProjectName"></asp:label>现金流量分析</td>
								<td></td>
								<td noWrap align="right">单位：百万&nbsp;&nbsp;
								</td>
							</tr>
						</table>
										<table class="list" cellSpacing="0" cellPadding="0" border="0">
											<tr class="list-title" align="center">
												<td noWrap rowSpan="2"><asp:label ID="lblTitle1" runat="server">年度</asp:label><br>
													<asp:label id="lblTitle2" Runat="server"></asp:label></td>
												<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
												
												
											</tr>
											<tr class="list-title" align="center">
												<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
											</tr>
											<asp:repeater id="dgList" Runat="server">
												<ItemTemplate>
													<tr>
														<td align="center" nowrap><%# DataBinder.Eval(Container.DataItem, "html")%>&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "name")%></td>
														<%# DataBinder.Eval(Container.DataItem, "MoneyHtml")%>
													</tr>
												</ItemTemplate>
											</asp:repeater>
											<asp:repeater id="dgListPercent" Runat="server" Visible="False">
												<ItemTemplate>
													<tr>
														<td align="left" nowrap><%# DataBinder.Eval(Container.DataItem, "html")%>&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "name")%></td>
														<%# DataBinder.Eval(Container.DataItem, "PercentHtml")%>
													</tr>
												</ItemTemplate>
											</asp:repeater>
										</table>
									</td>
								</tr>
							</table>
							<uc1:RptFinIOChartLine id="ucChartLine" runat="server" Visible="False"></uc1:RptFinIOChartLine>
							<uc1:RptFinIOChartColumn id="ucChartColumn" runat="server" Visible="False"></uc1:RptFinIOChartColumn>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0" style="DISPLAY:none">
								<tr align="left">
									<td noWrap><span style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; FONT-SIZE: 1px; BORDER-LEFT: black 1px outset; WIDTH: 12px; BORDER-BOTTOM: black 1px outset; HEIGHT: 12px; BACKGROUND-COLOR: #0000ff"></span>&nbsp;&nbsp;实际发生&nbsp;&nbsp;&nbsp;&nbsp;
										<span style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; FONT-SIZE: 1px; BORDER-LEFT: black 1px outset; WIDTH: 12px; BORDER-BOTTOM: black 1px outset; HEIGHT: 12px; BACKGROUND-COLOR: red">
										</span>&nbsp;&nbsp;计划基准&nbsp;&nbsp;&nbsp;&nbsp; <span style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; FONT-SIZE: 1px; BORDER-LEFT: black 1px outset; WIDTH: 12px; BORDER-BOTTOM: black 1px outset; HEIGHT: 12px; BACKGROUND-COLOR: #76d769">
										</span>&nbsp;&nbsp;计划调整
									</td>
								</tr>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input type="hidden" id="txtType" name="txtType" runat="server">
		</form>
	</body>
</HTML>
