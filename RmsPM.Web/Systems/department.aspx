<%@ Page language="c#" Inherits="RmsPM.Web.Systems.Department" CodeFile="Department.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>组织结构42</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XMLTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<style>.LeftMenuItem { FONT-SIZE: 12px; MARGIN: 1px; COLOR: #00309c }
	.LeftMenuItemOnMouseOver { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #fffbff }
	.LeftMenuItemOnActivty { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #ffe794 }
	A { COLOR: #000000; TEXT-DECORATION: none }
	A:hover { TEXT-DECORATION: none }
		</style>
		<SCRIPT language="javascript">

	var clicktd;
	var clicknode;
	
	function TDClick(obj) {
		if (clicktd != undefined) {
			clicktd.className='';
		}
		obj.className='LeftMenuItemOnMouseOver';
		clicktd = obj;
	}
		
	function DecodeId(id) {
		if (id == "")
			return id;
			
		var i = id.indexOf("_");
		if (i < 0)
			return id;
			
		return id.substr(i+1, id.length - i - 1);
	}
	
	function GetNodeType(id) {
		if (id == "")
			return "";
			
		return id.substr(0, 1);
	}
		


	//按id查找节点
	function FindNode(tree, NodeId) {
		var node;
		node = tree.childNodes[0];
		
		if (NodeId == "")
			return null;
			
		while ((node != undefined) && (node != null))
		{
			if (node.NodeId == NodeId)
			{
				return node;
			}

			node = node.nextSibling;
		}
	}
	
	function RemoveLast(s, sep) 
	{
		var i = s.lastIndexOf(sep);
		if (i > -1) 
		{
			return s.substr(0, i);
		}
		else
			return s;
	}

	function GetLast(s, sep) 
	{
		var i = s.lastIndexOf(sep);
		if (i > -1) 
		{
			return s.substr(i + 1, s.length - i - 1);
		}
		else
			return "";
	}

	function GetParentNodeId(node) {
		if ((node == undefined) || (node == null))
			return "";
			
		var s = node.NodeIndex;
		var i;
		var stemp;
		
		stemp = RemoveLast(s, ".");
		if (stemp == s)
			return "";
			
		s = stemp;
		stemp = GetLast(s, ".");
		if (stemp == "")
			return s;
		else
			return stemp;
	}

		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="100%">
						<TABLE  cellSpacing="0" cellPadding="0" border="1" height="100%" width="100%" class=table >
							<tr>
								<td width="40%">
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
										<TABLE id="Table3" borderColor="#e7e7e7" cellSpacing="0" cellPadding="3" width="100%" border="0">
											<TBODY id="Tree" align="top">
											</TBODY>
										</TABLE>
									</div>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				</table>
			<input id="txtFrom" type="hidden" name="txtFrom" runat="server"> <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
			<input id="txtRootUnitCode" type="hidden" name="txtRootUnitCode" runat="server">
		</form>
		<SCRIPT language="javascript">

// 显示部门
function showDepartment(obj, layer, parentCode, NodeType){
	var id = obj.id;
	var code = DecodeId(id);
	//OpenSmallWindow("DepartmentModify.aspx?RefreshScript=updateUnit('" + id + "'," + layer + ");&Action=Modify&UnitCode="+DecodeId(id),"");
	// OpenLargeWindow('DepartmentInfo.aspx?UnitCode='+ code ,'部门信息' );
	window.open('DepartmentInfo.aspx?UnitCode='+ code,'frameMain');
	//document.all("frameMain").src = 'DepartmentInfo.aspx?UnitCode='+ code;
	//window.open('DepartmentInfo.aspx?UnitCode='+ code);
}

function showStation(obj, layer, parentCode){
	var id = obj.id;
	var code = DecodeId(id);
	window.open('StationInfo.aspx?StationCode=' + code + '&UnitCode=' + DecodeId(parentCode),'frameMain');	
}

/*
function showRole(obj, layer, parentCode){
	var id = obj.id;
	var code = DecodeId(id);
	//OpenMiddleWindow('RoleInfo.aspx?RoleCode=' + code ,'角色信息');
	document.all("frameMain").src ='RoleInfo.aspx?RoleCode=' + code + '&UnitCode=' + DecodeId(parentCode) ;
}
*/


//Tree
var TreeObj=document.all("Tree");
var RowClassName="ListBodyTr1";
var GridClassName="TreeViewItemTd";

var DataSourceUrl="OBSData.aspx?TreeType=";

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点

//部门
var TreeModels=new Array();
var v0 ="<table name='tbTree' id='tbTree' cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td onclick=\"SpreadUnitNodes('@Code','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td style=\"CURSOR: hand\" id=\"@Code\" onclick=\"TDClick(this);showDepartment(this, '@Layer', '@ParentCode', '@NodeType');return false;\"><img src=\"../Images/@ImageName\">@Name (@UserCount)</td></tr></table>";
//v0+="<td><img src=\"../Images/@ImageName\"><a href=\"#\" id=\"@Code\" onclick=\"ShowEditMenu(this, '@Layer', '@ParentCode', '@NodeType');return false;\">@Name</a></td></tr></table>";
TreeModels.push(v0);

//岗位
var TreeModelsRole=new Array();
var v1 ="<table name='tbTree' id='tbTree' cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v1+="<td  width=\"20\" align=\"center\" >@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v1+="<td style=\"CURSOR: hand\" id=\"@Code\" onclick=\"TDClick(this);showStation(this, '@Layer', '@ParentCode');return false;\"><img src=\"../Images/group.gif\">@Name (@UserCount)</td></tr></table>";
TreeModelsRole.push(v1);



//展开部门
function SpreadUnitNodes(UnitCode,LayerNumber,obj){
	var node = obj;

		while(node.NodeName==null||node.NodeName!="_XmlTreeNode"){
			node=node.parentNode;
		}
		
		var shows=node.getElementsByTagName("div");
		var plusNode=null;
		var minusNode=null;
		var noneNode=null;
		for(var i=0;i<shows.length;i++){
			if(shows[i].id.toLowerCase()=="nodeplus"){
				plusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodeminus"){
				minusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodenone"){
				noneNode=shows[i];
			}
		}

		if(noneNode.style.display!=""){
			if(node.NodeStatus=="Closed"){
				GetChildNodes(DataSourceUrl+"&GetType=ChildNodesRoleOfUnit&NodeId="+UnitCode + "&ParentLayer=" + LayerNumber,node,TreeModelsRole,"Code",RowClassName,GridClassName);
				GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+UnitCode + "&ParentLayer=" + LayerNumber,node,TreeModels,"Code",RowClassName,GridClassName);
				node.NodeStatus="Opened";
				plusNode.style.display="none";
				minusNode.style.display="";
				InsertOpendNodeKeys(node.NodeId);
			}else if(node.NodeStatus=="Opened"){
				ClearChildNodes(node);
				node.NodeStatus="Closed";
				plusNode.style.display="";
				minusNode.style.display="none";
				RemoveOpendNodeKeys(node.NodeId);
			}
		}
}

/*
//展开角色
function SpreadRoleNodes(RoleCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodesUserOfRole&NodeId="+RoleCode + "&ParentLayer=" + LayerNumber,obj,TreeModelsUser,"Code");
}
*/
/*
function SpreadUserNodes(UserCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=Fix&NodeId="+UserCode + "&ParentLayer=" + LayerNumber,obj,TreeModelsUser,"Code");
}

//节点
function SpreadNodes(UnitCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+UnitCode,obj,TreeModels,"Code");
}
*/

//更新部门
function updateUnit(NodeId, Layer){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId+"&CurrLayer=" + Layer,TreeObj,NodeId,TreeModels,"Code",RowClassName,GridClassName);
}

//更新岗位 
function updateRole(NodeId, Layer){
	RefreshNode(DataSourceUrl+"&GetType=SingleNodeRole&NodeId="+NodeId+"&CurrLayer=" + Layer,TreeObj,NodeId,TreeModelsRole,"Code",RowClassName,GridClassName);
}

/*
//更新用户
function updateUser(NodeId, Layer){
	RefreshNode(DataSourceUrl+"&GetType=SingleNodeUser&NodeId="+NodeId+"&CurrLayer=" + Layer,TreeObj,NodeId,TreeModelsUser,"Code",RowClassName,GridClassName);
}
*/

//更新部门子节点
function updateUnitChildNodes(parentNodeId,Layer){
	if (parentNodeId == "") {
		RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+ Form1.txtRootUnitCode.value +"&ParentLayer="+Layer,TreeObj,parentNodeId,TreeModels,"Code",RowClassName,GridClassName);
	}
	else {

	//-------------begin
	var NodeId = parentNodeId;
	
	var obj=null;
	for(var i=0;i<TreeObj.childNodes.length;i++){
		if(TreeObj.childNodes[i].NodeId==NodeId){
			obj=TreeObj.childNodes[i];
			break;
		}
	}
	
	if(obj==null){
		RemoveAllChildNode(TreeObj);
//		GetChildNodes(url,null,Models,keyField,RowClassName,GridClassName);
	}else{
		var shows=obj.getElementsByTagName("div");
		var plusNode=null;
		var minusNode=null;
		var noneNode=null;
		for(var i=0;i<shows.length;i++){
			if(shows[i].id.toLowerCase()=="nodeplus"){
				plusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodeminus"){
				minusNode=shows[i];
			}
			if(shows[i].id.toLowerCase()=="nodenone"){
				noneNode=shows[i];
			}
		}
		ClearChildNodes(obj);
		RemoveOpendNodeKeys(obj.NodeId);
		
		var itemCount2=GetChildNodes(DataSourceUrl+"&GetType=ChildNodesRoleOfUnit&NodeId="+parentNodeId+"&ParentLayer="+Layer+"&Layer="+obj.NodeLayer,obj,TreeModelsRole,"Code",RowClassName,GridClassName);
		if(itemCount2){
			InsertOpendNodeKeys(obj.NodeId);
		}

		var itemCount1=GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&ParentLayer="+Layer+"&Layer="+obj.NodeLayer,obj,TreeModels,"Code",RowClassName,GridClassName);
		if(itemCount1){
			InsertOpendNodeKeys(obj.NodeId);
		}

		if(itemCount1 || itemCount2){
			obj.NodeStatus="Opened";
			plusNode.style.display="none";
			minusNode.style.display="";
			noneNode.style.display="none";
		}else{
			obj.NodeStatus="Closed";
			plusNode.style.display="none";
			minusNode.style.display="none";
			noneNode.style.display="";
		}
	}
	//-------------end

//		RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&ParentLayer="+Layer,TreeObj,parentNodeId,TreeModels,"Code",RowClassName,GridClassName);
//		RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodesRoleOfUnit&NodeId="+parentNodeId+"&ParentLayer="+Layer,TreeObj,parentNodeId,TreeModelsRole,"Code",RowClassName,GridClassName);
	}
}

