<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.BuildingTree" CodeFile="BuildingTree.ascx.cs" %>
<TABLE id="Table3" borderColor="#e7e7e7" class="tree" cellSpacing="0" cellPadding="3" rules="rows" width="100%"
	border="0">
	<thead>
		<TR class="tree-title">
			<TD noWrap align="left">楼栋名称</TD>
			<TD noWrap align="left">产品类型</TD>
			<TD noWrap align="right">层数</TD>
			<TD noWrap align="right">计划建设面积(平米)</TD>
			<TD noWrap align="right">实测面积(平米)</TD>
			<!--TD noWrap align="left">所属单位工程</TD-->
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
<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
<input type="hidden" id="txtBuildingCode" name="txtBuildingCode" runat="server">
<script language="javascript">

//查看区域、楼栋
function View(code){
	window.location.href = "PBSBuildInfo.aspx?BuildingCode="+code + "&FromUrl=" + escape(CurrUrl);
}

//查看单位工程
function GotoPBSUnitInfo(code)
{
	OpenCustomWindow("../PBS/PBSUnitInfo.aspx?action=view&FromUrl=" + escape(window.location) + "&PBSUnitCode=" + code + "&ProjectCode=" + Form1.txtProjectCode.value, "单位工程", 700, 500);
}

</script>
<Script language="javascript">
//Tree
var TreeObj=document.all("Tree");
var RowClassName="tree-tr";
var GridClassName="tree-tr";

var ProjectCode = document.all(ClientID + "_txtProjectCode").value;
var BuildingCode = document.all(ClientID + "_txtBuildingCode").value;

var DataSourceUrl="PBSBuildingData.aspx?ProjectCode=" + ProjectCode + "&BuildingCode=" + BuildingCode;

// @IndentStart			缩进内容循环开始点
// @IndentEnd			缩进内容循环结束点
// @NodeSymbolStart		节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @NodeSymbolEnd		节点标志开始点
// @JsCodeStart			节点标志开始点 开始和结束两点中写入 闭合标示|开启标示|无节点标示
// @JsCodeEnd			节点标志开始点
var TreeModels=new Array();
TreeModels[0]="<table cellSpacing=\"0\" cellPadding=\"0\" border=\"0\"><tr>@IndentStart<td width=\"10\">&nbsp;</td>@IndentEnd";
TreeModels[0]+="<td onclick=\"SpreadNodes('@BuildingCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/Plus.gif\">|<img src=\"../Images/Minus.gif\">|@NodeSymbolEnd</td>";
//TreeModels[0]+="<td onclick=\"SpreadNodes('@BuildingCode','@Layer',this);\" width=\"20\" align=\"center\" style=\"cursor:hand\">@NodeSymbolStart<img src=\"../Images/FolderClose.gif\">|<img src=\"../Images/FolderOpen.gif\">|<img src=\"../Images/FileIcon.gif\">@NodeSymbolEnd</td>";
TreeModels[0]+="<td><img src=\"../Images/@IconName\"><a style=\"display=@ShowHref\" href=\"#\" id=\"@BuildingCode\" onclick=\"javascript:View('@BuildingCode');\">@BuildingName</a><span style=\"display=@ShowSpan\">@BuildingName</span></td></tr></table>";
//TreeModels[0]+="<td><img src=\"../Images/@IconName\"><a href=\"#\" id=\"@BuildingCode\" onclick=\"TDClick(this, '@BuildingCode');return false;\">@BuildingName</a></td></tr></table>";
//TreeModels[1]="<a href=\"#\" id=\"@BuildingCode\" onclick=\"InsertChildPBSBuild('@BuildingCode','Building');return false;\">新增楼栋</a>";
TreeModels[1]="@PBSTypeName";
TreeModels[2]="<div align=right>@FloorCount</div>";
TreeModels[3]='<div align=right>@HouseArea</div>';
TreeModels[4]="<div align=right>@RoomArea</div>";
//TreeModels[5]="<a href=\"#\" onclick=\"javascript:GotoPBSUnitInfo('@PBSUnitCode');\">@PBSUnitName</a>";

//节点
function SpreadNodes(BuildingCode,LayerNumber,obj){
	ShowChildNode(DataSourceUrl+"&GetType=ChildNodes&NodeId="+BuildingCode,obj,TreeModels,"BuildingCode");
}

//所有
function ShowAll(){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&GetType=All",null,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//指定层
function ShowLayer(layer){
	RemoveAllChildNode(TreeObj);
	GetChildNodes(DataSourceUrl+"&SelectedLayer="+layer+"&GetType=SelectLayer",null,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//更新节点
function updateNode(NodeId){
	RefreshNode(DataSourceUrl+"&GetType=SingleNode&NodeId="+NodeId,TreeObj,NodeId,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

//更新子节点
function updateChildNodes(parentNodeId){
	RefreshChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+parentNodeId,TreeObj,parentNodeId,TreeModels,"BuildingCode",RowClassName,GridClassName);
}

GetChildNodes(DataSourceUrl+"&GetType=ChildNodes&NodeId="+BuildingCode+"&ShowSum=1",null,TreeModels,"BuildingCode",RowClassName,GridClassName);

//合计行
if (document.all.Tree.childNodes.length > 0)
{
	var node = document.all.Tree.childNodes[document.all.Tree.childNodes.length - 1];
	for(var i=0;i<node.childNodes.length;i++)
	{
		node.childNodes[i].className = "tree-sum";
	}
}

</Script>
