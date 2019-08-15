<%@ Page language="c#" Inherits="RmsPM.Web.Cost.CostEstimate" CodeFile="CostEstimate.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>费用估算</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	
		// 当前选中的费用项编号
	var currentCostCode = "";
	
	function SelectCBS(CostCode,CostName){
		currentCostCode = CostCode;
		OpenFullWindow( '../Cost/BatchCostEstimate.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode='+currentCostCode,'费用估算');
	}
/*
	function CostEstimateCheck()
	{
		OpenSmallWindow("CostEstimateCheckInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>","费用估算审核");
	}
*/
	function DoBodyLoad()
	{
		var objFrame = document.all("TreeSplitTop");
		objFrame.src = "../Cost/CostEstimateTree.aspx?ProjectCode=<%=Request["ProjectCode"]%>";
	}
	
	
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
									-&nbsp;费用估算&nbsp;&nbsp;
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">&nbsp; <input id="btnCheck" class="button" onclick="if (!confirm('是否确定通过审核 ？')) return false;" type="button"
							value="估算审核" name="btnCheck" runat="server" onserverclick="btnCheck_ServerClick"></td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note"><asp:label id="lblProjectName" runat="server" BackColor="Transparent" BorderColor="Transparent"></asp:label>&nbsp;&nbsp;&nbsp;单位（万元）</td>
							</tr>
						</table>
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD class="form-item" width="20%">审核人：</TD>
								<TD width="30%"><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item" width="20%">审核时间：</TD>
								<TD width="30%"><asp:label id="lblCheckDate" runat="server"></asp:label></TD>
							</tr>
						</table>
						<TABLE id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD vAlign="top" align="left"><iframe id="TreeSplitTop" src="../Cost/LoadingPrepare.htm" width="100%" height="89%" frameborder="no"
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
		</form>
	</body>
</HTML>
