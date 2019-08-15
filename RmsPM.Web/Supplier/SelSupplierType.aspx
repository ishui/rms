<%@ Page language="c#" CodeFile="SelSupplierType.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Supplier.SelSupplierType" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择供应商类型</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
			function SelSupplierType(SupplierTypeCode,TypeName){
				window.opener.getReturnSupplierType(SupplierTypeCode,TypeName);
				window.close();
			}
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择供应商类型</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center" height=95%>
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" rules="rows" class="tree" border="0"
							width="98%">
							<thead>
								<TR class="tree" id="tree-title">
									<TD id="tdTypeName" noWrap align="left">类型名称</TD>
									<TD id="tdDescription" noWrap align="left">说 明</TD>
									<TD id="AddType" noWrap align="left"></TD>
								</TR>
							</thead>
							<tbody id="Tree">
							</tbody>
							<tfoot>
								<TR class="tree-title">
									<TD></TD>
									<TD></TD>
									<TD></TD>
								</TR>
							</tfoot>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
		<SCRIPT language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var DataSourceUrl="SupplierData.aspx?a=1" ;

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点


var TreeModels=new Array();
var v0 ="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
v0+="<td onclick=\"SpreadNodes('@SupplierTypeCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
v0+="<td>@TypeName</td></tr></table>";
v1="<a href=\"#\" id=\"@SupplierTypeCode\" onclick=\"SelSupplierType('@SupplierTypeCode','@TypeName');return false;\">选择</a>";

TreeModels.push(v0);
TreeModels.push("@Description");
TreeModels.push(v1);

//节点
function SpreadNodes(SupplierTypeCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+SupplierTypeCode,obj,TreeModels,"SupplierTypeCode");
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"SupplierTypeCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"SupplierTypeCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"SupplierTypeCode",RowClassName,GridClassName);
}

//更新子节点
function updateChildNodes(parentNodeId,Layer){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId+"&Layer="+Layer,TreeObj,parentNodeId,TreeModels,"SupplierTypeCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"",null,TreeModels,"SupplierTypeCode",RowClassName,GridClassName);

		</SCRIPT>
	</body>
</HTML>
