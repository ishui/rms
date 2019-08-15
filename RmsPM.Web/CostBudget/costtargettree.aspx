<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostTargetTree" CodeFile="CostTargetTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CostTargetTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	
//查看目标费用
function View(CostBudgetCode, CostBudgetSetCode)
{
    if (CostBudgetCode == "") //新建目标费用
		OpenFullWindow("CostTargetModify.aspx?CostBudgetSetCode=" + CostBudgetSetCode , "预算表目标费用修改");
	else if (CostBudgetCode.substr(0, 5) == "NULL_")
		OpenFullWindow("CostTargetInfo.aspx?CostBudgetSetCode=" + CostBudgetSetCode , "");
	else
		OpenFullWindow("CostTargetInfo.aspx?CostBudgetCode=" + CostBudgetCode + "&CostBudgetSetCode=" + CostBudgetSetCode , "");
}
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr height="100%">
					<td>
			<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%" id="tbl-container">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tbl-tree" border="0"
				width="100%">
				<thead>
					<TR class="tree-title" id="trTitle">
						<TH noWrap align="center">预算表</TH>
						<TH noWrap align="center">状态</TH>
						<TH noWrap align="center">预算费用(元)</TH>
						<TH noWrap align="center">创建人</TH>
						<TH noWrap align="center">创建日期</TH>
						<TH noWrap align="center">最后修改人</TH>
						<TH noWrap align="center">最后修改日期</TH>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
			</TABLE>
			</div>
					</td>
				</tr>
			</table>
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="CostTargetTreeData.aspx?ProjectCode=<%=Request["ProjectCode"]%>"  ;

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点



var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td><span>&nbsp;&nbsp;&nbsp;</span></td>@IndentEnd";
v0+="<td nowrap id='td@CostBudgetCode' onclick=\"SpreadNodes('@CostBudgetCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td nowrap><img src=\"../Images/@IconName\" style=\"display:@IconDisplay\"><a style=\"display=@ShowHref\" href=\"#\" id=\"@CostBudgetCode\" onclick=\"View('@CostBudgetCode', '@CostBudgetSetCode');return false;\">@CostBudgetSetName @VerName</a><span style=\"display=@ShowSpan\">@CostBudgetSetName @VerName</span></td></tr></table>";
TreeModels.push(v0);

//TreeModels.push("@VerName");
TreeModels.push("@StatusName");
TreeModels.push("<div align=right>@TotalBudgetMoney</div>");
TreeModels.push("@CreatePersonName");
TreeModels.push("@CreateDate");
TreeModels.push("@ModifyPersonName");
TreeModels.push("@ModifyDate");

//节点
function SpreadNodes(Code,LayerNumber,obj){
	if (Code.substr(0, 2) == "G_")  //预算表类型展开
	{
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfGroup&NodeId="+Code + "&Layer=" + (parseInt(LayerNumber)+1),obj,TreeModels,"CostBudgetCode");
	}
	else if (Code.substr(0, 2) == "D_")  //区域展开
	{
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfDistrict&NodeId="+Code + "&Layer=" + (parseInt(LayerNumber)+1),obj,TreeModels,"CostBudgetCode");
	}
	else
	{
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfTarget&NodeId="+Code + "&Layer=" + (parseInt(LayerNumber)+1),obj,TreeModels,"CostBudgetCode");
	}
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"CostBudgetCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"CostBudgetCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"CostBudgetCode",RowClassName,CostBudgetCode);
}

//更新子节点
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"CostBudgetCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"CostBudgetCode",RowClassName,GridClassName);

/*
//合计行
if (document.all.Tree.childNodes.length > 0)
{
	var node = document.all.Tree.childNodes[document.all.Tree.childNodes.length - 1];
	for(var i=0;i<node.childNodes.length;i++)
	{
		node.childNodes[i].className = "tree-sum";
	}
}
*/

//展开根结点
if (document.all.Tree.childNodes.length == 1)
{
	SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}

		</Script>
	</body>
</HTML>
