<%@ Page language="c#" Inherits="RmsPM.Web.CashFlow.RptFinIOFrame" CodeFile="RptFinIOFrame.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectYmd" Src="../CostBudget/CostBudgetSelectYmd.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RptFinIO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Rms.js"></script>
		<script language="javascript">
		
function winload()
{
	var objMain = document.all("frameMain");
	DoReport();
}

//选择年份范围后确定
function SelectMonth()
{
	DoReport();
	return false;
}

function DoReport()
{
	var MonthType = Form1.sltMonthType.value;
	var Type = Form1.txtType.value;
	var Source = Form1.sltSource.value;
	var IsSum = Form1.sltIsSum.value;
	var ChartType = Form1.sltChartType.value;
	
	var StartY = CostBudgetSelectYmd_GetStartY("ucCostBudgetSelectMonth");
	var EndY = CostBudgetSelectYmd_GetEndY("ucCostBudgetSelectMonth");
	
	var DiscountRate = 0;
	if (Type == "1")
//	if ((Form1.sltDiscountRate) && (Form1.sltDiscountRate.style.display != "none"))
	{
		DiscountRate = Form1.sltDiscountRate.value;
	}
	
	var objMain = document.all("frameMain");
	
	if (Type == "2")
	{
		objMain.src = "RptFinIOChart.aspx?Type=" + Type + "&ProjectCode=" + Form1.txtProjectCode.value + "&MonthType=" + MonthType + "&StartY=" + StartY + "&EndY=" + EndY + "&Source=" + Source;
	}
	else
	{
		objMain.src = "RptFinIOChart.aspx?ChartType=" + ChartType + "&Type=" + Type + "&ProjectCode=" + Form1.txtProjectCode.value + "&MonthType=" + MonthType + "&StartY=" + StartY + "&EndY=" + EndY + "&Source=" + Source + "&IsSum=" + IsSum + "&DiscountRate=" + DiscountRate;
	}
	
//	document.all.tableHint.style.display = "block";
//	return true;
}

		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" onload="winload();" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff"
				border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" noWrap background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="lblTitle" runat="server">项目管理>财务管理>流量分析</span>
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="table" vAlign="top" align="left">
					    <table width="100%" height="100%" border="0">
					        <tr>
					            <td>
						            <TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							            <tr>
								            <td width="100%"><select class="select" id="sltMonthType" onchange1="DoReport();" name="sltMonthType" runat="server">
										            <option value="q" selected>季度</option>
										            <option value="m">月度</option>
										            <option value="d">每日</option>
									            </select>
									            <select class="select" id="sltSource" onchange1="DoReport();" name="sltSource" runat="server">
										            <option value="">－－－－净流量统计－－－</option>
										            <option value="Plan">计划</option>
										            <option value="Fact">已批</option>
										            <option value="FactPay">已付</option>
										            <option value="Plan,Fact">计划-已批对比</option>
										            <option value="Plan,FactPay">计划-已付对比</option>
										            <option value="Fact,FactPay">已批-已付对比</option>
										            <option value="Plan,Fact,FactPay" selected>计划-已批-已付对比</option>
										            <option value="">－－－－流入统计－－－－</option>
										            <option value="PlanI">计划收入</option>
										            <option value="FactI">实际收入</option>
										            <option value="PlanI,FactI">计划收入-实际收入对比</option>
										            <option value="">－－－－流出统计－－－－</option>
										            <option value="PlanO">计划支出</option>
										            <option value="FactO">实际支出</option>
										            <option value="PlanO,FactO">计划支出-实际支出对比</option>
									            </select>
									            <select class="select" id="sltIsSum" onchange1="DoReport();" name="sltIsSum" runat="server">
										            <option value="0" selected>流水图</option>
										            <option value="1">累计图</option>
									            </select>
									            <select class="select" id="sltChartType" onchange1="DoReport();" name="sltChartType" runat="server">
										            <option value="line" selected>线性图</option>
										            <option value="column">体量图</option>
									            </select>
									            <select class="select" id="sltDiscountRate" style="DISPLAY: none" onchange="DoReport();"
										            name="sltDiscountRate" runat="server">
									            </select>
									            <uc1:costbudgetselectymd id="ucCostBudgetSelectMonth" runat="server" OnClientPost="SelectMonth()" ShowMonthVisible="False"
														            ShowMonth="True" MaxYearsBetween=20 ShowButton="false"></uc1:costbudgetselectymd>
									            <input class="button-small" id="btnOk" onclick="DoReport(); return false;"
										            type="button" value="显 示" name="btnOk" runat="server">
								            </td>
							            </tr>
						            </TABLE>
					            </td>
					        </tr>
					        <tr height="100%">
					            <td>
						            <table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							            <tr>
								            <TD vAlign="top" align="left"><iframe name="frameMain" marginWidth="0" marginHeight="0" src="../Cost/LoadingPrepare.htm"
										            frameBorder="no" width="100%" scrolling="no" height="100%"></iframe>
								            </TD>
							            </tr>
						            </table>
					            </td>
					        </tr>
					    </table>
					</TD>
				</TR>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</TABLE>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtProjectName" type="hidden" name="txtProjectName" runat="server">
			<input id="txtType" type="hidden" name="txtType" runat="server">
		</form>
	</body>
</HTML>