/*

//更新角色子节点
function updateRoleChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodesUserOfRole&NodeId="+parentNodeId+"&ParentLayer="+Layer,TreeObj,parentNodeId,TreeModelsUser,"Code",RowClassName,GridClassName);
}

*/
//展开某个节点
function expandNode(node) {
	var id, layer, type;
	
	if ((node == undefined) || (node == null))
		return;

	if (node.NodeStatus == "Closed")
	{
		id = node.NodeId;
		layer = node.NodeLayer;
		type = GetNodeType(id);
		
		if (type == "D") {
			SpreadUnitNodes(id, layer, node);
		}
/*		
		if (type == "R") {
			SpreadRoleNodes(id, layer, node);
		}
*/
	}
}

/*
//全部展开
function expandAll() {
	var tree = document.all.Tree;
	var i;
	var node;
	var id;
	var layer;
	var type;

//alert(tree.childNodes[3].nextSibling);
//alert(tree.childNodes[1].NodeId);
//return;  

	node = tree.childNodes[0];
	while ((node != undefined) && (node != null))
	{
		expandNode(node);
		node = node.nextSibling;
	}
}
*/

//展开当前节点
function expandSingle() {
	if (clicknode == undefined)
		return;
		
	var origin_id = clicknode.id;
	
	var tree = document.all.Tree;
	var i;
	var id;
	var layer;
	var type;

	node = FindNode(tree, origin_id);
	if (node == null)
		return;
	
	var origin_layer = node.NodeLayer;

	while ((node != undefined) && (node != null))
	{
		if ((node.NodeId != origin_id) && (node.NodeLayer <= origin_layer))
			return;
			
		expandNode(node);
		node = node.nextSibling;
	}
}

