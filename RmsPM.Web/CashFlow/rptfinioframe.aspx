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

//ѡ����ݷ�Χ��ȷ��
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
								<td class="topic" noWrap background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="lblTitle" runat="server">��Ŀ����>�������>��������</span>
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
										            <option value="q" selected>����</option>
										            <option value="m">�¶�</option>
										            <option value="d">ÿ��</option>
									            </select>
									            <select class="select" id="sltSource" onchange1="DoReport();" name="sltSource" runat="server">
										            <option value="">��������������ͳ�ƣ�����</option>
										            <option value="Plan">�ƻ�</option>
										            <option value="Fact">����</option>
										            <option value="FactPay">�Ѹ�</option>
										            <option value="Plan,Fact">�ƻ�-�����Ա�</option>
										            <option value="Plan,FactPay">�ƻ�-�Ѹ��Ա�</option>
										            <option value="Fact,FactPay">����-�Ѹ��Ա�</option>
										            <option value="Plan,Fact,FactPay" selected>�ƻ�-����-�Ѹ��Ա�</option>
										            <option value="">������������ͳ�ƣ�������</option>
										            <option value="PlanI">�ƻ�����</option>
										            <option value="FactI">ʵ������</option>
										            <option value="PlanI,FactI">�ƻ�����-ʵ������Ա�</option>
										            <option value="">������������ͳ�ƣ�������</option>
										            <option value="PlanO">�ƻ�֧��</option>
										            <option value="FactO">ʵ��֧��</option>
										            <option value="PlanO,FactO">�ƻ�֧��-ʵ��֧���Ա�</option>
									            </select>
									            <select class="select" id="sltIsSum" onchange1="DoReport();" name="sltIsSum" runat="server">
										            <option value="0" selected>��ˮͼ</option>
										            <option value="1">�ۼ�ͼ</option>
									            </select>
									            <select class="select" id="sltChartType" onchange1="DoReport();" name="sltChartType" runat="server">
										            <option value="line" selected>����ͼ</option>
										            <option value="column">����ͼ</option>
									            </select>
									            <select class="select" id="sltDiscountRate" style="DISPLAY: none" onchange="DoReport();"
										            name="sltDiscountRate" runat="server">
									            </select>
									            <uc1:costbudgetselectymd id="ucCostBudgetSelectMonth" runat="server" OnClientPost="SelectMonth()" ShowMonthVisible="False"
														            ShowMonth="True" MaxYearsBetween=20 ShowButton="false"></uc1:costbudgetselectymd>
									            <input class="button-small" id="btnOk" onclick="DoReport(); return false;"
										            type="button" value="�� ʾ" name="btnOk" runat="server">
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
