<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.BuildingFloorProgressInfo" CodeFile="BuildingFloorProgressInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>楼栋工程结构</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="tools-area" vAlign="top">
						形象进度：<select class="select" id="sltVisualProgress" onchange="VisualProgressChange();" name="sltVisualProgress"
							runat="server">
							<option value="" selected>----请选择----</option>
						</select>
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnGotoView1" onclick="GotoView(1)" type="button" value="分项工程图"
							name="btnGotoView1" runat="server"> <input class="button" id="btnGotoView2" onclick="GotoView(2)" type="button" value="分项图"
							name="btnGotoView2" runat="server"> <input class="button" id="btnBatchEdit" onclick="BatchModify()" type="button" value="修改"
							name="btnBatchEdit" runat="server"> <input class="button" id="btnPrint" onclick="Print()" type="button" value="打 印" name="btnPrint">
						<input class="button" id="btnDelete" onclick="if (!Delete()) return false;" type="button"
							value="重 填" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnGotoFloor" onclick="GotoFloor()" type="button" value="楼栋结构"
							name="btnGotoFloor" runat="server">
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><iframe name="frameChart" marginWidth="0" marginHeight="0" src="" frameBorder="no" width="100%"
								scrolling="no" height="100%"></iframe>
						</div>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server">
			<input id="txtDefaultVisualProgress" type="hidden" name="txtDefaultVisualProgress" runat="server"><input id="txtDefaultVisualProgressName" type="hidden" name="txtDefaultVisualProgressName"
				runat="server"> <input id="txtViewType" type="hidden" name="txtViewType" runat="server" value="1">
			<input id="txtMulti" type="hidden" name="txtMulti" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

//转到形象进度
function VisualProgressChange()
{
	var VisualProgress = Form1.sltVisualProgress.value;
	var type = Form1.txtViewType.value;
	
	if (VisualProgress == "")
	{
		document.all.divMain.style.display = "none";
		Form1.btnPrint.style.display = "none";
		
		if (Form1.btnBatchEdit) Form1.btnBatchEdit.style.display = "none";
		if (Form1.btnDelete) Form1.btnDelete.style.display = "none";
	}
	else
	{
		document.all.divMain.style.display = "";
		Form1.btnPrint.style.display = "";

		if (Form1.btnBatchEdit) Form1.btnBatchEdit.style.display = "";
		if (Form1.btnDelete) Form1.btnDelete.style.display = "";

		if (type == "2")
		{
			document.all("frameChart").src = "../ConstructProg/BuildingFloorProgressChartB.aspx?BuildingCode=" + Form1.txtBuildingCode.value + "&VisualProgress=" + escape(VisualProgress) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Multi=" + Form1.txtMulti.value;
		}
		else
		{
//			document.all("frameChart").src = "../ConstructProg/BuildingFloorProgressChart.aspx?BuildingCode=10124,10127&VisualProgress=" + escape('结构') + "&ProjectCode=" + Form1.txtProjectCode.value + "&IsMulti=1";
			document.all("frameChart").src = "../ConstructProg/BuildingFloorProgressChart.aspx?BuildingCode=" + Form1.txtBuildingCode.value + "&VisualProgress=" + escape(VisualProgress) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Multi=" + Form1.txtMulti.value;
		}
	}
	
	//更新父窗口的缺省形象进度
	window.parent.document.all.txtDefaultVisualProgressName.value = Form1.sltVisualProgress.item(Form1.sltVisualProgress.selectedIndex).innerText;
}

//打印
function Print()
{
	var VisualProgress = Form1.sltVisualProgress.value;
	var type = Form1.txtViewType.value;
	
	if (VisualProgress == "")
	{
	}
	else
	{
		if (type == "2")
		{
			OpenPrintWindow("../ConstructProg/BuildingFloorProgressChartB.aspx?BuildingCode=" + Form1.txtBuildingCode.value + "&VisualProgress=" + escape(VisualProgress) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Multi=" + Form1.txtMulti.value);
		}
		else
		{
			OpenPrintWindow("../ConstructProg/BuildingFloorProgressChart.aspx?BuildingCode=" + Form1.txtBuildingCode.value + "&VisualProgress=" + escape(VisualProgress) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Multi=" + Form1.txtMulti.value);
		}
	}
}

//切换到楼栋结构
function GotoFloor()
{
	document.all.divHintLoad.style.display='';
	window.parent.navigate("../ConstructProg/BuildingFloorFrame.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&BuildingCode=" + Form1.txtBuildingCode.value + "&DefaultVisualProgress=" + Form1.sltVisualProgress.value);
}

function winload()
{
	SetViewButton();
	VisualProgressChange();
}

//批修改进度
function BatchModify()
{
	OpenCustomWindow("../ConstructProg/BuildingFloorProgressBatchModify.aspx?BuildingCode=" + Form1.txtBuildingCode.value + "&VisualProgressCode=" + Form1.sltVisualProgress.value, "楼栋进度批修改", 500, 300);
}

//批修改进度返回
function BatchModifyReturn()
{
	document.all("frameChart").src = document.all("frameChart").src;
}

//删除
function Delete()
{
	if (!confirm("确实要删除该形象进度下的所有进度记录吗？")) return false;
	
	document.all.divHintLoad.style.display='';
	return true;
}

//切换视图
function GotoView(type)
{
	Form1.txtViewType.value = type;
	SetViewButton();
	VisualProgressChange();
}

function SetViewButton()
{
	var type = Form1.txtViewType.value;
	
	if (type == "2")
	{
		Form1.btnGotoView1.style.display = "";
		Form1.btnGotoView2.style.display = "none";
	}
	else
	{
		Form1.btnGotoView1.style.display = "none";
		Form1.btnGotoView2.style.display = "";
	}
}

//-->
		</SCRIPT>
	</body>
</HTML>