function CreateTree()
{
	RemoveAllChildNode(document.all.Tree);
	
	var rootCode = Form1.txtRootUnitCode.value;

	if (rootCode != "")
	{
		GetChildNodes(DataSourceUrl+"&GetType=SingleNode&NodeId=" + rootCode,null,TreeModels,"Code",RowClassName,GridClassName);
	//	SpreadUnitNodes(rootCode, 2, document.all.Tree.childNodes[0]);
	}
	else
	{
		GetChildNodes(DataSourceUrl+"&NodeId=" + rootCode,null,TreeModels,"Code",RowClassName,GridClassName);
	}

	if (document.all.Tree.childNodes.length == 1) {
		SpreadUnitNodes(document.all.Tree.childNodes[0].NodeId, document.all.Tree.childNodes[0].NodeLayer, document.all.Tree.childNodes[0]);
	}
}

CreateTree();
	
//alert(document.all.Tree.childNodes[0]);
//SpreadRootNodes("", 1, document.all.Tree.childNodes[0]);

		
/*  注释掉  2004.5.26  ------------------------------------------------------------------------------------
//ContentMenu
function ShowEditMenu(obj){
	var cssFile="../Images/ContentMenu.css";		
	var Items=new Array();
	Items[0]=new Array(2);
	Items[0][0]="修改科目";
	Items[0][1]="";
	Items[0][2]="ModifyUnit('"+obj.id+"');";
	Items[1]=new Array(2);
	Items[1][0]="删除科目";
	Items[1][1]="";
	Items[1][2]="RemoveUnit('"+obj.id+"');";
	Items[2]=new Array(2);
	Items[2][0]="新增下属部门";
	Items[2][1]="";
	Items[2][2]="InsertUnit('"+obj.id+"');";
	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

//Tree
var TreeObj=document.all("Tree");
var RowClassName="TreeViewItemTr";
var GridClassName="TreeViewItemTd";

var DataSourceUrl="DepartmentData.aspx";
// #Id					节点的唯一编号
// #LayerNumber			层次关键字
var DataSourceUrlParams=new Array();
DataSourceUrlParams[0]="UnitCode";
DataSourceUrlParams[1]="#Id";
DataSourceUrlParams[2]="Layer";
DataSourceUrlParams[3]="#LayerNumber";

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@Id','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
TreeModels[0]+="<td><a href=\"#\" id=\"@Id\" onclick=\"ShowEditMenu(this);return false;\">@UnitName</a></td></tr></table>";
TreeModels[1]="<font color=red>@PrincipalName</font>";
TreeModels[2]="<div align=\"center\">@Remark</div>";

function SpreadNodes(UnitCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"?Layer="+(parseInt(LayerNumber)+1)+"&UnitCode="+UnitCode,obj,TreeModels,"UnitCode");
}

function ShowLayer(layer){
	ShowChildNodeByLayer(TreeObj,layer,true,TreeModels,DataSourceUrl,DataSourceUrlParams,"UnitCode");
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"UnitCode",RowClassName,GridClassName);
*/
		</SCRIPT>
	</body>
</HTML>