<%@ Page language="c#" Inherits="RmsPM.Web.Project.CBSTree" CodeFile="CBSTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CBSTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/style.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	

	function SelectCBS(CostCode,CostName){
		window.parent.frames.SelectCBS(CostCode,CostName);
	}
	
	function ShowSumMoney( EstimateCost)
	{
		document.all("tdFootTotalMoney").innerText = EstimateCost;

	}

	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
		<div id="tbl-container" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tbl-tree" border="0"
				width="100%">
				<thead>
					<TR class="tree-title" id="trTitle">
						<th noWrap align="left" id="tdCostName">费用项目</th>
						<th noWrap align="left" id="tdSubjectName">科目名称</th>
						<th noWrap align="left">类型</th>
						<th noWrap align="left" id="tdCostAllocationDescription">费用分解说明</th>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
				<tfoot style="display:none">
					<TR class="sum">
						<TD noWrap id="tdFootCostName" class="sum-item"></TD>
						<TD noWrap id="tdFootSubjectName">&nbsp;</TD>
						<TD noWrap align="left" id="tdFootCostAllocationDescription">&nbsp;</TD>
					</TR>
				</tfoot>
			</TABLE>
			</div>
		</form>
		
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="CBSData.aspx?ProjectCode=<%=Request["ProjectCode"]%>"  ;

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点



var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td  id='td@CostCode' onclick=\"SpreadNodes('@CostCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a  style=\"display=@ShowHref\" href=\"#\" id=\"@CostCode\" onclick=\"SelectCBS('@CostCode','@CostName');return false;\">@CostName</a><span style=\"display=@ShowSpan\">@SortID  @CostName</span></td></tr></table>";

TreeModels.push(v0);

//科目编号和名称
TreeModels.push("@SubjectCode @SubjectName ");

TreeModels.push("@BudgetTName");
TreeModels.push("@CostAllocationDescription");


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

//展开根结点
if (document.all.Tree.childNodes.length == 1)
{
	SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}

		</Script>
	</body>
</HTML>
