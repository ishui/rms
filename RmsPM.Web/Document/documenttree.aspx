<%@ Page language="c#" Inherits="RmsPM.Web.Document.DocumentTree" CodeFile="DocumentTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DocumentTree</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<style>
.LeftMenuItem{
	font-size: 12px;
	margin: 1px;
	color:#00309C;
}
.LeftMenuItemOnMouseOver{
	border: #2155bd 1px solid;
	font-size: 12px;
	margin: 0px;
	background-color: #fffbff;
	color:#00309C;
}
.LeftMenuItemOnActivty{
	border: #2155bd 1px solid;
	font-size: 12px;
	margin: 0px;
	background-color: #ffe794;
	color:#00309C;
}
		</style>
		<SCRIPT language="javascript">
	
	var clicktd;
	
	function TDClick(obj) {
		if (clicktd != undefined) {
			clicktd.className='';
		}
		obj.className='LeftMenuItemOnMouseOver';
		clicktd = obj;
	}
	
	function ShowDocumentList(DocumentTypeCode){
		window.parent.ShowDocumentList(DocumentTypeCode);
	}

		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="20" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr vAlign="top">
					<td class="table">
						<TABLE id="Table3" borderColor="#e7e7e7" cellSpacing="0" cellPadding="3" rules="rows" width="100%"
							border="0">
							<tbody id="Tree">
							</tbody>
							<tfoot>
							</tfoot>
						</TABLE>
					</td>
				</tr>
			</table>
			<input id="txtShowItems" type="hidden" name="txtShowItems" runat="server"><INPUT id="txtTreeType" type="hidden" name="txtTreeType" runat="server"><INPUT id="txtNodeType" type="hidden" name="txtNodeType" runat="server">
		</form>
		<SCRIPT language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="ListBodyTr1";
var GridClassName="TreeViewItemTd";

var TreeType = Form1.txtTreeType.value; 
var DataSourceUrl="DocumentTreeData.aspx?TreeType=" + TreeType;

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
	if ( obj != null )
	{
		if ( HasString (showItemString,SItems[i]  ) )
			obj.style.display = "";
		else
			obj.style.display = "none";
	}
}

var TreeModels=new Array();
var v0 ="<table name='tbTree' id='tbTree' cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td onclick=\"SpreadNodes('@DocumentTypeCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td style=\"CURSOR: hand\" onclick=\"TDClick(this);ShowDocumentList('@DocumentTypeCode');return false;\"><img src=\"../Images/FolderClose.gif\">@TypeName</td></tr></table>";
//v0+="<td><img src=\"../Images/orange.gif\"><a href=\"#\" id=\"@DocumentTypeCode\" onclick=\"ShowDocumentList('@DocumentTypeCode');return false;\">@TypeName</a></td></tr></table>";
TreeModels.push(v0);

var TreeModelsRoot=new Array();
var v1 ="<table name='tbTree' id='tbTree' cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v1+="<td onclick=\"SpreadRootNodes('@DocumentTypeCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v1+="<td style=\"CURSOR: hand\" onclick=\"TDClick(this);ShowDocumentList('@DocumentTypeCode');return false;\"><img src=\"../Images/FolderClose.gif\">@TypeName</td></tr></table>";
//v1+="<td><img src=\"../Images/FolderClose.gif\"><a href=\"#\" id=\"@DocumentTypeCode\" onclick=\"ShowDocumentList('@DocumentTypeCode');return false;\">@TypeName</a></td></tr></table>";
TreeModelsRoot.push(v1);

var TreeModelsFix=new Array();
var v2 ="<table name='tbTree' id='tbTree' cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v2+="<td onclick=\"SpreadFixNodes('@DocumentTypeCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v2+="<td style=\"CURSOR: hand\" onclick=\"TDClick(this);ShowDocumentList('@DocumentTypeCode');return false;\"><img src=\"../Images/FolderClose.gif\">@TypeName</td></tr></table>";
//v2+="<td><img src=\"../Images/pear.gif\"><a href=\"#\" id=\"@DocumentTypeCode\" onclick=\"ShowDocumentList('@DocumentTypeCode');return false;\">@TypeName</a></td></tr></table>";
TreeModelsFix.push(v2);


function SpreadRootNodes(DocumentTypeCode,LayerNumber,obj){
//	ShowChildNode(DataSourceUrl+"&GetType=Fix&NodeId="+DocumentTypeCode,obj,TreeModelsFix,"DocumentTypeCode");
//	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+DocumentTypeCode,obj,TreeModels,"DocumentTypeCode");

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
				GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+DocumentTypeCode,node,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
				GetChildNodes(DataSourceUrl+"&GetType=Fix&NodeId="+DocumentTypeCode,node,TreeModelsFix,"DocumentTypeCode",RowClassName,GridClassName);
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

function SpreadFixNodes(DocumentTypeCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=Fix&NodeId="+DocumentTypeCode,obj,TreeModelsFix,"DocumentTypeCode");
}

//节点
function SpreadNodes(DocumentTypeCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+DocumentTypeCode,obj,TreeModels,"DocumentTypeCode");
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

//更新子节点
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"DocumentTypeCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModelsRoot,"DocumentTypeCode",RowClassName,GridClassName);
//alert(document.all.Tree.childNodes[0]);
SpreadRootNodes("", 1, document.all.Tree.childNodes[0]);
		</SCRIPT>
	</body>
</HTML>
