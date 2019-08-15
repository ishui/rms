<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicCostTree" CodeFile="DynamicCostTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>动态费用</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">

	var budgetCode = '<%=Request["BudgetCode"]%>';

	function doSelectDynamicNode(CostCode){
		window.parent.frames.doSelectDynamicNode(CostCode);
	}

/*	
	function ViewDynamicCostGraph(CostCode){
		OpenMiddleWindow("../Cost/CostContrast.aspx?CostCode=" + CostCode + "&BudgetCode=" + budgetCode ,"动态曲线");
	}

*/	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE class="tree" id="Table3" cellSpacing="0" cellPadding="0" rules="rows" width="100%"
				border="0">
				<thead>
					<TR class="tree-title" id="trTitle">
						<TD id="tdCostName" style="DISPLAY: none" noWrap align="left">费用项目</TD>
						<TD id="tdDynamicCost" style="DISPLAY: none" noWrap align="right">动态费用</TD>
						<TD id="tdCurrentPlanCost" style="DISPLAY: none" noWrap align="right">本期动态</TD>
						<TD id="tdCurrentMonthBudget" style="DISPLAY: none" noWrap align="right">本月动态</TD>
						<TD id="tdCurrentMonthAH" style="DISPLAY: none" noWrap align="right">本月已发生</TD>
						<TD id="tdCurrentMonthContract" style="DISPLAY: none" noWrap align="right">本月合同占用</TD>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
				<tfoot>
					<TR class="sum">
						<TD id="tdFootCostName" style="DISPLAY: none" noWrap align="left">&nbsp;&nbsp;</TD>
						<TD id="tdFootDynamicCost" style="DISPLAY: none" noWrap align="right" runat="server">&nbsp;</TD>
						<TD id="tdFootCurrentPlanCost" style="DISPLAY: none" noWrap align="right" runat="server">&nbsp;</TD>
						<TD id="tdFootCurrentMonthBudget" style="DISPLAY: none" noWrap align="right" runat="server">&nbsp;</TD>
						<TD id="tdFootCurrentMonthAH" style="DISPLAY: none" noWrap align="right" runat="server">&nbsp;</TD>
						<TD id="tdFootCurrentMonthContract" style="DISPLAY: none" noWrap align="right" runat="server">&nbsp;</TD>
					</TR>
				</tfoot>
			</TABLE>
			<input id="txtShowItems" type="hidden" name="txtShowItems" runat="server"><INPUT id="txtTreeType" type="hidden" name="txtTreeType" runat="server"><INPUT id="txtCheckBalance" type="hidden" name="txtCheckBalance" runat="server">
		</form>
		<SCRIPT language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var CheckBalance = Form1.txtCheckBalance.value;
var TreeType = Form1.txtTreeType.value; 
var DataSourceUrl="DynamicCostData.aspx?ProjectCode=<%=Request["ProjectCode"]%>&TreeType=" + TreeType + "&CheckBalance=" + CheckBalance + "&BudgetCode=" + budgetCode  ;


// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点


var showItemString = Form1.txtShowItems.value;
var SItems = showItemString.split(",")

var iCount = SItems.length;
for ( var i= 0; i<iCount ; i++)
{
	var obj = document.all("td" + SItems[i]  );
	var objFoot = document.all("tdFoot" + SItems[i]  );
	if ( obj != null && objFoot != null )
	{
		if ( HasString (showItemString,SItems[i]  ) )
		{
			obj.style.display = "";
			objFoot.style.display = "";
		}
		else
		{
			obj.style.display = "none";
			objFoot.style.display = "none";
		}
		
	}
	
}

var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td onclick=\"SpreadNodes('@CostCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a  style=\"display=@ShowHref\" href=\"#\" id=\"@CostCode\" onclick=\"doSelectDynamicNode('@CostCode');return false;\">@SortID  @CostName</a><span style=\"display=@ShowSpan\">@SortID  @CostName</span></td></tr></table>";


//第一列，费用项
if ( HasString(showItemString,"CostName" ))
	TreeModels.push(v0);

//动态费用
if ( HasString(showItemString,"DynamicCost" ))
	TreeModels.push("<div align=right>@DynamicCost</div>");

//今年预算
if ( HasString(showItemString,"CurrentPlanCost" ))
	TreeModels.push("<div align=right>@CurrentPlanCost</div>");

//本月预算
if ( HasString(showItemString,"CurrentMonthBudget" ))
	TreeModels.push("<div align=right>@CurrentMonthBudget</div>");

//本月已经发生
if ( HasString(showItemString,"CurrentMonthAH" ))
	TreeModels.push("<div align=right>@CurrentMonthAH</div>");
	
//本月合同应付
if ( HasString(showItemString,"CurrentMonthContract" ))
	TreeModels.push("<div align=right>@CurrentMonthContract</div>");


// -----------------------------------
// 树通用，不变
// -----------------------------------

//节点
function SpreadNodes(CostCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+CostCode,obj,TreeModels,"CostCode");
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"CostCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"CostCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"CostCode",RowClassName,GridClassName);
}

//更新子节点
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"CostCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"CostCode",RowClassName,GridClassName);

		</SCRIPT>
	</body>
</HTML>
