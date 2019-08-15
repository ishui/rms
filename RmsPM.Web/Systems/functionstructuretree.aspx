<%@ Page language="c#" Inherits="RmsPM.Web.Systems.FunctionStructureTree" CodeFile="FunctionStructureTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>功能结构树</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	

	function SelectFunctionStructure(FunctionStructureCode,FunctionStructureName){
		window.parent.frames.SelectFunctionStructure(FunctionStructureCode,FunctionStructureName);
	}

		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tree" border="0"
				width="100%">
				<thead>
					<TR class="tree-title" id="trTitle">
						<TD noWrap align="left" id="tdFunctionStructureCode">功能点</TD>
						<TD noWrap align="left" id="tdIsAvailable">是否有效</TD>
						<TD noWrap align="left" id="tdIsRightControlPoint">是否权限控制点</TD>
						<TD noWrap align="left" id="tdIsRoleControlPoint">是否角色控制点</TD>
						<TD noWrap align="left" id="tdIsSystemClassPoint">是否系统类别大类</TD>
						<TD noWrap align="left" id="tdDescription">说明</TD>
					</TR>
				</thead>
				<tbody id="Tree">
				</tbody>
			</TABLE>
		</form>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="FunctionStructureData.aspx?a=1"  ;

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点



var TreeModels=new Array();

var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td  id='td@FunctionStructureCode' onclick=\"SpreadNodes('@FunctionStructureCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td><a href=\"#\" id=\"@FunctionStructureCode\" onclick=\"SelectFunctionStructure('@FunctionStructureCode','@FunctionStructureName');return false;\">@FunctionStructureCode  @FunctionStructureName</a></td></tr></table>";


TreeModels.push(v0);
TreeModels.push("@IsAvailable");
TreeModels.push("@IsRightControlPoint");
TreeModels.push("@IsRoleControlPoint");
TreeModels.push("@IsSystemClass");
TreeModels.push("@Description");

//节点
function SpreadNodes(FunctionStructureCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+FunctionStructureCode,obj,TreeModels,"FunctionStructureCode");
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);
}

//更新子节点
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"FunctionStructureCode",RowClassName,GridClassName);

//展开根结点
if (document.all.Tree.childNodes.length == 1)
{
	SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, document.all.Tree.childNodes[0]);
}

		</Script>
	</body>
</HTML>
