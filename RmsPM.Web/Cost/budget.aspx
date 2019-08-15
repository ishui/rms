<%@ Page language="c#" Inherits="RmsPM.Web.Cost.Budget" CodeFile="Budget.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>费用预算</title>
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


	
	function doSelectBudgetNode( costCode )
	{
		
		if ( budgetCode == "" )
		{
			alert('请先制定新预算');
			return;
		}
		
		currentCostCode = costCode;
		OpenFullWindow( "../Cost/BudgetInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode=" + budgetCode + "&CostCode=" + currentCostCode  );
		
		/*
		document.all("btnBudgetInfo").style.display = "";
		document.all("btnBudgetModify").style.display = "";
		*/
	}
	
	function ViewBudgetHistory()
	{
		window.navigate('BudgetHistoryList.aspx?IsDynamic=0&ProjectCode=<%=Request["ProjectCode"]%>');
	}
	

	function gotoBudget( budgetCode )
	{
		window.navigate( '../Cost/Budget.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode='+budgetCode );
	}


	function BudgetModifyCheck( CostCode )
	{
		OpenMiddleWindow('../Cost/BudgetModifyCheck.aspx?ProjectCode=<%=Request["ProjectCode"]%>&budgetCode=' + budgetCode ,"预算审核");
	}
	
	function DoBodyLoad()
	{
		var treeType = Form1.txtTreeType.value;
		var objFrame = document.all("TreeSplitTop");
		if ( treeType != "")
			objFrame.src = "../Cost/BudgetTree.aspx?ProjectCode=<%=Request["ProjectCode"]%>&TreeType=" + treeType + "&BudgetCode=" + budgetCode  ;
		else
			objFrame.src = "../Cost/BudgetTree.aspx?ProjectCode=<%=Request["ProjectCode"]%>&TreeType=Budget"  + "&BudgetCode=" + budgetCode ;
	}
	
	function doNewBudget()
	{
		OpenMiddleWindow( "../Cost/SelectBudgetYear.aspx?ProjectCode=<%=Request["ProjectCode"]%>"   );
	}
	
/*
	function doViewBudgetInfo()
	{
		OpenFullWindow( "../Cost/BudgetInfo.aspx?BudgetCode=" + budgetCode + "&CostCode=" + currentCostCode  );
	}
	
	function doModifyBudget()
	{
		if ( budgetCode == '' )
		{
			alert("当前没有预算，请先新建一个预算 ！");
			OpenSmallWindow( "../Cost/SelectBudgetYear.aspx"   );
		}
		else
			OpenFullWindow( "../Cost/BudgetModify.aspx?BudgetCode=" + budgetCode + "&CostCode=" + currentCostCode  );
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
									-&nbsp;费用预算&nbsp;&nbsp;
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><input class="button" id="btnBudgetCheck" onclick="BudgetModifyCheck('');return false;"
							type="button" value="预算审核" name="btnBudgetCheck" runat="server">&nbsp;<input class="button" id="btnNewBudget" onclick="doNewBudget('');return false;" type="button"
							value="新建预算" name="btnNewBudget" runat="server">&nbsp; <input class="button" id="btnNewestBudget" type="button" value="最新预算" name="btnNewBudget"
							runat="server"> <INPUT class="button" id="btnBudgetHistory" onclick="ViewBudgetHistory('');return false;"
							type="button" value="历次预算" name="btnBudgetHistory" runat="server"></td>
				</tr>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note"><asp:label id="lblBudgetName" runat="server" BackColor="Transparent" BorderColor="Transparent"></asp:label>&nbsp;&nbsp;&nbsp;单位（万元）</td>
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
								<TD vAlign="top"><iframe id="TreeSplitTop" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%"
										scrolling="auto" height="78%"></iframe>
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
			</TABLE><input id="txtTreeType" type="hidden" name="txtTreeType" runat="server">
		</form>
	</body>
</HTML>
