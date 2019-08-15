<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostTargetFrame" CodeFile="CostTargetFrame.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>预算汇总</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/XmlCom.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
		
function winload()
{
	var objMain = document.all("frameMain");
	objMain.src = "CostTargetTree.aspx?ProjectCode=" + Form1.txtProjectCode.value;
}

var CurrUrl = window.location.href;

//刷新
function Refresh()
{
	Form1.btnSearch.click();
}

//切换到目标费用汇总表
function GotoCostTargetMain()
{
	window.location.href = "CostTargetMain.aspx?ProjectCode=" + Form1.txtProjectCode.value;
}

//新建目标费用
function AddTarget()
{
	var CostBudgetSetCode = OpenCustomDialog("SelectCostBudgetSet.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&type=NoTarget", "", 25, 20);
	if (!CostBudgetSetCode) return;
	
	OpenFullWindow("CostTargetModify.aspx?CostBudgetSetCode=" + CostBudgetSetCode, "预算表目标费用修改");
}

		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">项目管理>成本控制><asp:label runat="server" ID="lblTitle">预算汇总</asp:label></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAddTarget" onclick="AddTarget();" type="button" value="新建目标费用"
							name="btnAddTarget" runat="server" style="display:none"> <input class="button" type="button" value="目标费用汇总" name="btnGotoCostTargetMain" id="btnGotoCostTargetMain"
							runat="server" onclick="GotoCostTargetMain();">
					</td>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="note" nowrap><asp:label id="lblProjectName" Runat="server"></asp:label>目标费用（元）</TD>
								<td class="note" width="100%"></td>
							</TR>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<td valign="top" class="table">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
							<tr>
								<TD vAlign="top" align="left">
									<iframe name="frameMain" src='../Cost/LoadingPrepare.htm' frameBorder="no" width="100%"
										scrolling="no" height="100%" marginwidth="0" marginheight="0"></iframe>
								</TD>
							</tr>
						</table>
					</td>
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
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 80px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 80px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server">
		</form>
	</body>
</HTML>
