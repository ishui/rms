<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.ProjectProgressChartInfraMain" CodeFile="ProjectProgressChartInfraMain.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>项目进度</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XMLTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/JoyBox.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<style>chart { FONT-SIZE: 12px; FONT-FAMILY: "Tahoma","宋体" }
		</style>
		<script language="javascript">

		</script>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no" onload="winload()">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="0">
							<tr>
								<td class="intopic" width="200"><span id="spanGanttTypeName"></span></td>
								<td><input type="button" id="btnPrint" name="btnPrint" value="打印" class="button-small" onclick="Print()">
								    &nbsp;&nbsp;<a href="#" onclick="ShowMenuLevel();return false;">显示</a>
								    &nbsp;&nbsp;<a href="#" onclick="ShowMenuView();return false;">切换视图</a>
									&nbsp;&nbsp;<input style="display:none" type="button" id="btnGotoGanttA" name="btnGotoGanttA" class="button-small" value="甘特图" onclick="GotoGantt('A');"><input style="display:none" type="button" id="btnGotoGantt" name="btnGotoGantt" class="button-small" value="对比图" onclick="GotoGantt('');">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<table style="BORDER-RIGHT: #a3afb8 1px solid; PADDING-RIGHT: 9px; BORDER-TOP: #a3afb8 1px solid; PADDING-LEFT: 9px; PADDING-BOTTOM: 9px; BORDER-LEFT: #a3afb8 1px solid; PADDING-TOP: 9px; BORDER-BOTTOM: #a3afb8 1px solid"
							height="100%" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td>
									<table height="100%" width="100%">
										<tr>
											<td id="tdMain">
												<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
													<table id="tbList" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<tr>
															<td style="padding-left:40px">
																<iframe name="frameGantt" src="" frameBorder="no" width="100%" scrolling="no" height="100%"
																	marginwidth="0" marginheight="0"></iframe>
															</td>
														</tr>
													</table>
													<table cellSpacing="0" cellPadding="0" width="100%" border="0" id="tbLegend" style="display:none">
														<tr align="center">
															<td noWrap><span style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; FONT-SIZE: 1px; BORDER-LEFT: black 1px outset; WIDTH: 50px; BORDER-BOTTOM: black 1px outset; HEIGHT: 5px; BACKGROUND-COLOR: #76d769"></span>&nbsp;&nbsp;计划&nbsp;&nbsp;&nbsp;&nbsp;
																<span style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; FONT-SIZE: 1px; BORDER-LEFT: black 1px outset; WIDTH: 50px; BORDER-BOTTOM: black 1px outset; HEIGHT: 5px; BACKGROUND-COLOR: red">
																</span>&nbsp;&nbsp;实际
															</td>
														</tr>
													</table>
													<table cellSpacing="0" cellPadding="0" width="100%" border="0" id="tbLegendB" style="display:none">
														<tr align="center">
															<td noWrap><span style="BORDER-RIGHT: brown 1px outset; BORDER-TOP: brown 1px outset; FONT-SIZE: 1px; BORDER-LEFT: brown 1px outset; WIDTH: 50px; BORDER-BOTTOM: brown 1px outset; HEIGHT: 8px; BACKGROUND-COLOR: #ffffff"></span>&nbsp;&nbsp;计划&nbsp;&nbsp;&nbsp;&nbsp;
																<span style="BORDER-RIGHT: black 0px outset; BORDER-TOP: black 1px outset; FONT-SIZE: 1px; BORDER-LEFT: black 1px outset; WIDTH: 30px; BORDER-BOTTOM: black 1px outset; HEIGHT: 8px; BACKGROUND-COLOR: #008080">
																</span><span style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; FONT-SIZE: 1px; BORDER-LEFT: black 0px outset; WIDTH: 20px; BORDER-BOTTOM: black 1px outset; HEIGHT: 8px; BACKGROUND-COLOR: #A6D2D2">
																</span>&nbsp;&nbsp;实际
															</td>
														</tr>
													</table>
													<div id="MyLabelY" style="LEFT: 0px; WIDTH: 195px; POSITION: absolute; TOP: 0px">
														<table id="pnLabelY" style="MARGIN-TOP: 15px; MARGIN-LEFT: 0px" height="1" cellSpacing="0"
															cellPadding="0" width="100%" border="0">
															<TBODY id="Tree" align="top">
															</TBODY>
														</table>
													</div>
												</div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<div id="myjoybox" style="DISPLAY: none; LEFT: 10px; POSITION: absolute; TOP: 200px; HEIGHT: 80px"
							myDiv="" myOffsetBottom="0" myOffsetRight="40" myOffsetTop="0px">
							<table id="joyboxTable" style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; BORDER-LEFT: black 1px outset; BORDER-BOTTOM: black 1px outset"
								height="80" cellSpacing="0" cellPadding="0" width="180">
								<tr>
									<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" noWrap bgColor="#ffffcc"><label id="linktitle"></label></td>
								</tr>
							</table>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server">
			<input id="txtChartWBSCode" type="hidden" name="txtChartWBSCode" runat="server"><input id="txtChartCompletePercent" type="hidden" name="txtChartCompletePercent" runat="server">
			<input id="txtChartStatusName" type="hidden" name="txtChartStatusName" runat="server">
			<input id="txtChartRowHeight" type="hidden" name="txtChartRowHeight" runat="server" value="26"><input id="txtChartDataHeight" type="hidden" name="txtChartDataHeight" runat="server">
			<input id="txtChartTopHeight" type="hidden" name="txtChartTopHeight" runat="server" value="15"><input id="txtChartBottomHeight" type="hidden" name="txtChartBottomHeight" runat="server"
				value="85"> <input id="txtChartHeight" type="hidden" name="txtChartHeight" runat="server">
			<input id="txtGanttType" type="hidden" name="txtGanttType" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

