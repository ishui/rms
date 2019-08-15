<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicCost" CodeFile="DynamicCost.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>动态费用</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">

	var currentCostCode = "";	
	var budgetCode = '<%=Request["BudgetCode"]%>';
	var IsDynamic = '<%=Request["IsDynamic"]%>';
	
	function DoBodyLoad()
	{
		var treeType = Form1.txtTreeType.value;
		var objFrame = document.all("TreeSplitTop");
		if ( treeType != "")
			objFrame.src = "../Cost/DynamicCostTree.aspx?ProjectCode=<%=Request["ProjectCode"]%>&TreeType=" + treeType + "&BudgetCode=" + budgetCode  ;
		else
			objFrame.src = "../Cost/DynamicCostTree.aspx?ProjectCode=<%=Request["ProjectCode"]%>&TreeType=DynamicCost"  + "&BudgetCode=" + budgetCode ;
	}
	
	function doSelectDynamicNode( costCode )
	{
		currentCostCode = costCode;
		OpenFullWindow( "../Cost/DynamicCostInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode=" + currentCostCode,"动态费用信息"  );
	}
	
	function doDynamicApplyList()
	{
		window.navigate('../Cost/DynamicCostApplyList.aspx?ProjectCode=<%=Request["ProjectCode"]%>');
	}
	
	function ViewBudgetHistory()
	{
		window.navigate('BudgetHistoryList.aspx?IsDynamic=1&ProjectCode=<%=Request["ProjectCode"]%>');
	}
	
	function doSearchBudgetCost()
	{
		OpenCustomWindow( '../Cost/DynamicCostModifyList.aspx?ProjectCode=<%=Request["ProjectCode"]%>','动态调整记录','900','640'  );
	}
	
/*	
	function ViewBudgetHistory()
	{
		window.navigate('BudgetHistoryList.aspx');
	}
	function doViewBudgetInfo()
	{
		OpenFullWindow( "../Cost/BudgetInfo.aspx?BudgetCode=" + budgetCode + "&CostCode=" + currentCostCode  );
	}

	function doNewBudget()
	{
		OpenSmallWindow( "../Cost/ConfirmDynamic.aspx?IsDynamic=0"   );
	}
	function BudgetModifyCheck( CostCode )
	{
		OpenMiddleWindow('../Cost/BudgetModifyCheck.aspx?budgetCode=' + budgetCode ,"预算审核");
	}
*/	
		</SCRIPT>
	</HEAD>
	<body scroll="no" onload="DoBodyLoad();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">费用管理 
									- 动态费用
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">&nbsp; <INPUT class="button" id="btnBudgetHistory" onclick="ViewBudgetHistory('');return false;"
							type="button" value="历次动态" name="btnBudgetHistory" runat="server">&nbsp;<INPUT class="button" id="btnSearchBudgetCost" onclick="doSearchBudgetCost('');return false;"
							type="button" value="调整记录" name="btnBudgetHistory" runat="server"></td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note"><asp:label id="lblBudgetName" runat="server" BorderColor="Transparent" BackColor="Transparent"></asp:label>&nbsp;&nbsp;单位（万元）&nbsp; 
									今日
									<asp:Label id="lblToDay" runat="server"></asp:Label></td>
							</tr>
						</table>
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD class="form-item">状态：</TD>
								<TD width="20%" class="note"><asp:label id="lblStatus" runat="server" BackColor="Transparent" BorderColor="Transparent"></asp:label></TD>
								<TD class="form-item" width="13%">预算周期：</TD>
								<TD width="20%"><asp:label id="lblPeriodMonth" runat="server" BackColor="Transparent" BorderColor="Transparent"></asp:label></TD>
								<td class="form-item" width="13%">后续预算：</td>
								<td width="20%"><asp:label id="lblAfterPeriod" runat="server" BackColor="Transparent" BorderColor="Transparent"></asp:label></td>
							</tr>
							<TR>
								<TD class="form-item">制 定 人：</TD>
								<TD><asp:label id="lblMakePersonName" runat="server" BackColor="Transparent" BorderColor="Transparent"></asp:label></TD>
								<TD class="form-item">审 核 人：</TD>
								<TD><asp:label id="lblCheckPersonName" runat="server" BackColor="Transparent" BorderColor="Transparent"></asp:label></TD>
								<td class="form-item">审核时间：</td>
								<td><asp:label id="lblCheckDate" runat="server" BackColor="Transparent" BorderColor="Transparent"></asp:label></td>
							</TR>
							<TR>
								<TD class="form-item">备注：</TD>
								<TD colSpan="5"><asp:label id="lblRemark" runat="server" BackColor="Transparent" BorderColor="Transparent"></asp:label></TD>
							</TR>
						</table>
						<TABLE id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD vAlign="top" align="left"><iframe id="TreeSplitTop" src="../Cost/LoadingPrepare.htm" width="100%" height="75%" frameborder="no"
										scrolling="auto"></iframe>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
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
			</table>
			</TABLE> <input type="hidden" id="txtTreeType" runat="server" NAME="txtTreeType">
		</form>
	</body>
</HTML>
