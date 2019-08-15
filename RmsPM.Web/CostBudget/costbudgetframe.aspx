<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetFrame" CodeFile="CostBudgetFrame.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>项目费用</title>
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
    document.all.divHintLoad.style.display = '';
	var objMain = document.all("frameMain");
	objMain.src = "CostBudgetTree.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&CostBudgetBackupCode=" + Form1.txtCostBudgetBackupCode.value + "&FullCost=<%=Request["FullCost"]%>" + "&HideBudget=<%=Request["HideBudget"]%>";
}

//即时更新
function OnlineUpdate()
{
	OpenCustomWindow("CostBudgetOnlineUpdate.aspx?ProjectCode=" + document.all.txtProjectCode.value, "CostBudgetOnlineUpdate", 420, 250);
}

//即时更新返回
function OnlineUpdateReturn(CostBudgetBackupCode)
{
	window.location.href = "CostBudgetFrame.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&CostBudgetBackupCode=" + CostBudgetBackupCode;
}

//设置
function Setup()
{
	OpenCustomWindow("CostBudgetDynamicSetup.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&ReturnFunc=SetupReturn", "CostBudgetDynamicSetup", 310, 210);
}

//设置返回
function SetupReturn()
{
	window.location.href = "CostBudgetFrame.aspx?ProjectCode=" + Form1.txtProjectCode.value;
}

//存档
function Backup()
{
	OpenCustomWindow("CostBudgetBackup.aspx?ProjectCode=" + document.all.txtProjectCode.value, "项目费用备份", 420, 250);
}

//查看存档
function LoadBackup()
{
	OpenCustomWindow("CostBudgetBackupList.aspx?ProjectCode=" + document.all.txtProjectCode.value, "项目费用备份列表", 780, 560);
}

//切换到存档
function sltBackupChange(sender)
{
	var CostBudgetBackupCode = sender.value;
	
	if (CostBudgetBackupCode == "") //即时
	{
		window.location.href = "CostBudgetFrame.aspx?CostBudgetBackupCode=" + CostBudgetBackupCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&Online=1";
	}
	else if (CostBudgetBackupCode == "more") //弹出存档列表
	{
		OpenCustomWindow("CostBudgetBackupList.aspx?ProjectCode=" + document.all.txtProjectCode.value, "项目费用备份列表", 780, 560);
	}
	else //查看存档
	{
		window.location.href = "CostBudgetFrame.aspx?CostBudgetBackupCode=" + CostBudgetBackupCode + "&ProjectCode=" + Form1.txtProjectCode.value;
	}
}

//刷新存档
function RefreshBackup()
{
	var slt = document.all.sltBackup;
	var selectedValue = slt.value;
	slt.innerHTML = "";

	var items = GetXMLResult("GetCostBudgetBackupData.aspx?SelectCostBudgetBackupCode=" + escape(document.all.sltBackup.value) + "&ProjectCode=" + document.all.txtProjectCode.value);

	for(var i=0;i<items.childNodes.length;i++)
	{
		var code = GetXMLTagData(items.childNodes(i), "CostBudgetBackupCode");
		var name = GetXMLTagData(items.childNodes(i), "Desc");

		ele = document.createElement("OPTION");
		ele.value = code;
		ele.text = name;
		slt.add(ele);
	}
	
	slt.value = selectedValue;
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">项目管理>成本控制><asp:label runat="server" ID="lblTitle">造价控制</asp:label></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"><input class="button" id="btnOnlineUpdate" onclick="OnlineUpdate();" type="button" value="刷新"
							name="btnOnlineUpdate" runat="server"> <input class="button" id="btnSetup" onclick="Setup();" type="button" value="设 置" name="btnSetup"
							runat="server"> <input class="button" id="btnBackup" onclick="Backup();" type="button" value="备份" name="btnBackup"
							runat="server"> <input style="DISPLAY:none" class="button" id="btnLoadBackup" onclick="LoadBackup();" type="button"
							value="查看存档" name="btnLoadBackup" runat="server">&nbsp;切换至：<select runat="server" id="sltBackup" name="sltBackup" class="select" onchange="sltBackupChange(this);">
						</select>
					</td>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="note" nowrap><asp:label id="lblProjectName" Runat="server"></asp:label>项目费用（元）</TD>
								<td class="note" width="100%">（<span runat="server" id="lblDynamicDateDesc"></span>）</td>
							</TR>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<td valign="top" class="table">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
							<tr>
								<TD vAlign="top" align="left">
									<iframe name="frameMain" src='' frameBorder="no" width="100%"
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
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 180px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 180px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtStatus" type="hidden" name="txtStatus" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtCostBudgetBackupCode" type="hidden" name="txtCostBudgetBackupCode" runat="server">
			<input id="txtOnline" type="hidden" name="txtOnline" runat="server">
		</form>
	</body>
</HTML>
