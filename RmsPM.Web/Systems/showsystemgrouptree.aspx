<%@ Page language="c#" Inherits="RmsPM.Web.Systems.ShowSystemGroupTree" CodeFile="ShowSystemGroupTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>

		<style>.LeftMenuItem { FONT-SIZE: 12px; MARGIN: 1px; COLOR: #00309c }
	.LeftMenuItemOnMouseOver { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #fffbff }
	.LeftMenuItemOnActivty { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #ffe794 }

		</style>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr height="100%">
					<td class="table" vAlign="top" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<TABLE id="Table3" borderColor="#e7e7e7" class="" cellSpacing="0" cellPadding="3" rules="rows"
								width="100%" border="0">
								<thead>
									<TR class="tree-title" style="display:none">
										<TD noWrap align="left">类别名称</TD>
									</TR>
								</thead>
								<tbody id="Tree">
								</tbody>
								<tfoot>
									<TR class="tree-title" style="DISPLAY:none">
										<TD noWrap align="center">&nbsp;</TD>
									</TR>
								</tfoot>
							</TABLE>
						</div>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server" value="0">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input id="txtClassCode" type="hidden" name="txtClassCode" runat="server">
			<input id="txtRootGroupName" type="hidden" name="txtRootGroupName" runat="server">
			<input id="txtRootGroupCode" type="hidden" name="txtRootGroupCode" runat="server">
		</form>
		<SCRIPT language="javascript">
	var clicktd;
	var clickNodeID;
	var clickNode;
	var CurrCode;
	var CurrItemType;
	var srcNode;
	var srcNodeID = "";
	
	function TDClick(obj, NodeID) {
		clickNodeID = NodeID;
		
		if (clicktd != undefined) {
			clicktd.className='';
		}
		obj.className='LeftMenuItemOnMouseOver';
		clicktd = obj;
		
		clickNode = FindNode(document.all.Tree, clickNodeID);
	}
	
	function ShowInfo(code, ItemType)
	{
		CurrCode = code;
		CurrItemType = ItemType;
		
		var newcode = code;
		
		if (ItemType == "C")
		{
			newcode = "";
		}
		
		window.parent.<%=ViewState["MainFunc"]%>(newcode);
	}	
	
		</SCRIPT>
		<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="TreeViewItemTd";
var GridClassName="TreeViewItemTd";

var DataSourceUrl="../Systems/SystemGroupData.aspx?1=1";

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@GroupCode','@Layer','@ItemType',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
//TreeModels[0]+="<td onclick=\"SpreadNodes('@GroupCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/FolderClose.gif\">|<img src=\"../Images/FolderOpen.gif\">|<img src=\"../Images/FileIcon.gif\">@NodeSymbolEnd</td>";

//是否显示排序号
var ShowSortID = '<%=Request["ShowSortID"]%>';
if (ShowSortID == "1") //排序号 + 类别名
	TreeModels[0]+="<td style=\"CURSOR: hand\" id=\"td@GroupCode\" onclick=\"javascript:TDClick(this, '@GroupCode');ShowInfo('@GroupCode', '@ItemType');\">@SortID @GroupName</td></tr></table>";
else //类别名
	TreeModels[0]+="<td style=\"CURSOR: hand\" id=\"td@GroupCode\" onclick=\"javascript:TDClick(this, '@GroupCode');ShowInfo('@GroupCode', '@ItemType');\">@GroupName</td></tr></table>";

//TreeModels[0]+="<td><img src=\"../Images/@IconName\"><a href=\"#\" id=\"@GroupCode\" onclick=\"TDClick(this, '@GroupCode');return false;\">@BuildingName</a></td></tr></table>";
//TreeModels[1]="<a href=\"#\" id=\"@GroupCode\" onclick=\"InsertChildPBSBuild('@GroupCode','Building');return false;\">新增楼栋</a>";

//节点
function SpreadNodes(code,layer,ItemType, obj){
	if (ItemType == "C")
	{
		//大类
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodesOfClass&NodeId=&ClassCode=" + code + "&NodeLayer=" + layer,obj,TreeModels,"GroupCode");
	}
	else
	{
		//类别
		ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+code + "&NodeLayer=" + layer,obj,TreeModels,"GroupCode");
	}
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"GroupCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"GroupCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId, ItemType){
	var node = FindNode(document.all.Tree, NodeId);
	var layer = node.NodeLayer;
	if(layer) layer = parseInt(layer)-1;

	if (ItemType == "C")
	{
		//大类
		RefreshNode(DataSourceUrl+"&GetType=SingleNodeOfClass&NodeId="+NodeId + "&NodeLayer=" + layer,TreeObj,NodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
	}
	else
	{
		//类别
		RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId + "&NodeLayer=" + layer,TreeObj,NodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
	}
}

//更新子节点
function updateChildNodes(parentNodeId, ItemType){
	var node = FindNode(document.all.Tree, parentNodeId);
	var layer = node.NodeLayer;
	if(layer) layer = parseInt(layer)-1;

	if (ItemType == "C")
	{
		//大类
		RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodesOfClass&NodeId=&ClassCode="+parentNodeId + "&NodeLayer=" + layer,TreeObj,parentNodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
	}
	else
	{
		//类别
		RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId + "&NodeLayer=" + layer,TreeObj,parentNodeId,TreeModels,"GroupCode",RowClassName,GridClassName);
	}
}

var ClassCode = '<%=Request["ClassCode"]%>';

var RootGroupCode = Form1.txtRootGroupCode.value;
if (RootGroupCode != "") //只显示某个枝条
{
    GetChildNodes(DataSourceUrl+"&GetType=SingleNode&NodeId=" + RootGroupCode +"&NodeLayer=0",null,TreeModels,"GroupCode",RowClassName,GridClassName);

    if (document.all.Tree.childNodes.length == 1)
    {
	    SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, "", document.all.Tree.childNodes[0]);
    }
}
else
{
    GetChildNodes(DataSourceUrl+"&GetType=Class&NodeId=" + ClassCode+"&NodeLayer=0",null,TreeModels,"GroupCode",RowClassName,GridClassName);

    if (document.all.Tree.childNodes.length == 1)
    {
	    SpreadNodes(document.all.Tree.childNodes[0].NodeId, parseInt(document.all.Tree.childNodes[0].NodeLayer) - 1, "C", document.all.Tree.childNodes[0]);
    }
}

		</Script>
	</body>
</HTML>