function winload()
{
	IniGotoGantt();
}

function IniGotoGantt()
{
	var GanttType = Form1.txtGanttType.value;
	
//	Form1.btnGotoGantt.style.display = "none";
//	Form1.btnGotoGanttA.style.display = "none";

	document.all("tbLegend").style.display = "none";
	document.all("tbLegendB").style.display = "none";
	
	if (GanttType == "A")
	{
		document.all("spanGanttTypeName").innerText = "甘特图";
//		Form1.btnGotoGantt.style.display = "";
	}
	else if (GanttType == "B")
	{
		document.all("spanGanttTypeName").innerText = "跟踪甘特图";
		document.all("tbLegendB").style.display = "";
	}
	else
	{
		document.all("spanGanttTypeName").innerText = "对比图";
//		Form1.btnGotoGanttA.style.display = "";
		document.all("tbLegend").style.display = "";
	}
	
}

//切换视图
function GotoGantt(GanttType)
{
	Form1.txtGanttType.value = GanttType;
	ShowGantt("");
	IniGotoGantt();
}

//打印
function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=divMain", "打印");
}

//弹出菜单选择级别
function ShowMenuLevel()
{
	var cssFile="../Images/ContentMenu.css";

	var Items = new Array();

	Items[0] = new Array(3);
	Items[0][0] = "所有子项";
	Items[0][1] = "";
	Items[0][2] = "LevelChange(-1);";

	for(var i=1;i<=9;i++)
	{
		Items[i] = new Array(3);
		Items[i][0] = i + "级";
		Items[i][1] = "";
		Items[i][2] = "LevelChange(" + i + ");";
	}
	
	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

function GetMenuCheck(MenuType, CurrentType)
{
    if (MenuType == CurrentType)
        return "../images/icon_yes.gif";
    else
        return "";
}

//弹出菜单选择视图
function ShowMenuView()
{
	var cssFile="../Images/ContentMenu.css";
	var CurrentType = Form1.txtGanttType.value;
	var MenuType;

	var Items = new Array();
	var i = -1;

    i++;
    MenuType = "B";
	Items[i] = new Array(3);
	Items[i][0] = "跟踪甘特图";
	Items[i][1] = GetMenuCheck(MenuType, CurrentType);
	Items[i][2] = "GotoGantt('" + MenuType + "');";

    i++;
    MenuType = "A";
	Items[i] = new Array(3);
	Items[i][0] = "甘特图";
	Items[i][1] = GetMenuCheck(MenuType, CurrentType);
	Items[i][2] = "GotoGantt('" + MenuType + "');";

    i++;
    MenuType = "";
	Items[i] = new Array(3);
	Items[i][0] = "对比图";
	Items[i][1] = GetMenuCheck(MenuType, CurrentType);
	Items[i][2] = "GotoGantt('" + MenuType + "');";

	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

function ShowWBS(WBSCode)
{
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}

function GotoTask(WBSCode)
{
	window.parent.GotoTask(WBSCode);
}

function GetValueByChart(id, name)
{
	var val = "";
	var vals = document.all(name).value;
	var arr = vals.split(";");
	
	if (arr.length > id) val = arr[id];
	
	return val;
}

function ShowGantt(CollapseWBSCode)
{
	var GanttType = Form1.txtGanttType.value;
	var objFrame = document.all("frameGantt");
	
	if (GanttType == "A")
	{
		objFrame.src = "../ConstructProg/ProjectProgressChartInfraGanttA.aspx?WBSCode=" + Form1.txtWBSCode.value +"&CollapseWBSCode=" + CollapseWBSCode;
	}
	else if (GanttType == "B")
	{
		objFrame.src = "../ConstructProg/ProjectProgressChartInfraGanttB.aspx?WBSCode=" + Form1.txtWBSCode.value +"&CollapseWBSCode=" + CollapseWBSCode;
	}
	else
	{
		objFrame.src = "../ConstructProg/ProjectProgressChartInfraGantt.aspx?WBSCode=" + Form1.txtWBSCode.value +"&CollapseWBSCode=" + CollapseWBSCode;
	}
	
//	objFrame.height = Form1.txtChartHeight.value;
}

var TreeObj=document.all("Tree");
var RowClassName="";
var GridClassName="";

var DataSourceUrl="ProjectProgressChartData.aspx?TreeType=";

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点

var TreeModels=new Array();
var v0 ="<table name='tbTree' id='tbTree' cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td nowrap onclick=\"SpreadNodes('@WBSCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a style=\"display:@HrefDisplay\" href=\"#\" onclick=\"GotoTask(@WBSCode);\" hint=\"@TaskHint\" onMouseOver=\"init(myjoybox, joyboxTable, linktitle, hint.replace(/%br%/ig, '<br>'));\" onMouseOut=\"mouseend();\">@TaskDesc</a>";
v0+="<span style=\"display:@NoHrefDisplay\" hint=\"@TaskHint\" onMouseOver=\"init(myjoybox, joyboxTable, linktitle, hint.replace(/%br%/ig, '<br>'));\" onMouseOut=\"mouseend();\">@TaskDesc</span>";
v0+="</td></tr></table>";
TreeModels.push(v0);

function SpreadNodes(Code,LayerNumber,obj)
{
	ShowChildNode(DataSourceUrl + "&GetType=ChildNodes&NodeId="+Code + "&Layer=" + LayerNumber,obj,TreeModels,"WBSCode");

	//新的结点状态
	var NodeStatus = GetNodeStatus(obj);
	var CollapseWBSCode = (NodeStatus=="Closed")?Code:"";
	
	HintLoadStart();
	ShowGantt(CollapseWBSCode);
}

//载入开始
function HintLoadStart()
{
	document.all.divMain.style.display = "none";
	document.all.divHintLoad.style.display = "";
}

//载入结束
function HintLoadEnd()
{
	document.all.divHintLoad.style.display = "none";
	document.all.divMain.style.display = "";
}

//自动展开到指定级别
function LevelChange(level)
{
	HintLoadStart();
	
	window.setTimeout("AutoLevelChange('" + level + "');", 1);

/*	
	if (level < 0)
	{
		//展开全部
		MyExpandToLayer(TreeObj, 999, TreeModels, "WBSCode");
	}
	else
	{
		//折叠到指定级别
		MyCollapseToLayer(TreeObj, level);

		//展开到指定级别
		MyExpandToLayer(TreeObj, level, TreeModels, "WBSCode");
	}
	*/
}

function AutoLevelChange(level)
{
	var CollapseWBSCode = "";
	var isExpand = false;
	
	if (level < 0)
	{
		//展开全部
		isExpand = MyExpandToLayer(TreeObj, 999, TreeModels, "WBSCode");
	}
	else
	{
		//折叠到指定级别
		CollapseWBSCode = MyCollapseToLayer(TreeObj, level);

		//展开到指定级别
		isExpand = MyExpandToLayer(TreeObj, level, TreeModels, "WBSCode");
	}
	
	if ((isExpand) || (CollapseWBSCode != ""))
		ShowGantt(CollapseWBSCode);
	else
		HintLoadEnd();
}

//折叠到指定级别，返回折叠的结点编号
function MyCollapseToLayer(TreeObj, Layer)
{
	if (TreeObj.childNodes.length <= 0) return "";
	if (Layer < 0) return "";

	var CollapseWBSCode = "";
	var node = TreeObj.childNodes[0];

	while(node)
	{
		if ((node.NodeLayer - 1) == Layer)
		{
			if (CollapseNode(node))
			{
				if (CollapseWBSCode != "") CollapseWBSCode += ",";
				CollapseWBSCode += node.NodeId;
			}
		}
		
		var node = node.nextSibling;
	}
	
	return CollapseWBSCode;
}

//展开到指定级别
function MyExpandToLayer(TreeObj, Layer, Models, keyField)
{
	if (TreeObj.childNodes.length <= 0) return false;	

	var isExpand = false;
	var node = TreeObj.childNodes[0];

	while(node)
	{
		if ((node.NodeLayer - 1) < Layer)
		{
			if (ExpandNode(DataSourceUrl + "&GetType=ChildNodes&NodeId="+ node.NodeId + "&Layer=" + (node.NodeLayer - 1), node, Models, keyField))
			{
				isExpand = true;
			}
		}
		
		node = node.nextSibling;
	}

	return isExpand;
}

HintLoadStart();

var rootCode = Form1.txtWBSCode.value;
GetChildNodes(DataSourceUrl + "&GetType=ChildNodes&NodeId=" + rootCode + "&Layer=0",null,TreeModels,"WBSCode",RowClassName,GridClassName);

ShowGantt("");

//-->

		</SCRIPT>
	</body>
</HTML>
