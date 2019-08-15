<%@ Page language="c#" Inherits="RmsPM.Web.Project.CBS" CodeFile="CBS.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CBS</title>
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
	function InsertCBS(){
			OpenLargeWindow("CBSModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&Action=AddChild&CostCode=");
	}
	
	// 选择一个费用项,显示明细信息
	function SelectCBS(CostCode,CostName)
	{
		currentCostCode = CostCode;
		OpenLargeWindow("CBSModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&Action=Modify&CostCode="+currentCostCode);
	}


	function CBSTemplateInput()
	{
//		if ( !confirm("导入费用项会覆盖当前的费用结构，是否继续导入 ？"))
//			return;
			
		OpenSmallWindow("CBSTempletOut.aspx?ProjectCode=<%=Request["ProjectCode"]%>&Act=CBS","");
		
	}

	function CBSTemplateOutput()
	{
		OpenSmallWindow("CBSTempletIn.aspx?ProjectCode=<%=Request["ProjectCode"]%>&Act=CBS","");
	}
	
	function DoBodyLoad()
	{
		var objFrame = document.all("TreeSplitTop");
		objFrame.src = "CBSTree.aspx?ProjectCode=<%=Request["ProjectCode"]%>";
	}
	
		</SCRIPT>
	</HEAD>
	<body scroll="no" onload="DoBodyLoad();" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" background="../images/topic_bg.gif">
							<tr>
								<td class="topic" background="../images/topic_bg.gif" width="100%"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									项目管理>成本控制>项目编排
								</td>
								<td><IMG height="25" src="../images/topic_corr.gif"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAddChild" onclick="InsertCBS(); return false;" type="button"
										value="新增费用项" name="btnCancel" runat="server">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnInput" onclick="CBSTemplateInput('');return false;" type="button"
							value="导入费用项" name="btnInput" runat="server"> <input class="button" id="btnOutput" onclick="CBSTemplateOutput('');return false;" type="button"
							value="导出费用项" name="btnInput" runat="server">
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD vAlign="top" align="left"><iframe id="TreeSplitTop" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%"
										scrolling="auto" height="100%"></iframe>
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
